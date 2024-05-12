using Discus.SDK.Repository.SqlSugar.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Repository.SqlSugar.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly ISqlSugarClient _sugarClient;

        public BaseRepository(ISqlSugarClient sugarClient)
        {
            _sugarClient = sugarClient;
        }

        public async Task<int> AddAsync(TEntity model)
        {
            return await _sugarClient.Insertable(model).ExecuteCommandAsync();
        }
        public async Task<int> AddRangeAsync(List<TEntity> list)
        {
            return await _sugarClient.Insertable(list).ExecuteCommandAsync();
        }
        public async Task<int> UpdateAsync(TEntity model)
        {
            return await _sugarClient.Updateable(model).ExecuteCommandAsync();
        }
        public async Task<int> UpdateIgnoreAsync(TEntity model, Expression<Func<TEntity, object>> expression)
        {
            return await _sugarClient.Updateable(model).IgnoreColumnsIF(true, expression).ExecuteCommandAsync();
        }
        public async Task<int> UpdateContainAsync(TEntity model, Expression<Func<TEntity, object>> expression)
        {
            return await _sugarClient.Updateable(model).UpdateColumns(expression).ExecuteCommandAsync();
        }
        public async Task<TEntity> GetByIdAsync(long id) 
        {
            return await _sugarClient.Queryable<TEntity>().InSingleAsync(id);
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _sugarClient.Queryable<TEntity>().ToListAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _sugarClient.Queryable<TEntity>().FirstAsync(whereExpression);
        }
    }
}
