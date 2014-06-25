namespace Tests
{
	public class SomeSampleAsyncMockingShit
	{
		#region some good async testing shit

		//			var mc = new Mock<AmazonS3Client>();
		//			mc.Setup(x => x.GetBucketLocationAsync(new GetBucketLocationRequest
		//			{
		//				BucketName = nonExistantBucket
		//			}, new CancellationToken())).Returns(() => new Task<GetBucketLocationResponse>(() => new GetBucketLocationResponse
		//			{
		//				ContentLength = 0,
		//				HttpStatusCode = HttpStatusCode.BadGateway,
		//				Location = S3Region.US,
		//			}));
		//
		//			mc.Setup(x => x.PutBucketAsync(new PutBucketRequest
		//			{
		//				BucketName = nonExistantBucket,
		//			}, new CancellationToken())).Returns(() => new Task<PutBucketResponse>(() => new PutBucketResponse
		//			{
		//				ContentLength = 9999,
		//				HttpStatusCode = HttpStatusCode.Accepted,
		//			}));
		//
		//			mc.Setup(x => x.PutObjectAsync(new PutObjectRequest
		//			{
		//				BucketName = nonExistantBucket
		//			}, new CancellationToken())).Returns(() => new Task<PutObjectResponse>(() => new PutObjectResponse
		//			{
		//				ContentLength = 9999,
		//				HttpStatusCode = HttpStatusCode.Accepted,
		//			}));

		#endregion 
	}
}