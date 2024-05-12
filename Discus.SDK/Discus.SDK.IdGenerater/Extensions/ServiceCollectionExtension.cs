using Discus.SDK.IdGenerater.IdGeneraterFactory;
using Discus.SDK.Redis.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.IdGenerater.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddIdGenerater(this IServiceCollection services, IConfigurationSection redisSection)
        {
            if (services.HasRegistered(nameof(AddIdGenerater)))
                return services;

            return services.AddSingleton<WorkerNode>()
                .AddHostedService<WorkerNodeHostedService>();
        }
    }
}
