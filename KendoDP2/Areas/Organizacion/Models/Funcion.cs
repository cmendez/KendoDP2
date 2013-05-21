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
    public class Funcion : DBObject
    {
        public string Descripcion { get; set; }
        public int PuestoID { get; set; }
        public virtual Puesto Puesto { get; set; }
        
        public Funcion() { }

        public Funcion(FuncionDTO f) : this()
        {
            LoadFromDTO(f);
        }

        public Funcion LoadFromDTO(FuncionDTO f)
        {
            ID = f.ID;
            Descripcion = f.Descripcion;
            PuestoID = f.PuestoID;

            return this;
        }

        public FuncionDTO ToDTO()
        {
            return new FuncionDTO(this);
        }
    }

    public class FuncionDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
        [Required]
        [DisplayName("Descripción")]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [UIHint("GridForeignKey")]
        [DisplayName("Puesto de trabajo")]
        public int PuestoID { get; set; }

        public FuncionDTO() { }

        public FuncionDTO(Funcion f)
        {
            ID = f.ID;
            Descripcion = f.Descripcion;
            PuestoID = f.PuestoID;
            
        }
    }
}
