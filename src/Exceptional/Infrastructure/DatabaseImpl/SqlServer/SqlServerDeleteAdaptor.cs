using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure.DatabaseImpl.SqlServer
{
    public class SqlServerDeleteAdaptor : IDeleteAdaptor
    {
        private readonly IDatabaseAdaptor dbAdaptor;

        public SqlServerDeleteAdaptor(IDatabaseAdaptor dbAdaptor)
        {
            this.dbAdaptor = dbAdaptor;
        }

        public void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entityType = typeof(TEntity);
            var setters = GetSetFields(entityType, entity);
            using (var command = BuildDeleteCommand(setters, entityType))
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

        private DbCommand BuildDeleteCommand(SetField[] setters, Type entityType)
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
}