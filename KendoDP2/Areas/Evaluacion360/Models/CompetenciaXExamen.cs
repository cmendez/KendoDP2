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
        public int NivelID { get; set; }
        public String Descripcion { get; set; }
        
        public virtual ICollection<Pregunta> ListaPreguntas { get; set; }

        public CompetenciaXExamen() { 
        }

        public CompetenciaXExamen(CompetenciaXPuesto cxp, Examen examen) {
            CompetenciaID = cxp.CompetenciaID;
            Nota = 0;
            ExamenID = examen.ID;
            NivelID = cxp.NivelID;
            Descripcion = cxp.Competencia.Nombre;
        }
    }
}