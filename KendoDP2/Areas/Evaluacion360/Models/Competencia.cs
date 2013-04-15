using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class Competencia : DBObject
    {
        public string Nombre { get; set; }

        public Competencia() { }
        
        public Competencia(CompetenciaDTO dto)
        {
            LoadFromDTO(dto);
        }

        public Competencia LoadFromDTO(CompetenciaDTO dto)
        {
            ID = dto.ID;
            Nombre = dto.Nombre;
            return this;
        }

        public CompetenciaDTO ToDTO()
        {
            return new CompetenciaDTO(this);
        }
    }

    public class CompetenciaDTO
    {
        public string Nombre { get; set; }
        public int ID { get; set; }

        public CompetenciaDTO(Competencia c)
        {
            Nombre = c.Nombre;
            ID = c.ID;
        }
        public CompetenciaDTO() { }
    }

}