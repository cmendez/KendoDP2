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

                List<ObjetivoRDTO> ListaObjetivos2 = new List<ObjetivoRDTO>();
                List<ObjetivoDTO> ListaObjetivos3 = new List<ObjetivoDTO>();

                ListaObjetivos3 = context.TablaObjetivos.All().Select(o => o.ToDTO(context)).ToList();
                ListaObjetivos2 = context.TablaObjetivos.Where(o => o.TipoObjetivoBSCID == BSCId && o.BSCID == idperiodo && (o.ObjetivoPadreID == null || o.ObjetivoPadreID < 0)).Select(p => p.ToRDTO(context)).ToList();
                foreach (ObjetivoRDTO obj in ListaObjetivos2)
                {
                    obj.hijos = context.TablaObjetivos.Where(o => o.ObjetivoPadreID == obj.idObjetivo).ToList().Count;

                }
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
                            ObjetivoRDTO unObjetivoNieto=context.TablaObjetivos.Where(o => o.ObjetivoPadreID ==objhijo.idObjetivo).Select(p => p.ToRDTO(context)).ToList()[0];
                            objhijo.descripcion=unObjetivoNieto.descripcion;
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


        

        

        public ActionResult ListarObjetivosXPadre2(int PadreId)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaObjetivos.Where(o => o.ObjetivoPadreID == PadreId).Select(p => p.ToRDTO(context)).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult ListarObjetivosXPadre(int PadreId)
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        List<ObjetivoRDTO> ListaObjetivos = new List<ObjetivoRDTO>();

        //        ObjetivoRDTO ob1 = new ObjetivoRDTO();
        //        ob1.idObjetivo = 1;
        //        ob1.descripcion = "Objetivo1";
        //        ListaObjetivos.Add(ob1);

        //        ObjetivoRDTO ob2 = new ObjetivoRDTO();
        //        ob2.idObjetivo = 1;
        //        ob2.descripcion = "Objetivo2";
        //        ListaObjetivos.Add(ob2);

        //        ObjetivoRDTO ob3 = new ObjetivoRDTO();
        //        ob3.idObjetivo = 3;
        //        ob3.descripcion = "Objetivo3";
        //        ListaObjetivos.Add(ob3);

        //        ObjetivoRDTO ob4 = new ObjetivoRDTO();
        //        ob4.idObjetivo = 4;
        //        ob4.descripcion = "Objetivo4";
        //        ListaObjetivos.Add(ob4);

        //        //return Json(ListaObjetivos, JsonRequestBehavior.AllowGet);
        //        return Json(context.TablaObjetivos.Where(o => o.ObjetivoPadreID == PadreId).Select(p => p.ToRDTO()).ToList(), JsonRequestBehavior.AllowGet);
        //    }
        //}
        public ActionResult ListaObjetivos()
        {
            using (DP2Context context = new DP2Context())
            {
            return Json(context.TablaObjetivos.All().Select(o=>o.ObjetivoConPadreDTO(context)).ToList(), JsonRequestBehavior.AllowGet); 
            }
        }
        public ActionResult ObjetivosxPersona(int idObjetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                //Buscaremos el objetivo padre para ver si es un objetivo intermedio
                List<ObjetivoConPadreDTO> listaObjAux = context.TablaObjetivos.Where(p => p.ObjetivoPadreID == idObjetivo).Select(p => p.ObjetivoConPadreDTO(context)).ToList();
                List<ObjetivosXPersonaRDTO> ListaReporteAvanceXPersona = new List<ObjetivosXPersonaRDTO>();
                if (listaObjAux.Count>0 && listaObjAux[0].padreEsIntermedio)
                {
                    //Se mostrarán a todos los colaboradores que tengan objetivos con padre igual al intermedio encontrado
                    List<ColaboradorDTO> Colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                    foreach (ColaboradorDTO col in Colaboradores)
                    {
                        if (col.Objetivos.Find(obj => obj.ObjetivoPadreID == listaObjAux[0].ObjetivoPadreID)!=null)
                        {
                            ObjetivosXPersonaRDTO perXObj = new ObjetivosXPersonaRDTO();
                            perXObj.nombreColaborador = col.NombreCompleto;
                            perXObj.avance = col.Objetivos.Find(obj => obj.ObjetivoPadreID == listaObjAux[0].ObjetivoPadreID).AvanceFinal;
                        }
                    }
                }
                #region porProbar
                
                //Considerando que los ob
                
                //List<ObjetivosXPersonaRDTO> ListaReporteAvanceXPersona = new List<ObjetivosXPersonaRDTO>();
                //List<ColaboradorDTO> ColabObjetivos = new List<ColaboradorDTO>();
                //List<ObjetivoDTO> ObjetivosXPersona = new List<ObjetivoDTO>();
                //foreach (ColaboradorDTO Colaborador in Colaboradores)
                //{
                //    foreach (ObjetivoDTO ob in Colaborador.Objetivos)
                //    {
                        
                //        if (ob.ObjetivoPadreID == idObjetivo)
                //        {
                //            ObjetivosXPersona.Add(ob);
                //            if (ColabObjetivos.Find(c => c.NombreCompleto==Colaborador.NombreCompleto)==null)
                //            {
                //                ColabObjetivos.Add(Colaborador);
                //            }
                //        }
                //    }
                //}
                //foreach (ColaboradorDTO colab in ColabObjetivos)
                //{
                //    int avance = 0;
                //    int totalobj = 0;
                //    foreach (ObjetivoDTO obj in colab.Objetivos){
                //        if (obj.ObjetivoPadreID == idObjetivo)
                //        {
                //            avance += obj.AvanceFinal;
                //            totalobj+=1;
                //        }
                //    }
                //    ObjetivosXPersonaRDTO objxcolrep = new ObjetivosXPersonaRDTO();
                //    objxcolrep.nombreColaborador = colab.NombreCompleto;
                //    objxcolrep.avance = avance / totalobj;
                //    ListaReporteAvanceXPersona.Add(objxcolrep);
                //}

                
#endregion

                return Json(ListaReporteAvanceXPersona, JsonRequestBehavior.AllowGet); 
                //int idpuesto=context.TablaObjetivos.One(d => d.ID.Equals(idObjetivo)).PuestoAsignadoID.Value;
                
                //List<ColaboradorXPuestoDTO> ColaboradoresXPuesto =context.TablaColaboradoresXPuestos.Where( p => p.PuestoID==idpuesto).Select (p => p.ToDTO()).ToList();

                

                //foreach (ColaboradorXPuestoDTO ColXPues in ColaboradoresXPuesto)
                //{
                //    ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { ColaboradorDTO =ColXPues.ColaboradorDTO, avance = 50});
                //}
                //List<ColaboradorDTO> Colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                
                //List<ColaboradorXPuestoDTO> ColaboradoresXPuesto = context.TablaColaboradoresXPuestos.Where(p => p.PuestoID == idpuesto).Select(p => p.ToDTO()).ToList();
                //ColaboradorDTO col = context.TablaColaboradores.FindByID(1).ToDTO();

                //ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[10].NombreCompleto, avance = 50 });
                //ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[9].NombreCompleto, avance = 50 });
                //ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[11].NombreCompleto, avance = 50 });
                //ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[8].NombreCompleto, avance = 50 });

                //return Json(ListaObjetivosXPersona, JsonRequestBehavior.AllowGet); 
            }

        }
        //public ActionResult ListarPuestos()
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        List<PuestoDTO> ListaPuestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
        //        return Json(ListaPuestos, JsonRequestBehavior.AllowGet);
        //    }
        //}

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

        public ActionResult PostulacionySeleccion(int idpuesto)
        {
            using (DP2Context context = new DP2Context())
            {
                //List<OfertaLaboralDTO> ListaOfertas = context.TablaOfertaLaborales.Where(p => p.PuestoID==idpuesto).Select(p => p.Postulantes).ToList();
                //List<OfertaLaboralXPostulante> ListaOfertas = context.TablaOfertaLaborales.Where(p => p.PuestoID==idpuesto).Select(p => p.Postulantes);
                List<PostulanteDTO> ListaPostulantes =context.TablaOfertaLaboralXPostulante.Where(p => p.OfertaLaboral.PuestoID==idpuesto).Select(p=>p.Postulante.ToDTO()).ToList();
                List<ROfertasLaborales> ListaROfertas = new  List<ROfertasLaborales>() ;
                
                foreach (PostulanteDTO pos in ListaPostulantes)
                {
                    ROfertasLaborales prov = ListaROfertas.Find(p => p.nombreProveniencia == pos.CentroEstudios);

                    if (prov == null)
                    {
                        prov = new ROfertasLaborales();
                        prov.nombreProveniencia = pos.CentroEstudios;
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
        
        //public ActionResult SeleccionXUniversidades(int idPuesto, string fecha)
        //{

        //}
        
    }
}
