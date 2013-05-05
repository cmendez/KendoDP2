using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Objetivos.Models
{
    public class BSC : DBObject
    {
        public virtual ICollection<Objetivo> objetivos { get; set; }

        public double NotaFinalFinanciero { get; set; }
        public double NotaFinalAprendizaje { get; set; }
        public double NotaFinalCliente { get; set; }
        public double NoteFinalProcesosInternos { get; set; }

        public int PeriodoID { get; set; }
        [Required]
        public virtual Periodo Periodo { get; set; }

        public BSC() { }
        
    }
}