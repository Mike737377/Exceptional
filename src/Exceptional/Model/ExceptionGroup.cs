using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Model
{
    public class ExceptionGroup
    {
        public Guid ExceptionGroupId { get; set; }
        public Guid ApplicationId { get; set; }
        public int ExceptionHash { get; set; }
        public string Message { get; set; }
        public string ExceptionType { get; set; }
    }
}