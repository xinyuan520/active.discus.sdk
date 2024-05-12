using Discus.SDK.Redis.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace Discus.Shared.Webapi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar
    {
        /// <summary>
        /// 注册健康监测组件
        /// </summary>
        protected IHealthChecksBuilder AddHealthChecks(bool checkingMysql = true,bool checkingRedis=true)
        {
            var checksBuilder = Services.AddHealthChecks();

            if (checkingMysql)
            {
                var mysqlConnectionString = Configuration.GetValue(NodeConsts.Mysql_ConnectionString, string.Empty);
                if (mysqlConnectionString.IsNullOrEmpty())
                    throw new NullReferenceException("mysqlconfig is null");
                checksBuilder.AddMySql(mysqlConnectionString);
            }

            if (checkingRedis)
            {
                var redisConfig = Configuration.GetSection(NodeConsts.Redis).Get<RedisConfiguration>();
                if (redisConfig is null)
                    throw new NullReferenceException("redisConfig is null");
                checksBuilder.AddRedis(redisConfig.Dbconfig.ConnectionString);
            }

            return checksBuilder;
        }
    }
}
