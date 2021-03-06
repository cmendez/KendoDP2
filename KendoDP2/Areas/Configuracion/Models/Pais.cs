﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;


namespace KendoDP2.Areas.Configuracion.Models
{
    public class Pais : DBObject
    {
        public string Nombre { get; set; }
        public virtual List<Colaborador> Colaboradores { get; set; }

        public Pais() { }

        public Pais(string nombre)
        {
            Nombre = nombre;
        }

        public Pais(PaisDTO p)
        {
            LoadFromDTO(p);
        }

        public Pais LoadFromDTO(PaisDTO p)
        {
            Nombre = p.Nombre;
            ID = p.ID;
            return this;
        }
        
        public PaisDTO ToDTO()
        {
            return new PaisDTO(this);
        }
                      
    }

    public class PaisDTO
    {
        
        [ScaffoldColumn(false)]
        public int ID { get; set; }


        public string Nombre { get; set; }

        public PaisDTO() { }

        public PaisDTO(Pais p)
        {
            ID = p.ID;
            Nombre = p.Nombre;
        }
    
    }
}