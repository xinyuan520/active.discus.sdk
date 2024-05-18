using Discus.CommonService.Authority.Application.Contracts.Dtos;
using Discus.Shared.Webapi.Controller;
using Discus.Shared.WebApi.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Discus.CommonService.Authority.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : BasicController
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        [HttpGet("GetAll"),]
        public async Task<List<UserInfoDto>> GetAll()
        {
            return await _userInfoService.GetAll();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        //[CustomerAuthorize]
        [HttpGet("GetById"), CustomerAuthorize(AuthCode = "")]
        public async Task<UserInfoDto> GetById()
        {
            AutoInfoModel autoInfoModel = this.GetAutoInfoModel();
            return await _userInfoService.GetById(autoInfoModel);
        }
    }
}
