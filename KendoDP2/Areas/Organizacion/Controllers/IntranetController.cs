using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    public class IntranetController : Controller
    {
        //
        // GET: /Organizacion/Intranet/
      
        public IntranetController()
        {
            ViewBag.Area = "Organizacion";
        }

        public ActionResult Index()
        {
                return View();
            
        }

    }
}
