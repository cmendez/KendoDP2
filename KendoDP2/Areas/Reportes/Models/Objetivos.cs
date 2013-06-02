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
    public class ObjetivoRDTO
    {
        public int idObjetivo { get; set; }
        public string descripcion { get; set; }
        public int numPersonas { get; set; }
        public int avance { get; set; }
        public int hijos { get; set; }

        public ObjetivoRDTO(Objetivo o)
        {

            idObjetivo = o.ID;
            descripcion = o.Nombre;
            numPersonas = 5;
            avance = 50;

        }

        public ObjetivoRDTO()
        {
            numPersonas = 10;
            avance = 50;
        }
    }

    public class ObjetivosXPersonaRDTO
    {
        public int avance { get; set; }
       
        public string nombreColaborador;

        public ObjetivosXPersonaRDTO()
        {
        }

        public ObjetivosXPersonaRDTO(int idcol)
        {
            
        }
    }

    public class BSCAvanceDTO
    {
        public double NotaFinalFinanciero { get; set; }
        public double NotaFinalAprendizaje { get; set; }
        public double NotaFinalCliente { get; set; }
        public double NotaFinalProcesosInternos { get; set; }

        public BSCAvanceDTO(BSC bsc)
        {
            //NotaFinalAprendizaje= bsc.NotaFinalAprendizaje;
            //NotaFinalCliente= bsc.NotaFinalCliente;
            //NotaFinalFinanciero = bsc.NotaFinalFinanciero;
            //NoteFinalProcesosInternos= bsc.NoteFinalProcesosInternos;

            NotaFinalAprendizaje = 50; ;
            NotaFinalCliente = 50;
            NotaFinalFinanciero = 50;
            NotaFinalProcesosInternos = 50;
            //int aaaa;
        }

    }

    public class SeleccionXUniversidadRDTO
    {

    }
}