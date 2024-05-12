using Discus.SDK.Consul.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Consul.Extensions
{
    public static class ConfigurationBuilderExtension
    {
        public static IConfigurationBuilder AddConsulConfiguration(this IConfigurationBuilder configurationBuilder, ConsulConfiguration config, bool reloadOnChanges = false)
        {
            var consulClient = new ConsulClient(client => client.Address = new Uri(config.ConsulUrl));
            var pathKeys = config.ConsulKeyPath.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (var pathKey in pathKeys)
            {
                configurationBuilder.Add(new BasicConsulSource(consulClient, pathKey, reloadOnChanges));
            }
            return configurationBuilder;
        }
    }
}
