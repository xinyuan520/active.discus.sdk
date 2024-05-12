using Discus.Shared.WebApi.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.RegularExpressions;

namespace Discus.Shared.Webapi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar : IMiddlewareRegistrar
    {
        protected readonly IApplicationBuilder App;
        protected AbstractWebApiDependencyRegistrar(IApplicationBuilder app)
        {
            App = app;
        }

        /// <summary>
        /// 注册中间件入口方法
        /// </summary>
        /// <param name="app"></param>
        public abstract void UseSharedDefault();

        /// <summary>
        /// 注册webapi通用中间件
        /// </summary>
        protected virtual void UseWebApiDefault(Action<IEndpointRouteBuilder> endpointRoute = null)
        {
            ServiceLocator.Provider = App.ApplicationServices;
            var serviceInfo = App.ApplicationServices.GetService<IServiceInfo>();
            var consulOptions = App.ApplicationServices.GetService<IOptions<ConsulConfig>>();
            App.UseSwagger(c => c.RouteTemplate = $"/{serviceInfo.ServiceName}/swagger/{{documentName}}/swagger.json")
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{serviceInfo.ServiceName}/swagger/{serviceInfo.Version}/swagger.json", $"{serviceInfo.ServiceName}-{serviceInfo.Version}");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
                c.DefaultModelsExpandDepth(-1);//不显示Models
            })
            .UseHealthChecks($"/{consulOptions.Value.HealthCheckUrl}", new HealthCheckOptions()
            {
                Predicate = _ => true,
                // 该响应输出是一个json，包含所有检查项的详细检查结果
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            })
            .UseRouting()
            .UseHttpMetrics()
            .UseExceptionMiddlewareHandler();

            App.UseAuthentication();
            App.UseAuthorization();

            App.UseEndpoints(endpoints =>
            {
                endpointRoute?.Invoke(endpoints);
                endpoints.MapControllers();
            });
        }
    }
}
