using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Exceptions
{
    public class GetExceptionInfoHandler : IHandler<GetExceptionInfo>
    {
        private readonly IDatabase database;

        public GetExceptionInfoHandler(IDatabase database)
        {
            this.database = database;
        }

        public void Execute(GetExceptionInfo message)
        {
            throw new NotImplementedException();
        }
    }
}