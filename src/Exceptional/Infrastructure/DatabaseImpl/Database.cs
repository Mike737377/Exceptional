using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure.DatabaseImpl
{
    public class Database
    {
        private readonly MsSqlAdaptor dbAdaptor;

        public Database()
            : this("Exceptional")
        { }

        public Database(string connectionString)
        {
            var connString = ConfigurationManager.ConnectionStrings[connectionString];
            dbAdaptor = new MsSqlAdaptor(connString.ConnectionString);
        }

        public IQueryBuilder<TEntity> Query<TEntity>()
            where TEntity : class
        {
            return new QueryBuilder<TEntity>(new MsSqlQueryAdaptor(dbAdaptor));
        }

        public void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            new MsSqlDeleteAdaptor(dbAdaptor).Delete(entity);
        }

        public void Insert<TEntity>(TEntity entity)
            where TEntity : class
        {
            new MsSqlInsertAdaptor(dbAdaptor).Insert(entity);
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            new MsSqlUpdateAdaptor(dbAdaptor).Update(entity);
        }
    }
}