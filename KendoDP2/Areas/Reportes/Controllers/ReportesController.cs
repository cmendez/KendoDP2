using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Reportes.Models;
using KendoDP2.Areas.Objetivos.Models;
using ExtensionMethods;

namespace KendoDP2.Areas.Reportes.Controllers
{
    public class ReportesController : WSController
    {

        //
        // GET: /Reportes/Reportes/

        public ActionResult ListarObjetivosXPadre(int PadreId)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ObjetivoRDTO> ListaObjetivos = new List<ObjetivoRDTO>();

                ObjetivoRDTO ob1 = new ObjetivoRDTO();
                ob1.idObjetivo = 1;
                ob1.descripcion = "Objetivo1";
                ListaObjetivos.Add(ob1);

                ObjetivoRDTO ob2 = new ObjetivoRDTO();
                ob2.idObjetivo = 1;
                ob2.descripcion = "Objetivo2";
                ListaObjetivos.Add(ob2);

                ObjetivoRDTO ob3 = new ObjetivoRDTO();
                ob3.idObjetivo = 3;
                ob3.descripcion = "Objetivo3";
                ListaObjetivos.Add(ob3);

                ObjetivoRDTO ob4 = new ObjetivoRDTO();
                ob4.idObjetivo = 4;
                ob4.descripcion = "Objetivo4";
                ListaObjetivos.Add(ob4);

                List<ObjetivoRDTO> ListaObjetivosR = context.TablaObjetivos.Where(o => o.ObjetivoPadreID == PadreId).Select(p => p.ToRDTO()).ToList();
                foreach (ObjetivoRDTO obj in ListaObjetivosR){
                    obj.hijos = context.TablaObjetivos.Where(o=> o.ObjetivoPadreID == obj.idObjetivo).Select(p => p.ToRDTO()).ToList().Count;
                }
                //return Json(ListaObjetivos, JsonRequestBehavior.AllowGet);
                return Json(ListaObjetivosR, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ListarObjetivos(int idperiodo)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ObjetivoRDTO> ListaObjetivos = new List<ObjetivoRDTO>();

                ObjetivoRDTO ob1 = new ObjetivoRDTO();
                ob1.idObjetivo = 1;
                ob1.descripcion = "Objetivo1";
                ListaObjetivos.Add(ob1);

                ObjetivoRDTO ob2 = new ObjetivoRDTO();
                ob2.idObjetivo = 1;
                ob2.descripcion = "Objetivo2";
                ListaObjetivos.Add(ob2);

                ObjetivoRDTO ob3 = new ObjetivoRDTO();
                ob3.idObjetivo = 3;
                ob3.descripcion = "Objetivo3";
                ListaObjetivos.Add(ob3);

                ObjetivoRDTO ob4 = new ObjetivoRDTO();
                ob4.idObjetivo = 4;
                ob4.descripcion = "Objetivo4";
                ListaObjetivos.Add(ob4);

                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, Creador = TablaColaboradores.FindByID(1) });
                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 2", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, Creador= TablaColaboradores.FindByID(1) });
                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1.1", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, ObjetivoPadreID = 1, Creador = TablaColaboradores.FindByID(1) });
                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1.2", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, ObjetivoPadreID = 1, Creador = TablaColaboradores.FindByID(1) });


                return Json(ListaObjetivos, JsonRequestBehavior.AllowGet);
                //return Json(ob.ToDTO(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarObjetivosXBSC(int BSCId, int idperiodo)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ObjetivoRDTO> ListaObjetivos = new List<ObjetivoRDTO>();

                ObjetivoRDTO ob1 = new ObjetivoRDTO();
                ob1.idObjetivo = 1;
                ob1.descripcion = "Objetivo1";
                ListaObjetivos.Add(ob1);

                ObjetivoRDTO ob2 = new ObjetivoRDTO();
                ob2.idObjetivo = 1;
                ob2.descripcion = "Objetivo2";
                ListaObjetivos.Add(ob2);

                ObjetivoRDTO ob3 = new ObjetivoRDTO();
                ob3.idObjetivo = 3;
                ob3.descripcion = "Objetivo3";
                ListaObjetivos.Add(ob3);

                ObjetivoRDTO ob4 = new ObjetivoRDTO();
                ob4.idObjetivo = 4;
                ob4.descripcion = "Objetivo4";
                ob4.descripcion = "Objetivo4";
                ListaObjetivos.Add(ob4);

                List<ObjetivoRDTO> ListaObjetivos2 = new List<ObjetivoRDTO>();
                ListaObjetivos2 = context.TablaObjetivos.Where(o => o.TipoObjetivoBSCID == BSCId && o.BSCID == idperiodo).Select(p => p.ToRDTO()).ToList();

                return Json(ListaObjetivos2, JsonRequestBehavior.AllowGet);
                //return Json(ListaObjetivos, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarObjetivosXPadre2(int PadreId)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaObjetivos.Where(o => o.ObjetivoPadreID == PadreId).Select(p => p.ToRDTO()).ToList(), JsonRequestBehavior.AllowGet);
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
        public ActionResult ObjetivosxPersona(int idObjetivo)
        {
            using (DP2Context context = new DP2Context())
            {

                //int idpuesto=context.TablaObjetivos.One(d => d.ID.Equals(idObjetivo)).PuestoAsignadoID.Value;
                
                //List<ColaboradorXPuestoDTO> ColaboradoresXPuesto =context.TablaColaboradoresXPuestos.Where( p => p.PuestoID==idpuesto).Select (p => p.ToDTO()).ToList();

                List<ObjetivosXPersonaRDTO> ListaObjetivosXPersona = new List<ObjetivosXPersonaRDTO>();

                //foreach (ColaboradorXPuestoDTO ColXPues in ColaboradoresXPuesto)
                //{
                //    ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { ColaboradorDTO =ColXPues.ColaboradorDTO, avance = 50});
                //}
                List<ColaboradorDTO> Colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                
                //List<ColaboradorXPuestoDTO> ColaboradoresXPuesto = context.TablaColaboradoresXPuestos.Where(p => p.PuestoID == idpuesto).Select(p => p.ToDTO()).ToList();
                //ColaboradorDTO col = context.TablaColaboradores.FindByID(1).ToDTO();

                ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[10].NombreCompleto, avance = 50 });
                ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[9].NombreCompleto, avance = 50 });
                ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[11].NombreCompleto, avance = 50 });
                ListaObjetivosXPersona.Add(new ObjetivosXPersonaRDTO { nombreColaborador = Colaboradores[8].NombreCompleto, avance = 50 });

                return Json(ListaObjetivosXPersona, JsonRequestBehavior.AllowGet); 
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

        //public ActionResult PostulacionySeleccion(int idpuesto)
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        List<BSCAvanceDTO> ListaAvanceBSC = context.TablaOfertaLaborales.Where()Select(p => p.ToRAvanceBSCDTO()).ToList();
        //        return Json(ListaAvanceBSC, JsonRequestBehavior.AllowGet);
        //    }
        //}
        
        //public ActionResult SeleccionXUniversidades(int idPuesto, string fecha)
        //{

        //}
        
    }
}
