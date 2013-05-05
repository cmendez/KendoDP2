using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class Evaluacion: DBObject 
    {

        public String Nombre;
        public DateTime FechaCierre;
        public String Autorizado;
        public int Estado;

    }
}