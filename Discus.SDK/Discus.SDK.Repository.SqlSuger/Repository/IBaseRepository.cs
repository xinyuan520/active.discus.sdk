using Discus.SDK.Repository.SqlSugar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Repository.SqlSugar.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {

        /// <summary>
        /// 增加单条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> AddAsync(TEntity model);

        /// <summary>
        /// 增加多条数据
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns>操作是否成功</returns>
        Task<int> AddRangeAsync(List<TEntity> list);

        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity model);

        /// <summary>
        /// 更新单条数据 忽略筛选条件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> UpdateIgnoreAsync(TEntity model, Expression<Func<TEntity, object>> expression);

        /// <summary>
        /// 更新单条数据 包含筛选条件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> UpdateContainAsync(TEntity model, Expression<Func<TEntity, object>> expression);

        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(long id);

        /// <summary>
        /// 查询所有列表
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression);
    }
}
