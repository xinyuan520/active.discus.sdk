using Discus.SDK.Consul.Configurations;
using Serilog;

namespace Discus.Shared.WebApi.Extensions
{
    public static class SerilogBuilderExtension
    {
        public static WebApplicationBuilder UseDefaultSerilog(this WebApplicationBuilder builder, LogConfig logConfig)
        {
            // 使用日志
            builder.Host.UseSerilog((context, logger) =>
            {
                logger
                 .Enrich.FromLogContext()
                 .ReadFrom.Configuration(context.Configuration)
                 .WriteTo.MySQL(logConfig.ConnectionString, logConfig.TableName);
            });

            return builder;
        }
    }
}
