using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KendoDP2.Areas.Personal.Models;
using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class Puesto : DBObject
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int AreaID { get; set; }
        public int? PuestoSuperiorID { get; set; }

        public Area Area { get; set; }       
        public virtual Puesto PuestoSuperior { get; set; }
        public virtual ICollection<ColaboradorXPuesto> ColaboradorPuestos { get; set; }
        
        public Puesto() { }

        public Puesto(PuestoDTO p) : this()
        {
            LoadFromDTO(p);
        }

        public Puesto LoadFromDTO(PuestoDTO p)
        {
            ID = p.ID;
            Nombre = p.Nombre;
            Descripcion = p.Descripcion;
            PuestoSuperiorID = p.PuestoSuperiorID;

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
            ID = p.ID;
            Nombre = p.Nombre;
            Descripcion = p.Descripcion;
            AreaID = p.AreaID;
            PuestoSuperiorID = p.PuestoSuperiorID;
        }
    }
}
