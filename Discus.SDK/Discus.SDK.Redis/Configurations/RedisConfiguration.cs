using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Redis.Configurations
{
    public class RedisConfiguration
    {
        public string Provider { get; set; } = "CsRedis";
        public bool EnableBloomFilter { get; set; }
        public string SerializerName { get; set; }
        public DBConfiguration Dbconfig { get; set; } = default!;
    }
}
