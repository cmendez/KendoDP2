using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class Pregunta: DBObject
    {
        public String TextoPregunta { get; set; }
        public int ExamenID { get; set; }
        //public virtual  Examen examen { get; set; }

        public int CapacidadID { get; set; }
        //public virtual Capacidad capacidad;
     
        public int Puntuacion { get; set; }
        public int Peso { get; set; }

        public int competenciaID { get; set; }
        //public virtual CompetenciaXExamen CompetenciasXExamen { get; set; }
    }
}