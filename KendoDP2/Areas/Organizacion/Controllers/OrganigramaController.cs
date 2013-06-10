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
                var hijos = context.TablaColaboradores.All().Select(t => t.ColaboradoresPuesto.Any());
                return Json(hijos.ToList(), JsonRequestBehavior.AllowGet);

                //context.TablaColaboradores.Where(t => id.HasValue ? t.ColaboradoresPuesto.Last().Puesto.PuestoSuperiorID == id : t.ColaboradoresPuesto.Last().Puesto.PuestoSuperiorID == null).Select(t => t.ToNodoOrganigramaDTO(context)).OrderBy(t => t.Nombre);
            }
        }

    }
}
