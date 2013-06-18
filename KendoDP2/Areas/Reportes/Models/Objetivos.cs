using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Objetivos.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoDP2.Areas.Reportes.Models
{
    public class ObjetivoConPadreDTO {
        public int ID { get; set; }

        public string Nombre { get; set; }
        public int Peso { get; set; }
        public int AvanceFinal { get; set; }
        public int TipoObjetivoBSCID { get; set; }
        public int ObjetivoPadreID { get; set; }
        public bool padreEsIntermedio { get; set; }
        public ObjetivoDTO ObjetivoPadreDTO { get; set; }
        public int  iddueño { get; set; }
        public ColaboradorDTO dueño { get; set; }
        public int BSCID { get; set; }
        public int puestoID { get; set; }
        

        public string FechaCreacion { get; set; }
        public string FechaFinalizacion { get; set; }

        public ObjetivoConPadreDTO(Objetivo o, DP2Context context)
        {
            ID = o.ID;
            Nombre = o.Nombre;
            Peso = o.Peso;
            AvanceFinal = o.AvanceFinal;
            TipoObjetivoBSCID = o.TipoObjetivoBSCID.GetValueOrDefault();
            if (o.ObjetivoPadreID != 0)
            {
                Objetivo padre = context.TablaObjetivos.FindByID(o.ObjetivoPadreID);
                padreEsIntermedio = padre.IsObjetivoIntermedio;
                ObjetivoPadreDTO = padre.ToDTO(context);
            }
            else
            {
                padreEsIntermedio = false;
                ObjetivoPadreDTO = null;
            }
            ObjetivoPadreID = o.ObjetivoPadreID;

            puestoID = o.PuestoAsignadoID.GetValueOrDefault();

            if (o.Dueño != null)
            {
                dueño = o.Dueño.ToDTO();
            }

            BSCID = o.GetBSCIDRaiz(context);

            //PeriodoID = o.PeriodoID;

        }
    
    }
    public class ObjetivoRDTO
    {
        public int idObjetivo { get; set; }
        public string descripcion { get; set; }
        public int numPersonas { get; set; }
        public int avance { get; set; }
        public int hijos { get; set; }
        public int peso { get; set; }
        public bool esIntermedio { get; set; }
        public int idPuesto { get; set; }
        public int idpadre { get; set; }
        public int idperiodo { get; set; }
        public int BSCId { get; set; }
        public int ColaboradorID { get; set; }
        public string ColaboradorNombre { get; set; }

        public ObjetivoRDTO(Objetivo o,DP2Context context)
        {

            idObjetivo = o.ID;
            descripcion = o.Nombre;

            List<ColaboradorDTO> ListaCOlaboradores= context.TablaColaboradores.All().Select(col => col.ToDTO()).ToList();
            foreach (ColaboradorDTO col in ListaCOlaboradores)
            {
                numPersonas = 0;
                foreach (ObjetivoDTO obj in col.Objetivos)
                {
                    if (obj.ObjetivoPadreID!=0 &&obj.ObjetivoPadreID == o.ID) numPersonas += 1; 
                }
            }
            peso = o.Peso;
            hijos = o.ObjetivosHijos(context).Count;            
            avance = o.AvanceFinal;
            esIntermedio = o.IsObjetivoIntermedio;
            if (o.ObjetivoPadreID != 0)
            {
                idpadre = o.ObjetivoPadreID;
            }
            else
            {
                idpadre = -1;
            }

            Objetivo ob = o;
            while (ob.ObjetivoPadreID!=0 && ob.ObjetivoPadreID > 0)
            {
                ob = context.TablaObjetivos.FindByID(ob.ObjetivoPadreID);
            }
            BSCId = ob.TipoObjetivoBSCID.Value;

            idperiodo = o.GetBSCIDRaiz(context);

            if (o.PuestoAsignadoID != null)
            {
                idPuesto = o.PuestoAsignadoID.Value;
                List<ColaboradorXPuesto> cxpaux = context.TablaColaboradoresXPuestos.Where(cxp => cxp.Puesto.ID == idPuesto && (cxp.FechaSalidaPuesto == null || DateTime.Today <= cxp.FechaSalidaPuesto));
                if (cxpaux.Count > 0)
                {

                    List<Colaborador> cdtoaux = cxpaux.Select(p => p.Colaborador).ToList();

                    ColaboradorDTO cdto = cdtoaux.Select(c => c.ToDTO()).ToList()[cdtoaux.Count - 1];

                    ColaboradorID = cdto.ID;
                    ColaboradorNombre = cdto.NombreCompleto;
                }
            }
            else
            {
                if (o.ObjetivoPadreID !=0 && context.TablaObjetivos.FindByID(o.ObjetivoPadreID).PuestoAsignadoID != null)
                {
                    idPuesto = context.TablaObjetivos.FindByID(o.ObjetivoPadreID).PuestoAsignado.ID;
                    List<ColaboradorXPuesto> cxpaux = context.TablaColaboradoresXPuestos.Where(cxp => cxp.Puesto.ID == idPuesto && (cxp.FechaSalidaPuesto == null || DateTime.Today <= cxp.FechaSalidaPuesto));
                    if (cxpaux.Count > 0)
                    {

                        List<Colaborador> cdtoaux = cxpaux.Select(p => p.Colaborador).ToList();

                        ColaboradorDTO cdto = cdtoaux.Select(c => c.ToDTO()).ToList()[cdtoaux.Count - 1];

                        ColaboradorID = cdto.ID;
                        ColaboradorNombre = cdto.NombreCompleto;
                    }
                }
                else
                {
                    if (o.ObjetivoPadreID==0)
                    {
                        idPuesto=1;
                    }
                    else
                    {
                        var padre = context.TablaObjetivos.FindByID(o.ObjetivoPadreID);
                        if (padre.ObjetivoPadreID != 0)
                        {
                            var abuelo = context.TablaObjetivos.FindByID(padre.ObjetivoPadreID);
                            idPuesto = abuelo.PuestoAsignadoID.Value;
                        }
                    }
                }
                
            }
            //if (o.Dueño != null)
            //{
            //    idPuesto=context.
            //}

        }

        public ObjetivoRDTO()
        {
            numPersonas = 10;
            avance = 50;
        }
        public bool esPropioColaborador(int idpadre, DP2Context context){
            if (idpadre == -1) return true;
            ObjetivoDTO padre=context.TablaObjetivos.FindByID(idpadre).ToDTO(context);
            ObjetivoRDTO abuelo= context.TablaObjetivos.FindByID(padre.ObjetivoPadreID).ToRDTO(context);
            return abuelo.esIntermedio;
        }
    }

    public class PersonaXObjetivoDTO
    {
        public int avance { get; set; }
       
        public string nombreColaborador { get; set; }

        public int idObjetivo { get; set; }

        public List<ObjetivoRDTO> objetivos { get; set; }
    }

    public class HistoricoBSC
    {
        public int idperiodo { get; set; }

        public string nombrePeriodo { get; set; }

        public string nombreColaborador { get; set; }

        public List<ObjetivoRDTO> objetivos { get; set; }
    }

    public class BSCAvanceDTO
    {
        public double NotaFinalFinanciero { get; set; }
        public double NotaFinalAprendizaje { get; set; }
        public double NotaFinalCliente { get; set; }
        public double NotaFinalProcesosInternos { get; set; }

        public BSCAvanceDTO(BSC bsc)
        {
            NotaFinalAprendizaje= bsc.NotaFinalAprendizaje;
            NotaFinalCliente= bsc.NotaFinalCliente;
            NotaFinalFinanciero = bsc.NotaFinalFinanciero;
            NotaFinalProcesosInternos= bsc.NoteFinalProcesosInternos;

            //int aaaa;
        }

    }



    public class ColaboradorRDTO
    {
        public int idColaborador { get; set; }
        public string nombreColaborador { get; set; }
        public string puesto { get; set; }
        //public  List<ColaboradorRDTO> Subordinados;
        
        public ColaboradorRDTO(Colaborador c,DP2Context context)
        {
            idColaborador = c.ID;
            nombreColaborador = c.Nombres + " "+c.ApellidoPaterno+" "+c.ApellidoMaterno;
            puesto = context.TablaColaboradoresXPuestos.One(cxp => cxp.ColaboradorID == c.ID && cxp.FechaSalidaPuesto == null).Puesto.Nombre;
            //PuestosHijos = context.TablaPuestos.Where(p=> p.PuestoSuperiorID==a.ID).Select(p=>p.ToRDTO(context)).ToList();
        }
    }

    public class PuestoRDTO
    {
        public int idPuesto { get; set; }
        public string nombrePuesto { get; set; }
        //public List<PuestoRDTO> PuestosHijos { get; set; }

        public PuestoRDTO(Puesto a,DP2Context context)
        {
            idPuesto = a.ID;
            nombrePuesto = a.Nombre;
           //  PuestosHijos = context.TablaPuestos.Where(p=> p.PuestoSuperiorID==a.ID).Select(p=>p.ToRDTO(context)).ToList();
        }

    }

    public class AreaRDTO
    {
        public int idArea { get; set; }
        public string nombreArea { get; set; }
        public List<PuestoRDTO> Puestos { get; set; }

        public AreaRDTO(Area a,DP2Context context)
        {
            idArea = a.ID;
            nombreArea = a.Nombre;
            Puestos = context.TablaPuestos.Where(p => p.AreaID == a.ID).Select(p=> p.ToRDTO(context)).ToList();
        }
    }
}