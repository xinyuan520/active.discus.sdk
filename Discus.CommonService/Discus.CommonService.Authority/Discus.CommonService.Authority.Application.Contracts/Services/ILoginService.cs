using Discus.CommonService.Authority.Application.Contracts.Dtos;
using Discus.SDK.Tools.HttpResult;
using Discus.Shared.Application.Services;
using Discus.Shared.WebApi.Authorization;

namespace Discus.CommonService.Authority.Application.Contracts.Services
{
    public interface ILoginService : IService
    {
        Task<ApiResult> Login(LoginRequestDto request);
    }
}
