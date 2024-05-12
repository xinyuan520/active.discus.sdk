using Discus.User.Application.Contracts.Dtos;
using Discus.User.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.Contracts.Services
{
    public interface IUserInfoService : IService
    {

        Task<ApiResult> Create(UserInfoRequestDto request);
        Task<ApiResult> Delete(long userId);
        Task<ApiResult> Update(UserInfoRequestDto request);
        Task<UserInfoDto> GetById(long id);
        Task<List<UserInfoDto>> GetAll();
    }
}
