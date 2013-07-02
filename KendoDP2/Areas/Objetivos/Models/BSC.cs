using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Reportes.Models;
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

        public int GetPromedio(int tipoID)
        {
            if(objetivos == null) return 0;
            List<Objetivo> os = objetivos.Where(x => x.TipoObjetivoBSCID == tipoID).ToList();
            if(os.Count == 0) return 0;
            double pesos = 0;
            double res = 0;
            os.ForEach(x => { res += x.AvanceFinal * x.Peso; pesos += x.Peso; });
            if(pesos == 0) return 0;
            return (int)Math.Round(res / pesos);
        }

        public int NotaFinalFinanciero { get { return GetPromedio(1); } }
        public int NotaFinalAprendizaje { get { return GetPromedio(2); } }
        public int NotaFinalCliente { get { return GetPromedio(3); } }
        public int NoteFinalProcesosInternos { get { return GetPromedio(4); } }

        public int PeriodoID { get; set; }
        [Required]
        public virtual Periodo Periodo { get; set; }

        public BSC() { }
        public BSC(int periodo)
        {
            PeriodoID = periodo;
        }
        public BSCAvanceDTO ToRAvanceBSCDTO()
        {
            return new BSCAvanceDTO(this);
        }
        
    }
}