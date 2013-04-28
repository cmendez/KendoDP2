using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Personal.Models
{
    public class TipoDocumento: DBObject
    {
        public string Descripcion { get; set; }
        public virtual ICollection<Persona> ListaPersonas { get; set; }

        public TipoDocumento() { }

        public TipoDocumento(string descripcion)
        {
            Descripcion = descripcion;
        }
    }
}