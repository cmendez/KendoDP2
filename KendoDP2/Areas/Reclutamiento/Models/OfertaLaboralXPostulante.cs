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

        [InverseProperty("OfertaLaboralXPostulante")] //Cambiar el nombre por uno mas idoneo xD
        public virtual ICollection<FasePostulacionXOfertaLaboralXPostulante> Fases { get; set; }

        public string FlagAprobado { get; set; }
        public int PuntajeTotal { get; set; }
        public string MotivoRechazo { get; set; }
        public string Comentarios { get; set; }
        public string Observaciones { get; set; }

    }
}