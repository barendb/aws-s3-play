using Amazon.S3;

namespace aws_play.Factories
{
	public interface IS3Factory
	{
		AmazonS3Client Create(AwsCredentials awsCredentials);
	}
}