using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;

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

        public JsonResult GetNodosHijo(int? id)
        {
            using (DP2Context context = new DP2Context())
            {
                var hijos = context.TablaAreas.Where(a => id.HasValue ? a.AreaSuperiorID == id : a.AreaSuperiorID == null).Select(a => a.ToTreeDTO()).OrderBy(a => a.Name);
                return Json(hijos.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

    }
}
