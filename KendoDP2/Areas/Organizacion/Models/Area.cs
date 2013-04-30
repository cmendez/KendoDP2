using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class Area : DBObject
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? AreaSuperiorID { get; set; }

        public virtual Area AreaSuperior { get; set; }
        public virtual ICollection<Puesto> Puestos { get; set; }
        
        public Area() { }

        public Area(AreaDTO a) : this()
        {
            LoadFromDTO(a);
        }

        public Area LoadFromDTO(AreaDTO a)
        {
            ID = a.ID;
            Nombre = a.Nombre;
            Descripcion = a.Descripcion;
            AreaSuperiorID = a.AreaSuperiorID;

            return this;
        }

        public AreaDTO ToDTO()
        {
            return new AreaDTO(this);
        }
    }

    public class AreaDTO
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
        [DisplayName("Área superior")]
        public int? AreaSuperiorID { get; set; }

        public AreaDTO() { }

        public AreaDTO(Area a)
        {
            ID = a.ID;
            Nombre = a.Nombre;
            Descripcion = a.Descripcion;
            AreaSuperiorID = a.AreaSuperiorID;
        }

    }
}
