using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class FasePostulacionXOfertaLaboralXPostulante : DBObject
    {

        public int FasePostulacionID { get; set; }
        public int OfertaLaboralXPostulanteID { get; set; }

        [ForeignKey("FasePostulacionID")]
        public virtual FasePostulacion FasePostulacion { get; set; }
        [ForeignKey("OfertaLaboralXPostulanteID ")]
        public virtual OfertaLaboralXPostulante OfertaLaboralXPostulante { get; set; }

        public int EvaluacionXFaseXPostulacionID { get; set; }
        [ForeignKey("EvaluacionXFaseXPostulacionID")]
        public virtual EvaluacionXFaseXPostulacion EvaluacionXFaseXPostulacion { get; set; }

    }
}