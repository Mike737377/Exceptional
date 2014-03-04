using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Exceptions
{
    public class ExceptionDetailViewModel
    {
        public Guid ExceptionDetailId { get; set; }
        public DateTime DateTime { get; set; }
        public string ExceptionType { get; set; }
        public string StackTrace { get; set; }
        public string Message { get; set; }

        public string UserName { get; set; }
        public Guid ApplicationUserId { get; set; }
    }
}