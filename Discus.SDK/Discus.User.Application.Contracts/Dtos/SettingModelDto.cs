using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.Contracts.Dtos
{
    public class SettingModelDto
    {
        public string Name { get; set; }

        /// <summary>
        /// 是否开启Redis
        /// </summary>
        public int EnableRedis { get; set; }

        /// <summary>
        /// Redis连接配置
        /// </summary>
        public string RedisSetting { get; set; }
    }
}
