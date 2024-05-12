using Discus.SDK.Tools.HttpResult;
using Discus.User.Application.Contracts.Dtos;
using Discus.User.Repository.SqliteEnities;

namespace Discus.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqliteController : ControllerBase
    {
        private readonly ISqliteServices _sqliteServices;

        public SqliteController(ISqliteServices sqliteServices)
        {
            _sqliteServices = sqliteServices;
        }

        /// <summary>
        /// 新增配置项
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(SettingModelDto dto)
        {
            return await _sqliteServices.Create(dto);
        }

        /// <summary>
        /// 修改配置项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<ApiResult> Update(SettingModel model)
        {
            return await _sqliteServices.Update(model);
        }

        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Delete/{id}")]
        public async Task<ApiResult> Delete(int id)
        {
            return await _sqliteServices.Delete(id);
        }

        /// <summary>
        /// 查询配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<SettingModel> GetById(int id)
        {
            return await _sqliteServices.GetById(id);
        }
    }
}
