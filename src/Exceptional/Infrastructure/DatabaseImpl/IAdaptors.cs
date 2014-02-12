using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exceptional.Infrastructure.DatabaseImpl
{
    public interface IInsertAdaptor
    {
        void Insert<TEntity>(TEntity entity)
            where TEntity : class;
    }

    public interface IDeleteAdaptor
    {
        void Delete<TEntity>(TEntity entity)
           where TEntity : class;
    }

    public interface IQueryAdaptor
    {
        TEntity[] ExecuteReader<TEntity>(Query query, int? maxResults = null)
            where TEntity : class;

        int ExecuteScalar(Query query);
    }

    public interface IUpdateAdaptor
    {
        void Update<TEntity>(TEntity entity)
            where TEntity : class;
    }
}