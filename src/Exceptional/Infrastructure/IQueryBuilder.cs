using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    public interface IQueryBuilder<TEntity>
           where TEntity : class
    {
        IQueryBuilder<TEntity> Select<TSelector>(TSelector selector);

        IQueryBuilder<TEntity> Where<TCriteria>(TCriteria criteria);

        IQueryBuilder<TEntity> SortBy(Expression<Func<TEntity, object[]>> sortExpressions);

        int Count();

        TEntity[] ToArray();

        TEntity FirstOrDefault();
    }
}