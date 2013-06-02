using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class Pregunta: DBObject
    {
        public int capacidadID { get; set; }
        //public virtual Capacidad capacidad { get; set; }
     
        public int puntuacion;

    }
}