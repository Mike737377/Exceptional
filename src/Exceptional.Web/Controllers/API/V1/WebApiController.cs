using Exceptional.Model;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Exceptional.Web.Controllers.API.V1
{
    [RoutePrefix("api/v1/apps")]
    public class WebApiController : ApiBaseController
    {

        private Fixture autoFixture = new Fixture();

        [AllowAnonymous]
        [Route(""), HttpGet]
        public IEnumerable<Application> Applications()
        {
            return autoFixture.CreateMany<Application>(5);
        }

        [AllowAnonymous]
        [Route("create"), HttpPost]
        public Application CreateApplication()
        {
            return autoFixture.Create<Application>();
        }

        //[AllowAnonymous]
        //[Route("{applicationId}/machines"), HttpGet]
        //public IEnumerable<> Machines(int applicationId)
        //{
        //}

        [AllowAnonymous]
        [Route("{applicationId}"), HttpGet]
        public Application ApplicationDetails(Guid applicationId)
        {
            return autoFixture.Create<Application>();
        }

        [AllowAnonymous]
        [Route("{applicationId}/exceptions"), HttpGet]
        public IEnumerable<ExceptionInstance> Exceptions(Guid applicationId)
        {
            return autoFixture.CreateMany<ExceptionInstance>(5);
        }

        [AllowAnonymous]
        [Route("{applicationId}/users"), HttpGet]
        public IEnumerable<ApplicationUser> ApplicationUsers(Guid applicationId)
        {
            return autoFixture.CreateMany<ApplicationUser>(5);
        }
    }
}