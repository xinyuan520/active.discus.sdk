using Discus.SDK.Tools.HttpResult;

namespace Discus.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;


        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// 获取所有存储桶
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllMyBucketList")]
        public async Task<List<string>> GetAllMyBucketList()
        {
            return await _fileService.GetAllMyBucketList();
        }

        /// <summary>
        /// 上传文件-文件流
        /// </summary>
        /// <returns></returns>
        [Route("UploadFile/{ext}"), HttpPost]
        public async Task<ApiResult> UploadStreamAsync(string ext)
        {
            var fileName = Guid.NewGuid().ToString("N");
            return await _fileService.UploadStreamAsync(fileName + "." + ext.ToLower(), Request.Body);
        }

        /// <summary>
        /// 上传单个文件-文件路径  {"filePath": "F:\\TestImages1\\1.jpg"},
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Route("UploadFilePath"), HttpPost]
        public async Task<ApiResult> UploadFileAsync(RequestFileParam param)
        {
            var fileName = Guid.NewGuid().ToString("N");
            var fleName = fileName + Path.GetExtension(param.FilePath);
            return await _fileService.UploadFileAsync(fleName, param.FilePath);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [Route("DownLoadFile/{fileName}"), HttpGet]
        public async Task<Stream> DownLoadFileAsync(string fileName)
        {
            return await _fileService.DownLoadFileAsync(fileName);
        }

        /// <summary>
        /// 保存单个文件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Route("SaveFileAsync"), HttpPost]
        public async Task<ApiResult> SaveFileAsync(RequestFileParam param)
        {
            return await _fileService.SaveFileAsync(param.FileName, param.FilePath);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [Route("DeleteFileAsync/{fileName}"), HttpDelete]
        public async Task<ApiResult> DeleteFileAsync(string fileName)
        {
            return await _fileService.DeleteFileAsync(fileName);
        }
    }
}
