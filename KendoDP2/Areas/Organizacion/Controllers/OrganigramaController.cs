using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;

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
                var hijos = context.TablaPuestos.Where(p => id.HasValue ? p.PuestoSuperiorID == id : p.PuestoSuperiorID == null).Select(p => p.ToNodoOrganigramaDTO()).OrderBy(t => t.Nombre);
                return Json(hijos.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

    }
}
