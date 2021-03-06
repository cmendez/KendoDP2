﻿using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Objetivos.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Configuracion.Models
{
    public class Periodo : DBObject
    {
        public string Nombre { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public int? BSCID { get; set; }
        public virtual BSC BSC { get; set; }

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
            BSCID = periodo.BSCID;
            return this;
        }
        public PeriodoDTO ToDTO()
        {
            return new PeriodoDTO(this);
        }

        public static Periodo GetUltimoPeriodo(DP2Context context){
            return context.TablaPeriodos.All().OrderByDescending(x => x.FechaInicio).First();
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

        [DisplayName("Fecha de fin")]
        [ScaffoldColumn(false)]
        public string FechaFinDisplay { get; set; }

        [ScaffoldColumn(false)]
        public int BSCID { get; set; }

        public PeriodoDTO() { }
        public PeriodoDTO(Periodo p)
        {
            ID = p.ID;
            Nombre = p.Nombre;
            FechaInicio = p.FechaInicio;
            FechaFin = p.FechaFin;
            FechaFinDisplay = FechaFin == null ? "Activo" : FechaFin.GetValueOrDefault().ToString("dd/MM/yyyy");
            BSCID = p.BSC == null ? 0 : p.BSC.ID;
        }

    }
}