using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Evaluacion360.Models;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    [Authorize()]
    public class CapacidadesController : Controller
    {
        public CapacidadesController()
            : base()
        {
            ViewBag.SidebarSelectedOption = "Evaluacion360";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.competencias = context.TablaCompetencias.GetAll().Select(c => c.ToDTO()).ToList();
                ViewBag.niveles = context.TablaNivelCapacidades.GetAll();
                return View();
            }
        }

        

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request, int nivelID, int competenciaID)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaCapacidades.Where(c => c.NivelCapacidadID == nivelID && c.CompetenciaID == competenciaID).Select(p => p.ToDTO()).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, CapacidadDTO capacidad)
        {
            using (DP2Context context = new DP2Context())
            {
                Capacidad c = new Capacidad(capacidad);
                context.TablaCapacidades.AddElement(c);
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, CapacidadDTO capacidad)
        {
            using (DP2Context context = new DP2Context())
            {
                Capacidad c = context.TablaCapacidades.FindByID(capacidad.ID).LoadFromDTO(capacidad);
                context.TablaCapacidades.ModifyElement(c);
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, CapacidadDTO capacidad)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaCapacidades.RemoveElementByID(capacidad.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }
    }
}
