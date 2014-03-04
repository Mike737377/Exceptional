using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Model
{
    public class ExceptionInstance
    {
        public Guid ExceptionInstanceId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public DateTime DateOccurred { get; set; }
        public string UrlReferrer { get; set; }
        public string Url { get; set; }
        public string MachineName { get; set; }
        public int? HttpCode { get; set; }
        public string HtmlErrorMessage { get; set; }
    }
}