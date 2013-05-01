using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class Puesto : DBObject
    {

        //public virtual ICollection<ColaboradorXPuesto> ColaboradorPuestos { get; set; }
        public int AreaID { get; set; }
        public Area Area { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        

        //public virtual ICollection<Objetivo> Objetivos { get; set; }
        
      //  public virtual ICollection<ColaboradorXPuesto> ColaboradoresPuesto { get; set; }
      //  public int ColaboradorXPuestoID { get; set; }
        
        public int? EstadosColaboradorID { get; set; }
        //public virtual EstadosColaborador EstadoColaborador { get; set; }

        
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
        //[UIHint("GridForeignKey")]
        [DisplayName("Área")]
        public int AreaID { get; set; }

        //[UIHint("GridForeignKey")]
        [DisplayName("Puesto superior")]
        public int? PuestoSuperiorID { get; set; }
        

        public PuestoDTO() { }

        public PuestoDTO(Puesto p)
        {

            Nombre = p.Nombre ;
            Descripcion = p.Descripcion;
            ID = p.ID;
     
            try {
            //    ColaboradorXPuesto cruce = c.ColaboradoresPuesto.OrderByDescending(a => a.ID).First();
             //   AreaID = cruce.Puesto.AreaID;
               
            } catch(Exception){
              //  AreaID = 0;
               
            }


         }

    }
}
