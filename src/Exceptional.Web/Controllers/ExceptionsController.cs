using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exceptional.Web.Controllers
{
    public class ExceptionsController : BaseController
    {
        //
        // GET: /Exceptions/

        public ActionResult Index()
        {
            return View();
        }

    }
}
