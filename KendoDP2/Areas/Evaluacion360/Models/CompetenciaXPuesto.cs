using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class CompetenciaXPuesto : DBObject
    {
        public int CompetenciaID { get; set; }
        public Competencia Competencia { get; set; }

        public int PuestoID { get; set; }
        public Puesto Puesto { get; set; }

        public int NivelID { get; set; }
        public int Peso { get; set; }
    }

    public class CompetenciaXPuestoDTO
    {
        public int ID { get; set; }

        [Required]
        [DisplayName("Competencia")]
        public int CompetenciaID { get; set; }
        
        public int PuestoID { get; set; }

        public int Peso { get; set; }

        [Required]
        [DisplayName("Nivel")]
        public int NivelID { get; set; }

        public CompetenciaXPuestoDTO() { }

        public CompetenciaXPuestoDTO(CompetenciaXPuesto x)
        {
            ID = x.ID;
            CompetenciaID = x.CompetenciaID;
            PuestoID = x.PuestoID;
            NivelID = x.NivelID;
            Peso = x.Peso;
        }

    }
}