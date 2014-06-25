namespace aws_play.Factories
{
	public class AwsCredentialsFactory : IAwsCredentialsFactory
	{
		public AwsCredentials Create()
		{
			//TODO: read from config or store
			return new AwsCredentials
			{
				AwsAccessKeyId = "## some key ##",
				AwsSecretAccessKey = "## some secret ##",

			};
		}
	}
}