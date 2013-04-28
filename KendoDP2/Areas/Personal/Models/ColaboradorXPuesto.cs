using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Personal.Models
{
    public class ColaboradorXPuesto: DBObject
    {
        //public int AreaID { get; set; }
        public int PuestoID { get; set; }
        public int ColaboradorID { get; set; }
        public int Sueldo { get; set; }
        public DateTime? FechaIngresoPuesto { get; set; }
        public DateTime? FechaSalidaPuesto { get; set; }
        public string Comentarios { get; set; }

        public virtual Colaborador Colaborador { get; set; }
       



    }
}