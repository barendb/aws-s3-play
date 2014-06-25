using Amazon;

namespace aws_play
{
	public class AwsCredentials
	{
		public string AwsAccessKeyId { get; set; }
		public string AwsSecretAccessKey { get; set; }
		public RegionEndpoint Region { get; set; }

		public AwsCredentials()
		{
			Region = RegionEndpoint.USWest1;
		}
	}
}