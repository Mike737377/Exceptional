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
        public object where = null;
        public Expression<Func<TEntity, object[]>> sortExpressions = null;

        public readonly IQueryAdaptor queryAdaptor;
        private readonly Query joinedQuery;

        public QueryBuilder(IQueryAdaptor queryAdaptor)
        {
            this.queryAdaptor = queryAdaptor;
        }

        public QueryBuilder(IQueryAdaptor queryAdaptor, Query joinedQuery)
        {
            this.queryAdaptor = queryAdaptor;
            this.joinedQuery = joinedQuery;
        }

        public IQueryBuilder<TEntity> Where<TCriteria>(TCriteria criteria)
        {
            this.where = criteria;
            return this;
        }

        public IQueryBuilder<TEntity> SortBy(Expression<Func<TEntity, object[]>> sortExpressions)
        {
            this.sortExpressions = sortExpressions;
            return this;
        }

        public int Count()
        {
            return queryAdaptor.ExecuteScalar(Query.BuildQuery(typeof(TEntity), selector, where, sortExpressions, joinedQuery).AsCount());
        }

        public TEntity[] ToArray()
        {
            return queryAdaptor.ExecuteReader<TEntity>(Query.BuildQuery(typeof(TEntity), selector, where, sortExpressions, joinedQuery));
        }

        public TEntity FirstOrDefault()
        {
            var result = queryAdaptor.ExecuteReader<TEntity>(Query.BuildQuery(typeof(TEntity), selector, where, sortExpressions, joinedQuery), 1);
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

        public IQueryBuilder<TJoin> Join<TJoin>(TJoin join)
            where TJoin : class
        {
            var query = Query.BuildQuery(typeof(TEntity), selector, where, sortExpressions, joinedQuery);
            return new QueryBuilder<TJoin>(queryAdaptor, query);
        }
    }
}