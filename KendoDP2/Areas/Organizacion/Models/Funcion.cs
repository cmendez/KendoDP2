using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class Funcion : DBObject
    {
        public string Nombre { get; set; }

        
        public int PuestoID { get; set; }
        public virtual Puesto Puesto { get; set; }

        public int Peso { get; set; }

        public Funcion() { }

        public Funcion(FuncionDTO f)
        {
            LoadFromDTO(f);
        }
        public Funcion LoadFromDTO(FuncionDTO f)
        {
            ID = f.ID;
            Nombre = f.Nombre;

            PuestoID = f.PuestoID;
            Peso = f.Peso;
            return this;
        }
        public FuncionDTO ToDTO()
        {
            return new FuncionDTO(this);
        }
    }

    public class FuncionDTO
    {
      
        [Required]
        [ScaffoldColumn(false)]
        public int PuestoID { get; set; }
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Range(0, 100)]
        public int Peso { get; set; }

        public FuncionDTO() { }

        public FuncionDTO(Funcion f)
        {
            ID = f.ID;
            
            Nombre = f.Nombre;
            Peso = f.Peso;
            PuestoID = f.PuestoID;
        }
    }
}