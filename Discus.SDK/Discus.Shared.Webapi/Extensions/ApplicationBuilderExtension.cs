using Discus.SDK.Core.System.Extensions;
using Discus.Shared.Webapi.Registrar;

namespace Discus.Shared.Webapi.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {
            var serviceInfo = app.ApplicationServices.GetRequiredService<IServiceInfo>();
            var middlewareRegistarType = serviceInfo.StartAssembly.ExportedTypes.FirstOrDefault(m => m.IsAssignableTo(typeof(IMiddlewareRegistrar)) && m.IsNotAbstractClass(true));
            if (middlewareRegistarType is null)
                throw new NullReferenceException(nameof(IMiddlewareRegistrar));

            var middlewareRegistar = Activator.CreateInstance(middlewareRegistarType, app) as IMiddlewareRegistrar;
            middlewareRegistar.UseSharedDefault();

            return app;
        }
    }
}
