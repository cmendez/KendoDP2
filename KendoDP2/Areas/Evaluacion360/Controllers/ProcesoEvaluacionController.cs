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
using KendoDP2.Areas.Organizacion.Models;

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
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
                ViewBag.estados = context.TablaEstadoProcesoEvaluacion.All().Select(e => e.ToDTO()).ToList();
                return View();
            }
        }

        public ActionResult ElegirEvaluados(int procesoEvaluacionID)
        {
            using (DP2Context context = new DP2Context())
            {
                ProcesoEvaluacion proceso = context.TablaProcesoEvaluaciones.FindByID(procesoEvaluacionID);
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
                ViewBag.estados = context.TablaEstadoColaboradorXProcesoEvaluaciones.All().Select(c => c.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
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
                EstadoProcesoEvaluacion iniciado = context.TablaEstadoProcesoEvaluacion.One(x => x.Descripcion.Equals(ConstantsEstadoProcesoEvaluacion.Creado));
                
                ProcesoEvaluacion p = new ProcesoEvaluacion(proceso);
                p.EstadoProcesoEvaluacion = iniciado;
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
                context.TablaProcesoEvaluaciones.RemoveElementByID(proceso.ID, true);
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

        // devuelve si se cambio a true ReferenciaDirecta
        private bool AddColaboradorToProceso(int colaboradorID, int procesoID, DP2Context context, bool esReferenciaDirecta)
        {
            var cruce = context.TablaColaboradorXProcesoEvaluaciones.One(x => x.ColaboradorID == colaboradorID && x.ProcesoEvaluacionID == procesoID);
            EstadoColaboradorXProcesoEvaluacion pendiente = context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x => x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Pendiente));

            if (cruce == null)
            { // nuevo
                context.TablaColaboradorXProcesoEvaluaciones.AddElement(
                    new ColaboradorXProcesoEvaluacion
                    {
                        ColaboradorID = colaboradorID,
                        ProcesoEvaluacionID = procesoID,
                        EstadoColaboradorXProcesoEvaluacion = pendiente,
                        ReferenciaDirecta = esReferenciaDirecta,
                        ReferenciasPorAreas = esReferenciaDirecta ? 0 : 1
                    });
                return esReferenciaDirecta;
            } else if(!esReferenciaDirecta)
            {
                cruce.ReferenciasPorAreas++;
                context.TablaColaboradorXProcesoEvaluaciones.ModifyElement(cruce);
                return false;
            } else
            { // no tenia referencia directa
                if (!cruce.ReferenciaDirecta)
                {
                    cruce.ReferenciaDirecta = true;
                    context.TablaColaboradorXProcesoEvaluaciones.ModifyElement(cruce);
                    return true;
                }
                return false;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddEvaluadosColaborador(int procesoID, int colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                bool isNuevaReferenciaDirecta = AddColaboradorToProceso(colaboradorID, procesoID, context, true);
                return Json(new {success = isNuevaReferenciaDirecta});
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddEvaluadosAreas(int procesoID, int areaID)
        {
            using (DP2Context context = new DP2Context())
            {
                if (context.TablaAreaXProcesoEvaluaciones.One(x => x.ProcesoEvaluacionID == procesoID && x.AreaID == areaID) != null)
                {
                    return Json(new { success = false });
                }
                context.TablaAreaXProcesoEvaluaciones.AddElement(new AreaXProcesoEvaluacion
                {
                    ProcesoEvaluacion = context.TablaProcesoEvaluaciones.FindByID(procesoID),
                    Area = context.TablaAreas.FindByID(areaID)
                });
                List<Area> areasHijas = context.TablaAreas.FindByID(areaID).GetAreasHijas(context);
                List<int> areasHijasIDs = areasHijas.Select(c => c.ID).ToList();
                context.TablaColaboradores.All().Select(c => c.ToDTO()).Where(c => areasHijasIDs.Contains(c.AreaID)).Each(c => AddColaboradorToProceso(c.ID, procesoID, context, false));
                return Json(new { success = true });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyEvaluados([DataSourceRequest] DataSourceRequest request, EstadoColaboradorXProcesoEvaluacionDTO cruce)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaColaboradorXProcesoEvaluaciones.RemoveElementByID(cruce.ID, true);
                return Json(ModelState.ToDataSourceResult());
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyEvaluadoDirecto(int procesoID, int colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                var cruce = context.TablaColaboradorXProcesoEvaluaciones.One(x => x.ProcesoEvaluacionID == procesoID && x.ColaboradorID == colaboradorID);
                if (cruce != null)
                {
                    context.TablaColaboradorXProcesoEvaluaciones.RemoveElementByID(cruce.ID, true);
                }
                return Json(new { sucess = true });
            }
        }

        public ActionResult GetEvaluadosDirectos(int procesoID)
        {
            using (DP2Context context = new DP2Context())
            {
                var lista = context.TablaColaboradorXProcesoEvaluaciones.Where(x => x.ProcesoEvaluacionID == procesoID && x.ReferenciaDirecta).ToList().Select(c => c.ToDTO()).ToList();
                return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAreas(int procesoID)
        {
            using (DP2Context context = new DP2Context())
            {
                var lista = context.TablaAreaXProcesoEvaluaciones.Where(x => x.ProcesoEvaluacionID == procesoID).ToList().Select(c => c.ToDTO()).ToList();
                return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyArea(int procesoID, int areaID)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaAreaXProcesoEvaluaciones.RemoveElementByID(context.TablaAreaXProcesoEvaluaciones.One(x => x.ProcesoEvaluacionID == procesoID && x.AreaID == areaID).ID);
                List<Area> areasHijas = context.TablaAreas.FindByID(areaID).GetAreasHijas(context);
                List<int> areasHijasIDs = areasHijas.Select(c => c.ID).ToList();
                foreach(ColaboradorDTO c in context.TablaColaboradores.All().Select(c => c.ToDTO()).Where(c => areasHijasIDs.Contains(c.AreaID)).ToList()){
                    ColaboradorXProcesoEvaluacion cruce = context.TablaColaboradorXProcesoEvaluaciones.One(x => x.ColaboradorID == c.ID && x.ProcesoEvaluacionID == procesoID);
                    if (cruce != null)
                    {
                        cruce.ReferenciasPorAreas--;
                        context.TablaColaboradorXProcesoEvaluaciones.ModifyElement(cruce);
                        if(cruce.ReferenciasPorAreas == 0 && !cruce.ReferenciaDirecta){
                            context.TablaColaboradorXProcesoEvaluaciones.RemoveElementByID(cruce.ID, true);
                        }
                    }
                }
                return Json(new {success = true});
            }
        }


        public ActionResult IniciarProcesoEvaluacion(int procesoEvaluacionID) 
        {
            using (DP2Context context = new DP2Context()) 
            {
                ProcesoEvaluacion p = context.TablaProcesoEvaluaciones.FindByID(procesoEvaluacionID, false);

                List<ColaboradorXProcesoEvaluacion> list = context.TablaColaboradorXProcesoEvaluaciones.Where(x => x.ProcesoEvaluacionID == procesoEvaluacionID);
                using (CorreoController correoController = new CorreoController()){
                    correoController.EnviarEmailsInicio(list, p);
                }

                EstadoProcesoEvaluacion enProceso = context.TablaEstadoProcesoEvaluacion.One(x => x.Descripcion.Equals(ConstantsEstadoProcesoEvaluacion.EnProceso));
                p.EstadoProcesoEvaluacion = enProceso;
                context.TablaProcesoEvaluaciones.ModifyElement(p);
                //return Json(new { success = true });
                return View();
            }
        }


     
        public ActionResult CerrarProcesoEvaluacion(int procesoEvaluacionID)
      {
          using (DP2Context context = new DP2Context())
          {
              ViewBag.terminado = false;
              ProcesoEvaluacion proceso = context.TablaProcesoEvaluaciones.FindByID(procesoEvaluacionID);
              ViewBag.proceso = proceso;
              if (proceso.EstadoProcesoEvaluacionID == context.TablaEstadoProcesoEvaluacion.One(x => x.Descripcion.Equals(ConstantsEstadoProcesoEvaluacion.Terminado)).ID)
              {
                  ViewBag.terminado = true;
                  return View(proceso);
              }
              
              //Procesar resultados parciales y modificar estados 
                
              // 
              EstadoProcesoEvaluacion terminado = context.TablaEstadoProcesoEvaluacion.One(x => x.Descripcion.Equals(ConstantsEstadoProcesoEvaluacion.Terminado));
              proceso.EstadoProcesoEvaluacion = terminado;
              context.TablaProcesoEvaluaciones.ModifyElement(proceso);
              return View(proceso);
          }
      }

        public ActionResult Editing_ReadCapEvaluacion([DataSourceRequest] DataSourceRequest request, int puestoID, int tablaEvaluadoresID)
        {
            using (DP2Context context = new DP2Context())
            {
                Examen examen = context.TablaExamenes.One(x => x.EvaluadorID == tablaEvaluadoresID);
                
                //return Json(context.TablaCapacidades.Where(c => c.NivelCapacidadID == nivelID && c.CompetenciaID == competenciaID).OrderBy(y => y.CompetenciaID).Select(p => p.ToDTO()).ToDataSourceResult(request));
                return Json(context.TablaPreguntas.Where(x => x.ExamenID == examen.ID).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GuardarPuntuacionPregunta([DataSourceRequest] DataSourceRequest request, int preguntaID, int puntuacion)
        {

            using (DP2Context context = new DP2Context()) {
                Pregunta p = context.TablaPreguntas.FindByID(preguntaID);
                p.Puntuacion = puntuacion;
                context.TablaPreguntas.ModifyElement(p);
                return Json(new { success = true });
            }
     
        }
    }
}
