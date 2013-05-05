using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class PuestoXArea: DBObject
    {
        public int PuestoID { get; set; }
        public int AreaID { get; set; }
        public string  Descripcion { get; set; }
        public DateTime? FechaRegistroPuesto { get; set; }
      
        public string Comentarios { get; set; }

        public virtual Area Area { get; set; }
        public virtual Puesto Puesto { get; set; }



    }
}