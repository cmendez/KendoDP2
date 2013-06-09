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
        public bool tienepadre { get; set; }
        public int BSCId { get; set; }

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
            tienepadre = o.ObjetivoPadreID.Value>0;
            BSCId = o.BSCID.Value;
            if (o.PuestoAsignado != null)
            {
                idPuesto = o.PuestoAsignado.ID;
            }
            else
            {
                idPuesto = -1;
            }
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