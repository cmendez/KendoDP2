using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class Evaluacion: DBObject 
    {

        public DateTime FechaCierre { get; set; }
        //public int Estado;
        public int Puntuacion { get; set; }

        public int EvaluadoID { get; set; }
        public virtual ColaboradorXProcesoEvaluacion Evaluado { get; set; }

        public int EvaluadorID { get; set; }
        public virtual ColaboradorXProcesoEvaluacion Evaluador { get; set; }

        public Evaluacion() { }

    }
}