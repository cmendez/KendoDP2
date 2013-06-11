﻿using Kendo.Mvc.UI;
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

        public ActionResult Index(int instanciaEvaluadores, int colaboradorEvaluadoIDP)
        {
            int colaboradorEvaluadoID =colaboradorEvaluadoIDP;
            using (DP2Context context = new DP2Context())
            {
                ColaboradorDTO evaluado = context.TablaColaboradores.One(c => c.ID == colaboradorEvaluadoID).ToDTO();
                ViewBag.evaluado = evaluado;
                ViewBag.instanciaEvaluadores = instanciaEvaluadores;
                return View();
            }
        }

        public ActionResult GuardarEvaluacion(int tablaEvaluadoresID)
        {
            // Calcular nota de evaluacion
            using (DP2Context context = new DP2Context()){
                //Evaluador evaluador = context.TablaEvaluadores.FindByID(tablaEvaluadoresID);
                Examen examen = context.TablaExamenes.One(x => x.EvaluadorID == tablaEvaluadoresID);
               // Falta agregar el promedio final (incluye competencias)
                IList<CompetenciaXExamen> competenciasEvaluadas = context.TablaCompetenciaXExamen.Where(x=>x.ExamenID == examen.ID );
                int nota = 0;
                foreach (CompetenciaXExamen c in competenciasEvaluadas) {
                    //
                    IList<Pregunta> preguntasXCompetencia = context.TablaPreguntas.Where(x => x.competenciaID==c.CompetenciaID && x.ExamenID == examen.ID);
                    int notaCompetencia = preguntasXCompetencia.Sum(x => x.Puntuacion);//Convert.ToInt32(Decimal.Floor(preguntasXCompetencia.Sum(x => x.Puntuacion)/100));
                    c.Nota = notaCompetencia;

                    nota += notaCompetencia;
                    context.TablaCompetenciaXExamen.ModifyElement(c);
                }
                if (competenciasEvaluadas != null && competenciasEvaluadas.Count>0) {
                    nota = Convert.ToInt32(Decimal.Floor(nota/competenciasEvaluadas.Count));
                    examen.NotaExamen = nota;
                    EstadoColaboradorXProcesoEvaluacion terminado = context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x => x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Terminado));
                    examen.EstadoExamenID = terminado.ID;

                    context.TablaExamenes.ModifyElement(examen);
                }
                
                return View();
            }
            
        }
    }
}
