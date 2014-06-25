using Amazon.S3;

namespace aws_play.Factories
{
	public class S3Factory : IS3Factory
	{
		public AmazonS3Client Create(AwsCredentials awsCredentials)
		{
			return new AmazonS3Client(awsCredentials.AwsAccessKeyId, awsCredentials.AwsSecretAccessKey, awsCredentials.Region);
		}
	}
}
