using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Exceptions
{
    public class GetExceptionDetail : IMessage
    {
        public Guid ExceptionDetailId { get; set; }
    }
}