using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exceptional.Client.SampleWebsite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThrowException()
        {
            throw new Exception("Oh no, something went wrong! you'll need to check the inner exception.", new Exception("This was the root cause for the exception"));
        }
    }
}