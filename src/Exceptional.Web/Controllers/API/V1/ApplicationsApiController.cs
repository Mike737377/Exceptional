using Exceptional.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Exceptional.Web.Controllers.API.V1
{
    [RoutePrefix("api/v1/applications")]
    public class ApplicationsApiController : ApiBaseController
    {
        [AllowAnonymous]
        [Route(""), HttpGet]
        public IEnumerable<Application> Applications()
        {
            var apps = new List<Application>();

            apps.Add(new Application { Name = "Test", Website = "http://test", ApiKey = Guid.NewGuid(), ApplicationId = Guid.NewGuid() });

            return apps;
        }

    }
}