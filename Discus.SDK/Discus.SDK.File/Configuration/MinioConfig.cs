using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.File.Configuration
{
    public class MinioConfig
    {
        public string Endpoint { get; set; } = "endpoint";

        //public string MinioUrl { get; set; }

        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public bool Secure { get; set; }

        public string BucketName { get; set; }
    }
}
