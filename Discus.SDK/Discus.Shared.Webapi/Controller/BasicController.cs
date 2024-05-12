using Discus.Shared.WebApi.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Profiling.Internal;
using System.Net;
using System.Security.Claims;

namespace Discus.Shared.Webapi.Controller
{
    public abstract class BasicController : ControllerBase
    {
        [HttpGet]
        public AutoInfoModel GetAutoInfoModel()
        {
            AutoInfoModel autoInfoModel = new AutoInfoModel();
            var authHeader = Request.Headers["Authorization"].ToString();
            if (authHeader is not null && authHeader.StartsWith(JwtBearerDefaults.AuthenticationScheme))
            {
                var startIndex = JwtBearerDefaults.AuthenticationScheme.Length + 1;
                var token = authHeader[startIndex..].Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                if (!token.IsNullOrWhiteSpace())
                {
                    JwtSecurityToken jwt = tokenHandler.ReadJwtToken(token);
                    autoInfoModel.Id= long.Parse(jwt.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value);
                    autoInfoModel.UserName = jwt.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                }
            }
            return autoInfoModel;
        }
    }
}
