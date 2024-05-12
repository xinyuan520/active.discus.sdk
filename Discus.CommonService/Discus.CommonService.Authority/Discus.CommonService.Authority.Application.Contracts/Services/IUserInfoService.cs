using Discus.CommonService.Authority.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.CommonService.Authority.Application.Contracts.Services
{
    public interface IUserInfoService : IService
    {
        string ShowUser();

        Task<List<UserInfoDto>> GetAll();

        Task<UserInfoDto> GetById(long id);
    }
}
