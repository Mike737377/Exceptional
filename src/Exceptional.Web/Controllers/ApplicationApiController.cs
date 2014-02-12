using Exceptional.Client.Messages;
using Exceptional.Domain.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Exceptional.Web.Controllers
{
    [RoutePrefix("api/{apiKey}")]
    public class ApplicationApiController : ApiBaseController
    {
        //
        // GET: /Api/
        [AllowAnonymous]
        public string Index(string apiKey)
        {
            return string.Format(@"Pong {0} @ {1}", apiKey, DateTime.UtcNow.ToString("o"));
        }

        [AllowAnonymous]
        [Route("report"), HttpPost]
        public Guid ReportException(string apiKey, ExceptionalReport report)
        {
            var exceptionReport = new ReportException()
            {
                //StackTrace = report.StackTrace,
                //ExceptionMessage = report.Message,
                UserName = report.UserName
            };
            Bus.Send(exceptionReport);
            return report.ExceptionInstanceId;
        }
    }
}