using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Personal.Models
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
    
    }

   
}