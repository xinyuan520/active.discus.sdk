using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.File
{
    public class RequestFileParam
    {
        /// <summary>
        /// minio文件名称
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// 本地文件路径
        /// </summary>
        public string FilePath { get; set; }
    }
}
