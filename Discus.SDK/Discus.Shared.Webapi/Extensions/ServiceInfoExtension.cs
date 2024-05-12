namespace Discus.Shared.Webapi.Extensions
{
    public static class ServiceInfoExtension
    {
        private static readonly object lockObj = new();
        private static Assembly appAssembly;

        /// <summary>
        /// 获取WebApiAssembly程序集
        /// </summary>
        /// <returns></returns>     
        public static Assembly GetWebApiAssembly(this IServiceInfo serviceInfo) => serviceInfo.StartAssembly;

        /// <summary>
        /// 获取Application程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly GetApplicationAssembly(this IServiceInfo serviceInfo)
        {
            if (appAssembly is null)
            {
                lock (lockObj)
                {
                    if (appAssembly is null)
                    {
                        var appAssemblyName = serviceInfo.AppAssemblyName;
                        appAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => string.Equals(x.FullName, appAssemblyName, StringComparison.OrdinalIgnoreCase));
                        if (appAssembly is null)
                            appAssembly = Assembly.Load(appAssemblyName);
                    }
                }
            }
            return appAssembly;
        }
    }
}
