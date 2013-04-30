﻿using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class Competencia : DBObject
    {
        public string Nombre { get; set; }
        public int NroDeNiveles { get; set; }
        public virtual ICollection<Capacidad> Capacidades { get; set; }

        public Competencia() { }

        public Competencia(string nombre, int nroDeNiveles)
        {
            Nombre = nombre;
            NroDeNiveles = nroDeNiveles;
        }

        public Competencia(string nombre)
        {
            Nombre = nombre;
        }

        public Competencia(CompetenciaDTO dto)
        {
            LoadFromDTO(dto);
        }

        public Competencia LoadFromDTO(CompetenciaDTO dto)
        {
            ID = dto.ID;
            Nombre = dto.Nombre;
            NroDeNiveles = 3; //Por defecto siempre es 3
            return this;
        }

        public CompetenciaDTO ToDTO()
        {
            return new CompetenciaDTO(this);
        }
    }

    public class CompetenciaDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.Text, ErrorMessage = "Solo se admite caracteres")]
        public string Nombre { get; set; }
        
        public CompetenciaDTO(Competencia c)
        {
            Nombre = c.Nombre;
            ID = c.ID;
        }
        public CompetenciaDTO() { }
    }

    public class CompetenciaConTotalNivelesDTO
    { 
        public string Nombre { get; set; }
        public int TotalNiveles { get; set; }
        public int ID { get; set; }

        public CompetenciaConTotalNivelesDTO(Competencia c)
        {
            TotalNiveles = c.NroDeNiveles;
            Nombre = c.Nombre;
            ID = c.ID;
        }
        public CompetenciaConTotalNivelesDTO() { }        
    }

}