using aws_play.Factories;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class S3FactoryTests
	{
		[Test]
		public void S3Factory_should_create_client()
		{
			var awsCredentialsFactory = new AwsCredentialsFactory();


			var s3factory = new S3Factory();
			var client = s3factory.Create(awsCredentialsFactory.Create());


			Assert.IsNotNull(client);
		}
	}
}