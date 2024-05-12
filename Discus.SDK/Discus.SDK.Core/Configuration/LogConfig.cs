using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Core.Configuration
{
    public class LogConfig
    {
        public string TableName { get; set; } = "logs";

        public string ConnectionString { get; set; } = string.Empty;
    }
}
