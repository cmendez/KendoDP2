using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class Area : DBObject
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? AreaSuperiorID { get; set; }


        public virtual Area AreaSuperior { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
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
            if(a.AreaSuperiorID > 0) AreaSuperiorID = a.AreaSuperiorID;

            return this;
        }

        public AreaDTO ToDTO()
        {
            return new AreaDTO(this);
        }

        public AreaTreeDTO ToTreeDTO()
        {
            return new AreaTreeDTO(this);
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

         [UIHint("GridForeignKey")]
        [DisplayName("Área superior")]
        public int AreaSuperiorID { get; set; }

        public AreaDTO() { }

        public AreaDTO(Area a)
        {
            ID = a.ID;
            Nombre = a.Nombre;
            Descripcion = a.Descripcion;
            AreaSuperiorID = a.AreaSuperiorID.GetValueOrDefault();
            
        }
    }

    public class AreaTreeDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public bool hasChildren { get; set; }
        public string TreeIcon { get; set; }

        public AreaTreeDTO() { }

        public AreaTreeDTO(Area a)
        {
            id = a.ID;
            Name = a.Nombre;
            TreeIcon = "../../Images/areas_icon.png";
            hasChildren = a.Areas.Any();
        }
    }
}
