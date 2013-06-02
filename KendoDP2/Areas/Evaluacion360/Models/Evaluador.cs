using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class Evaluador: DBObject
    {
        public virtual ColaboradorXProcesoEvaluacion Evaluado { get; set; }
        public int EvaluadoID { get; set; }

        public virtual Colaborador EvaluadorX { get; set; }
        public int EvaluadorID { get; set; }

        public virtual Evaluacion Evaluacion { get; set; }
        public int EvaluacionID { get; set; }




            


    }

}