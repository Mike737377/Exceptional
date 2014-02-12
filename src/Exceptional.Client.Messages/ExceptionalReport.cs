using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exceptional.Client.Messages
{
    public class ExceptionalReport
    {
        public Guid ExceptionInstanceId { get; set; }
        public string UserName { get; set; }
        public string UrlReferrer { get; set; }
        public string Url { get; set; }
        public string MachineName { get; set; }
        public int? HttpCode { get; set; }
        public string HtmlErrorMessage { get; set; }

        public ExceptionDetails[] Exceptions { get; set; }

        public string[][] QueryData { get; set; }

        public string[][] PostData { get; set; }

        public string[][] ServerVariable { get; set; }

        public string[][] Cookies { get; set; }
    }
}