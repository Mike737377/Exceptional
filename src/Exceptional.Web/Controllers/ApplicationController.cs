﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exceptional.Web.Controllers
{
    public class ApplicationController : Controller
    {
        //
        // GET: /Application/

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