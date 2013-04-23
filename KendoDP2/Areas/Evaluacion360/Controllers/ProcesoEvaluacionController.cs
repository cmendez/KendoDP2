using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kendo.Mvc.Extensions;
using System.Web.Mvc;
using KendoDP2.Areas.Evaluacion360.Models;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    [Authorize()]
    public class ProcesoEvaluacionController : Controller
    {
        public ProcesoEvaluacionController()
            : base()
        {
            ViewBag.Area = "Evaluacion360";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                return View();
            }
        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaProcesoEvaluaciones.All().Select(p => p.ToDTO()).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, ProcesoEvaluacionDTO proceso)
        {
            using (DP2Context context = new DP2Context())
            {
                ProcesoEvaluacion p = new ProcesoEvaluacion(proceso);
                context.TablaProcesoEvaluaciones.AddElement(p);
                return Json(new[] { p.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, ProcesoEvaluacionDTO proceso)
        {
            using (DP2Context context = new DP2Context())
            {
                ProcesoEvaluacion p = context.TablaProcesoEvaluaciones.FindByID(proceso.ID).LoadFromDTO(proceso);
                context.TablaProcesoEvaluaciones.ModifyElement(p);
                return Json(new[] { p.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, ProcesoEvaluacionDTO proceso)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaProcesoEvaluaciones.RemoveElementByID(proceso.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }
    }
}
