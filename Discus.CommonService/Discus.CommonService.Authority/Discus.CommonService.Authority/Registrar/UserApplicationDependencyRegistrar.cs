using Discus.Shared.Webapi.Registrar;

namespace Discus.CommonService.Authority.Registrar
{
    public sealed class UserApplicationDependencyRegistrar : AbstractWebApiDependencyRegistrar
    {
        public UserApplicationDependencyRegistrar(IServiceCollection services) : base(services)
        {
        }

        public UserApplicationDependencyRegistrar(IApplicationBuilder app) : base(app)
        {

        }

        public override void AddService()
        {
            AddWebApiDefault();
        }

        public override void UseSharedDefault()
        {
            UseWebApiDefault(endpointRoute: endpoint =>
            {
            });
        }
    }
}
