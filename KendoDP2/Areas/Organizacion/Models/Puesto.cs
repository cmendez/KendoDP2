using System;
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
        public virtual Area Area { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? PuestoSuperiorID { get; set; }
        
        
        public virtual Puesto PuestoSuperior { get; set; }

       public virtual ICollection<PuestoXArea> PuestosArea { get; set; }
       public virtual ICollection<Area> Areas { get; set; }
       public virtual ICollection<Puesto> Puestos { get; set; }
       public virtual ICollection<Funcion> Funciones { get; set; }


       public int PuestoXAreaID { get; set; }
        
        public int? EstadosPuestoID { get; set; }
        public virtual EstadosPuesto EstadoPuesto { get; set; }


        
        public Puesto() { }

        public Puesto(string nombre, string descripcion        )
        {
            Nombre = nombre;
            Descripcion = descripcion;
            
        }
        public Puesto(PuestoDTO a)  : this()
        {
            LoadFromDTO(a);
        }

       // public Puesto(PuestoDTO p)
        //{
       //     LoadFromDTO(p);
        //}

        public Puesto LoadFromDTO(PuestoDTO p)
        {
            ID = p.ID;
            Nombre = p.Nombre;
            AreaID = p.AreaID;
            Descripcion = p.Descripcion;
            if (p.PuestoSuperiorID > 0) PuestoSuperiorID = p.PuestoSuperiorID;
            return this;
        }


        public PuestoDTO ToDTO()
        {
            return new PuestoDTO(this);
        }
        public PuestoTreeDTO ToTreeDTO()
        {
            return new PuestoTreeDTO(this);
        }

        public List<Puesto> GetAreasHijas(DP2Context context)
        {
            List<Puesto> resultado = new List<Puesto>();
            resultado.Add(this);
            foreach (Puesto p in Puestos)
            {
                resultado.AddRange(p.GetAreasHijas(context));
            }
            return resultado;
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
        
   //     [Required]
        [UIHint("GridForeignKey")]
        [DisplayName("Área")]
        public int AreaID { get; set; }

  //      [Required]
        [UIHint("GridForeignKey")]
        [DisplayName("Puesto superior")]
        public int PuestoSuperiorID { get; set; }
        

        public PuestoDTO() { }

        public PuestoDTO(Puesto p)
        {

            Nombre = p.Nombre ;
            Descripcion = p.Descripcion;
            ID = p.ID;
            AreaID = p.AreaID;
            
            PuestoSuperiorID = p.PuestoSuperiorID.GetValueOrDefault();

            if (p.PuestoSuperiorID.HasValue)
                PuestoSuperiorID = p.PuestoSuperiorID.Value;
            else PuestoSuperiorID=0;
            //PuestoSuperiorID = p.PuestoSuperiorID.Value;

            try
            {
                PuestoXArea cruce = p.PuestosArea.OrderByDescending(a => a.ID).First();
                AreaID = cruce.Puesto.AreaID;
                //necesitamos obtener el Puesto Superior mediante un artificio
               ////// PuestoSuperiorID = p.PuestoSuperiorID.Value ;
              
            }
            catch (Exception)
            {
              // AreaID = 1;
               //PuestoSuperiorID = 1;
                
            }


         }

    }
    public class PuestoTreeDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public bool hasChildren { get; set; }
        public string TreeIcon { get; set; }

        public PuestoTreeDTO() { }

        public PuestoTreeDTO(Puesto p)
        {
            id = p.ID;
            Name = p.Nombre;
            TreeIcon = "../../Images/areas_icon.png";
            hasChildren = p.Puestos.Any(i => !i.IsEliminado);
        }
    }
}
