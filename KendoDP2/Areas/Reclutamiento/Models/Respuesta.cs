using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Evaluacion360.Models;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class Respuesta : DBObject
    {

        public string Comentario { get; set; }
        public int Puntaje { get; set; }

        public int EvaluacionXFaseXPostulacionID { get; set; }
        public virtual EvaluacionXFaseXPostulacion EvaluacionXFaseXPostulacion { get; set; }

        public int? FuncionID { get; set; }
        public virtual Funcion Funcion { get; set; }

        public int? CompetenciaID { get; set; }
        public virtual Competencia Competencia { get; set; }

        public Respuesta() { }

        public Respuesta(RespuestaDTO r)
        {
            LoadFromDTO(r);
        }

        public Respuesta LoadFromDTO(RespuestaDTO r)
        {
            ID = r.ID;
            Comentario = r.Comentario;
            Puntaje = r.Puntaje;
            EvaluacionXFaseXPostulacionID = r.EvaluacionXFaseXPostulacionID;
            if (r.FuncionID != 0 && r.CompetenciaID == 0) FuncionID = r.FuncionID;
            if (r.FuncionID == 0 && r.CompetenciaID != 0) CompetenciaID = r.CompetenciaID;

            return this;
        }

        public RespuestaDTO ToDTO()
        {
            return new RespuestaDTO(this);
        }
    }

    public class RespuestaDTO
    {
        public int ID { get; set; }
        public string Comentario { get; set; }
        public int Puntaje { get; set; }
        public int EvaluacionXFaseXPostulacionID { get; set; }
        public int FuncionID { get; set; }
        public int CompetenciaID { get; set; }

        public RespuestaDTO() { }

        public RespuestaDTO(Respuesta r)
        {
            ID = r.ID;
            Comentario = r.Comentario;
            Puntaje = r.Puntaje;
            EvaluacionXFaseXPostulacionID = r.EvaluacionXFaseXPostulacionID;
            if (r.FuncionID.HasValue) FuncionID = r.FuncionID.GetValueOrDefault();
            if (r.CompetenciaID.HasValue) CompetenciaID = r.CompetenciaID.GetValueOrDefault();
        }

    }
}