namespace Discus.SDK.File
{
    public interface IStorageClient
    {
        /// <summary>
        /// 获取存储桶列表
        /// </summary>
        /// <returns></returns>
        Task<List<Bucket>> GetAllMyBucketList();

        /// <summary>
        /// 上传文件-文件流
        /// </summary>
        /// <returns></returns>
        Task<bool> UploadStreamAsync(string fileName, Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// 上传文件-文件路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UploadFileAsync(string fileName, string filePath, CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存单个文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Stream> DownLoadFileAsync(string fileName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SaveFileAsync(string fileName, string filePath, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteFileAsync(string fileName, CancellationToken cancellationToken = default);
    }
}
