using Exceptional.Infrastructure.DatabaseImpl;
using Exceptional.Infrastructure.DatabaseImpl.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    public class Database : IDatabase
    {
        private readonly IDatabaseAdaptor dbAdaptor;

        public Database()
            : this("Exceptional")
        { }

        public Database(string connectionString)
        {
            var connString = ConfigurationManager.ConnectionStrings[connectionString];
            dbAdaptor = new SqlServerAdaptor(connString.ConnectionString);
        }

        public IQueryBuilder<TEntity> Query<TEntity>()
            where TEntity : class
        {
            return new QueryBuilder<TEntity>(dbAdaptor.GetQueryAdaptor());
        }

        public void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            dbAdaptor.GetDeleteAdaptor().Delete(entity);
        }

        public void Insert<TEntity>(TEntity entity)
            where TEntity : class
        {
            dbAdaptor.GetInsertAdaptor().Insert(entity);
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            dbAdaptor.GetUpdateAdaptor().Update(entity);
        }
    }
}