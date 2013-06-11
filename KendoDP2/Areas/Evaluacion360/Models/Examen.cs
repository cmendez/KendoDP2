using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class Examen: DBObject 
    {
        public int EvaluadorID { get; set; }
        public int EstadoExamenID { get; set; }
        public virtual EstadoColaboradorXProcesoEvaluacion Estado { get; set; }

        public int NotaExamen { get; set; }
        public Examen() { }

    }
}