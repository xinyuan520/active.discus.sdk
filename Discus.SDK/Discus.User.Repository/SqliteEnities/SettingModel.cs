using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Repository.SqliteEnities
{
    [SugarTable("setting")]
    //[SugarTable("main.setting")]
    public  class SettingModel
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

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
