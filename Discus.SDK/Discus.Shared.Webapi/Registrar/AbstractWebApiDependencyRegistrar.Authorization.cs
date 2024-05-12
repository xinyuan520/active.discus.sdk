using Discus.Shared.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Discus.Shared.Webapi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar
    {
        /// <summary>
        /// 注册授权组件
        /// PermissionHandlerRemote 跨服务授权
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        protected virtual void AddAuthorization()
        {
            Services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            Services.AddAuthorization(options =>
                {
                    options.AddPolicy(AuthorizePolicy.Policy, policy =>
                    {
                        policy.Requirements.Add(new PermissionRequirement());
                    });
                });
        }
    }


}
