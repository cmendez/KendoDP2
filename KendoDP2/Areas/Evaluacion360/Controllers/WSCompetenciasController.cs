using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class WSCompetenciasController : Controller
    {
        
        public ActionResult Test(string nombre)
        {
            return Json(new { nombre = nombre + "x" }, JsonRequestBehavior.AllowGet);
        }

    }
}
