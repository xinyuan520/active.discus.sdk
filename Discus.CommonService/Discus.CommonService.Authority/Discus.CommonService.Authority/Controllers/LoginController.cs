using Discus.CommonService.Authority.Application.Contracts.Dtos;
using Discus.SDK.Tools.HttpResult;
using Microsoft.AspNetCore.Authorization;

namespace Discus.CommonService.Authority.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ApiResult> Login(LoginRequestDto request)
        {
            return await _loginService.Login(request);
        }
    }
}
