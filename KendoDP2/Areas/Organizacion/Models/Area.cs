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
        public bool IsAudit { get; set; }

        public int ColorID { get; set; }

        [ForeignKey("ColorID")]
        public virtual AColor AColor { get; set; }
        
        public int? AreaSuperiorID { get; set; }
        [ForeignKey("AreaSuperiorID")]
        public virtual Area AreaSuperior { get; set; }
        
        [InverseProperty("AreaSuperior")]
        public virtual ICollection<Area> Areas { get; set; }
        [InverseProperty("Area")]
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
            ColorID = a.ColorID;

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

        public List<Area> GetAreasHijas(DP2Context context)
        {
            List<Area> resultado = new List<Area>();
            resultado.Add(this);
            foreach (Area a in Areas)
            {
                resultado.AddRange(a.GetAreasHijas(context));
            }
            return resultado;
        }
    }

    public class AreaDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [UIHint("GridForeignKey")]
        [DisplayName("Color")]
        public int ColorID { get; set; }

        [DisplayName("Es auditoría")]
        public bool IsAudit { get; set; }

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
            ColorID = a.ColorID;
            
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
            hasChildren = a.Areas.Any(i => !i.IsEliminado);
        }
    }
}
