using Discus.SDK.Tools.HttpResult;
using Discus.User.Application.Contracts.Dtos;

namespace Discus.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;
        private readonly ILogger<UserInfoController> _logger;
        public UserInfoController(IUserInfoService userInfoService, ILogger<UserInfoController> logger)
        {
            _logger = logger;
            _userInfoService = userInfoService;
        }

        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <returns></returns>
        [Route("Create"), HttpPost]
        public async Task<ApiResult> Create(UserInfoRequestDto request)
        {
            return await _userInfoService.Create(request);
        }


        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <returns></returns>
        [Route("Update"), HttpPost]
        public async Task<ApiResult> Update(UserInfoRequestDto request)
        {
            return await _userInfoService.Update(request);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("Delete/{userId}"), HttpDelete]
        public async Task<ApiResult> Delete(long userId)
        {
            return await _userInfoService.Delete(userId);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[CustomerAuthorize]
        [Route("GetById/{id}"), HttpGet]
        public async Task<UserInfoDto> GetById(long id)
        {
            return await _userInfoService.GetById(id);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<List<UserInfoDto>> GetAll()
        {
            return await _userInfoService.GetAll();
        }
    }
}
