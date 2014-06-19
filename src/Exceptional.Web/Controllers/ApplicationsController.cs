using Exceptional.Domain.Application;
using Exceptional.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exceptional.Web.Controllers
{
    public class ApplicationsController : BaseController
    {
        //
        // GET: /Applications/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewApplication(NewApplication newApplication)
        {
            var app = Bus.Send<NewApplication, Application>(newApplication);
            return Redirect("/#/apps/" + app.ApplicationId.ToString());
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}