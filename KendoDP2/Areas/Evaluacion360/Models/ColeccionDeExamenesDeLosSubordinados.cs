using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class ColeccionDeExamenesDeLosSubordinados
    {

        //public int ElIDDelJefe { get; set; }
        public ColaboradorDTO ElJefe { get; set; }
        //public List<ProcesoEvaluacion>

        //public List<ExamenEnMiSubordinado>
        public List<ExamenEnUnIntegranteDeMiEquipo> examenes;

        public ColeccionDeExamenesDeLosSubordinados()
        {

        }

    }

    public class ExamenEnUnIntegranteDeMiEquipo
    {
        //public int ElIDDelColaborador { get; set; }
        public ColaboradorDTO ElEmpleado { get; set; }
        //public int elProceso { get; set; }
        //public int 
        public ProcesoEvaluacionDTO ElEventoDeEvaluacion { get; set; }
        //public List<>
        //public int ElSubordinadoID { get; set; }
        public ColaboradorDTO ElSubordinado { get; set; }

        //public List<EvaluadorDTO>
        //public List<Prueba>
        //public List<ExamenDTO>
        //public List<DTO>
        //public List<ExamenDeSubordin>
        //public List<ExamenDeSubordinado> 
        //public List<EvaluadorDTO> 
        //public List<ExamenDeSubordinado>  
        //public List<ParticipanteDTO> evaluadores { get; set; }
        public List<ParticipanteDTO> LosEvaluadores { get; set; }

        public ExamenEnUnIntegranteDeMiEquipo()
        {

        }

    }

    public class ParticipanteDTO
    {
        //public ColaboradorDTO Jefe
        public ColaboradorDTO ElJefe { get; set; }
        public ProcesoEvaluacionDTO ElProcesoDeEvaluacion { get; set; }
        public ColaboradorDTO ElEvaluado { get; set; }
        //public ColaboradorDTO ElEvaluador { get; set; }
        public ColaboradorDTO ElColaboradorEvaluador { get; set; }
        //public string ElEstado
        public string ElEstadoDeLaEvaluacion { get; set; }
        public int Puntaje { get; set; }

        public ParticipanteDTO()
        {


        }
    }
}