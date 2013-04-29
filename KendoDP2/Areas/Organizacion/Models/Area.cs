using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class Area : DBObject
    {
        public virtual ICollection<Puesto> Puestos {get;set;}
    }
}
