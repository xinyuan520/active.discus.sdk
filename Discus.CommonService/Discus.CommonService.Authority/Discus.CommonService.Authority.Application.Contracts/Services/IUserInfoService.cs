using Discus.CommonService.Authority.Application.Contracts.Dtos;
using Discus.Shared.WebApi.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.CommonService.Authority.Application.Contracts.Services
{
    public interface IUserInfoService : IService
    {
        Task<List<UserInfoDto>> GetAll();

        Task<UserInfoDto> GetById(long Id);

        Task<UserInfoDto> GetById(AutoInfoModel autoInfoModel);
    }
}
