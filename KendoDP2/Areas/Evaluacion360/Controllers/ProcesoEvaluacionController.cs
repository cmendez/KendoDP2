using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kendo.Mvc.Extensions;
using System.Web.Mvc;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Models.Seguridad;
using KendoDP2.Areas.Personal.Models;

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

        public ActionResult ElegirEvaluados(int procesoEvaluacionID)
        {
            using (DP2Context context = new DP2Context())
            {
                ProcesoEvaluacion proceso = context.TablaProcesoEvaluaciones.FindByID(procesoEvaluacionID);
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.estados = context.TablaEstadoColaboradorXProcesoEvaluaciones.All().Select(c => c.ToDTO()).ToList();
                return View(proceso);
            }
        }

        // Grid de procesos de evaluacion
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaProcesoEvaluaciones.All().Select(p => p.ToDTO()).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ProcesoEvaluacionDTO proceso)
        {
            using (DP2Context context = new DP2Context())
            {
                ProcesoEvaluacion p = new ProcesoEvaluacion(proceso);
                context.TablaProcesoEvaluaciones.AddElement(p);
                return Json(new[] { p.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, ProcesoEvaluacionDTO proceso)
        {
            using (DP2Context context = new DP2Context())
            {
                ProcesoEvaluacion p = context.TablaProcesoEvaluaciones.FindByID(proceso.ID).LoadFromDTO(proceso);
                context.TablaProcesoEvaluaciones.ModifyElement(p);
                return Json(new[] { p.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, ProcesoEvaluacionDTO proceso)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaProcesoEvaluaciones.RemoveElementByID(proceso.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }

        // Grid de evaluados

        public ActionResult ReadEvaluados([DataSourceRequest] DataSourceRequest request, int procesoID)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaColaboradorXProcesoEvaluaciones.Where(x => x.ProcesoEvaluacionID == procesoID)
                    .Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }

        private void AddColaboradorToProceso(int colaboradorID, int procesoID, DP2Context context)
        {
            EstadoColaboradorXProcesoEvaluacion pendiente = context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x => x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Pendiente));
            context.TablaColaboradorXProcesoEvaluaciones.AddElement(
                new ColaboradorXProcesoEvaluacion
                {
                    ColaboradorID = colaboradorID,
                    ProcesoEvaluacionID = procesoID,
                    EstadoColaboradorXProcesoEvaluacion = pendiente
                });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddEvaluadosColaborador(int procesoID, int colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                if (context.TablaColaboradorXProcesoEvaluaciones.Any(x => x.ProcesoEvaluacionID == procesoID && x.ColaboradorID == colaboradorID))
                    return Json(new {success = false});
                else{
                    AddColaboradorToProceso(colaboradorID, procesoID, context);
                    return Json(new {success = true});
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddEvaluadosAreas(int procesoID, int areaID)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaColaboradores.All().Select(c => c.ToDTO()).Where(c => c.AreaID == areaID).Each(c => AddColaboradorToProceso(c.ID, procesoID, context));
                return Json(new { success = true });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyEvaluados([DataSourceRequest] DataSourceRequest request, EstadoColaboradorXProcesoEvaluacionDTO cruce)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaColaboradorXProcesoEvaluaciones.RemoveElementByID(cruce.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }

    }
}
