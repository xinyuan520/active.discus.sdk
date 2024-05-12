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
        /// ShowInfo
        /// </summary>
        /// <returns></returns>
        [HttpGet("ShowInfo")]
        public string ShowInfo()
        {
            return _loginService.ShowInfo();
        }

    }
}
