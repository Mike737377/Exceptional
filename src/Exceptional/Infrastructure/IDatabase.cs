using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    public interface IDatabase
    {
        IQueryBuilder<TEntity> Query<TEntity>() where TEntity : class;

        void Delete<TEntity>(TEntity entity) where TEntity : class;

        void Insert<TEntity>(TEntity entity) where TEntity : class;

        void Update<TEntity>(TEntity entity) where TEntity : class;
    }
}