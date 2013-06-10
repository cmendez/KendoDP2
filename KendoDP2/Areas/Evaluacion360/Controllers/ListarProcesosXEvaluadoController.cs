﻿using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Seguridad;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class ListarProcesosXEvaluadoController : Controller
    {
        //
        // GET: /Evaluacion360/ListarProcesosXEvaluado/

         public ListarProcesosXEvaluadoController()
            : base()
        {
            ViewBag.Area = "Evaluacion360";
        }


        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.estados = context.TablaEstadoProcesoEvaluacion.All().Select(e => e.ToDTO()).ToList();
                return View();

            }
        }



        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
                //var ret = context.TablaColaboradores.FindByID(ColaboradorID).ColaboradorXProcesoEvaluaciones.Select(x => x.ProcesoEvaluacion.ToDTO()).ToList();

                // context.TablaColaboradores.FindByID(ColaboradorID).Evaluadores.where(x => x.ProcesoEvaluacionXColaborador.ProcesoEvaluacionID == procesoID).toList();
                List<ProcesoEvaluacion> listaProceso = new List<ProcesoEvaluacion>();

                IList<Evaluador> listaProcesosEvaluador = (context.TablaEvaluadores.Where(a =>a.ElEvaluado == ColaboradorID));

                for (int i = 0; i < listaProcesosEvaluador.Count; i++)
                {
                    listaProceso.Add(context.TablaProcesoEvaluaciones.FindByID(listaProcesosEvaluador.ElementAt(i).ProcesoEnElQueParticipanID));

                    //listaProceso.Add(context.TablaProcesoEvaluaciones.One(b => b.ID == (listaProcesosEvaluador.ElementAt(i).ProcesoEnElQueParticipanID) &&
                    //  b.EstadoProcesoEvaluacionID == context.TablaEstadoProcesoEvaluacion.One(e=> e.Descripcion.Equals(ConstantsEstadoProcesoEvaluacion.EnProceso)).ID));
                }

                return Json(listaProceso.GroupBy(x => x.ID).Select(y => y.FirstOrDefault()).Select(x => x.ToDTO()).ToDataSourceResult(request));

            }
        }



        public ActionResult ListarResultados(int procesoEvaluacionID)
        {
            using (DP2Context context = new DP2Context())
            {
                ProcesoEvaluacion proceso = context.TablaProcesoEvaluaciones.FindByID(procesoEvaluacionID);
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.estados = context.TablaEstadoColaboradorXProcesoEvaluaciones.All().Select(c => c.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
                return View(proceso);

            }
        }



        public ActionResult ReadEvaluaciones([DataSourceRequest] DataSourceRequest request, int procesoID)
        {
            using (DP2Context context = new DP2Context())
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
                //return Json(context.TablaColaboradores.FindByID(ColaboradorID).OcurrenciasComoEvaluador.Where(x => x.Evaluado.ProcesoEvaluacionID == procesoID).ToList());

                IList<Evaluador> listaEvaluaciones = new List<Evaluador>();
                listaEvaluaciones = (context.TablaEvaluadores.Where(y => y.ElIDDelEvaluador == ColaboradorID &&
                                                                                   y.ProcesoEnElQueParticipanID == procesoID));

                ColaboradorXPuesto colaboradorXpuesto= context.TablaColaboradoresXPuestos.One(x=>x.ColaboradorID==ColaboradorID);
                Puesto puesto=context.TablaPuestos.One(x=>x.ID==colaboradorXpuesto.PuestoID);

                IList<Competencia> listaCompetencia = new List<Competencia>();
                IList<CompetenciaXPuesto> listaCompetenciaXpuesto = (context.TablaCompetenciaXPuesto.Where(a => a.PuestoID == puesto.ID));

                for (int i = 0; i < listaCompetenciaXpuesto.Count; i++)
                {
                    listaCompetencia.Add(context.TablaCompetencias.FindByID(listaCompetenciaXpuesto.ElementAt(i).CompetenciaID));                                        
                }

                //competencias llenas segun puesto 
                //evaluador, tiene evaluado, evaluador
                //saco todos los evaluadores

                IList<Evaluador> listaEvaluadores = (context.TablaEvaluadores.Where(a => a.ElEvaluado == ColaboradorID));
                IList<Examen> listaExamenes = new List<Examen>();
                for (int i = 0; i < listaEvaluadores.Count; i++)
                {
                    Examen auxexamen = context.TablaExamenes.One(a => a.EvaluadorID == listaEvaluadores.ElementAt(i).ElIDDelEvaluador && a.EstadoExamenID==3);

                    if (auxexamen != null) {
                        listaExamenes.Add(auxexamen);
                    }
                    //listaExamenes.Add(context.TablaExamenes.One(a => a.EvaluadorID == listaEvaluadores.ElementAt(i).ElIDDelEvaluador ));
                    //estado finalizado
                }

                IList<CompetenciaXExamen> listaCompetenciaXExamenFinal = new List<CompetenciaXExamen>();
                var conteo = listaExamenes.Count;

                if (listaExamenes.Count >= 2)
                {
                     listaCompetenciaXExamenFinal = (context.TablaCompetenciaXExamen.Where(a => a.ExamenID == listaExamenes.ElementAt(0).ID));


                    for (int i = 1; i < listaExamenes.Count; i++)
                    {
                        IList<CompetenciaXExamen> listaCompetenciaXExamenParcial = new List<CompetenciaXExamen>();
                        listaCompetenciaXExamenParcial.AddRange(context.TablaCompetenciaXExamen.Where(a => a.ExamenID == listaExamenes.ElementAt(i).ID));

                        for (int j = 0; j < listaCompetenciaXExamenParcial.Count; j++)
                        {

                            listaCompetenciaXExamenFinal.ElementAt(j).Nota = listaCompetenciaXExamenParcial.ElementAt(j).Nota + listaCompetenciaXExamenFinal.ElementAt(j).Nota;

                        }

                    }

                }

                if (listaExamenes.Count == 1) {
                    listaCompetenciaXExamenFinal = (context.TablaCompetenciaXExamen.Where(a => a.ExamenID == listaExamenes.ElementAt(0).ID));                
               }

                if (listaExamenes.Count == 0) { 
                //devuelvo una lista vacia
                }

                for (int j = 0; j < listaCompetenciaXExamenFinal.Count; j++)
                {

                    listaCompetenciaXExamenFinal.ElementAt(j).Nota = listaCompetenciaXExamenFinal.ElementAt(j).Nota / conteo;

                }
              
                return Json(listaCompetenciaXExamenFinal.Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }

    }
}
