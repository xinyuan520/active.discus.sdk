using Discus.User.Application.Contracts.Dtos;
using Discus.User.Repository.SqliteEnities;
using SqlSugar;

namespace Discus.User.Application.Services
{
    public class SqliteServices : ISqliteServices
    {

        private readonly ISqlSugarClient _sugarClient;
        private readonly IMapper _mapper;

        public SqliteServices(ISqlSugarClient sugarClient, IMapper mapper)
        {
            _sugarClient = sugarClient;
            _mapper = mapper;
            CreateTable();
        }

        private void CreateTable()
        {
            // 创建表
            _sugarClient.CodeFirst.InitTables(typeof(SettingModel));
        }

        public async Task<ApiResult> Create(SettingModelDto dto)
        {
            var model = _mapper.Map<SettingModel>(dto);
            var result = await _sugarClient.Insertable(model).ExecuteCommandAsync();
            return result > 0 ? ApiResult.IsSuccess("新增成功！") : ApiResult.IsFailed("新增失败！");
        }

        public async Task<ApiResult> Update(SettingModel model)
        {
            var result = await _sugarClient.Updateable(model).ExecuteCommandAsync();
            return result > 0 ? ApiResult.IsSuccess("编辑成功！") : ApiResult.IsFailed("编辑失败！");
        }

        public async Task<ApiResult> Delete(int id)
        {
            var result = await _sugarClient.Deleteable<SettingModel>().Where(x => x.Id == id).ExecuteCommandAsync();
            return result > 0 ? ApiResult.IsSuccess("删除成功！") : ApiResult.IsFailed("删除失败！");
        }

        public async Task<SettingModel> GetById(int id)
        {
            var result = await _sugarClient.Queryable<SettingModel>().Where(x => x.Id == id).FirstAsync();
            return result != null ? result : new SettingModel();
        }
    }
}
