using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Consul.Configurations
{
    public class BasicConsulSource : IConfigurationSource
    {
        private readonly ConsulClient _configClient;
        private readonly string _consulKeyPath;
        private readonly bool _reloadOnChanges;

        public BasicConsulSource(ConsulClient configClient, string consulKeyPath, bool reloadOnChanges)
        {
            _configClient = configClient;
            _consulKeyPath = consulKeyPath;
            _reloadOnChanges = reloadOnChanges;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new BasicConsulProvider(_configClient, _consulKeyPath, _reloadOnChanges);
        }
    }
}
