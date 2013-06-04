using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    [Authorize()]
    public class FuncionesController : Controller
    {
        public FuncionesController()
            : base()
        {
            ViewBag.Area = "Organizacion";
        }

        public ActionResult Index(int? puestoID)
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.PageSize = 8;
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.competencias = context.TablaCompetencias.All().Select(c => c.ToDTO()).ToList();
                ViewBag.puestoID = puestoID ?? 0;
                return View();
            }
        }

        // Botones de niveles

        
        // Grid capacidades
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request, int puestoID)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaFunciones.Where(c => c.PuestoID == puestoID).Select(p => p.ToDTO()).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, FuncionDTO funcion)
        {
            using (DP2Context context = new DP2Context())
            {
                Funcion c = new Funcion(funcion);
                context.TablaFunciones.AddElement(c);
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, FuncionDTO funcion)
        {
            using (DP2Context context = new DP2Context())
            {
                Funcion c = context.TablaFunciones.FindByID(funcion.ID).LoadFromDTO(funcion);
                context.TablaFunciones.ModifyElement(c);
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, FuncionDTO funcion)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaFunciones.RemoveElementByID(funcion.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }


        //***************************************
        //***************************************

        public ActionResult ReadEvaluados([DataSourceRequest] DataSourceRequest request, int procesoID)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaColaboradorXProcesoEvaluaciones.Where(x => x.ProcesoEvaluacionID == procesoID)
                    .Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }





        //***************************************

    }
}
