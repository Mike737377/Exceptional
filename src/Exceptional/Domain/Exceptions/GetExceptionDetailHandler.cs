using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Exceptions
{
    public class GetExceptionDetailHandler : IHandler<GetExceptionDetail>
    {
        private readonly IDatabase database;

        public GetExceptionDetailHandler(IDatabase database)
        {
            this.database = database;
        }

        public void Execute(GetExceptionDetail message)
        {
            throw new NotImplementedException();
        }
    }
}