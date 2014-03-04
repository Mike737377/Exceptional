using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure.DatabaseImpl
{
    public class WhereField
    {
        public string Name { get; set; }
        public string Comparer { get; set; }
        public object Value { get; set; }

        public string ToSqlString(int param)
        {
            return string.Format("[{0}] {1} @{2}", Name, Comparer, param);
        }
    }

    public class SelectField
    {
        public PropertyInfo EntityProperty { get; set; }
        public string Name { get; set; }
    }

    public class SetField
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    public class Query
    {
        public Type EntityType { get; private set; }
        public SelectField[] SelectFields { get; private set; }
        public WhereField[] WhereFields { get; private set; }
        public Query JoinedQuery { get; private set; }

        public Query(Type entityType, SelectField[] selectFields, WhereField[] whereFields)
        {
            this.EntityType = entityType;
            this.SelectFields = selectFields;
            this.WhereFields = whereFields;
        }

        public static Query BuildQuery(Type entityType, object selector, object where, object sortExpressions, Query joinedQuery)
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

        private string[] GetJoinedFields()
        {
            List<string> joinedFields = new List<string>();

            EntityType.GetProperties().Each(right =>
                {
                    JoinedQuery.EntityType.GetProperties().Each(left =>
                        {
                            if (right.Name.Equals(left.Name))
                            {
                                joinedFields.Add(left.Name);
                            }
                        });
                });

            return joinedFields.ToArray();
        }

        public Query AsCount()
        {
            IsCount = true;
            return this;
        }

        public bool IsCount { get; set; }
    }
}