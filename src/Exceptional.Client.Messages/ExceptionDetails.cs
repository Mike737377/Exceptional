using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exceptional.Client.Messages
{
    public class ExceptionDetails
    {
        public int ExceptionHash { get; set; }
        public string StackTrace { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
    }
}