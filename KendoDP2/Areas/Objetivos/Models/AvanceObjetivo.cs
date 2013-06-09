
﻿using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace KendoDP2.Areas.Objetivos.Models
{
    public class AvanceObjetivo: DBObject
    {
        public int Valor { get; set; }
        public string FechaCreacion {get; set;}
        
        public int CreadorID { get; set; }
        public virtual Colaborador Creador { get; set; }

        public int ObjetivoID { get; set; }
        public virtual Objetivo Objetivo { get; set; }

        public AvanceObjetivo() { }

        public AvanceObjetivo(AvanceObjetivoDTO avance)
        {
            LoadFromDTO(avance);
        }
        public AvanceObjetivo LoadFromDTO(AvanceObjetivoDTO avance)
        {
            ID = avance.ID;
            Valor = avance.Valor;
            FechaCreacion = avance.FechaCreacion;
            CreadorID = avance.CreadorID;
            ObjetivoID = avance.ObjetivoID;
            return this;
        }
    }

    public class AvanceObjetivoDTO
    {
        public int ID { get; set; }
        [Range(0, 100)]
        public int Valor { get; set; }
        public string FechaCreacion { get; set; }
        public int CreadorID { get; set; }
        public int ObjetivoID { get; set; }

        public AvanceObjetivoDTO() { }

        public AvanceObjetivoDTO(AvanceObjetivo avance)
        {
            ID = avance.ID;
            Valor = avance.Valor;
            FechaCreacion = avance.FechaCreacion;
            CreadorID = avance.CreadorID;
            ObjetivoID = avance.ObjetivoID;
        }
    }

}