using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.RabbitMQ.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEventBusCap(this IServiceCollection services, Action<CapOptions> setupAction)
        {
            if (services.HasRegistered(nameof(AddEventBusCap)))
                return services;

            services.AddCap(setupAction);
            return services;
        }
    }
}
