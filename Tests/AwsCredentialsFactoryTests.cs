using aws_play.Factories;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class AwsCredentialsFactoryTests
	{
		[Test]
		public void AwsCredentialsFactory_should_return_credentials()
		{
			IAwsCredentialsFactory awsCredFactory = new AwsCredentialsFactory();
			var creds = awsCredFactory.Create();

			Assert.AreEqual("AKIAJLNF4DABPNTN77BA", creds.AwsAccessKeyId);
			Assert.AreEqual("teCQu8ftTxq0rgMl2rSQKroX4gKIbSM73uwQVgZV", creds.AwsSecretAccessKey);
		}
	}
}