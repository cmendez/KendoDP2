using Kendo.Mvc.UI;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    [Authorize()]
    public class EvaluacionController : Controller
    {
        //
        // GET: /Evaluacion360/Evaluacion/
        public EvaluacionController()
            : base()
        {
            ViewBag.Area = "Evaluacion360";
            
        }

        public ActionResult Index()//int instanciaEvaluadores, int colaboradorEvaluadoID)
        {
            // Recibir esta variable como parámetro
            int colaboradorEvaluadoID = 2; //colaboradorEvaluadoID;
            using (DP2Context context = new DP2Context())
            {
                ColaboradorDTO evaluado = context.TablaColaboradores.One(c => c.ID == colaboradorEvaluadoID).ToDTO();
           /*     CompetenciaXPuesto competenciaPuesto = context.TablaCompetenciaXPuesto.One(x => x.PuestoID == evaluado.PuestoID);
                IList<Capacidad> capacidades = context.TablaCapacidades.Where(x => x.NivelCapacidadID==competenciaPuesto.NivelID && x.CompetenciaID == competenciaPuesto.CompetenciaID).ToList();
                IList<CompetenciaXPuesto> competencias = context.TablaCompetenciaXPuesto.Where(x => x.PuestoID == evaluado.PuestoID);
              */
                ViewBag.evaluado = evaluado;
                ViewBag.instanciaEvaluadores = 1; //instanciaEvaluadores;
                return View();
            }
        }

        public ActionResult GuardarEvaluacion(int tablaEvaluadoresID)
        {
            // Calcular nota de evaluacion
            using (DP2Context context = new DP2Context()){
                Examen examen = context.TablaExamenes.One(x => x.EvaluadorID == tablaEvaluadoresID);
               
                IList<Pregunta> preguntas = context.TablaPreguntas.Where(x => x.ExamenID == examen.ID);
                int nota = preguntas.Sum(x => x.Puntuacion);
                examen.NotaExamen = Convert.ToInt32(Decimal.Floor(nota/100));
                
                EstadoColaboradorXProcesoEvaluacion terminado = context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x => x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Terminado));
                examen.EstadoExamenID = terminado.ID;

                context.TablaExamenes.ModifyElement(examen);
                return View();
            }
            
        }
    }
}
