using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace aws_play.Services
{
	public class CloudStorageService : ICloudStorageService
	{
		private readonly AmazonS3Client _client;

		public CloudStorageService(AmazonS3Client client)
		{
			_client = client;
		}

		public void Dispose()
		{
			_client.Dispose();
		}

		public async Task PutObjectAsync(string bucket, string key, Stream stream)
		{
			if (stream.Length == 0)
				throw new Exception("Stream is empty");

			var bucketExists = await BucketExistsAsync(bucket);

			if (!bucketExists)
				await CreateBucket(bucket);

			var objectResponse = await UploadObject(bucket, key, stream);
			
			if (objectResponse.HttpStatusCode != HttpStatusCode.OK)
				throw new Exception("Could not upload object. HttpStatusCode: " + objectResponse.HttpStatusCode);
		}

		public async Task<Stream> GetObjectAsync(string bucket, string key)
		{
			var objectExists = await ObjectExistsAsync(bucket, key);

			if (!objectExists)
				return null;

			var response = await _client.GetObjectAsync(new GetObjectRequest
			{
				BucketName = bucket,
				Key = key,
			});

			return response.HttpStatusCode != HttpStatusCode.OK ? null : response.ResponseStream;
		}

		public async Task<bool> BucketExistsAsync(string bucket)
		{
			var response = await _client.ListBucketsAsync(new ListBucketsRequest());

			return response.Buckets.Any(x => x.BucketName == bucket);
		}

		public async Task DeleteObjectAsync(string bucket, string key)
		{
			await _client.DeleteObjectAsync(new DeleteObjectRequest
			{
				BucketName = bucket,
				Key = key
			});
		}

		public async Task<bool> ObjectExistsAsync(string bucket, string key)
		{
			var response = await _client.ListObjectsAsync(new ListObjectsRequest
			{
				BucketName = bucket
			});

			return response.S3Objects.Any(s3Object => s3Object.Key == key);
		}

		private async Task<PutBucketResponse> CreateBucket(string bucket)
		{
			return await _client.PutBucketAsync(new PutBucketRequest
			{
				BucketName = bucket,
				BucketRegion = S3Region.USW1,
				CannedACL = S3CannedACL.Private,
			});
		}

		private async Task<PutObjectResponse> UploadObject(string bucket, string key, Stream stream)
		{
			return await _client.PutObjectAsync(new PutObjectRequest
			{
				Key = key,
				BucketName = bucket,
				AutoCloseStream = true,
				InputStream = stream
			});
		}
	}
}