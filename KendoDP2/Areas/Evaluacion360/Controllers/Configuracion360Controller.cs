using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Evaluacion360.Models;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    [Authorize()]
    public class Configuracion360Controller : Controller
    {
        public Configuracion360Controller()
            : base()
        {
            ViewBag.Area = "Evaluacion360";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaPerfiles.All().Select(p => p.ToDTO()).ToDataSourceResult(request));
            }
        }

        // Grid capacidades
        public ActionResult EditingInline_Read_competencia([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {

                List<Competencia> competencias = context.TablaCompetencias.Where(c => true);
                //IEnumerable<CompetenciaConNivelDTO> competenciasConNivelDTO = new IEnumerable<>();
                List<CompetenciaConNivelDTO> competenciasConNivelDTO = new List<CompetenciaConNivelDTO>();



                foreach (Competencia competencia in competencias)
                {
                    competenciasConNivelDTO.Add(new CompetenciaConNivelDTO(competencia, 1, 0, false));
                    competenciasConNivelDTO.Add(new CompetenciaConNivelDTO(competencia, 2, 0, false));
                    competenciasConNivelDTO.Add(new CompetenciaConNivelDTO(competencia, 3, 0, false));
                
                }

                //return Json(context.TablaCompetencias.Where(c => true).Select(p => p.clasificarEnNiveles()).ToDataSourceResult(request));
                return Json(competenciasConNivelDTO.ToDataSourceResult(request));
            }
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, CapacidadDTO capacidad)
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        Capacidad c = new Capacidad(capacidad);
        //        context.TablaCapacidades.AddElement(c);
        //        return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
        //    }
        //}

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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update_competencia([DataSourceRequest] DataSourceRequest request, CompetenciaConNivelDTO competenciaConNivel)
        {

            using (DP2Context context = new DP2Context())
            {
                //Primero solo para un solo perfil:
                Perfil perfilDelGerente = context.TablaPerfiles.One(p => p.Nombre.Equals("Gerente general"));
                Competencia competencia = context.TablaCompetencias.One(c => c.ID == ( competenciaConNivel.ID / 10 ));


                PerfilXCompetencia perfilXCompetencia = new PerfilXCompetencia(perfilDelGerente, competencia, competenciaConNivel.Nivel, competenciaConNivel.Peso);
                context.TablaPerfilXCompetencia.AddElement(perfilXCompetencia);
                return Json(new[] { perfilXCompetencia.aDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingInline_Update_competencia([DataSourceRequest] DataSourceRequest request, CompetenciaConNivelDTO competenciaConNivel)
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        Competencia c = context.TablaCompetencias.FindByID(competencia.ID).LoadFromDTO(competencia);
        //        context.TablaCompetencias.ModifyElement(c);
        //        return json(new[] { c.todto() }.todatasourceresult(request, modelstate));
        //    }
        //}

        //[acceptverbs(httpverbs.post)]
        //public actionresult editinginline_destroy([datasourcerequest] datasourcerequest request, competenciadto competencia)
        //{
        //    using (dp2context context = new dp2context())
        //    {
        //        context.tablacompetencias.removeelementbyid(competencia.id);
        //        return json(modelstate.todatasourceresult());
        //    }
        //}

    }
}
