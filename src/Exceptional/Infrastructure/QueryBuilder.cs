using Exceptional.Infrastructure.DatabaseImpl;
using FubuCore.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    public class QueryBuilder<TEntity> : IQueryBuilder<TEntity>
        where TEntity : class
    {
        public object selector = null;
        public object criteria = null;
        public Expression<Func<TEntity, object[]>> sortExpressions = null;

        public readonly IQueryAdaptor queryAdaptor;

        public QueryBuilder(IQueryAdaptor queryAdaptor)
        {
            this.queryAdaptor = queryAdaptor;
        }

        public IQueryBuilder<TEntity> Where<TCriteria>(TCriteria criteria)
        {
            this.criteria = criteria;
            return this;
        }

        public IQueryBuilder<TEntity> SortBy(Expression<Func<TEntity, object[]>> sortExpressions)
        {
            this.sortExpressions = sortExpressions;
            return this;
        }

        public int Count()
        {
            return queryAdaptor.ExecuteScalar(Query.BuildQuery(typeof(TEntity), selector, criteria).AsCount());
        }

        public TEntity[] ToArray()
        {
            return queryAdaptor.ExecuteReader<TEntity>(Query.BuildQuery(typeof(TEntity), selector, criteria));
        }

        public TEntity FirstOrDefault()
        {
            var result = queryAdaptor.ExecuteReader<TEntity>(Query.BuildQuery(typeof(TEntity), selector, criteria), 1);
            if (result == null || result.Length < 1)
            {
                return null;
            }

            return result[0];
        }

        public IQueryBuilder<TEntity> Select<TSelector>(TSelector selector)
        {
            this.selector = selector;
            return this;
        }
    }
}