using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class ProcesoXEvaluado : DBObject
    {
        public int procesoID { get; set; }
        [ForeignKey("procesoID")]
        public ProcesoEvaluacion proceso { get; set; }

        public int evaluadoID { get; set; }
        [ForeignKey("evaluadoID")]
        public Colaborador colaborador { get; set; }

        //public virtual List<int> evaluadoresID { get; set; }
        public virtual List<Colaborador> evaluadores { get; set; }

        public ProcesoXEvaluado()
        {
            evaluadores = new List<Colaborador>();
        }

        public ProcesoXEvaluado(int p_procesoID, int p_evaluadoID, List<Colaborador> p_evaluadores)
        {
            procesoID = p_procesoID;
            evaluadoID = p_evaluadoID;
            evaluadores = p_evaluadores;
        }

    }
}