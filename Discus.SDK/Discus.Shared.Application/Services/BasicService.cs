using Discus.Shared.Application.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Discus.Shared.Application.Services
{
    public class BasicService : IService
    {
        protected Expression<Func<TEntity, object>> UpdatingProps<TEntity>(Expression<Func<TEntity, object>> expressions) => expressions;
        //protected Expression<Func<TEntity, object>>[] UpdatingProps<TEntity>(params Expression<Func<TEntity, object>>[] expressions) => expressions;
    }
}
