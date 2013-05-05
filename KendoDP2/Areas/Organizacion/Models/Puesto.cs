﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using System.Web;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class Puesto : DBObject
    {

        //public virtual ICollection<ColaboradorXPuesto> ColaboradorPuestos { get; set; }
        public int AreaID { get; set; }
        public Area Area { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        

       // public virtual ICollection<Objetivo> Funciones { get; set; }
        
       public virtual ICollection<PuestoXArea> PuestosArea { get; set; }
       public int PuestoXAreaID { get; set; }
        
        public int? EstadosPuestoID { get; set; }
        public virtual EstadosPuesto EstadoPuesto { get; set; }

        
        public Puesto() { }

        public Puesto(string nombre, string descripcion        )
        {
            Nombre = nombre;
            Descripcion = descripcion;
            
        }

        public Puesto(PuestoDTO p)
        {
            LoadFromDTO(p);
        }

        public Puesto LoadFromDTO(PuestoDTO p)
        {
            ID = p.ID;
            Nombre = p.Nombre;
            AreaID = p.AreaID;
            return this;
        }


        public PuestoDTO ToDTO()
        {
            return new PuestoDTO(this);
        }
    
    
    }

    public class PuestoDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [DisplayName("Descripción")]
        [MaxLength(200)]
        public string Descripcion { get; set; }
        
        [Required]
        [UIHint("GridForeignKey")]
        [DisplayName("Área")]
        public int AreaID { get; set; }

        [UIHint("GridForeignKey")]
        [DisplayName("Puesto superior")]
        public int? PuestoSuperiorID { get; set; }
        

        public PuestoDTO() { }

        public PuestoDTO(Puesto p)
        {

            Nombre = p.Nombre ;
            Descripcion = p.Descripcion;
            ID = p.ID;
            AreaID = p.AreaID;


            try
            {
                PuestoXArea cruce = p.PuestosArea.OrderByDescending(a => a.ID).First();
                AreaID = cruce.Puesto.AreaID;
                //necesitamos obtener el Puesto Superior mediante un artificio
                PuestoSuperiorID = 0;
              
            }
            catch (Exception)
            {
                AreaID = 0;
                PuestoSuperiorID = 0;
                
            }


         }

    }
}
