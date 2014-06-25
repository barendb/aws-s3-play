using System;
using System.IO;
using Amazon.S3;
using aws_play.Factories;
using aws_play.Services;
using Moq;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class CloudStorageServiceTests
	{
		private Mock<AmazonS3Client> _mockClient;

		[SetUp]
		public void SetUp()
		{
			var awsCredsFactory = new AwsCredentialsFactory();
			var creds = awsCredsFactory.Create();

			_mockClient = new Mock<AmazonS3Client>(creds.AwsAccessKeyId, creds.AwsSecretAccessKey, creds.Region);
		}

		[Test]
		public void Ordered_file_test()
		{
			var awsCredsFactory = new AwsCredentialsFactory();
			var creds = awsCredsFactory.Create();
			var client = new S3Factory().Create(creds);

			var bucketName = "test-" + Guid.NewGuid();
			var key = Guid.NewGuid().ToString();


			Ordered_bucket_does_not_exists(client, bucketName);
			Ordered_put(client, bucketName, key);
			Ordered_bucket_does_exists(client, bucketName);
			Ordered_get_file_found(client, bucketName, key);
			Ordered_delete(client, bucketName, key);
			Ordered_get_file_not_found(client, bucketName, key);
		}

		private void Ordered_bucket_does_not_exists(AmazonS3Client client, string bucketName)
		{
			using (ICloudStorageService cloudStorage = new CloudStorageService(client))
			{
				Assert.DoesNotThrow(async () =>
				{
					var exists = await cloudStorage.BucketExistsAsync(bucketName);
					Assert.IsFalse(exists);
				});
			}
		}


		public void Ordered_put(AmazonS3Client client, string bucketName, string key)
		{
			using (var cloudStorage = new CloudStorageService(client))
			{
				var emptyStream = CreateRandomStream();

				Assert.DoesNotThrow(async () =>
				{
					await cloudStorage.PutObjectAsync(bucketName, key, emptyStream);
				});
			}
		}

		private void Ordered_bucket_does_exists(AmazonS3Client client, string bucketName)
		{
			using (ICloudStorageService cloudStorage = new CloudStorageService(client))
			{
				Assert.DoesNotThrow(async () =>
				{
					var exists = await cloudStorage.BucketExistsAsync(bucketName);
					Assert.IsTrue(exists);
				});
			}
		}

		private void Ordered_get_file_found(AmazonS3Client client, string bucketName, string key)
		{
			using (ICloudStorageService cloudStorage = new CloudStorageService(client))
			{
				Assert.DoesNotThrow(async () =>
				{
					var stream = await cloudStorage.GetObjectAsync(bucketName, key);
					
					Assert.IsNotNull(stream);

					Assert.AreEqual(256, stream.Length);

					stream.Close();
				});
			}
		}

		private void Ordered_delete(AmazonS3Client client, string bucketName, string key)
		{
			using (ICloudStorageService cloudStorage = new CloudStorageService(client))
			{
				Assert.DoesNotThrow(async () =>
				{
					await cloudStorage.DeleteObjectAsync(bucketName, key);
				});
			}
		}

		private void Ordered_get_file_not_found(AmazonS3Client client, string bucketName, string key)
		{
			using (ICloudStorageService cloudStorage = new CloudStorageService(client))
			{
				Assert.DoesNotThrow(async () =>
				{
					var stream = await cloudStorage.GetObjectAsync(bucketName, key);
					
					Assert.IsNull(stream);
				});
			}
		}


		[Test]
		public void AsyncFileUpload_should_error_on_empty_stream()
		{
			using (var cloudStorage = new CloudStorageService(_mockClient.Object))
			{
				var emptyStream = Stream.Null;

				Assert.Throws<Exception>(async () =>
				{
					await cloudStorage.PutObjectAsync("someBucket", Guid.NewGuid().ToString(), emptyStream);
				});
			}
		}


		private Stream CreateRandomStream()
		{
			var array = new byte[256];
			var random = new Random();
			random.NextBytes(array);

			return new MemoryStream(array);
		}
	}
}