using Kendo.Mvc.UI;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

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

        public ActionResult Index(int instanciaEvaluadores, int colaboradorEvaluadoID)
        {
            // Recibir esta variable como parámetro
            int colaboradorEvaluadoIDP = colaboradorEvaluadoID;
            using (DP2Context context = new DP2Context())
            {
                ColaboradorDTO evaluado = context.TablaColaboradores.One(c => c.ID == colaboradorEvaluadoIDP).ToDTO();
                ViewBag.evaluado = evaluado;
                ViewBag.instanciaEvaluadores = instanciaEvaluadores;
                
                //ViewBag.rendirEvaluacion=false;

                Boolean auxiliar = false;
                Boolean auxiliarFecha = false;

                //int estado = context.TablaExamenes.One(x => x.EvaluadorID.Equals(ConstantsEstadoProcesoEvaluacion.Terminado)).EstadoExamenID;

                Examen examen = context.TablaExamenes.One(x => x.EvaluadorID == instanciaEvaluadores);

                Evaluador evaluador = context.TablaEvaluadores.One(x => x.ID == instanciaEvaluadores);

                ProcesoEvaluacion proceso = context.TablaProcesoEvaluaciones.One(x => x.ID == evaluador.ProcesoEnElQueParticipanID);

                DateTime dateahora= DateTime.Now;                

                if (examen.EstadoExamenID == context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x => x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Terminado)).ID)
                {
                    auxiliar = false;
                }
                else
                {
                    auxiliar = true;
                }

                //DateTime fin = DateTime.ParseExact(proceso.FechaCierre, "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
                
                //DateTime fechacierre = (DateTime)proceso.FechaCierre;
                DateTime fechacierre = proceso.FechaCierre.GetValueOrDefault();

                if (fechacierre.CompareTo(dateahora) > 0) { auxiliarFecha = true; }
                else { auxiliarFecha = false; }

                ViewBag.rendirEvaluacion = auxiliar;
                ViewBag.fechaEvaluacion = auxiliarFecha;

                return View();
            }
        }

        public ActionResult _GuardarEvaluacion_ws(int tablaEvaluadoresID, DP2Context context)
        {
            Examen examen = context.TablaExamenes.One(x => x.EvaluadorID == tablaEvaluadoresID);
            IList<CompetenciaXExamen> competenciasEvaluadas = context.TablaCompetenciaXExamen.Where(x => x.ExamenID == examen.ID);

            int notaExamen = 0;
            int acumuladoPesos = 0;
            foreach (CompetenciaXExamen c in competenciasEvaluadas)
            {
                IList<Pregunta> preguntasXCompetencia = context.TablaPreguntas.Where(x => x.competenciaID == c.CompetenciaID && x.ExamenID == examen.ID);
                c.Nota = Convert.ToInt32(Math.Round(preguntasXCompetencia.Sum(x => x.Puntuacion), 0, MidpointRounding.AwayFromZero));

                notaExamen += (c.Nota * c.Peso);
                acumuladoPesos += c.Peso;
                context.TablaCompetenciaXExamen.ModifyElement(c);
            }
            if (competenciasEvaluadas != null && competenciasEvaluadas.Count > 0)
            {
                notaExamen = Convert.ToInt32(Math.Round((double)notaExamen / (double)acumuladoPesos, 0, MidpointRounding.AwayFromZero));
                examen.NotaExamen = notaExamen;

                EstadoColaboradorXProcesoEvaluacion terminado = context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x => x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Terminado));
                examen.EstadoExamenID = terminado.ID;

                context.TablaExamenes.ModifyElement(examen);
            }

            return Json(new { success = true });//View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GuardarEvaluacion(int tablaEvaluadoresID)
        {
            // Calcular nota de evaluacion
            using (DP2Context context = new DP2Context()){
                Examen examen = context.TablaExamenes.One(x => x.EvaluadorID == tablaEvaluadoresID);
                IList<CompetenciaXExamen> competenciasEvaluadas = context.TablaCompetenciaXExamen.Where(x=>x.ExamenID == examen.ID );
                
                int notaExamen = 0;
                int acumuladoPesos = 0;
                foreach (CompetenciaXExamen c in competenciasEvaluadas) {
                    IList<Pregunta> preguntasXCompetencia = context.TablaPreguntas.Where(x => x.competenciaID==c.CompetenciaID && x.ExamenID == examen.ID);
                    c.Nota = Convert.ToInt32(Math.Round(preguntasXCompetencia.Sum(x => x.Puntuacion), 0, MidpointRounding.AwayFromZero));

                    notaExamen+= (c.Nota * c.Peso);
                    acumuladoPesos+= c.Peso;
                    context.TablaCompetenciaXExamen.ModifyElement(c);
                }
                if (competenciasEvaluadas != null && competenciasEvaluadas.Count>0) {
                    notaExamen = Convert.ToInt32(Math.Round((double)notaExamen / (double)acumuladoPesos, 0, MidpointRounding.AwayFromZero));
                    examen.NotaExamen = notaExamen;

                    EstadoColaboradorXProcesoEvaluacion terminado = context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x => x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Terminado));
                    examen.EstadoExamenID = terminado.ID;

                    context.TablaExamenes.ModifyElement(examen);
                }
                
                return Json(new { success = true });//View();
            }
            
        }
    }
}
