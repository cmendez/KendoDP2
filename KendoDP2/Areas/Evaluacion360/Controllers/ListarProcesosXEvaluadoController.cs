using Kendo.Mvc.UI;
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

                    ProcesoEvaluacion procesoauxiliar = context.TablaProcesoEvaluaciones.One(b => b.ID == (listaProcesosEvaluador.ElementAt(i).ProcesoEnElQueParticipanID) &&
                        b.EstadoProcesoEvaluacionID == context.TablaEstadoProcesoEvaluacion.One(e => e.Descripcion.Equals(ConstantsEstadoProcesoEvaluacion.Terminado)).ID);

                    if (procesoauxiliar != null)
                    {
                        listaProceso.Add(procesoauxiliar);
                    }
                                                                         
                    //listaProceso.Add(context.TablaProcesoEvaluaciones.FindByID(listaProcesosEvaluador.ElementAt(i).ProcesoEnElQueParticipanID));

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
                

                //IList<Evaluador> listaEvaluaciones = new List<Evaluador>();
                //listaEvaluaciones = (context.TablaEvaluadores.Where(y => y.ElIDDelEvaluador == ColaboradorID &&
                //                                                                   y.ProcesoEnElQueParticipanID == procesoID));

                //ColaboradorXPuesto colaboradorXpuesto= context.TablaColaboradoresXPuestos.One(x=>x.ColaboradorID==ColaboradorID);
                //Puesto puesto=context.TablaPuestos.One(x=>x.ID==colaboradorXpuesto.PuestoID);

                //IList<Competencia> listaCompetencia = new List<Competencia>();
                //IList<CompetenciaXPuesto> listaCompetenciaXpuesto = (context.TablaCompetenciaXPuesto.Where(a => a.PuestoID == puesto.ID));

                //for (int i = 0; i < listaCompetenciaXpuesto.Count; i++)
                //{
               //     listaCompetencia.Add(context.TablaCompetencias.FindByID(listaCompetenciaXpuesto.ElementAt(i).CompetenciaID));                                        
                //}

                //competencias llenas segun puesto 
                //evaluador, tiene evaluado, evaluador
                //saco todos los evaluadores

                IList<Evaluador> listaEvaluadores = (context.TablaEvaluadores.Where(a => a.ElEvaluado == ColaboradorID));
                IList<Examen> listaExamenes = new List<Examen>();
                for (int i = 0; i < listaEvaluadores.Count; i++)
                {
                    Examen auxexamen = context.TablaExamenes.One(
                        a => a.EvaluadorID == listaEvaluadores.ElementAt(i).ID && 
                            a.EstadoExamenID == context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x=>x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Terminado)).ID);
                    
                    if (auxexamen != null) {
                        listaExamenes.Add(auxexamen);
                    }
                    //listaExamenes.Add(context.TablaExamenes.One(a => a.EvaluadorID == listaEvaluadores.ElementAt(i).ElIDDelEvaluador ));
                    //estado finalizado
                }

                IList<CompetenciaXExamen> listaCompetenciaXExamenFinal = new List<CompetenciaXExamen>();
                //IList<CompetenciaXExamen> listaCompetenciaXExamenFinal = new List<CompetenciaXExamen>();                                
                Dictionary<int, int> sumaPesos = new Dictionary<int,int>();
                if (listaExamenes.Count >= 2)
                {
                    listaCompetenciaXExamenFinal = (context.TablaCompentenciaXExamen.Where(a => a.ExamenID == listaExamenes.ElementAt(0).ID));
                     var IDPuestoevaluador1 = context.TablaEvaluadores.One(x => x.ID == listaExamenes.ElementAt(0).ID).ElIDDelEvaluador;
                    
                     for (int k = 0; k < listaCompetenciaXExamenFinal.Count; k++)
                     {
                         var IDCompetencia = listaCompetenciaXExamenFinal.ElementAt(k).CompetenciaID;
                         //var peso = context.TablaCompetenciaXPuesto.One(x => x.PuestoID == IDPuestoevaluador1 && x.CompetenciaID == IDCompetencia).Peso;
                         CompetenciaXPuesto auxcompetenciaxpeso = context.TablaCompetenciaXPuesto.One(x => x.PuestoID == IDPuestoevaluador1 && x.CompetenciaID == IDCompetencia);
                         int peso;
                         if (auxcompetenciaxpeso != null)
                         {
                             peso = auxcompetenciaxpeso.Peso;
                         }
                         else
                         {
                             peso = 1;
                         }


                         if (!sumaPesos.ContainsKey(IDCompetencia)) sumaPesos.Add(IDCompetencia, peso);
                         else sumaPesos[IDCompetencia] = sumaPesos[IDCompetencia] + peso;
                         listaCompetenciaXExamenFinal.ElementAt(k).Nota = listaCompetenciaXExamenFinal.ElementAt(k).Nota * peso;
                     }


                    for (int i = 1; i < listaExamenes.Count; i++)
                    {
                        IList<CompetenciaXExamen> listaCompetenciaXExamenParcial = new List<CompetenciaXExamen>();
                        listaCompetenciaXExamenParcial.AddRange(context.TablaCompentenciaXExamen.Where(a => a.ExamenID == listaExamenes.ElementAt(i).ID));

                        var IDPuestoevaluador = context.TablaEvaluadores.One(x => x.ID == listaExamenes.ElementAt(i).ID).ElIDDelEvaluador;

                        for (int j = 0; j < listaCompetenciaXExamenParcial.Count; j++)
                        {
                            var IDCompetencia = listaCompetenciaXExamenFinal.ElementAt(j).CompetenciaID;                            
                            //var peso = context.TablaCompetenciaXPuesto.One(x => x.PuestoID == IDPuestoevaluador && x.CompetenciaID == IDCompetencia).Peso;
                            CompetenciaXPuesto auxcompetenciaxpeso = context.TablaCompetenciaXPuesto.One(x => x.PuestoID == IDPuestoevaluador && x.CompetenciaID == IDCompetencia);
                            int peso;
                            if (auxcompetenciaxpeso != null)
                            {
                                peso = auxcompetenciaxpeso.Peso;
                            }
                            else
                            {
                                peso = 1;
                            }

                            if (!sumaPesos.ContainsKey(IDCompetencia)) sumaPesos.Add(IDCompetencia, peso);
                            else sumaPesos[IDCompetencia] = sumaPesos[IDCompetencia] + peso;
                            listaCompetenciaXExamenFinal.ElementAt(j).Nota = listaCompetenciaXExamenFinal.ElementAt(j).Nota + listaCompetenciaXExamenParcial.ElementAt(j).Nota * peso;                                                       
                        }

                    }
                    
                }

                if (listaExamenes.Count == 1) 
                {
                    foreach (Examen e in listaExamenes)
                    {

                        listaCompetenciaXExamenFinal = context.TablaCompentenciaXExamen.Where(a => a.ExamenID == e.ID).ToList();

                        var IDPuestoevaluador2 = context.TablaEvaluadores.One(x => x.ID == listaExamenes.ElementAt(0).ID).ElIDDelEvaluador;

                        for (int k = 0; k < listaCompetenciaXExamenFinal.Count; k++)
                        {
                            var IDCompetencia = listaCompetenciaXExamenFinal.ElementAt(k).CompetenciaID;
                            CompetenciaXPuesto auxcompetenciaxpeso = context.TablaCompetenciaXPuesto.One(x => x.PuestoID == IDPuestoevaluador2 && x.CompetenciaID == IDCompetencia);
                            int peso ;
                            if (auxcompetenciaxpeso != null)
                            {
                                peso = auxcompetenciaxpeso.Peso;                                
                            }
                            else {
                                peso = 1;
                            }
                            if (!sumaPesos.ContainsKey(IDCompetencia)) sumaPesos.Add(IDCompetencia, peso);
                            else sumaPesos[IDCompetencia] = sumaPesos[IDCompetencia] + peso;
                            listaCompetenciaXExamenFinal.ElementAt(k).Nota = listaCompetenciaXExamenFinal.ElementAt(k).Nota * peso;
                        }

                    }

                }

                if (listaExamenes.Count == 0) { 
                //devuelvo una lista vacia
                //
                }

                for (int j = 0; j < listaCompetenciaXExamenFinal.Count; j++)
                {
                    
                    listaCompetenciaXExamenFinal.ElementAt(j).Nota = listaCompetenciaXExamenFinal.ElementAt(j).Nota / Math.Max(1, sumaPesos[listaCompetenciaXExamenFinal[j].CompetenciaID]);
                }
                 
                 return Json(listaCompetenciaXExamenFinal.Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }

    }
}
