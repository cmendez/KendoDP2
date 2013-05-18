using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class AreaXProcesoEvaluacion : DBObject
    {
        public int AreaID { get; set; }
        public virtual Area Area { get; set; }

        public int ProcesoEvaluacionID { get; set; }
        public virtual ProcesoEvaluacion ProcesoEvaluacion { get; set; }

        public AreaXProcesoEvaluacionDTO ToDTO()
        {
            return new AreaXProcesoEvaluacionDTO(this);
        }
    }

    public class AreaXProcesoEvaluacionDTO
    {
        public int ID { get; set; }
        public AreaDTO AreaDTO { get; set; }

        public AreaXProcesoEvaluacionDTO() { }

        public AreaXProcesoEvaluacionDTO(AreaXProcesoEvaluacion x)
        {
            ID = x.ID;
            AreaDTO = x.Area.ToDTO();
        }
    }
}