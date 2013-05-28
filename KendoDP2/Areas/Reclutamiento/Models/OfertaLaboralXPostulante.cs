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

        public bool FlagAprobado { get; set; }
        public int PuntajeTotal { get; set; }
        public string MotivoRechazo { get; set; }
        public string Comentarios { get; set; }
        public string Observaciones { get; set; }
        
        public int EstadoPostulacionPorOfertaID { get; set; }
        public virtual EstadoPostulantePorOferta EstadoPostulantePorOferta { get; set; }

        public OfertaLaboralXPostulanteDTO ToDTO()
        {
            return new OfertaLaboralXPostulanteDTO(this);
        }
    }


    public class OfertaLaboralXPostulanteDTO
    {
        public int ID { get; set; }
                
        public PostulanteDTO Postulante { get; set; }

        public bool FlagAprobado { get; set; }
        
        public int PuntajeTotal { get; set; }
        
        public string MotivoRechazo { get; set; }
        
        public string Comentarios { get; set; }
        
        public string Observaciones { get; set; }

        //no olvidar que se puede mandar las fases a traves de un list
        //probando algo nuevo
        public int EstadoPostulantePorOfertaID { get; set; }

        public string EstadoPostulantePorOfertaNombre { get; set; }
        
        public OfertaLaboralXPostulanteDTO()
        {
        }

        public OfertaLaboralXPostulanteDTO(OfertaLaboralXPostulante op)
        {
            ID = op.ID;
            Postulante = op.Postulante.ToDTO();
            FlagAprobado = op.FlagAprobado;
            PuntajeTotal = op.PuntajeTotal;
            MotivoRechazo = op.MotivoRechazo;
            Comentarios = op.Comentarios;
            Observaciones = op.Observaciones;
            EstadoPostulantePorOfertaID = op.EstadoPostulacionPorOfertaID;
            EstadoPostulantePorOfertaNombre = op.EstadoPostulantePorOferta.Descripcion;


        }
    }
}