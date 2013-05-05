using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class GradoAcademico: DBObject
    {
        public string Descripcion { get; set; }
        public virtual ICollection<Persona> ListaPersonas { get; set; }
   
        public GradoAcademico() {}

        public GradoAcademico(string descripcion)
        {
            Descripcion = descripcion;
        }

        public GradoAcademico(GradoAcademicoDTO g)
        {
            LoadFromDTO(g);
        }

        public GradoAcademico LoadFromDTO(GradoAcademicoDTO g)
        {
            Descripcion = g.Descripcion;
            ID = g.ID;
            return this;
        }
        
        public GradoAcademicoDTO ToDTO()
        {
            return new GradoAcademicoDTO(this);
        }
    
    }

    public class GradoAcademicoDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string Descripcion { get; set; }

        public GradoAcademicoDTO() { }

        public GradoAcademicoDTO(GradoAcademico g)
        {
            ID = g.ID;
            Descripcion = g.Descripcion;
        }
    }
    
   
}