using AutoMapper;
using Discus.CommonService.Authority.Application.Contracts.Dtos;
using Discus.CommonService.Authority.Repository.Entities;
using Discus.SDK.Core.Configuration;
using Discus.SDK.Redis;
using Discus.SDK.Repository.SqlSugar.Repository;
using Discus.SDK.Tools.HttpResult;
using Discus.SDK.Tools.HttpResult.Enums;
using Discus.Shared.WebApi.Authorization;
using Discus.Shared.WebApi.Authorization.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;

namespace Discus.CommonService.Authority.Application.Services
{
    public class LoginService : BasicService, ILoginService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<UserInfo> _userinfoRepository;
        private readonly IOptions<JWTConfig> _jwtOptions;
        private readonly ILogger<LoginService> _logger;
        private readonly IRedisClient _redisClient;


        public LoginService(IMapper mapper, IBaseRepository<UserInfo> userinfoRepository, IOptions<JWTConfig> jwtOptions, ILogger<LoginService> logger, IRedisClient redisClient)
        {
            _mapper = mapper;
            _userinfoRepository = userinfoRepository;
            _logger = logger;
            _jwtOptions = jwtOptions;
            _redisClient= redisClient;
        }

        public async Task<ApiResult> Login(LoginRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                return ApiResult.IsFailed("用户名或密码不能为空！");
            }

            var userInfo = await _userinfoRepository.FirstOrDefaultAsync(x => x.UserName == request.Account && x.Password == request.Password);
            if (userInfo != null)
            {
                if (userInfo.State)
                { 
                    return ApiResult.IsFailed("用户已被禁用！");
                }
                //var redist = _redisClient.StringGet(userInfo.Id.ToString());
                //var failLoginCount = await _cacheService.GetFailLoginCountByUserIdAsync(user.Id);


                var accessToken = JwtTokenHelper.CreateAccessToken(_jwtOptions.Value, userInfo.Id.ToString(), userInfo.UserName);
                var tokenInfo = new UserTokenInfoDto(accessToken.Token, accessToken.Expire);
                return ApiResult.IsSuccess("登录成功！", ApiResultCode.Succeed, tokenInfo);
            }
            return ApiResult.IsFailed("用户名或密码错误！");
        }
    }
}
