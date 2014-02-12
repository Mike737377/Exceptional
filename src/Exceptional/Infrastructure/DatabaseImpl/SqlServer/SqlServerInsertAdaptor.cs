using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure.DatabaseImpl.SqlServer
{
    public class SqlServerInsertAdaptor : IInsertAdaptor
    {
        private readonly IDatabaseAdaptor dbAdaptor;

        public SqlServerInsertAdaptor(IDatabaseAdaptor dbAdaptor)
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

        private DbCommand BuildInsertCommand(SetField[] setters, Type entityType)
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
}