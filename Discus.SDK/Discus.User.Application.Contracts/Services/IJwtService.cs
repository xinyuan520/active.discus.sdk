using Discus.User.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.Contracts.Services
{
    public interface IJwtService : IService
    {
        Task<ApiResult> Login(LoginRequestDto request);
        Task<UserInfoDto> GetById(long id);
    }
}
