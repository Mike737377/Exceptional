using Exceptional.Client.Messages;
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
        public ReportException()
        {
            Report = new ExceptionalReport();
            Details = new ExceptionDetails();
        }

        public Guid ApiKey { get; set; }
        public ExceptionalReport Report { get; set; }
        public ExceptionDetails Details { get; set; }
    }
}