using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Application
{
    public class ApplicationSearchHandler : IHandler<ApplicationSearchQuery>
    {
        public void Execute(ApplicationSearchQuery message)
        {
            throw new NotImplementedException();
        }
    }
}