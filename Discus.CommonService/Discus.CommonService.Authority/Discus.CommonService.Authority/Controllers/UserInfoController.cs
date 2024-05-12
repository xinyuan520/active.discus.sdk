using Discus.CommonService.Authority.Application.Contracts.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Discus.CommonService.Authority.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("ShowUser")]
        public string ShowUser()
        {
           return _userInfoService.ShowUser();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<List<UserInfoDto>> GetAll()
        {
            return await _userInfoService.GetAll();
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
    }
}
