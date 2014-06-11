using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exceptional.Web.Controllers
{
    public class ApplicationsController : Controller
    {
        //
        // GET: /Applications/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Applications()
        {
            return View();
        }
    }
}