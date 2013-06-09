using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class CompetenciaXExamen : DBObject
    {

        public int CompetenciaID { get; set; }
        public int Nota { get; set; }
        public int ExamenID { get; set; }
        public String Descripcion { get; set; }
        public virtual ICollection<Pregunta> ListaPreguntas { get; set; }

        public CompetenciaXExamen() { 
        }
    }
}