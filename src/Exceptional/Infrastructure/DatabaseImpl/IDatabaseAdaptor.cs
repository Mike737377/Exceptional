using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure.DatabaseImpl
{
    public interface IDatabaseAdaptor : IDisposable
    {
        TEntity[] Execute<TEntity>(DbCommand command, SelectField[] selectFields)
            where TEntity : class;

        int ExecuteNonQuery(DbCommand command);

        int ExecuteScalar(DbCommand command);

        IUpdateAdaptor GetUpdateAdaptor();

        IDeleteAdaptor GetDeleteAdaptor();

        IQueryAdaptor GetQueryAdaptor();

        IInsertAdaptor GetInsertAdaptor();
    }
}