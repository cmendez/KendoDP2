using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class Respuesta : DBObject
    {

        public string Comentario { get; set; }
        public double Puntaje { get; set; }

        public int EvaluacionXFaseXPostulacionID { get; set; }
        public virtual EvaluacionXFaseXPostulacion EvaluacionXFaseXPostulacion { get; set; }

        public int FuncionID { get; set; }
        public virtual Funcion Funcion { get; set; }

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
            FuncionID = r.FuncionID;

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
        public double Puntaje { get; set; }
        public int EvaluacionXFaseXPostulacionID { get; set; }
        public int FuncionID { get; set; }

        public RespuestaDTO() { }

        public RespuestaDTO(Respuesta r)
        {
            ID = r.ID;
            Comentario = r.Comentario;
            Puntaje = r.Puntaje;
            EvaluacionXFaseXPostulacionID = r.EvaluacionXFaseXPostulacionID;
            FuncionID = r.FuncionID;
        }

    }
}