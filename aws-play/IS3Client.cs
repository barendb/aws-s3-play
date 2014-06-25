using Amazon.S3;

namespace aws_play
{
	public interface IS3Client
	{
		AmazonS3Client Client { get; set; }
	}
}