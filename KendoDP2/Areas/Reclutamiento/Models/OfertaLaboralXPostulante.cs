using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class OfertaLaboralXPostulante : DBObject
    {
        public int OfertaLaboralID { get; set; }
        public int PostulanteID { get; set; }

        [ForeignKey("OfertaLaboralID")]
        public virtual OfertaLaboral OfertaLaboral { get; set; }
        [ForeignKey("PostulanteID")]
        public virtual Postulante Postulante { get; set; }

    }
}