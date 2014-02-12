using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Model
{
    public class ExceptionInfo
    {
        public Guid ExceptionId { get; set; }
        public Guid ApplicationId { get; set; }
        public int ExceptionHash { get; set; }
        public string Message { get; set; }
    }
}