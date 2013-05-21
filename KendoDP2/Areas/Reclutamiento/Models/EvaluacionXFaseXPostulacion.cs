using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class EvaluacionXFaseXPostulacion : DBObject
    {
        //public int FasePostulacionXOfertaLaboralXPostulanteID { get; set; }
        //[ForeignKey("FasePostulacionXOfertaLaboralXPostulanteID")]
        //public virtual FasePostulacionXOfertaLaboralXPostulante FasePostulacionXOfertaLaboralXPostulante { get; set; }
        
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        
        public int Puntaje { get; set; }
        public bool FlagAprobado { get; set; }
        
        public string Comentarios { get; set; }
        public string Observaciones { get; set; }

        public EvaluacionXFaseXPostulacion() { }

        public EvaluacionXFaseXPostulacion(EvaluacionXFaseXPostulacionDTO e)
        {
            LoadFromDTO(e);
        }

        public EvaluacionXFaseXPostulacion LoadFromDTO(EvaluacionXFaseXPostulacionDTO e)
        {
            ID = e.ID;
            FechaInicio = e.FechaInicio;
            FechaFin = e.FechaFin;
            Puntaje = e.Puntaje;
            FlagAprobado = e.FlagAprobado;
            Comentarios = e.Comentarios;
            Observaciones = e.Observaciones;

            return this;
        }

        public EvaluacionXFaseXPostulacionDTO ToDTO()
        {
            return new EvaluacionXFaseXPostulacionDTO(this);
        }
    }

    public class EvaluacionXFaseXPostulacionDTO
    {
        public int ID { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }

        public int Puntaje { get; set; }
        public bool FlagAprobado { get; set; }

        public string Comentarios { get; set; }
        public string Observaciones { get; set; }

        public EvaluacionXFaseXPostulacionDTO() { }

        public EvaluacionXFaseXPostulacionDTO(EvaluacionXFaseXPostulacion e)
        {
            ID = e.ID;
            FechaInicio = e.FechaInicio;
            FechaFin = e.FechaFin;
            Puntaje = e.Puntaje;
            FlagAprobado = e.FlagAprobado;
            Comentarios = e.Comentarios;
            Observaciones = e.Observaciones;
        }
    }
}