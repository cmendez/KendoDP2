using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Configuracion.Models
{
    public class Periodo : DBObject
    {
        public string Nombre { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public Periodo() { }
        public Periodo(string nombre, DateTime fechaInicio)
        {
            Nombre = nombre;
            FechaInicio = fechaInicio;
            FechaFin = null;
        }
        public Periodo(PeriodoDTO periodo){
            LoadFromDTO(periodo);
        }
        public Periodo LoadFromDTO(PeriodoDTO periodo)
        {
            FechaInicio = periodo.FechaInicio;
            FechaFin = periodo.FechaFin;
            Nombre = periodo.Nombre;
            return this;
        }
        public PeriodoDTO ToDTO()
        {
            return new PeriodoDTO(this);
        }
    }

    public class PeriodoDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        public string Nombre { get; set; }
        
        [DisplayName("Fecha de inicio")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInicio { get; set; }
        
        [DisplayName("Fecha de fin")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaFin { get; set; }

        public PeriodoDTO() { }
        public PeriodoDTO(Periodo p)
        {
            ID = p.ID;
            Nombre = p.Nombre;
            FechaInicio = p.FechaInicio;
            FechaFin = p.FechaFin;
        }
    }
}