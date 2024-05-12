

namespace Discus.SDK.Core.Microsoft.Extensions.Hosting
{
    public static class HostExtensions
    {
        public static IHost ChangeThreadPoolSettings(this IHost host)
        {
            var poolOptions = host.Services.GetService(typeof(IOptions<ThreadPoolSettings>)) as IOptions<ThreadPoolSettings>;
            if (poolOptions is not null)
                ChangeThreadPoolSettings(host, poolOptions);
            return host;
        }

        public static IHost ChangeThreadPoolSettings(this IHost host, IOptions<ThreadPoolSettings> poolOptions)
        {
            if (host.Services.GetService(typeof(ILogger<IHost>)) is not ILogger<IHost> logger)
                throw new NullReferenceException(nameof(logger));

            var poolSetting = poolOptions.Value;
            ThreadPool.SetMinThreads(poolSetting.MinThreads, poolSetting.MinCompletionPortThreads);
            ThreadPool.SetMaxThreads(poolSetting.MaxThreads, poolSetting.MaxCompletionPortThreads);
            ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);
            ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);
            logger.LogInformation("Setting MinThreads={0},MinCompletionPortThreads={1}", workerThreads, completionPortThreads);
            logger.LogInformation("Setting MaxThreads={0},MaxCompletionPortThreads={1}", maxWorkerThreads, maxCompletionPortThreads);
            return host;
        }
    }
}
