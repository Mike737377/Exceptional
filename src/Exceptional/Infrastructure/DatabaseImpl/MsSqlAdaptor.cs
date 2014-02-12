using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure.DatabaseImpl
{
    public class MsSqlAdaptor : IDisposable
    {
        private readonly SqlConnection connection;

        public MsSqlAdaptor(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public void Execute(SqlCommand sqlCommand)
        {
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public TEntity[] Execute<TEntity>(SqlCommand command, SelectField[] selectFields)
            where TEntity : class
        {
            PrepareCommand(command);

            var entities = new List<TEntity>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var entity = Activator.CreateInstance<TEntity>();
                    var columnIndex = 0;
                    selectFields.Each(x =>
                        {
                            var value = reader.GetValue(columnIndex);
                            x.EntityProperty.SetValue(entity, value);
                            columnIndex++;
                        });
                    entities.Add(entity);
                }
            }

            return entities.ToArray();
        }

        private void PrepareCommand(SqlCommand command)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            command.Connection = connection;
            LogCommand(command);
        }

        public int ExecuteNonQuery(SqlCommand command)
        {
            PrepareCommand(command);
            return command.ExecuteNonQuery();
        }

        public int ExecuteScalar(SqlCommand command)
        {
            PrepareCommand(command);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        private void LogCommand(SqlCommand command)
        {
            var quotedTypes = new SqlDbType[]
            {
                SqlDbType.Date,
                SqlDbType.DateTime,
                SqlDbType.DateTime2,
                SqlDbType.DateTimeOffset,
                SqlDbType.Char,
                SqlDbType.NChar,
                SqlDbType.NText,
                SqlDbType.NVarChar,
                SqlDbType.SmallDateTime,
                SqlDbType.Text,
                SqlDbType.Time,
                SqlDbType.Timestamp,
                SqlDbType.UniqueIdentifier,
                SqlDbType.VarChar
            };

            var s = command.CommandText;
            foreach (SqlParameter p in command.Parameters)
            {
                var wrapWithQuotes = quotedTypes.Any(x => x == p.SqlDbType);
                var valueFormat = wrapWithQuotes ? "'{0}'" : "{0}";
                var value = string.Format(valueFormat, p.SqlValue.ToString());
                s = s.Replace(p.ParameterName, value);
            }
            Console.WriteLine(s);
        }
    }

    public class MsSqlUpdateAdaptor
    {
        private readonly MsSqlAdaptor dbAdaptor;

        public MsSqlUpdateAdaptor(MsSqlAdaptor dbAdaptor)
        {
            this.dbAdaptor = dbAdaptor;
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entityType = typeof(TEntity);
            var setters = GetSetFields(entityType, entity);
            using (var command = BuildUpdateCommand(setters, entityType))
            {
                dbAdaptor.ExecuteNonQuery(command);
            }
        }

        private SetField[] GetSetFields(Type entityType, object entity)
        {
            var names = new List<SetField>();
            entityType.GetProperties().Each(x => names.Add(new SetField
            {
                Name = x.Name,
                Value = x.GetValue(entity)
            }));
            return names.ToArray();
        }

        private SqlCommand BuildUpdateCommand(SetField[] setters, Type entityType)
        {
            var sql = new StringBuilder();
            var command = new SqlCommand();
            string idField = string.Format("{0}Id", entityType.Name);
            SetField primaryKey = null;

            var setterIndex = 0;
            var fields = new List<string>();
            sql.AppendFormat("UPDATE [{0}] SET ", entityType.Name);
            setters.Each(x =>
            {
                if (idField.Equals(x.Name))
                {
                    primaryKey = x;
                    return;
                }

                string field = String.Format("@{0}", setterIndex);
                fields.Add(string.Format("[{0}]={1}", x.Name, field));
                command.Parameters.AddWithValue(field, x.Value);
                setterIndex++;
            });
            sql.Append(fields.Join(", "));
            sql.AppendFormat(" WHERE [{0}]=@{1}", primaryKey.Name, setterIndex);
            command.Parameters.AddWithValue(string.Format("@{0}", setterIndex), primaryKey.Value);

            command.CommandText = sql.ToString();
            return command;
        }
    }

    public class MsSqlDeleteAdaptor
    {
        private readonly MsSqlAdaptor dbAdaptor;

        public MsSqlDeleteAdaptor(MsSqlAdaptor dbAdaptor)
        {
            this.dbAdaptor = dbAdaptor;
        }

        public void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entityType = typeof(TEntity);
            var setters = GetSetFields(entityType, entity);
            using (var command = BuildUpdateCommand(setters, entityType))
            {
                dbAdaptor.ExecuteNonQuery(command);
            }
        }

        private SetField[] GetSetFields(Type entityType, object entity)
        {
            var names = new List<SetField>();
            entityType.GetProperties().Each(x => names.Add(new SetField
            {
                Name = x.Name,
                Value = x.GetValue(entity)
            }));
            return names.ToArray();
        }

        private SqlCommand BuildUpdateCommand(SetField[] setters, Type entityType)
        {
            var sql = new StringBuilder();
            var command = new SqlCommand();
            string idField = string.Format("{0}Id", entityType.Name);
            SetField primaryKey = null;

            sql.AppendFormat("DELETE FROM [{0}] ", entityType.Name);
            setters.Each(x =>
            {
                if (idField.Equals(x.Name))
                {
                    primaryKey = x;
                    return;
                }
            });
            sql.AppendFormat("WHERE [{0}]=@0", primaryKey.Name);
            command.Parameters.AddWithValue("@0", primaryKey.Value);

            command.CommandText = sql.ToString();
            return command;
        }
    }

    public class MsSqlInsertAdaptor
    {
        private readonly MsSqlAdaptor dbAdaptor;

        public MsSqlInsertAdaptor(MsSqlAdaptor dbAdaptor)
        {
            this.dbAdaptor = dbAdaptor;
        }

        public void Insert<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entityType = typeof(TEntity);
            var setters = GetSetFields(entityType, entity);
            using (var command = BuildInsertCommand(setters, entityType))
            {
                dbAdaptor.ExecuteNonQuery(command);
            }
        }

        private SetField[] GetSetFields(Type entityType, object entity)
        {
            var names = new List<SetField>();
            entityType.GetProperties().Each(x => names.Add(new SetField
            {
                Name = x.Name,
                Value = x.GetValue(entity)
            }));
            return names.ToArray();
        }

        private SqlCommand BuildInsertCommand(SetField[] setters, Type entityType)
        {
            var sql = new StringBuilder();
            var command = new SqlCommand();

            var setterIndex = 0;
            var fields = new List<string>();
            setters.Each(x =>
            {
                var field = String.Format("@{0}", setterIndex);
                fields.Add(field);
                command.Parameters.AddWithValue(field, x.Value);

                setterIndex++;
            });

            sql.AppendFormat("INSERT INTO [{0}]({1})", entityType.Name, setters.Select(x => x.Name).Join(", "));
            sql.AppendFormat(" VALUES ({0})", fields.Join(", "));

            command.CommandText = sql.ToString();
            return command;
        }
    }

    public class MsSqlQueryAdaptor
    {
        private readonly MsSqlAdaptor dbAdaptor;

        public MsSqlQueryAdaptor(MsSqlAdaptor dbAdaptor)
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

        private SqlCommand BuildQueryCommand(Query query, int? maxResults)
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
}