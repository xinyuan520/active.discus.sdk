using Discus.Shared.WebApi.Authorization;

namespace Discus.Shared.Webapi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar : IDependencyRegistrar
    {
        public string Name => "";
        protected IConfiguration Configuration { get; init; }
        protected IServiceCollection Services { get; init; }
        protected IServiceInfo ServiceInfo { get; init; }
        public AbstractWebApiDependencyRegistrar(IServiceCollection services)
        {
            Services = services;
            Configuration = services.GetConfiguration();
            ServiceInfo = services.GetServiceInfo();
        }

        public abstract void AddService();

        protected virtual void AddWebApiDefault()
        {
            Services.AddHttpContextAccessor().AddMemoryCache().AddControllers();
            AddAuthentication(Configuration);
            AddAuthorization();
            Services.AddEndpointsApiExplorer();
            Services.AddAutoMapper(ServiceInfo.GetApplicationAssembly());
            Configure();
            AddCors();
            AddHealthChecks(false, true);
            AddSwaggerGen();
            AddMiniProfiler();
            AddApplicationServices();
        }
    }
}
