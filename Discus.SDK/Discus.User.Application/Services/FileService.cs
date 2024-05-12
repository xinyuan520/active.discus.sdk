using Discus.SDK.File;
using Discus.SDK.Tools.HttpResult.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.Services
{
    public class FileService : BasicService, IFileService
    {
        private readonly IStorageClient _storageClient;

        public FileService(IStorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<List<string>> GetAllMyBucketList()
        {
            List<string> bucketNameList = new List<string>();
            var result = await _storageClient.GetAllMyBucketList();
            result.ForEach(x => bucketNameList.Add(x.Name));
            return bucketNameList;
        }

        public async Task<ApiResult> UploadStreamAsync(string fileName, Stream stream)
        {
            var result = await _storageClient.UploadStreamAsync(fileName, stream);
            if (result)
                return ApiResult.IsSuccess("上传成功！", ApiResultCode.Succeed, fileName);
            return ApiResult.IsFailed("上传失败！");
        }

        public async Task<ApiResult> UploadFileAsync(string fileName, string filepath)
        {
            var result = await _storageClient.UploadFileAsync(fileName, filepath);
            if (result)
                return ApiResult.IsSuccess("上传成功！", ApiResultCode.Succeed, fileName);
            return ApiResult.IsFailed("上传失败！");
        }

        public async Task<Stream> DownLoadFileAsync(string fileName)
        {
            return await _storageClient.DownLoadFileAsync(fileName);
        }

        public async Task<ApiResult> SaveFileAsync(string fileName, string filePath)
        {
            var result = await _storageClient.SaveFileAsync(fileName, filePath);
            if (result)
                return ApiResult.IsSuccess("保存成功！", ApiResultCode.Succeed, fileName);
            return ApiResult.IsFailed("保存失败！");
        }

        public async Task<ApiResult> DeleteFileAsync(string fileName)
        {
            var result = await _storageClient.DeleteFileAsync(fileName);
            if (result)
                return ApiResult.IsSuccess("删除成功！", ApiResultCode.Succeed, fileName);
            return ApiResult.IsFailed("删除失败！");
        }
    }
}
