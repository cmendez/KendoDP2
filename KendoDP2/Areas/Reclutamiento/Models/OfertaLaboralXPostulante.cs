﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        
        public int EstadoPostulantePorOfertaID { get; set; }
        public virtual EstadoPostulantePorOferta EstadoPostulantePorOferta { get; set; }

        public string FechaEvaluacionPrimeraFase { get; set; }
        public string FechaEvaluacionSegundaFase { get; set; }
        public string FechaEvaluacionTerceraFase { get; set; }

        public string FechaPostulacion { get; set; }

        public int PuntajeFase2 { get; set; }
        public int PuntajeAnterior { get; set; }


        public OfertaLaboralXPostulanteDTO ToDTO()
        {
            return new OfertaLaboralXPostulanteDTO(this);
        }
    }


    public class OfertaLaboralXPostulanteDTO
    {
        public int ID { get; set; }
                
        public int OfertaLaboralID { get; set; }
        public OfertaLaboralDTO OfertaLaboral { get; set; }

        public int PostulanteID { get; set; }
        public PostulanteDTO Postulante { get; set; }

        public bool FlagAprobado { get; set; }
        public int PuntajeTotal { get; set; }
        public string MotivoRechazo { get; set; }
        public string Comentarios { get; set; }
        public string Observaciones { get; set; }

        //no olvidar que se puede mandar las fases a traves de un list
        //probando algo nuevo
        public int EstadoPostulantePorOfertaID { get; set; }

        [DisplayName("Estado Postulante")]
        public string EstadoPostulantePorOfertaNombre { get; set; }

        [DisplayName("Fecha Evaluación Fase 1")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaEvaluacionPrimeraFase { get; set; }

        [DisplayName("Fecha Evaluación Fase 2")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaEvaluacionSegundaFase { get; set; }

        [DisplayName("Fecha Evaluación Fase 3")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaEvaluacionTerceraFase { get; set; }

        [DisplayName("Fecha Postulación")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaPostulacion { get; set; }

        [DisplayName("Puntaje de Fase")]
        [Range(1,20)]
        public int PuntajeFase2 { get; set; }
        
        public OfertaLaboralXPostulanteDTO()
        {
        }

        public OfertaLaboralXPostulanteDTO(OfertaLaboralXPostulante op)
        {
            ID = op.ID;

            OfertaLaboralID = op.OfertaLaboralID;
            OfertaLaboral = op.OfertaLaboral != null ? op.OfertaLaboral.ToDTO() : null;
            
            PostulanteID = op.PostulanteID;
            Postulante = op.Postulante != null ? op.Postulante.ToDTO() : null;
            
            FlagAprobado = op.FlagAprobado;
            PuntajeTotal = op.PuntajeTotal;
            MotivoRechazo = op.MotivoRechazo;
            Comentarios = op.Comentarios;
            Observaciones = op.Observaciones;
            
            EstadoPostulantePorOfertaID = op.EstadoPostulantePorOfertaID;
            EstadoPostulantePorOfertaNombre = op.EstadoPostulantePorOferta != null ? op.EstadoPostulantePorOferta.Descripcion : String.Empty;

            FechaEvaluacionPrimeraFase = op.FechaEvaluacionPrimeraFase;
            FechaEvaluacionSegundaFase = op.FechaEvaluacionSegundaFase;
            FechaEvaluacionTerceraFase = op.FechaEvaluacionTerceraFase;
            FechaPostulacion = op.FechaPostulacion;

            PuntajeFase2 = op.PuntajeFase2;


        }
    }
}