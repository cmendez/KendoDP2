using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Reportes.Models;
using KendoDP2.Areas.Objetivos.Models;
using KendoDP2.Areas.Reclutamiento.Models;
using ExtensionMethods;
using System;
using System.Globalization;

namespace KendoDP2.Areas.Reportes.Controllers
{
    public class ReportesController : WSController
    {

        //
        // GET: /Reportes/Reportes/

        public ActionResult ListarObjetivosXBSC(int BSCId, int idperiodo)
        {
            using (DP2Context context = new DP2Context())
            {
                //Objetivo de prueba
                //context.TablaObjetivos.AddElement(new Objetivo
                //{
                //    Peso = 0,
                //    Nombre = "",
                //    ObjetivoPadre = context.TablaObjetivos.FindByID(3),
                //    IsObjetivoIntermedio = true,
                //    IsEliminado = false,

                //});
                //context.TablaObjetivos.AddElement(new Objetivo
                //{
                //    Peso = 0,
                //    Nombre = "Objetivo Financiero 1.1.1",
                //    ObjetivoPadre = context.TablaObjetivos.FindByID(21),
                //    IsObjetivoIntermedio = false,
                //    IsEliminado = false,

                //});
                 

                //Colaborador col= context.TablaColaboradores.FindByID(2);
                //col.Objetivos.Add(context.TablaObjetivos.FindByID(22));
                //context.TablaColaboradores.ModifyElement(col);
                //PuestoDTO pu = context.TablaPuestos.One(perfil => perfil.Nombre.Equals("Gerente general")).ToDTO();
                //if (context.TablaColaboradoresXPuestos.Where(cxp => cxp.Colaborador.Nombres.CompareTo("colaborador modulo tres a") == 0 && cxp.Puesto.ID == pu.ID).Count == 0)
                //{
                //    context.TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = context.TablaPuestos.One(perfil => perfil.Nombre.Equals("Gerente general")).ID, ColaboradorID = context.TablaColaboradores.One(e => e.Nombres.CompareTo("colaborador modulo tres a") == 0).ID, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });

                //}

                List<ObjetivoRDTO> ListaObjetivos2 = new List<ObjetivoRDTO>();
                List<ObjetivoDTO> ListaObjetivos3 = new List<ObjetivoDTO>();

                
                ListaObjetivos3 = context.TablaObjetivos.All().Select(o => o.ToDTO(context)).ToList();
                foreach (ObjetivoDTO obj in ListaObjetivos3)
                {
                    if (obj.BSCID ==idperiodo && obj.TipoObjetivoBSCID==BSCId){
                        ListaObjetivos2.AddRange(context.TablaObjetivos.Where(o => o.ID == obj.ID).Select(oo => oo.ToRDTO(context)));
                    }
                }
                //List<Objetivo> ListaObjetivos2aux = context.TablaObjetivos.Where(o => o.ToDTO(context).TipoObjetivoBSCID == BSCId && o.ToDTO(context).BSCID == idperiodo).ToList();
                //if (ListaObjetivos2aux.Count>0){
                //    ListaObjetivos2=ListaObjetivos2aux.Select(p => p.ToRDTO(context)).ToList();
                //}

                return Json(ListaObjetivos2, JsonRequestBehavior.AllowGet);
                //return Json(ListaObjetivos, JsonRequestBehavior.AllowGet);
            }
        }
       
        public ActionResult ListarObjetivosXPadre(int PadreId)
        {
            using (DP2Context context = new DP2Context())
            {
                //List<ObjetivoDTO> ListaObjetivosDTO = context.TablaObjetivos.All().Select(p => p.ToDTO(context)).ToList();
                List<ObjetivoRDTO> ListaObjetivosR=new List<ObjetivoRDTO>();
                List<ObjetivoRDTO> ListaObjetivosHijos = context.TablaObjetivos.Where(o => o.ObjetivoPadreID ==PadreId).Select(p => p.ToRDTO(context)).ToList();
                if (ListaObjetivosHijos.Count > 0)
                {
                    if (ListaObjetivosHijos[0].esIntermedio)
                    {
                    //La nueva lista de objetivos serán los hijos de los objetivos intermedios
                        foreach (ObjetivoRDTO objhijo in ListaObjetivosHijos){
                            
                            List<ObjetivoRDTO> Objetivosnietos=context.TablaObjetivos.Where(o => o.ObjetivoPadreID ==objhijo.idObjetivo).Select(p => p.ToRDTO(context)).ToList();
                            if (Objetivosnietos.Count>0 )
                            objhijo.descripcion = Objetivosnietos[0].descripcion;
                            objhijo.numPersonas = objhijo.hijos;
                            
                        }                        
                    }
                    //else
                    //{
                    //    ListaObjetivosR=ListaObjetivosHijos;
                    //    //foreach (ObjetivoRDTO obj in ListaObjetivosR){
                    //    //obj.hijos = context.TablaObjetivos.Where(o=> o.ObjetivoPadreID == obj.idObjetivo).Select(p => p.ToRDTO(context)).ToList().Count;
                    //}
                }
                                
                //return Json(ListaObjetivos, JsonRequestBehavior.AllowGet);
                return Json(ListaObjetivosHijos, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PersonasXObjetivo(int idObjetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                List<PersonaXObjetivoDTO> PersonasXObjetivo = new List<PersonaXObjetivoDTO>();
                //Primera versión. Usando la lista de objetivos de los colaboradores
                //List<Colaborador> Colaboradoresaux = context.TablaColaboradores.All();
                //List<ObjetivoConPadreDTO> Objetivosaux = context.TablaObjetivos.All().Select(p => p.ObjetivoConPadreDTO(context)).ToList();
                //List<ColaboradorDTO> Colaboradores = context.TablaColaboradores.Where(col => col.Objetivos.Where(obj => obj.ObjetivoPadreID == idObjetivo).ToList().Count > 0).Select(c => c.ToDTO()).ToList();
                //foreach (ColaboradorDTO col in Colaboradores)
                //{
                //    PersonaXObjetivoDTO pxo = new PersonaXObjetivoDTO();
                //    pxo.nombreColaborador = col.NombreCompleto;
                //    pxo.avance = col.Objetivos.Find(obj => obj.ID == context.TablaObjetivos.One(o=>o.ObjetivoPadre.ID==idObjetivo).ToDTO(context).ID).AvanceFinal;
                //    pxo.idObjetivo = col.Objetivos.Find(obj => obj.ID == idObjetivo).ID;
                //    PersonasXObjetivo.Add(pxo);
                //}
                List<ObjetivoConPadreDTO> ListaObjetivosHijos = context.TablaObjetivos.Where(o => o.ObjetivoPadreID == idObjetivo).Select(o => o.ObjetivoConPadreDTO(context)).ToList();
                List<ColaboradorDTO> ListaColaboradores = new List<ColaboradorDTO>();

                foreach (ObjetivoConPadreDTO objhijo in ListaObjetivosHijos)
                {
                    ListaColaboradores.AddRange(context.TablaColaboradoresXPuestos.Where(cxp => cxp.PuestoID == objhijo.puestoID && (!cxp.FechaSalidaPuesto.HasValue )).Select(p => p.ToDTO().Colaborador));
                }
                //List<ColaboradorXPuestoDTO> ListaColaboradoresXPuesto = context.TablaColaboradoresXPuestos.Where(cxp => cxp.PuestoID == ListaObjetivosHijos[0].puestoID).Select(p => p.ToDTO()).ToList();
                
                //foreach (ColaboradorXPuestoDTO cxp in ListaColaboradoresXPuesto)
                //{
                //    ListaColaboradores.Add(cxp.Colaborador);
                //}
                foreach (ColaboradorDTO c in ListaColaboradores)
                {
                    foreach (ObjetivoConPadreDTO objetivohijo in ListaObjetivosHijos)
                    {
                        if (c.PuestoID == objetivohijo.puestoID)
                        {
                            PersonaXObjetivoDTO pxo = new PersonaXObjetivoDTO();
                            pxo.nombreColaborador = c.NombreCompleto;
                            ObjetivoConPadreDTO obj = ListaObjetivosHijos.Find(o => o.puestoID == c.PuestoID);
                            pxo.avance = obj.AvanceFinal;
                            pxo.idObjetivo = obj.ID;
                            //pxo.objetivos = context.TablaObjetivos.Where(ob => ob.ID==ListaObjetivosHijos.Find(o => o.puestoID == c.PuestoID).ID).Select(objj=> objj.ToRDTO(context)).ToList();
                            pxo.objetivos = context.TablaObjetivos.Where(ob => ob.ObjetivoPadreID != 0 && context.TablaObjetivos.FindByID(ob.ObjetivoPadreID).PuestoAsignadoID == c.PuestoID).Select(objj => objj.ToRDTO(context)).ToList();
                            PersonasXObjetivo.Add(pxo);
                        }
                    }
                }

                return Json(PersonasXObjetivo, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult ListarObjetivosXPadre2(int PadreId)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaObjetivos.Where(o => o.ObjetivoPadreID == PadreId).Select(p => p.ToRDTO(context)).ToList(), JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ListaObjetivos()
        {
            using (DP2Context context = new DP2Context())
            {
            return Json(context.TablaObjetivos.All().Select(o=>o.ObjetivoConPadreDTO(context)).ToList(), JsonRequestBehavior.AllowGet); 
            }
        }
        

//        public ActionResult ObjetivosxPersona(int idObjetivo)
//        {
//            using (DP2Context context = new DP2Context())
//            {
//                //Buscaremos el objetivo padre para ver si es un objetivo intermedio
//                List<ObjetivoConPadreDTO> listaObjAux = context.TablaObjetivos.Where(p => p.ObjetivoPadreID == idObjetivo).Select(p => p.ObjetivoConPadreDTO(context)).ToList();
//                List<ObjetivosXPersonaRDTO> ListaReporteAvanceXPersona = new List<ObjetivosXPersonaRDTO>();
//                if (listaObjAux.Count>0 && listaObjAux[0].padreEsIntermedio)
//                {
//                    //Se mostrarán a todos los colaboradores que tengan objetivos con padre igual al intermedio encontrado
//                    List<ColaboradorDTO> Colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
//                    foreach (ColaboradorDTO col in Colaboradores)
//                    {
//                        if (col.Objetivos.Find(obj => obj.ObjetivoPadreID == listaObjAux[0].ObjetivoPadreID)!=null)
//                        {
//                            ObjetivosXPersonaRDTO perXObj = new ObjetivosXPersonaRDTO();
//                            perXObj.nombreColaborador = col.NombreCompleto;
//                            perXObj.avance = col.Objetivos.Find(obj => obj.ObjetivoPadreID == listaObjAux[0].ObjetivoPadreID).AvanceFinal;
//                        }
//                    }
//                }
//                #region porProbar
                
//                //Considerando que los ob
                
//                //List<ObjetivosXPersonaRDTO> ListaReporteAvanceXPersona = new List<ObjetivosXPersonaRDTO>();
//                //List<ColaboradorDTO> ColabObjetivos = new List<ColaboradorDTO>();
//                //List<ObjetivoDTO> ObjetivosXPersona = new List<ObjetivoDTO>();
//                //foreach (ColaboradorDTO Colaborador in Colaboradores)
//                //{
//                //    foreach (ObjetivoDTO ob in Colaborador.Objetivos)
//                //    {
                        
//                //        if (ob.ObjetivoPadreID == idObjetivo)
//                //        {
//                //            ObjetivosXPersona.Add(ob);
//                //            if (ColabObjetivos.Find(c => c.NombreCompleto==Colaborador.NombreCompleto)==null)
//                //            {
//                //                ColabObjetivos.Add(Colaborador);
//                //            }
//                //        }
//                //    }
//                //}
//                //foreach (ColaboradorDTO colab in ColabObjetivos)
//                //{
//                //    int avance = 0;
//                //    int totalobj = 0;
//                //    foreach (ObjetivoDTO obj in colab.Objetivos){
//                //        if (obj.ObjetivoPadreID == idObjetivo)
//                //        {
//                //            avance += obj.AvanceFinal;
//                //            totalobj+=1;
//                //        }
//                //    }
//                //    ObjetivosXPersonaRDTO objxcolrep = new ObjetivosXPersonaRDTO();
//                //    objxcolrep.nombreColaborador = colab.NombreCompleto;
//                //    objxcolrep.avance = avance / totalobj;
//                //    ListaReporteAvanceXPersona.Add(objxcolrep);
//                //}

                
//#endregion

//                return Json(ListaReporteAvanceXPersona, JsonRequestBehavior.AllowGet); 
//                //int idpuesto=context.TablaObjetivos.One(d => d.ID.Equals(idObjetivo)).PuestoAsignadoID.Value;
                
//                //List<ColaboradorXPuestoDTO> ColaboradoresXPuesto =context.TablaColaboradoresXPuestos.Where( p => p.PuestoID==idpuesto).Select (p => p.ToDTO()).ToList();

                

//                //foreach (ColaboradorXPuestoDTO ColXPues in ColaboradoresXPuesto)
//                //{
//                //    ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { ColaboradorDTO =ColXPues.ColaboradorDTO, avance = 50});
//                //}
//                //List<ColaboradorDTO> Colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                
//                //List<ColaboradorXPuestoDTO> ColaboradoresXPuesto = context.TablaColaboradoresXPuestos.Where(p => p.PuestoID == idpuesto).Select(p => p.ToDTO()).ToList();
//                //ColaboradorDTO col = context.TablaColaboradores.FindByID(1).ToDTO();

//                //ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[10].NombreCompleto, avance = 50 });
//                //ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[9].NombreCompleto, avance = 50 });
//                //ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[11].NombreCompleto, avance = 50 });
//                //ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[8].NombreCompleto, avance = 50 });

//                //return Json(ListaObjetivosXPersona, JsonRequestBehavior.AllowGet); 
//            }

//        }
       

        public ActionResult ListarPuestos()
        {
            using (DP2Context context = new DP2Context())
            {
                List<PuestoDTO> ListaPuestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                return Json(ListaPuestos, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AvanceBSC(int idperiodo)
        {
            using (DP2Context context = new DP2Context())
            {
                List<BSCAvanceDTO> ListaAvanceBSC = context.TablaBSC.All().Select(p => p.ToRAvanceBSCDTO()).ToList();
                return Json(ListaAvanceBSC, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ObjetivosOffline(int idperiodo)
        {
            using (DP2Context context = new DP2Context())
            {
                List<Objetivo> ObjetivosPeriodoaux = context.TablaObjetivos.Where(obj => obj.GetBSCIDRaiz(context) == idperiodo);
                List<ObjetivoRDTO> ObjetivosPeriodo = ObjetivosPeriodoaux.Select(oxp => oxp.ToRDTO(context)).ToList();
                //List<ObjetivoRDTO> ObjetivosPeriodo = context.TablaObjetivos.Where(obj => obj.ToRDTO(context).idperiodo==idperiodo).Select(ob => ob.ToRDTO(context)).ToList();
                
                return Json(ObjetivosPeriodo, JsonRequestBehavior.AllowGet);
            }
            
        }

        //public ActionResult PostulacionySeleccion(int idpuesto)
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        return Json(context.TablaOfertaLaboralXPostulante.All()., JsonRequestBehavior.AllowGet);
        //    }
        //}
        public ActionResult PostulacionySeleccion(int idpuesto,string finicio,string ffin)
        {
            using (DP2Context context = new DP2Context())
            {

                List<OfertaLaboral> ListaOfertasaux = context.TablaOfertaLaborales.All();
                DateTime parser=new System.DateTime();
                //DateTime dateinicio = System.DateTime.ParseExact(finicio, "dd/MM/yyyy", CultureInfo.CurrentCulture) ;
                DateTime dateinicio = System.DateTime.ParseExact(ListaOfertasaux[0].FechaRequerimiento, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                int leng= finicio.Length;
                DateTime dateinicio2 = System.DateTime.ParseExact(finicio, "dd/MM/yyyy", CultureInfo.CurrentCulture); 
                //List<OfertaLaboralDTO> ListaOfertas = context.TablaOfertaLaborales.Where(p => p.PuestoID==idpuesto && Convert.ToDateTime(p.FechaFinVigenciaSolicitud)<=Convert.ToDateTime(ffin) && Convert.ToDateTime(p.FechaPublicacion)>Convert.ToDateTime(finicio)).Select(of=>of.ToDTO()).ToList();
                List<OfertaLaboral> ListaOfertasaux2 = ListaOfertasaux.Where(p => p.PuestoID == idpuesto && p.EstadoSolicitudOfertaLaboral.Descripcion == "Aprobado" &&
                    DateTime.ParseExact(p.FechaRequerimiento, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(dateinicio2) >= 0
                    && DateTime.ParseExact(p.FechaRequerimiento, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(DateTime.ParseExact(ffin, "dd/MM/yyyy", CultureInfo.CurrentCulture)) <= 0).ToList();
                    
                    ;
                
                List<OfertaLaboralDTO> ListaOfertas = new List<OfertaLaboralDTO>();
                if (ListaOfertasaux2!=null){
                    ListaOfertas=ListaOfertasaux2.Select(of=>of.ToDTO()).ToList();
                }
                    


                List<OfertaLaboralXPostulanteDTO> ListaOfertasXPostulante=new List<OfertaLaboralXPostulanteDTO>();
                foreach (OfertaLaboralDTO of in ListaOfertas)
                {
                    ListaOfertasXPostulante.AddRange(context.TablaOfertaLaboralXPostulante.Where(oxp => oxp.OfertaLaboralID==of.ID).Select(oxp=>oxp.ToDTO()).ToList());
                }
                List<PostulanteDTO> ListaPostulantes = new List<PostulanteDTO>();
                foreach (OfertaLaboralXPostulanteDTO of in ListaOfertasXPostulante)
                {
                    ListaPostulantes.AddRange(context.TablaPostulante.Where(c => c.ID == of.PostulanteID).Select(oxp => oxp.ToDTO()).ToList());
                }

                //List<OfertaLaboralXPostulante> ListaOfertas = context.TablaOfertaLaborales.Where(p => p.PuestoID==idpuesto).Select(p => p.Postulantes);
               //// List<PostulanteDTO> ListaPostulantes =context.TablaOfertaLaboralXPostulante.Where(p => p.OfertaLaboral.PuestoID==idpuesto).Select(p=>p.Postulante.ToDTO()).ToList();
                
                
                List<ROfertasLaborales> ListaROfertas = new  List<ROfertasLaborales>() ;
                
                foreach (PostulanteDTO pos in ListaPostulantes)
                {
                    ROfertasLaborales prov = ListaROfertas.Find(p => p.nombreProveniencia == pos.CentroEstudios && pos.GradoAcademico == p.gradoAcademico);

                    if (prov == null)
                    {
                        prov = new ROfertasLaborales();
                        prov.nombreProveniencia = pos.CentroEstudios;
                        prov.gradoAcademico = pos.GradoAcademico;
                        prov.cantPostulantes = 1;
                        prov.cantElegidos = 0;
                        ListaROfertas.Add(prov);
                    }
                    else
                    {
                        prov.cantPostulantes += 1;
                    }
                };
                
                return Json(ListaROfertas, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult HistoricoObjetivosOffline() 
        {
            using (DP2Context context = new DP2Context())
            {
                List<HistoricoBSC> ObjetivosHistoricosXPersona = new List<HistoricoBSC>();
                List<ColaboradorDTO> Colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                foreach (ColaboradorDTO col in Colaboradores)
                {
                    //List<ObjetivoRDTO> ObjetivosColaborador = context.TablaObjetivos.Where(ob => ob.Dueño.ID == col.ID ).Select(p => p.ToRDTO(context)).ToList();
                    List<ObjetivoRDTO> ObjetivosColaborador=new List<ObjetivoRDTO>();

                    
                    int puestoID = context.TablaColaboradores.FindByID(col.ID).ToDTO().PuestoID;
                    if (puestoID > 0)
                    {
                        Puesto puesto = context.TablaPuestos.FindByID(puestoID);
                        ObjetivosColaborador = puesto.Objetivos.Select(c => c.ToRDTO(context)).ToList();
                    }
                    List<ObjetivoRDTO> ObjetivosColaborador2= new List<ObjetivoRDTO>();
                    foreach (ObjetivoRDTO objpadre in ObjetivosColaborador)
                    {
                        ObjetivosColaborador2.AddRange(context.TablaObjetivos.Where(oo=>oo.ObjetivoPadreID==objpadre.idObjetivo).Select(ooo => ooo.ToRDTO(context)));
                    }
                    //List<ObjetivoRDTO> ObjetivosColaborador2 = ObjetivosColaborador.Where( o1=> o1.esPropioColaborador(o1.idpadre,context)).ToList();
                    ObjetivosColaborador =ObjetivosColaborador2;

                    foreach (ObjetivoRDTO o in ObjetivosColaborador)
                    {
                        HistoricoBSC PeriodoPersona = ObjetivosHistoricosXPersona.Find(oh => oh.idperiodo == o.idperiodo && o.ColaboradorNombre==oh.nombreColaborador);
                        if (PeriodoPersona != null)
                        {
                            PeriodoPersona.objetivos.Add(o);
                        }
                        else
                        {
                            HistoricoBSC NuevoPeriodoPersona = new HistoricoBSC();
                            NuevoPeriodoPersona.idperiodo = o.idperiodo;
                            NuevoPeriodoPersona.nombrePeriodo = context.TablaPeriodos.One(per => per.ID == o.idperiodo).ToDTO().Nombre;
                            NuevoPeriodoPersona.nombreColaborador = col.NombreCompleto;
                            NuevoPeriodoPersona.objetivos = new List<ObjetivoRDTO>();
                            NuevoPeriodoPersona.objetivos.Add(o);
                            ObjetivosHistoricosXPersona.Add(NuevoPeriodoPersona);
                        }
                    }
                }

                return Json(ObjetivosHistoricosXPersona, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Colaboradores()
        {
            using (DP2Context context = new DP2Context())
            {
                List<ColaboradorRDTO> Colaboradores = context.TablaColaboradores.All().Select(c => c.ToRDTO(context)).ToList();

                return Json(Colaboradores, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult HistoricoObjetivos(int idColaborador)
        {
            using (DP2Context context = new DP2Context())
            {
                List<HistoricoBSC> ObjetivosHistoricosXPersona = new List<HistoricoBSC>();

                ColaboradorDTO Colaborador = context.TablaColaboradores.FindByID(idColaborador).ToDTO();
                List<ObjetivoRDTO> ObjetivosColaborador = new List<ObjetivoRDTO>();

                int puestoID = context.TablaColaboradores.FindByID(Colaborador.ID).ToDTO().PuestoID;
                if (puestoID > 0)
                {
                    Puesto puesto = context.TablaPuestos.FindByID(puestoID);
                    ObjetivosColaborador = puesto.Objetivos.Select(c => c.ToRDTO(context)).ToList();
                }

                List<ObjetivoRDTO> ObjetivosColaborador2= new List<ObjetivoRDTO>();
                foreach (ObjetivoRDTO objpadre in ObjetivosColaborador)
                {
                    ObjetivosColaborador2.AddRange(context.TablaObjetivos.Where(oo=>oo.ObjetivoPadreID==objpadre.idObjetivo).Select(ooo => ooo.ToRDTO(context)));
                }
                //List<ObjetivoRDTO> ObjetivosColaborador2 = ObjetivosColaborador.Where( o1=> o1.esPropioColaborador(o1.idpadre,context)).ToList();
                ObjetivosColaborador =ObjetivosColaborador2;

                foreach (ObjetivoRDTO o in ObjetivosColaborador)
                {
                    HistoricoBSC PeriodoPersona = ObjetivosHistoricosXPersona.Find(oh => oh.idperiodo == o.idperiodo && o.ColaboradorNombre==oh.nombreColaborador);
                    if (PeriodoPersona != null)
                    {
                        PeriodoPersona.objetivos.Add(o);
                    }
                    else
                    {
                        HistoricoBSC NuevoPeriodoPersona = new HistoricoBSC();
                        NuevoPeriodoPersona.idperiodo = o.idperiodo;
                        NuevoPeriodoPersona.nombrePeriodo = context.TablaPeriodos.One(per => per.ID == o.idperiodo).ToDTO().Nombre;
                        NuevoPeriodoPersona.nombreColaborador = Colaborador.NombreCompleto;
                        NuevoPeriodoPersona.objetivos = new List<ObjetivoRDTO>();
                        NuevoPeriodoPersona.objetivos.Add(o);
                        ObjetivosHistoricosXPersona.Add(NuevoPeriodoPersona);
                    }
                }
                return Json(ObjetivosHistoricosXPersona, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarAreas()
        {
            using (DP2Context context = new DP2Context())
            {
                List<AreaRDTO> ListaAreas = new List<AreaRDTO>();
                ListaAreas = context.TablaAreas.All().Select(a => a.ToRDTO(context)).ToList();

                return Json(ListaAreas, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarEquipo(int idJefe)
        {
            using (DP2Context context = new DP2Context())
            {

                int PuestoID2 = context.TablaColaboradores.One(c => c.ID == idJefe).ToDTO().PuestoID;
                //int PuestoID = context.TablaColaboradoresXPuestos.One(cxp => cxp.Colaborador.ID == idJefe && !cxp.FechaSalidaPuesto.HasValue).ToDTO().PuestoID;
                List<ColaboradorRDTO> ListaEquipo = new List<ColaboradorRDTO>();

                List<PuestoDTO> Puestoshijos =context.TablaPuestos.Where(p => p.PuestoSuperiorID.HasValue && p.PuestoSuperiorID == PuestoID2).Select(p=>p.ToDTO()).ToList();
                //return Json(ListaEquipo, JsonRequestBehavior.AllowGet);
                //ListaEquipo = context.TablaColaboradoresXPuestos.Where(cxp => cxp.Puesto.PuestoSuperiorID== PuestoID2 && !cxp.FechaSalidaPuesto.HasValue).Select(a => a.Colaborador.ToRDTO(context)).ToList();

                foreach (PuestoDTO phijo in Puestoshijos)
                {
                    //ColaboradorRDTO colhijo;
                    ///colhijo =
                    ListaEquipo.AddRange(context.TablaColaboradores.Where(c => c.ToDTO().PuestoID == phijo.ID).Select(p => p.ToRDTO(context)));
                }

                return Json(ListaEquipo, JsonRequestBehavior.AllowGet);
            }
        }



        //public ActionResult SeleccionXUniversidades(int idPuesto, string fecha)
        //{

        //}
        
    }
}
