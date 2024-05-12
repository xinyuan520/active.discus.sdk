using Discus.Shared.WebApi.Authorization.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Discus.Shared.Webapi.Registrar
{
    public abstract partial class AbstractWebApiDependencyRegistrar
    {
        /// <summary>
        /// <summary>
        /// 注册身份认证组件
        /// </summary>
        protected virtual void AddAuthentication(IConfiguration Configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = JwtTokenHelper.GenarateTokenValidationParameters(Configuration.GetSection("JWT").Get<JWTConfig>());
                 });
        }
    }


}
