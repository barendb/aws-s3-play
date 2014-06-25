using System;
using System.IO;
using System.Threading.Tasks;

namespace aws_play.Services
{
	public interface ICloudStorageService : IDisposable
	{
		Task PutObjectAsync(string bucket, string key, Stream stream);

		Task<Stream> GetObjectAsync(string bucket, string key);

		Task<bool> BucketExistsAsync(string bucket);

		Task DeleteObjectAsync(string bucket, string key);

		Task<bool> ObjectExistsAsync(string bucket, string key);
	}
}