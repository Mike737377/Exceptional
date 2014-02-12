using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure.DatabaseImpl.SqlServer
{
    public class SqlServerQueryAdaptor : IQueryAdaptor
    {
        private readonly IDatabaseAdaptor dbAdaptor;

        public SqlServerQueryAdaptor(IDatabaseAdaptor dbAdaptor)
        {
            this.dbAdaptor = dbAdaptor;
        }

        public TEntity[] ExecuteReader<TEntity>(Query query, int? maxResults = null)
            where TEntity : class
        {
            using (var command = BuildQueryCommand(query, maxResults))
            {
                return dbAdaptor.Execute<TEntity>(command, query.SelectFields);
            }
        }

        private DbCommand BuildQueryCommand(Query query, int? maxResults)
        {
            var sql = new StringBuilder();
            var command = new SqlCommand();

            sql.Append("SELECT ");
            if (query.IsCount)
            {
                sql.Append("COUNT(*)");
            }
            else
            {
                if (maxResults.HasValue)
                {
                    sql.AppendFormat("TOP {0} ", maxResults.Value);
                }
                sql.Append(query.SelectFields.Select(x => string.Format("[{0}]", x.Name)).Join(", "));
            }
            sql.AppendFormat(" FROM [{0}]", query.EntityType.Name);
            sql.AppendLine();
            if (query.WhereFields.Length > 0)
            {
                sql.Append("WHERE ");
                var whereFilters = new List<string>();
                var whereIndex = 0;
                query.WhereFields.Each(x =>
                {
                    whereFilters.Add(x.ToSqlString(whereIndex));
                    command.Parameters.AddWithValue("@" + whereIndex.ToString(), x.Value);
                    whereIndex++;
                });

                sql.AppendLine(whereFilters.Join(" AND "));
            }
            //if (sortExpression != null)
            //{
            //    sql.Append("ORDER BY ");
            //    sql.AppendLine(sortExpression.ToAccessor().FieldName);
            //}

            command.CommandText = sql.ToString();

            return command;
        }

        public int ExecuteScalar(Query query)
        {
            using (var command = BuildQueryCommand(query, null))
            {
                return dbAdaptor.ExecuteScalar(command);
            }
        }
    }
}