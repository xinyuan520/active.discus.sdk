using Consul;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Discus.SDK.Consul.Provider
{
    public sealed class ConsulProvider
    {
        private readonly IOptions<ConsulConfig> _consulConfig;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<ConsulProvider> _logger;
        private readonly ConsulClient _consulClient;

        public ConsulProvider(IOptions<ConsulConfig> consulConfig
            , IHostApplicationLifetime hostApplicationLifetime
            , ILogger<ConsulProvider> logger
            , ConsulClient consulClient)
        {
            _consulConfig = consulConfig;
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
            _consulClient = consulClient;
        }
        public void Register(Uri serviceAddress, string? serviceId = null)
        {
            if (serviceAddress is null)
                throw new ArgumentNullException(nameof(serviceAddress));

            var instance = GetAgentServiceRegistration(serviceAddress, serviceId);
            Register(instance);
        }

        public void Register(AgentServiceRegistration instance)
        {
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));

            //CheckConfig();
            var protocol = instance.Meta["Protocol"];
            _logger.LogInformation(@$"register to consul ({protocol}://{instance.Address}:{instance.Port})");
            _hostApplicationLifetime.ApplicationStarted.Register(async () => await _consulClient.Agent.ServiceRegister(instance));
            _hostApplicationLifetime.ApplicationStopping.Register(async () => await _consulClient.Agent.ServiceDeregister(instance.ID));
        }

        private AgentServiceRegistration GetAgentServiceRegistration(Uri serviceAddress, string? serviceId = null)
        {
            if (serviceAddress is null)
                throw new ArgumentNullException(nameof(serviceAddress));

            var protocol = serviceAddress.Scheme;
            var host = serviceAddress.Host;
            var port = serviceAddress.Port;
            var consulHost =string.Empty;
            int consulPort = 0;
            if (!string.IsNullOrEmpty(_consulConfig.Value.ConsulUrl))
            {
                var consulUrl = new Uri(_consulConfig.Value.ConsulUrl);
                consulHost= consulUrl.Host;
                consulPort = consulUrl.Port;
            }
            var registrationInstance = new AgentServiceRegistration()
            {
                ID = serviceId ?? $"{_consulConfig.Value.ServiceName}-{DateTime.Now.GetTotalMilliseconds()}",
                Name = _consulConfig.Value.ServiceName,
                Address = !string.IsNullOrEmpty(consulHost) ? consulHost : host,
                Port = consulPort > 0 ? consulPort : port,
                Meta = new Dictionary<string, string>() { ["Protocol"] = protocol },
                Tags = _consulConfig.Value.ServerTags,
                Check = new AgentServiceCheck
                {
                    //服务停止多久后进行注销
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(_consulConfig.Value.DeregisterCriticalServiceAfter),
                    //健康检查间隔,心跳间隔
                    Interval = TimeSpan.FromSeconds(_consulConfig.Value.HealthCheckIntervalInSecond),
                    //健康检查地址
                    HTTP = $"{protocol}://{host}:{port}/{_consulConfig.Value.HealthCheckUrl}",
                    //超时时间
                    Timeout = TimeSpan.FromSeconds(_consulConfig.Value.Timeout),
                }
            };
            return registrationInstance;
        }

        #region 获取Ip地址
        /// <summary>
        /// 获取Ip地址
        /// </summary>
        /// <param name="netType"></param>
        /// <returns></returns>
        public List<string> GetLocalIpAddress(string netType)
        {
            string hostName = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);

            var IPList = new List<string>();
            if (netType == string.Empty)
            {
                for (int i = 0; i < addresses.Length; i++)
                {
                    IPList.Add(addresses[i].ToString());
                }
            }
            else
            {
                //AddressFamily.InterNetwork = IPv4,
                //AddressFamily.InterNetworkV6= IPv6
                for (int i = 0; i < addresses.Length; i++)
                {
                    if (addresses[i].AddressFamily.ToString() == netType)
                    {
                        IPList.Add(addresses[i].ToString());
                    }
                }
            }
            return IPList;
        }
        #endregion
    }
}
