using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Infrastructure
{
    public interface IApplicationBusContext
    {
        Guid UserId { get; }
    }

    public class ApplicationBusContext
    {
        public Guid UserId { get; private set; }
    }
}