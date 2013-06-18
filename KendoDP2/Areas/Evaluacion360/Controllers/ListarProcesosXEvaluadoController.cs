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


        internal List<ProcesoEvaluacion> _Read(int idUsuario, DP2Context context)
        {
            List<ProcesoEvaluacion> listaProcesos_ = new List<ProcesoEvaluacion>();
            List<int> listaExamenes = context.TablaEvaluadores.Where(x => x.ElIDDelEvaluador == idUsuario).Select(a => a.ProcesoEnElQueParticipanID).ToList();
            int estadoID = context.TablaEstadoProcesoEvaluacion.One(e => e.Descripcion.Equals(ConstantsEstadoProcesoEvaluacion.Terminado)).ID;
            listaProcesos_ = context.TablaProcesoEvaluaciones.Where(p => listaExamenes.Contains(p.ID) && p.EstadoProcesoEvaluacionID == estadoID).ToList();
            return listaProcesos_;

            /*
            List<ProcesoEvaluacion> listaProceso = new List<ProcesoEvaluacion>();

            IList<Evaluador> listaProcesosEvaluador = (context.TablaEvaluadores.Where(a => a.ElEvaluado == idUsuario));

            for (int i = 0; i < listaProcesosEvaluador.Count; i++)
            {

                ProcesoEvaluacion procesoauxiliar = context.TablaProcesoEvaluaciones.One(b => b.ID == (listaProcesosEvaluador.ElementAt(i).ProcesoEnElQueParticipanID) &&
                    b.EstadoProcesoEvaluacionID == context.TablaEstadoProcesoEvaluacion.One(e => e.Descripcion.Equals(ConstantsEstadoProcesoEvaluacion.Terminado)).ID);

                if (procesoauxiliar != null)
                {
                    listaProceso.Add(procesoauxiliar);
                }

            }
            return listaProceso.GroupBy(x => x.ID).Select(y => y.FirstOrDefault()).ToList();*/
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
                List<ProcesoEvaluacion> procesos = _Read(ColaboradorID, context);

                List<ProcesoEvaluacionDTO> auxlistaprocesosdto = new List<ProcesoEvaluacionDTO>();

                foreach (ProcesoEvaluacion e in procesos)
                {

                    ColaboradorXProcesoEvaluacion auxcolproceso = context.TablaColaboradorXProcesoEvaluaciones.One(x => x.ProcesoEvaluacionID == e.ID && x.ColaboradorID == ColaboradorID);

                    if (auxcolproceso != null) { 
                    ColaboradorXProcesoEvaluacionDTO auxcolprocesodto=auxcolproceso.ToDTO();

                    int puntuacion = (int)(auxcolprocesodto.Nota);

                    ProcesoEvaluacionDTO auxiliar = e.ToDTO();
                    auxiliar.Puntuacion = puntuacion;
                    auxlistaprocesosdto.Add(auxiliar);
                    }                                                           

                }                

                //return Json(procesos.Select(x => x.ToDTO()).ToDataSourceResult(request));
                return Json(auxlistaprocesosdto.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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

                Colaborador jefe = GestorServiciosPrivados.consigueElJefe(ColaboradorID, context);
                IList<Colaborador> listaCompañerosPares = GestorServiciosPrivados.consigueSusCompañerosPares(ColaboradorID, context);
                IList<Colaborador> listaSubordinados = GestorServiciosPrivados.consigueSusSubordinados(ColaboradorID, context);                                
               
                Dictionary<int, int> sumaPesos = new Dictionary<int,int>();
                Boolean bandera = false;
                if (listaExamenes.Count >= 2)
                {
                    listaCompetenciaXExamenFinal = (context.TablaCompetenciaXExamen.Where(a => a.ExamenID == listaExamenes.ElementAt(0).ID));
                    var IDevaluador1 = context.TablaEvaluadores.One(x => x.ID == listaExamenes.ElementAt(0).ID).ElIDDelEvaluador;

                     String claseentorno= "";                     
                     if (jefe.ID == IDevaluador1) { claseentorno="Jefe";}
                     else
                     {
                       if (IDevaluador1 == ColaboradorID) { claseentorno = "El mismo"; }
                       else{                       
                         foreach (Colaborador e in listaSubordinados)
                         {
                             if (e.ID == IDevaluador1) { bandera = true; claseentorno = "Subordinados"; break; };
                             
                         }
                         if (!bandera)
                         {
                             foreach (Colaborador e in listaCompañerosPares)
                             {
                                 if (e.ID == IDevaluador1) { claseentorno = "Pares"; };
                             }
                         }
                       }
                     }

                     int idPuesto = context.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == IDevaluador1).PuestoID;

                     
                     int cantidad = context.TablaPuestoXEvaluadores.One(x => x.PuestoID == idPuesto && x.ClaseEntorno.Equals(claseentorno)).Cantidad;

                     for (int k = 0; k < listaCompetenciaXExamenFinal.Count; k++)
                     {
                         var IDCompetencia = listaCompetenciaXExamenFinal.ElementAt(k).CompetenciaID;
                         var pesoCompetencia = listaCompetenciaXExamenFinal.ElementAt(k).Peso;

                         var pesoPuesto = context.TablaPuestoXEvaluadores.One(x => x.PuestoID == idPuesto && x.ClaseEntorno.Equals(claseentorno)).Peso;

                         if (cantidad > 1) { pesoPuesto = pesoPuesto / cantidad; }

                         if (!sumaPesos.ContainsKey(IDCompetencia)) sumaPesos.Add(IDCompetencia, pesoCompetencia * pesoPuesto);
                         else sumaPesos[IDCompetencia] = sumaPesos[IDCompetencia] + (pesoCompetencia * pesoPuesto);
                         listaCompetenciaXExamenFinal.ElementAt(k).Nota = listaCompetenciaXExamenFinal.ElementAt(k).Nota * pesoPuesto * pesoCompetencia;
                             
                     }


                    for (int i = 1; i < listaExamenes.Count; i++)
                    {
                        IList<CompetenciaXExamen> listaCompetenciaXExamenParcial = new List<CompetenciaXExamen>();
                        listaCompetenciaXExamenParcial.AddRange(context.TablaCompetenciaXExamen.Where(a => a.ExamenID == listaExamenes.ElementAt(i).ID));

                        var IDevaluador = context.TablaEvaluadores.One(x => x.ID == listaExamenes.ElementAt(i).ID).ElIDDelEvaluador;
                        bandera = false;
                        claseentorno = "";
                        if (jefe.ID == IDevaluador) { claseentorno = "Jefe"; }
                        else
                        {
                            if (IDevaluador == ColaboradorID) { claseentorno = "El mismo"; }
                            else
                            {
                                foreach (Colaborador e in listaSubordinados)
                                {
                                    if (e.ID == IDevaluador) { bandera = true; claseentorno = "Subordinados"; break; };                                    
                                }
                                if (!bandera)
                                {
                                    foreach (Colaborador e in listaCompañerosPares)
                                    {
                                        if (e.ID == IDevaluador) { claseentorno = "Pares"; };
                                    }
                                }
                            }
                        }

                        idPuesto = context.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == IDevaluador).PuestoID;

                        
                        cantidad = context.TablaPuestoXEvaluadores.One(x => x.PuestoID == idPuesto && x.ClaseEntorno.Equals(claseentorno)).Cantidad;

                        for (int j = 0; j < listaCompetenciaXExamenParcial.Count; j++)
                        {
                            var IDCompetencia = listaCompetenciaXExamenFinal.ElementAt(j).CompetenciaID;
                            var pesoCompetencia = listaCompetenciaXExamenFinal.ElementAt(j).Peso;

                            int pesoPuesto = context.TablaPuestoXEvaluadores.One(x => x.PuestoID == idPuesto && x.ClaseEntorno.Equals(claseentorno)).Peso;

                            if (cantidad > 1) { pesoPuesto = pesoPuesto / cantidad; }

                            if (!sumaPesos.ContainsKey(IDCompetencia)) sumaPesos.Add(IDCompetencia, pesoPuesto * pesoCompetencia);
                            else sumaPesos[IDCompetencia] = sumaPesos[IDCompetencia] + (pesoPuesto * pesoCompetencia);
                            listaCompetenciaXExamenFinal.ElementAt(j).Nota = listaCompetenciaXExamenFinal.ElementAt(j).Nota + listaCompetenciaXExamenParcial.ElementAt(j).Nota * pesoPuesto * pesoCompetencia ;                                                       
                        }

                    }
                    
                }

                if (listaExamenes.Count == 1) 
                {
                    foreach (Examen e in listaExamenes)
                    {

                        listaCompetenciaXExamenFinal = context.TablaCompetenciaXExamen.Where(a => a.ExamenID == e.ID).ToList();

                        var IDevaluador2 = context.TablaEvaluadores.One(x => x.ID == listaExamenes.ElementAt(0).ID).ElIDDelEvaluador;

                        String claseentorno = "";  
                        bandera = false;
                        claseentorno = "";
                        if (jefe.ID == IDevaluador2) { claseentorno = "Jefe"; }
                        else
                        {
                            if (IDevaluador2 == ColaboradorID) { claseentorno = "El mismo"; }
                            else
                            {
                                foreach (Colaborador f in listaSubordinados)
                                {
                                    if (f.ID == IDevaluador2) { bandera = true; claseentorno = "Subordinados"; break; };
                                    
                                }
                                if (!bandera)
                                {
                                    foreach (Colaborador f in listaCompañerosPares)
                                    {
                                        if (f.ID == IDevaluador2) { claseentorno = "Pares"; };
                                    }
                                }
                            }
                        }

                        int idPuesto2 = context.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == IDevaluador2).PuestoID;

                        
                        int cantidad = context.TablaPuestoXEvaluadores.One(x => x.PuestoID == idPuesto2 && x.ClaseEntorno.Equals(claseentorno)).Cantidad;
                        for (int k = 0; k < listaCompetenciaXExamenFinal.Count; k++)
                        {
                            var IDCompetencia = listaCompetenciaXExamenFinal.ElementAt(k).CompetenciaID;
                            var pesoCompetencia = listaCompetenciaXExamenFinal.ElementAt(k).Peso;

                            int pesoPuesto = context.TablaPuestoXEvaluadores.One(x => x.PuestoID == idPuesto2 && x.ClaseEntorno.Equals(claseentorno)).Peso;
                            if (cantidad > 1) { pesoPuesto = pesoPuesto / cantidad; }

                            if (!sumaPesos.ContainsKey(IDCompetencia)) sumaPesos.Add(IDCompetencia, pesoPuesto * pesoCompetencia);
                            else sumaPesos[IDCompetencia] = sumaPesos[IDCompetencia] + (pesoPuesto * pesoCompetencia);
                            listaCompetenciaXExamenFinal.ElementAt(k).Nota = listaCompetenciaXExamenFinal.ElementAt(k).Nota * pesoPuesto * pesoCompetencia;
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
