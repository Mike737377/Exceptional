using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure.DatabaseImpl.SqlServer
{
    public class SqlServerAdaptor : IDatabaseAdaptor
    {
        private readonly SqlConnection connection;

        public SqlServerAdaptor(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public TEntity[] Execute<TEntity>(DbCommand command, SelectField[] selectFields)
            where TEntity : class
        {
            PrepareCommand(command);

            var entities = new List<TEntity>();
            using (DbDataReader reader = command.ExecuteReader())
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

        private void PrepareCommand(DbCommand command)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            command.Connection = connection;
            LogCommand(command);
        }

        public int ExecuteNonQuery(DbCommand command)
        {
            PrepareCommand(command);
            return command.ExecuteNonQuery();
        }

        public int ExecuteScalar(DbCommand command)
        {
            PrepareCommand(command);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        private void LogCommand(DbCommand command)
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

        public IUpdateAdaptor GetUpdateAdaptor()
        {
            return new SqlServerUpdateAdaptor(this);
        }

        public IDeleteAdaptor GetDeleteAdaptor()
        {
            return new SqlServerDeleteAdaptor(this);
        }

        public IQueryAdaptor GetQueryAdaptor()
        {
            return new SqlServerQueryAdaptor(this);
        }

        public IInsertAdaptor GetInsertAdaptor()
        {
            return new SqlServerInsertAdaptor(this);
        }
    }
}