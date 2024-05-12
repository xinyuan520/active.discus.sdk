using System.Reflection;

namespace Discus.Shared.Webapi
{
    public class ServiceInfo : IServiceInfo
    {
        private static ServiceInfo _instance;
        private static readonly object _lockObj = new();

        public string Id { get; private set; }
        public string ServiceName { get; private set; }
        public string AppAssemblyName { get; private set; }
        
        public string CorsPolicy { get; set; }
        public string ShortName { get; private set; }
        public string Version { get; private set; }
        public string Description { get; private set; }
        public Assembly StartAssembly { get; private set; }

        private ServiceInfo()
        {
        }

        public static ServiceInfo CreateInstance(Assembly startAssembly,string applicationFullName)
        {
            if (_instance is not null)
                return _instance;

            lock (_lockObj)
            {
                if (_instance is not null)
                    return _instance;

                if (startAssembly is null)
                    startAssembly = Assembly.GetEntryAssembly();

                var description = startAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;
                var assemblyName = startAssembly.GetName();
                var version = assemblyName.Version;
                var fullName = assemblyName.Name.ToLower();
                var serviceName = fullName.Replace(".", "-");
                var ticks = Convert.ToInt64(new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds());
                var ticksHex = Convert.ToString(ticks, 16);
                _instance = new ServiceInfo
                {
                    Id = $"{serviceName}-{ticksHex}",
                    ServiceName = serviceName,
                    AppAssemblyName = applicationFullName,
                    ShortName = fullName.Split(".")[^2],
                    CorsPolicy = "default",
                    StartAssembly = startAssembly,
                    Description = description,
                    Version = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}"
                };
            }
            return _instance;
        }

        public static ServiceInfo GetInstance()
        {
            if (_instance is null)
                throw new NullReferenceException(nameof(ServiceInfo));

            return _instance;
        }
    }
}
