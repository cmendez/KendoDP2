﻿using KendoDP2.Areas.Organizacion.Models;
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

        public ColaboradorXProcesoEvaluacionDTO ToDTO()
        {
            return new ColaboradorXProcesoEvaluacionDTO(this);
        }
    }

    public class ColaboradorXProcesoEvaluacionDTO
    {
        public ColaboradorDTO ColaboradorDTO { get; set; }
        [DisplayName("Estado")]
        public int EstadoColaboradorXProcesoEvaluacionID { get; set; }
        public int ID { get; set; }

        public ColaboradorXProcesoEvaluacionDTO(ColaboradorXProcesoEvaluacion x)
        {
            ColaboradorDTO = x.Colaborador.ToDTO();
            ID = x.ID;
            EstadoColaboradorXProcesoEvaluacionID = x.EstadoColaboradorXProcesoEvaluacionID;
        }

        public ColaboradorXProcesoEvaluacionDTO() { }
    }

}