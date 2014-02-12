using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure.DatabaseImpl.SqlServer
{
    public class SqlServerUpdateAdaptor : IUpdateAdaptor
    {
        private readonly IDatabaseAdaptor dbAdaptor;

        public SqlServerUpdateAdaptor(IDatabaseAdaptor dbAdaptor)
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

        private DbCommand BuildUpdateCommand(SetField[] setters, Type entityType)
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
}