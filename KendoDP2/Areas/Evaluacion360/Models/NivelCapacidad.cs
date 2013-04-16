using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class NivelCapacidad : DBObject
    {
        public int Nivel { get; set; }
        public virtual ICollection<Competencia> Competencias { get; set; }

        public NivelCapacidad() { }
        public NivelCapacidad(int nivel)
        {
            Nivel = nivel;
        }
    }
}