using KendoDP2.Models.Generic;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Areas.Evaluacion360.Models;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    [Authorize()]
    public class PuestosEvaluacionController : Controller
    {
        public PuestosEvaluacionController()
        {
            ViewBag.Area = "Evaluacion360";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.puestos = context.TablaPuestos.All().Select(x => x.ToDTO()).ToList();
                ViewBag.niveles = context.TablaNivelCapacidades.All().Select(x => x.ToDTO()).ToList();
                ViewBag.competencias = context.TablaCompetencias.All().Select(x => x.ToDTO()).ToList();
                return View();
            }

        }

        //[AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request, int puestoID, string parametro)
        {
            using (DP2Context context = new DP2Context())
            {
                //return Json(context.TablaCompetencias.All().Select(p => p.ToDTO()).ToDataSourceResult(request));
                return Json(context.TablaPuestoXEvaluadores.Where(p => p.PuestoID == puestoID).Select(p => p.ToDTO()).ToDataSourceResult(request));
                //String laData = "la data";
                //return Json(laData);


            }
        }

        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, PuestoXEvaluadoresDTO pxeDTO, int puestoID)
        {
            using (DP2Context context = new DP2Context())
            {
                //Competencia c = context.TablaCompetencias.FindByID(competencia.ID).LoadFromDTO(competencia);
                //context.TablaCompetencias.ModifyElement(c);
                //return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));

                PuestoXEvaluadores pxe = context.TablaPuestoXEvaluadores.FindByID(pxeDTO.ID).LoadFromDTO(puestoID, pxeDTO);
                context.TablaPuestoXEvaluadores.ModifyElement(pxe);
                return Json(new[] { pxe.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

    }
}
