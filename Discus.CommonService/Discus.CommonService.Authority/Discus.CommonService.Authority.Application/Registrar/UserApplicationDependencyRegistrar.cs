using Discus.CommonService.Authority.Application.MessageHandler;
using Discus.Shared.Application.Registrar;
using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Discus.CommonService.Authority.Application.Registrar
{
    public sealed class UserApplicationDependencyRegistrar : AbstractApplicationDependencyRegistrar
    {
        public static readonly string ApplicationFullName = Assembly.GetExecutingAssembly().FullName;
        public override Assembly ApplicationLayerAssembly => Assembly.GetExecutingAssembly();

        public override Assembly ContractsLayerAssembly => typeof(ILoginService).Assembly;

        public UserApplicationDependencyRegistrar(IServiceCollection services) : base(services)
        {

        }

        public override void AddService()
        {
            AddApplicaitonDefault();

            //rabbitmq按需引入
            AddEventBusCap();

            Services.AddScoped<ICapSubscribe, CapSubscribe>();
        }
    }
}
