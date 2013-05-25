using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class ColaboradorXEvaluadores : DBObject
    {
        public int evaluadoID { get; set; }
        public virtual List<int> evaluadoresID { get; set; }

        public ColaboradorXEvaluadores()
        {

        }

        public ColaboradorXEvaluadores(int p_evaluadoID, List<int> p_evaluadoresID)
        {
            evaluadoID = p_evaluadoID;
            evaluadoresID = p_evaluadoresID;
        }

    }
}