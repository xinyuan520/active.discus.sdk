using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.Contracts.Services
{
    public interface IFileService : IService
    {
        Task<ApiResult> DeleteFileAsync(string fileName);
        Task<Stream> DownLoadFileAsync(string fileName);
        Task<List<string>> GetAllMyBucketList();
        Task<ApiResult> SaveFileAsync(string fileName, string filePath);
        Task<ApiResult> UploadFileAsync(string fileName, string filepath);
        Task<ApiResult> UploadStreamAsync(string fileName, Stream stream);
    }
}
