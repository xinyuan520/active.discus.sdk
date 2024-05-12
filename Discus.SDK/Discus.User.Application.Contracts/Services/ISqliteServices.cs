using Discus.User.Application.Contracts.Dtos;
using Discus.User.Repository.SqliteEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.Contracts.Services
{
    public interface ISqliteServices : IService
    {
        Task<ApiResult> Create(SettingModelDto model);

        Task<ApiResult> Update(SettingModel model);

        Task<ApiResult> Delete(int id);

        Task<SettingModel> GetById(int id);
    }
}
