using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Api
{
    public class ReportExceptionHandler : IHandler<ReportException>
    {
        public void Execute(ReportException message)
        {
            throw new NotImplementedException();
        }
    }
}