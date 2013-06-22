using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExtensionMethods;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using Kendo.Mvc.Extensions;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class GestorDatosDeColaboradoresController : WSController
    {
        //
        // GET: /Evaluacion360/GestorDatosDeColaboradores/

        public JsonResult consultarDatosDelEmpleado(string conEsteIdentificador)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    ColaboradorDTO colaborador = context.TablaColaboradores.FindByID(Convert.ToInt32(conEsteIdentificador)).ToDTO();
                    PuestoDTO puesto = colaborador.PuestoID == 0 ? new PuestoDTO() : context.TablaPuestos.FindByID(colaborador.PuestoID).ToDTO();
                    AreaDTO area = colaborador.AreaID == 0 ? new AreaDTO() : context.TablaAreas.FindByID(colaborador.AreaID).ToDTO();
                    return JsonSuccessGet(new
                    {
                        colaborador = colaborador,
                        puesto = puesto,
                        area = area
                    });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }

        public JsonResult consultarSusCompanerosPares(string deEsteColaborador)
        {

            using (DP2Context contexto = new DP2Context())
            {
                try
                {
                    int identificadorEnFormatoNumerico = Convert.ToInt32(deEsteColaborador);

                    //List<Colaborador> losPares = GestorServiciosPrivados.consigueSusCompañerosPares(identificadorEnFormatoNumerico);
                    List<Colaborador> losPares = GestorServiciosPrivados.consigueSusCompañerosPares(identificadorEnFormatoNumerico, contexto);

                    List<ColaboradorDTO> enFormatoParaElCliente = losPares.Select(p => p.ToDTO()).ToList();

                    return JsonSuccessGet(new
                        {
                            losColaboradoresEnElMismoRango = enFormatoParaElCliente
                        });
                }
                catch (Exception ocurrioUnProblema)
                {
                    return JsonErrorGet("Error en la BD: " + ocurrioUnProblema.Message);
                }
            }
        }

        public JsonResult consultarElJefe(string deEsteColaborador)
        {

            try
            {

                //using
                //using (DP2Context context = new DP2Context())
                using (DP2Context contexto = new DP2Context())
                {
                    int identificadorEnFormatoNumerico = Convert.ToInt32(deEsteColaborador);

                    //Colaborador losDatosDeSuJefe = GestorServiciosPrivados.consigueElJefe(identificadorEnFormatoNumerico);
                    Colaborador losDatosDeSuJefe = GestorServiciosPrivados.consigueElJefe(identificadorEnFormatoNumerico, contexto);

                    List<Colaborador> suJefeComoGrupoDeUno = new List<Colaborador> { losDatosDeSuJefe };

                    List<ColaboradorDTO> enFormatoParaElCliente = suJefeComoGrupoDeUno.Select(p => p.ToDTO()).ToList();

                    return JsonSuccessGet(new
                    {
                        suSuperior = enFormatoParaElCliente
                    });
                }
            }
            catch (Exception ocurrioUnProblema)
            {
                return JsonErrorGet("Error en la BD: " + ocurrioUnProblema.Message);
            }
        }

        public JsonResult conocerEquipoDeTrabajo(string deEsteColaborador)
        {

            using (DP2Context contexto = new DP2Context())
            {

                try
                {
                    int identificadorEnFormatoNumerico = Convert.ToInt32(deEsteColaborador);

                    //List<Colaborador> colegas = GestorServiciosPrivados.consigueSusCompañerosPares(identificadorEnFormatoNumerico);
                    //List<Colaborador> colegas = GestorServiciosPrivados.consigueSusCompañerosPares(identificadorEnFormatoNumerico, contexto);
                    List<Colaborador> colegas = GestorServiciosPrivados.consigueSusSubordinados(identificadorEnFormatoNumerico, contexto);

                    //List<Colaborador> suJefeComoGrupoDeUno = new List<Colaborador> { losDatosDeSuJefe };

                    List<ColaboradorDTO> enFormatoParaElCliente = colegas.Select(p => p.ToDTO()).ToList();

                    return JsonSuccessGet(new
                    {
                        losEmpleadosQueLeReportan = enFormatoParaElCliente
                    });
                }
                catch (Exception ocurrioUnProblema)
                {
                    return JsonErrorGet("Error en la BD: " + ocurrioUnProblema.Message);
                }
            }
        }

        public JsonResult consultarColaboradores(string asociadosA, string esteEmpleado)
        {

            try
            {
                int identificadorEnFormatoNumerico = Convert.ToInt32(esteEmpleado);

                switch (asociadosA)
                {
                    case "Sus_pares":
                        return consultarSusCompanerosPares(esteEmpleado);
                    case "Su_jefe":
                        return consultarElJefe(esteEmpleado);
                    case "Su_equipo_de_trabajo":
                        return conocerEquipoDeTrabajo(esteEmpleado);
                    default:
                        return JsonSuccessGet(new
                        {
                            interrupcion = "No se entendio lo que desea"
                        });      
                }
            }
            catch (Exception ocurrioUnProblema)
            {
                return JsonErrorGet("Error en la BD: " + ocurrioUnProblema.Message);
            }
        }

        public JsonResult consultarEvaluacionesDelEquipoDeTrabajo(int deEsteJefe)
        {

            int llaveDelJefe = deEsteJefe;

            using (DP2Context contexto = new DP2Context())
            {
                List<Colaborador> subordinados = GestorServiciosPrivados.consigueSusSubordinados(llaveDelJefe, contexto);
                List<int> llavesSubordinados = subordinados.Select(e => e.ID).ToList();

                List<Evaluador> procesoXEvaluadorXEvaluado = contexto.TablaEvaluadores.Where(pxexe => llavesSubordinados.Contains(pxexe.ElEvaluado)).ToList();
                List<ProcesoXEvaluadoXEvaluadorDTO> evaluadosEnFormatoMoviles = procesoXEvaluadorXEvaluado.Select(e => e.enFormatoParaElClienteVistaSubordinados()).ToList();

                return JsonSuccessGet(new { evaluacionesEnMisSubordinados = evaluadosEnFormatoMoviles });
            }


        }

        //public JsonResult consultar
        //public JsonResult consultarResultadosDeCompetencias
        public JsonResult consultarNotasDeProcesoDeEvaluaciones(int deEsteColaborador)
        {

            using (DP2Context contexto = new DP2Context())
            {
                ICollection<ProcesoXCompetenciasDTO> resultados = new List<ProcesoXCompetenciasDTO>();

                List<ProcesoEvaluacionDTO> procesosDondeParticipa = consigueProcesosRelacionados(deEsteColaborador, contexto);

                //for(ProcesoEvaluacion proceso in procesosDondeParticipa) 
                //{
                //    ProcesoXCompetenciasDTO unProceso = new ProcesoXCompetenciasDTO();




                //}

                foreach (ProcesoEvaluacionDTO proceso in procesosDondeParticipa)
                {
                    ProcesoXCompetenciasDTO unProceso = new ProcesoXCompetenciasDTO();

                    unProceso.Proceso = proceso/*.ToDTO()*/;
                    unProceso.ResultadosCompetencias = retornarNotasEnEseProceso(proceso.ID, deEsteColaborador, contexto);

                    //procesosDondeParticipa.Add(unProceso);
                    resultados.Add(unProceso);
                }

                return JsonSuccessGet(new { susProcesos = resultados } );
            }

            //return JsonSuccessGet(new { evaluacionesEnMisSubordinados = evaluadosEnFormatoMoviles });
            //return JsonSuccessGet(new { susProcesos = resultados });

        }

        private List<ProcesoEvaluacionDTO> consigueProcesosRelacionados(int deEsteColaborador, DP2Context contexto)
        {
            //int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
            int ColaboradorID = deEsteColaborador;
            List<ProcesoEvaluacion> procesos = _Read(ColaboradorID, contexto);

            List<ProcesoEvaluacionDTO> auxlistaprocesosdto = new List<ProcesoEvaluacionDTO>();

            foreach (ProcesoEvaluacion e in procesos)
            {

                ColaboradorXProcesoEvaluacion auxcolproceso = contexto.TablaColaboradorXProcesoEvaluaciones.One(x => x.ProcesoEvaluacionID == e.ID && x.ColaboradorID == ColaboradorID);

                if (auxcolproceso != null)
                {
                    ColaboradorXProcesoEvaluacionDTO auxcolprocesodto = auxcolproceso.ToDTO();

                    int puntuacion = (int)(auxcolprocesodto.Nota);

                    ProcesoEvaluacionDTO auxiliar = e.ToDTO();
                    auxiliar.Puntuacion = puntuacion;
                    auxlistaprocesosdto.Add(auxiliar);
                }

            }

            return auxlistaprocesosdto;

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

        //private List<CompetenciaXExamenDTO> retornarCompetencias
        //retornaNotasEnEseProceso
        //private List<CompetenciaXExamenDTO> retornaNotasDeProceso(int )
        //private List<CompetenciaXExamenDTO> retornarNotasEnEseProceso(int eventoId, int deEsteColaborador, DP2Context contexto) { 
        private List<CompetenciaXExamenDTO> retornarNotasEnEseProceso(int eventoId, int deEsteColaborador, DP2Context context) {

            //using (DP2Context context = new DP2Context())
            //{
                //int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
                int ColaboradorID = deEsteColaborador;


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
                var estadoId = context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x => x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Terminado)).ID;
                for (int i = 0; i < listaEvaluadores.Count; i++)
                {
                    Examen auxexamen = context.TablaExamenes.One(
                        a => a.EvaluadorID == listaEvaluadores.ElementAt(i).ID &&
                            a.EstadoExamenID == estadoId);

                    if (auxexamen != null)
                    {
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

                Dictionary<int, int> sumaPesos = new Dictionary<int, int>();
                Boolean bandera = false;
                if (listaExamenes.Count >= 2)
                {
                    listaCompetenciaXExamenFinal = (context.TablaCompetenciaXExamen.Where(a => a.ExamenID == listaExamenes.ElementAt(0).ID));
                    var IDevaluador1 = context.TablaEvaluadores.One(x => x.ID == listaExamenes.ElementAt(0).ID).ElIDDelEvaluador;

                    String claseentorno = "";
                    if (jefe.ID == IDevaluador1) { claseentorno = "Jefe"; }
                    else
                    {
                        if (IDevaluador1 == ColaboradorID) { claseentorno = "El mismo"; }
                        else
                        {
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
                            listaCompetenciaXExamenFinal.ElementAt(j).Nota = listaCompetenciaXExamenFinal.ElementAt(j).Nota + listaCompetenciaXExamenParcial.ElementAt(j).Nota * pesoPuesto * pesoCompetencia;
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

                if (listaExamenes.Count == 0)
                {
                    //devuelvo una lista vacia
                    //
                }

                for (int j = 0; j < listaCompetenciaXExamenFinal.Count; j++)
                {

                    listaCompetenciaXExamenFinal.ElementAt(j).Nota = listaCompetenciaXExamenFinal.ElementAt(j).Nota / Math.Max(1, sumaPesos[listaCompetenciaXExamenFinal[j].CompetenciaID]);
                }

                //return Json(listaCompetenciaXExamenFinal.Select(x => x.ToDTO()).ToDataSourceResult(request));
                //return listaCompetenciaXExamenFinal.Select(e => e.ToDTO());
                return listaCompetenciaXExamenFinal.Select(e => e.ToDTO()).ToList();
            //}
        

        
        }
        

    }
}













        ////public JsonResult consultar
        ////public JsonResult consultarResultadosDeCompetencias
        //public JsonResult consultarNotasDeProcesoDeEvaluaciones(int deEsteColaborador)
        //{

        //    ICollection<ProcesoXCompetenciasDTO> resultados = new List<ProcesoXCompetenciasDTO>();

        //    List<ProcesoEvaluacion> procesosDondeParticipa = consigueProcesosRelacionados(deEsteColaborador);
 
        //    //for(ProcesoEvaluacion proceso in procesosDondeParticipa) 
        //    //{
        //    //    ProcesoXCompetenciasDTO unProceso = new ProcesoXCompetenciasDTO();




        //    //}

        //    foreach (ProcesoEvaluacion proceso in procesosDondeParticipa)
        //    {
        //        ProcesoXCompetenciasDTO unProceso = new ProcesoXCompetenciasDTO();

        //        unProceso.Proceso = proceso.ToDTO();
        //        unProceso.ResultadosCompetencias = retornaNotasEnEseProceso(proceso.ID, deEsteColaborador);

        //    }

        //    //return JsonSuccessGet(new { evaluacionesEnMisSubordinados = evaluadosEnFormatoMoviles });
        //    return JsonSuccessGet(new { susProcesos = resultados });

        //}













        ////public JsonResult sinNombre(int unEntero)
        //public JsonResult consultarEvaluacionesDelEquipoDeTrabajo(int deEsteJefe)
        //{

        //    int llaveDelJefe = deEsteJefe;

        //    using (DP2Context contexto = new DP2Context())
        //    {
        //        List<Colaborador> subordinados = GestorServiciosPrivados.consigueSusSubordinados(llaveDelJefe, contexto);
        //        //subordinados.Select(e => e.ID).ToList();
        //        List<int> llavesSubordinados = subordinados.Select(e => e.ID).ToList();

        //        List<Evaluador> procesoXEvaluadorXEvaluado = contexto.TablaEvaluadores.Where(pxexe => llavesSubordinados.Contains(pxexe.ElEvaluado)).ToList();
        //        //List<EvaluadorDTO> evaluadosEnFormatoMoviles  
        //        //List<EvaluadorDTO> evaluadosEnFormatoMoviles = procesoXEvaluadorXEvaluado.Select(e => e.enFormatoParaElClienteVistaSubordinados()).ToList();
        //        List<ProcesoXEvaluadoXEvaluadorDTO> evaluadosEnFormatoMoviles = procesoXEvaluadorXEvaluado.Select(e => e.enFormatoParaElClienteVistaSubordinados()).ToList();

        //        return JsonSuccessGet(new { evaluacionesEnMisSubordinados = evaluadosEnFormatoMoviles });
        //        //List<EvaluadorDTO> evaluadosEnFormatoCliente 
        //        //List<Evaluador> ProcesoXEvaluadorXEvaluado = contexto.TablaEvaluadores.Where(pxexe => subordinados.)
        //        //List<Evaluador> ProcesoXEvaluadorXEvaluado = contexto.TablaEvaluadores.Where
        //    }


        //}