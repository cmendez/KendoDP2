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

        public ActionResult Create_competenciaAUnPuesto([DataSourceRequest] DataSourceRequest request, CompetenciaXPuestoDTO cxpDTO, int elPuestoID)
        {
            using (DP2Context context = new DP2Context())
            {
                CompetenciaXPuesto cxp = new CompetenciaXPuesto().cargaConDatosDelCliente(cxpDTO, elPuestoID);
                context.TablaCompetenciaXPuesto.AddElement(cxp);
                return Json(new[] { cxp.enFormatoParaElCliente() }.ToDataSourceResult(request, ModelState));
            }        

        }

        public ActionResult Read_lasCompetenciasDeUnPuesto([DataSourceRequest] DataSourceRequest request, int elPuestoID)
        {
            int elIDdelPuestoSolicitado = elPuestoID;

            using (DP2Context context = new DP2Context())
            {
                //return Json(context.TablaCompetencias.All().Select(p => p.ToDTO()).ToDataSourceResult(request));
                return Json(context.TablaCompetenciaXPuesto.Where(p => p.PuestoID == elIDdelPuestoSolicitado).Select(p => p.enFormatoParaElCliente()).ToDataSourceResult(request));
                //String laData = "la data";
                //return Json(laData);


            }
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update_laCompetenciaDeUnPuesto([DataSourceRequest] DataSourceRequest request, CompetenciaXPuestoDTO competenciaXPuestoModificado, int elPuestoID)
        {
            int elIDdelPuestoPadre = elPuestoID;

            using (DP2Context context = new DP2Context())
            {
                //Competencia c = context.TablaCompetencias.FindByID(competencia.ID).LoadFromDTO(competencia);
                //context.TablaCompetencias.ModifyElement(c);
                //return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));

                CompetenciaXPuesto puestoYCompetencia = context.TablaCompetenciaXPuesto.FindByID(competenciaXPuestoModificado.ID).cargaConDatosDelCliente(competenciaXPuestoModificado, elIDdelPuestoPadre);
                context.TablaCompetenciaXPuesto.ModifyElement(puestoYCompetencia);
                return Json(new[] { puestoYCompetencia.enFormatoParaElCliente() }.ToDataSourceResult(request, ModelState));
            }
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy_unaCompetenciaDeUnPuesto([DataSourceRequest] DataSourceRequest request, CompetenciaXPuestoDTO competenciaDelPuestoAEliminar)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaCompetenciaXPuesto.RemoveElementByID(competenciaDelPuestoAEliminar.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, CompetenciaDTO competencia)
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        Competencia c = new Competencia(competencia);
        //        context.TablaCompetencias.AddElement(c);
        //        return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
        //    }
        //}

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
