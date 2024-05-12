using Discus.SDK.Consul.Provider;
using Discus.SDK.Core.Configuration;
using Microsoft.Extensions.Logging;

namespace Discus.SDK.Consul.Extensions
{
    public static class ConsulExtensions
    {  
        public static IHost UseConsul(this IHost host, string? serviceId = null)
        {
            var kestrelConfig = host.Services.GetRequiredService<IOptions<KestrelConfig>>().Value;
            if (kestrelConfig is null)
                throw new NotImplementedException(nameof(kestrelConfig));

            var registration = ActivatorUtilities.CreateInstance<ConsulProvider>(host.Services);
            var ipAddresses = registration.GetLocalIpAddress("InterNetwork");
            if (ipAddresses == null|| !ipAddresses.Any())
                throw new NotImplementedException(nameof(kestrelConfig));

            var defaultEnpoint = kestrelConfig.Endpoints.FirstOrDefault(x => string.Equals(x.Key,"default", StringComparison.OrdinalIgnoreCase)).Value;
            if (defaultEnpoint is null || string.IsNullOrWhiteSpace(defaultEnpoint.Url))
                throw new NotImplementedException(nameof(kestrelConfig));

            var serviceAddress = new Uri(defaultEnpoint.Url);
            if (serviceAddress.Host == "0.0.0.0")
                serviceAddress = new Uri($"{serviceAddress.Scheme}://{ipAddresses.FirstOrDefault()}:{serviceAddress.Port}");

            Console.WriteLine(serviceAddress.AbsoluteUri);
            registration.Register(serviceAddress, serviceId);
            return host;
        }


    }
}
