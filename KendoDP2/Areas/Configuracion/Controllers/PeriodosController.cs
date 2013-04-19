using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Configuracion.Models;

namespace KendoDP2.Areas.Configuracion.Controllers
{
    [Authorize()]
    public class PeriodosController : Controller
    {
        public PeriodosController()
            : base()
        {
            ViewBag.Area = "Configuracion";
        }
        
        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.periodos = context.TablaPeriodos.GetAll();
                return View();
            }
        }

        // Grid periodos
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<PeriodoDTO> periodos = context.TablaPeriodos.GetAll().Select(p => p.ToDTO()).OrderByDescending(x => x.FechaInicio).ToList();
                return Json(periodos.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, PeriodoDTO periodo)
        {
            using (DP2Context context = new DP2Context())
            {
                DateTime now = DateTime.Now;
                
                Periodo anterior = context.TablaPeriodos.GetAll().OrderByDescending(x => x.FechaInicio).First();
                anterior.FechaFin = now;
                context.TablaPeriodos.ModifyElement(anterior);

                Periodo p = new Periodo(periodo.Nombre, now);
                context.TablaPeriodos.AddElement(p);
                return Json(new[] { p.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, PeriodoDTO periodo)
        {
            using (DP2Context context = new DP2Context())
            {
                Periodo c = context.TablaPeriodos.FindByID(periodo.ID).LoadFromDTO(periodo);
                context.TablaPeriodos.ModifyElement(c);
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy()
        {
            using (DP2Context context = new DP2Context())
            {
                if (context.TablaPeriodos.GetAll().Count == 1)
                {
                    ModelState.AddModelError("Destroy", "No se puede dejar la empresa sin periodos");
                    return Json(ModelState.ToDataSourceResult());
                }
                else
                {
                    Periodo ultimo = context.TablaPeriodos.GetAll().OrderByDescending(x => x.FechaInicio).First();
                    context.TablaPeriodos.RemoveElementByID(ultimo.ID);
                    
                    ultimo = context.TablaPeriodos.GetAll().OrderByDescending(x => x.FechaInicio).First();
                    ultimo.FechaFin = null;
                    context.TablaPeriodos.ModifyElement(ultimo);
                    return Json(new { success = true });
                }
            }
        }

    }
}
