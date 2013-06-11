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
            if (o.ObjetivoPadre != null)
            {
                padreEsIntermedio = o.ObjetivoPadre.IsObjetivoIntermedio;
                ObjetivoPadreDTO = o.ObjetivoPadre.ToDTO(context);
            }
            else
            {
                padreEsIntermedio = false;
                ObjetivoPadreDTO = null;
            }
            ObjetivoPadreID = o.ObjetivoPadreID.GetValueOrDefault();

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
                    if (obj.ObjetivoPadreID == o.ID) numPersonas += 1; 
                }
            }
            peso = o.Peso;
            hijos = o.ObjetivosHijos.Count;            
            avance = o.AvanceFinal;
            esIntermedio = o.IsObjetivoIntermedio;
            if (o.ObjetivoPadreID != null)
            {
                idpadre = o.ObjetivoPadreID.Value;
            }
            else
            {
                idpadre = -1;
            }

            Objetivo ob = o;
            while (ob.ObjetivoPadreID!=null && ob.ObjetivoPadreID.GetValueOrDefault() > 0)
            {
                ob = context.TablaObjetivos.FindByID(ob.ObjetivoPadreID.GetValueOrDefault());
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
                if (o.ObjetivoPadre!=null && o.ObjetivoPadre.PuestoAsignadoID != null)
                {
                    idPuesto = o.ObjetivoPadre.PuestoAsignado.ID;
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
                    if (o.ObjetivoPadre==null)
                    {
                        idPuesto=-1;
                    }
                    else
                    {
                        if (o.ObjetivoPadre.ObjetivoPadre!=null)
                        idPuesto = o.ObjetivoPadre.ObjetivoPadre.PuestoAsignadoID.Value;
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
    }

    public class PersonaXObjetivoDTO
    {
        public int avance { get; set; }
       
        public string nombreColaborador { get; set; }

        public int idObjetivo { get; set; }

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

    public class SeleccionXUniversidadRDTO
    {

    }
}