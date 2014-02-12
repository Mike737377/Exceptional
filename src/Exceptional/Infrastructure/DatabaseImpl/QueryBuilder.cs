using FubuCore.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure.DatabaseImpl
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

    public class QueryBuilder<TEntity> : IQueryBuilder<TEntity>
        where TEntity : class
    {
        public object selector = null;
        public object criteria = null;
        public Expression<Func<TEntity, object[]>> sortExpressions = null;

        public string ExecutedSqlCommandString = null;

        public readonly MsSqlQueryAdaptor queryAdaptor;

        public QueryBuilder(MsSqlQueryAdaptor queryAdaptor)
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

    public class Query
    {
        public Type EntityType { get; private set; }
        public SelectField[] SelectFields { get; private set; }
        public WhereField[] WhereFields { get; private set; }

        public Query(Type entityType, SelectField[] selectFields, WhereField[] whereFields)
        {
            this.EntityType = entityType;
            this.SelectFields = selectFields;
            this.WhereFields = whereFields;
        }

        public static Query BuildQuery(Type entityType, object selector, object where)
        {
            var selectFields = GetSelectFields(entityType, selector);
            var whereFields = GetWhereFields(entityType, where);

            return new Query(entityType, selectFields, whereFields);
        }

        private static SelectField[] GetSelectFields(Type entityType, object selector)
        {
            var names = new List<SelectField>();
            entityType.GetProperties().Each(x => names.Add(new SelectField
            {
                Name = x.Name,
                EntityProperty = x
            }));
            return names.ToArray();
        }

        private static WhereField[] GetWhereFields(Type entityType, object where)
        {
            if (where == null)
            {
                return new WhereField[] { };
            }

            var whereFields = new List<WhereField>();
            var properties = where.GetType().GetProperties();
            properties.Each(x =>
            {
                var propertyType = x.PropertyType;
                var isString = propertyType == typeof(string);
                var defaultValue = isString ? string.Empty : Activator.CreateInstance(propertyType);
                var value = x.GetValue(where);

                if (value != null && !value.Equals(defaultValue))
                {
                    var comparer = isString ? "LIKE" : "=";
                    whereFields.Add(new WhereField() { Name = x.Name, Comparer = comparer, Value = value });
                }
            });

            return whereFields.ToArray();
        }

        public Query AsCount()
        {
            IsCount = true;
            return this;
        }

        public bool IsCount { get; set; }
    }
}