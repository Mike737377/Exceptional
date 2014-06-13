using Exceptional.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Exceptional.Web.Controllers.API.V1
{
    [RoutePrefix("api/v1/users")]
    public class UsersApiController : ApiBaseController
    {

        [AllowAnonymous]
        [Route(""), HttpGet]
        public IEnumerable<User> Index()
        {

            var users = new List<User>();

            users.Add(new User() { Name = "John" });

            return users;

        }

    }
}