using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    public class OrganigramaController : Controller
    {
        public OrganigramaController()
        {
            ViewBag.Area = "Organigrama";
        }

        //
        // GET: /Organizacion/Organigrama/

        public ActionResult Index()
        {
            return View();
        }

    }
}
