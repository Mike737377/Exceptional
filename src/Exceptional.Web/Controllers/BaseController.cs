using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Exceptional.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public IApplicationBus Bus
        {
            get
            {
                return ServiceFactory.GetInstance<IApplicationBus>();
            }
        }
    }

    public abstract class ApiBaseController : ApiController
    {
        public IApplicationBus Bus
        {
            get
            {
                return ServiceFactory.GetInstance<IApplicationBus>();
            }
        }
    }
}