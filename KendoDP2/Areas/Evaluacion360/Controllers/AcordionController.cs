using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class AcordionController : Controller
    {
        //
        // GET: /Evaluacion360/Acordion/

        public ActionResult Index()
        {
            ViewBag.Area = "";
            return View();
        }

    }
}
