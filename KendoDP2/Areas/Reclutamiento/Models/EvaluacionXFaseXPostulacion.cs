using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class EvaluacionXFaseXPostulacion : DBObject
    {
        //Relacion One-to-One Bidireccional con FasePostulacionXOfertaLaboralXPostulante
        [Required]
        public virtual FasePostulacionXOfertaLaboralXPostulante FasePostulacionXOfertaLaboralXPostulante { get; set; }
        public int FasePostulacionXOfertaLaboralXPostulanteID { get; set; }

        public ICollection<Respuesta> RespuestasDeLaEvaluacion { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        
        public double Puntaje { get; set; }
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
            FechaInicio = DateTime.ParseExact(e.FechaInicio, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            FechaFin = DateTime.ParseExact(e.FechaFin, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            Puntaje = e.Puntaje;
            FlagAprobado = e.FlagAprobado;
            Comentarios = e.Comentarios;
            Observaciones = e.Observaciones;
            FasePostulacionXOfertaLaboralXPostulanteID = e.ID;

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

        public double Puntaje { get; set; }
        public bool FlagAprobado { get; set; }

        public string Comentarios { get; set; }
        public string Observaciones { get; set; }

        public int FasePostulacionXOfertaLaboralXPostulanteID { get; set; }

        public EvaluacionXFaseXPostulacionDTO() { }

        public EvaluacionXFaseXPostulacionDTO(EvaluacionXFaseXPostulacion e)
        {
            ID = e.ID;
            FechaInicio = e.FechaInicio.ToString("dd/MM/yyyy HH:mm:ss");
            FechaFin = e.FechaFin.ToString("dd/MM/yyyy HH:mm:ss");
            Puntaje = e.Puntaje;
            FlagAprobado = e.FlagAprobado;
            Comentarios = e.Comentarios;
            Observaciones = e.Observaciones;
            FasePostulacionXOfertaLaboralXPostulanteID = e.FasePostulacionXOfertaLaboralXPostulanteID;
        }
    }
}