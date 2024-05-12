using Discus.SDK.Core.Configuration;
using Discus.SDK.Repository.SqlSugar.Repository;
using Discus.SDK.Tools.HttpResult.Enums;
using Discus.Shared.WebApi.Authorization.JwtBearer;
using Discus.User.Application.Contracts.Dtos;
using Discus.User.Repository.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.Services
{
    public class JwtService: BasicService, IJwtService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<UserInfo> _userinfoRepository;
        private readonly IOptions<JWTConfig> _jwtOptions;
        private readonly ILogger<JwtService> _logger;

        public JwtService(IMapper mapper, IBaseRepository<UserInfo> userinfoRepository, IOptions<JWTConfig> jwtOptions, ILogger<JwtService> logger)
        {
            _mapper = mapper;
            _userinfoRepository = userinfoRepository;
            _logger = logger;
            _jwtOptions = jwtOptions;
        }

        public async Task<ApiResult> Login(LoginRequestDto request)
        {
            var userInfo = await _userinfoRepository.FirstOrDefaultAsync(x => x.UserName == request.Account && x.Password == request.Password);
            if (userInfo != null)
            {
                var accessToken = JwtTokenHelper.CreateAccessToken(_jwtOptions.Value, userInfo.Id.ToString(), userInfo.UserName);
                var tokenInfo = new UserTokenInfoDto(accessToken.Token, accessToken.Expire);
                return ApiResult.IsSuccess("登录成功！", ApiResultCode.Succeed, tokenInfo);
            }
            return ApiResult.IsFailed("用户名或密码错误！");
        }

        public async Task<UserInfoDto> GetById(long id)
        {
            var userInfo = await _userinfoRepository.GetByIdAsync(id);
            return _mapper.Map<UserInfoDto>(userInfo);
        }
    }
}
