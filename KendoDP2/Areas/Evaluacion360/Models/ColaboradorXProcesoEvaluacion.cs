using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class ColaboradorXProcesoEvaluacion : DBObject
    {
        public int ColaboradorID { get; set; }
        public virtual Colaborador Colaborador { get; set; }

        public int ProcesoEvaluacionID { get; set; }
        public virtual ProcesoEvaluacion ProcesoEvaluacion { get; set; }

        public int EstadoColaboradorXProcesoEvaluacionID { get; set; }
        public virtual EstadoColaboradorXProcesoEvaluacion EstadoColaboradorXProcesoEvaluacion { get; set; }

        public int ReferenciasPorAreas { get; set; }
        public bool ReferenciaDirecta { get; set; }
        // sobre 100 siempre
        public int? Puntuacion { get; set; }
        
        public ColaboradorXProcesoEvaluacionDTO ToDTO()
        {
            return new ColaboradorXProcesoEvaluacionDTO(this);
        }
    }

    public class ColaboradorXProcesoEvaluacionDTO
    {
        public ColaboradorDTO ColaboradorDTO { get; set; }
        public int ColaboradorID {get; set;}
        [DisplayName("Estado")]
        public int EstadoColaboradorXProcesoEvaluacionID { get; set; }
        public int ID { get; set; }
        public ProcesoEvaluacion ProcesoEvaluacion { get; set; }
        public int ProcesoID { get; set; }
        public int? Nota { get; set; }

        public ColaboradorXProcesoEvaluacionDTO(ColaboradorXProcesoEvaluacion x)
        {
            ColaboradorDTO = x.Colaborador.ToDTO();
            ColaboradorID = x.ColaboradorID;
            ID = x.ID;
            EstadoColaboradorXProcesoEvaluacionID = x.EstadoColaboradorXProcesoEvaluacionID;
            Nota = x.Puntuacion;
            ProcesoID = x.ProcesoEvaluacionID;
        }

        public ColaboradorXProcesoEvaluacionDTO() { }
    }

}