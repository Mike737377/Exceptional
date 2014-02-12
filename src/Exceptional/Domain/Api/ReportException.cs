using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Api
{
    [Serializable]
    public class ReportException : IMessage
    {
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string UserName { get; set; }
    }
}