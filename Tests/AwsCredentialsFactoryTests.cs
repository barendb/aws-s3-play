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

			Assert.AreEqual("/* some key */", creds.AwsAccessKeyId);
			Assert.AreEqual("/* some secret */", creds.AwsSecretAccessKey);
		}
	}
}
