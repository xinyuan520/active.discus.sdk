namespace Discus.Shared.Webapi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar
    {
        /// <summary>
        /// 注册配置类到IOC容器
        /// </summary>
        protected virtual void Configure()
        {
            Services
                .Configure<JWTConfig>(Configuration.GetSection(NodeConsts.JWT))
                .Configure<ThreadPoolSettings>(Configuration.GetSection(ThreadPoolSettings.Name))
                .Configure<KestrelConfig>(Configuration.GetSection(KestrelConfig.Name));
        }
    }
}
