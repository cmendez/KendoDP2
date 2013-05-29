using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Eventos.Models
{
    public class EstadoEvento : DBObject
    {
        public string Descripcion { get; set; }

        public virtual ICollection<Evento> EventosConEstado { get; set; }
    }
}