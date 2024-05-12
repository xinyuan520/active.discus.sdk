using Microsoft.Extensions.Logging;
using Minio.DataModel;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading;

namespace Discus.SDK.File
{
    /// <summary>
    /// 上传文件相关
    /// </summary>
    public class StorageClient : IStorageClient
    {
        private readonly IOptions<MinioConfig> _options;

        private readonly ILogger<StorageClient> _logger;

        private readonly MinioClient _minioClient;

        private readonly string _bucketName;

        private bool _bucketExists = false;

        

        public StorageClient(IOptions<MinioConfig> options, ILogger<StorageClient> logger)
        {
            _options = options;
            _minioClient = new MinioClient().WithEndpoint(_options.Value.Endpoint)
                                            .WithCredentials(_options.Value.AccessKey, _options.Value.SecretKey)
                                            .WithSSL(_options.Value.Secure)
                                            .Build();
            _logger = logger;
            _bucketName = options.Value.BucketName;
        }

       
        public async Task<List<Bucket>> GetAllMyBucketList()
        {
            var list = await _minioClient.ListBucketsAsync();
            return list.Buckets;
        }

        public async Task<bool> UploadStreamAsync(string fileName, Stream stream, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            await EnsureBucketExists();

            var seekableStream = stream.CanSeek ? stream : new MemoryStream();
            if (!stream.CanSeek)
            {
                await stream.CopyToAsync(seekableStream);
                seekableStream.Seek(0, SeekOrigin.Begin);
            }

            try
            {
                await _minioClient.PutObjectAsync(new PutObjectArgs().WithBucket(_bucketName).WithObject(NormalizePath(fileName)).WithStreamData(seekableStream).WithObjectSize(seekableStream.Length - seekableStream.Position), cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"UploadStreamAsync-{fileName}");
                return false;
            }
            finally
            {
                if (!stream.CanSeek)
                    seekableStream.Dispose();
            }
        }

        public async Task<bool> UploadFileAsync(string fileName,string filePath, CancellationToken cancellationToken = default)
        {

            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            try
            {
                await _minioClient.PutObjectAsync(new PutObjectArgs().WithBucket(_bucketName).WithObject(NormalizePath(fileName)).WithFileName(filePath), cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"UploadFileAsync-{fileName}");
                return false;
            }
        }

        public async Task<Stream> DownLoadFileAsync(string fileName, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            await EnsureBucketExists();

            try
            {
                Stream result = new MemoryStream();
                await _minioClient.GetObjectAsync(new GetObjectArgs().WithBucket(_bucketName).WithObject(NormalizePath(fileName)).WithCallbackStream(stream => stream.CopyToAsync(result).GetAwaiter().GetResult()), cancellationToken);
                result.Seek(0, SeekOrigin.Begin);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"DownLoadFileAsync-{fileName}");
                return null;
            }
        }

        public async Task<bool> SaveFileAsync(string fileName, string filePath, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            await EnsureBucketExists();

            try
            {
                await _minioClient.GetObjectAsync(new GetObjectArgs().WithBucket(_bucketName).WithObject(NormalizePath(fileName)).WithFile(filePath), cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"SaveFileAsync-{fileName}");
                return false;
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            await EnsureBucketExists();

            try
            {
                await _minioClient.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(_bucketName).WithObject(NormalizePath(fileName)), cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"DeleteFileAsync-{fileName}");
                return false;
            }
        }

        private async Task EnsureBucketExists(CancellationToken cancellationToken = default)
        {
            if (_bucketExists)
                return;

            var exist = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName), cancellationToken);
            if (!exist)
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));

            _bucketExists = true;
        }

        private string NormalizePath(string fileName)
        {
            return fileName?.Replace('\\', '/');
        }
    }
}
