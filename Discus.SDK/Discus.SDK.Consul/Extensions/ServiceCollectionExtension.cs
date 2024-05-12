using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Consul.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServiceConsul(this IServiceCollection services, IConfigurationSection consulSection)
        {
            if (services.HasRegistered(nameof(AddServiceConsul)))
                return services;

            return services
                .Configure<ConsulConfig>(consulSection)
                .AddSingleton(provider =>
                {
                    var configOptions = provider.GetService<IOptions<ConsulConfig>>();
                    if (configOptions is null)
                        throw new NullReferenceException(nameof(configOptions));
                    return new ConsulClient(x => x.Address = new Uri(configOptions.Value.ConsulUrl));
                })
                ;
        }
    }
}
