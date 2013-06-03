using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class Capacidad : DBObject
    {
        public string Nombre { get; set; }

        public int NivelCapacidadID { get; set; }
        public virtual NivelCapacidad NivelCapacidad { get; set; }

        public int CompetenciaID { get; set; }
        public virtual Competencia Competencia { get; set; }

        public int Peso { get; set; }

        public Capacidad() { }

        public Capacidad(string nombre, int nivelCapacidadId, int competenciaId, int peso) 
        {
            Nombre = nombre;
            NivelCapacidadID = nivelCapacidadId;
            CompetenciaID = competenciaId;
            Peso = peso;
        }

        public Capacidad(CapacidadDTO c)
        {
            LoadFromDTO(c);
        }
        public Capacidad LoadFromDTO(CapacidadDTO c){
            ID = c.ID;
            Nombre = c.Nombre;
            NivelCapacidadID = c.NivelCapacidadID;
            CompetenciaID = c.CompetenciaID;
            Peso = c.Peso;
            return this;
        }
        public CapacidadDTO ToDTO()
        {
            return new CapacidadDTO(this);
        }
    }   

    public class CapacidadDTO
    {
        [Required]
        [ScaffoldColumn(false)]
        public int NivelCapacidadID { get; set; }
        [Required]
        [ScaffoldColumn(false)]
        public int CompetenciaID { get; set; }
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Range(0,100)]
        public int Peso { get; set; }

        public CapacidadDTO() { }
        
        public CapacidadDTO(Capacidad c)
        {
            ID = c.ID;
            NivelCapacidadID = c.NivelCapacidadID;
            Nombre = c.Nombre;
            Peso = c.Peso;
            CompetenciaID = c.CompetenciaID;
        }
    }
}