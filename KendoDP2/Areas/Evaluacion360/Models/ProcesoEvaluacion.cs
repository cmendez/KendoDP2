using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KendoDP2.Models.Seguridad;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class ProcesoEvaluacion: DBObject 
    {

        public string Nombre { get; set; }
        public DateTime? FechaCierre { get; set; }

        public int AutorizadorID { get; set; }
        public Colaborador Autorizador { get; set; }

        public int EstadoProcesoEvaluacionID { get; set; }
        public virtual EstadoProcesoEvaluacion EstadoProcesoEvaluacion { get; set; }

        public ICollection<ColaboradorXProcesoEvaluacion> PersonaXProcesoEvaluaciones { get; set; }
        //public List<ColaboradorXProcesoEvaluacion> PersonaXProcesoEvaluaciones { get; set; }

        public ProcesoEvaluacion() { }

        public ProcesoEvaluacion(ProcesoEvaluacionDTO p)
        {
            LoadFromDTO(p);
        }

        public ProcesoEvaluacion LoadFromDTO(ProcesoEvaluacionDTO p)
        {
            Nombre = p.Nombre;
            ID = p.ID;
            FechaCierre = DateTime.Parse(p.FechaCierre);
            AutorizadorID = p.AutorizadorID;
            EstadoProcesoEvaluacionID = p.EstadoProcesoEvaluacionID;
            return this;
        }

        public ProcesoEvaluacionDTO ToDTO()
        {
            return new ProcesoEvaluacionDTO(this);
        }
    }

    public class ProcesoEvaluacionDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }
        
        [DisplayName("Estado")]
        [ScaffoldColumn(false)]
        public int EstadoProcesoEvaluacionID { get; set; }
        [ScaffoldColumn(false)]
        public string EstadoNombre { get; set; }

        [DisplayName("Fecha de cierre a evaluadores")]
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaCierre { get; set; }

        [Required]
        [UIHint("GridForeignKey")]
        [DisplayName("Autorizado por")]
        public int AutorizadorID { get; set; }

        public ICollection<ColaboradorXProcesoEvaluacion> PersonaXProcesoEvaluaciones { get; set; }
        //public List<ColaboradorXProcesoEvaluacion> PersonaXProcesoEvaluaciones { get; set; }
        
        [ScaffoldColumn(false)]
        public int Puntuacion { get; set; }

        public ProcesoEvaluacionDTO() { }
        public ProcesoEvaluacionDTO(ProcesoEvaluacion p)
        {
            ID = p.ID;
            Nombre = p.Nombre;
            FechaCierre = p.FechaCierre.GetValueOrDefault().ToShortDateString();
            AutorizadorID = p.AutorizadorID;
            EstadoProcesoEvaluacionID = p.EstadoProcesoEvaluacionID;
            //Puntuacion = (new DP2Context()).TablaColaboradorXProcesoEvaluaciones.FindByID(evaluador.ElEvaluado).ToDTO();

            if (p.EstadoProcesoEvaluacion != null)
                EstadoNombre = p.EstadoProcesoEvaluacion.Descripcion;
        }
    }

    public class ResultadoEvaluacionesRDTO
    {
        //public int id
        //public string idEmpleado;
        public string IdEmpleado { get;  set; }
        public string IdCargo { get; set; }

        //public Evaluacio
        //public ResultadoDeUnaEvaluacionRDTO
        //public PuntajeDeUnaEvaluacionRDTO evaluaciones;
        public List<PuntajeEnUnEventoDeEvaluacionRDTO> evaluaciones;

        public ResultadoEvaluacionesRDTO()
        {

        }
    }


    public class PuntajeEnUnEventoDeEvaluacionRDTO
    {
        public string IdEmpleado { get; set; }
        //public string IdCargo { get; set; }
        public string IdDelCargo { get; set; }

        public string IdDelProceso { get; set; }
        public string Estado { get; set; }
        //public List<ResultadoCompetenciaRDTO> 

        //public string PuntajeGlo
        public string ResultadoGlobal { get; set; }
        public List<ResultadoCompetenciaRDTO> puntajes;

        public PuntajeEnUnEventoDeEvaluacionRDTO()
        {

        }

    }

    public class ResultadoCompetenciaRDTO
    {
        public string IdEmpleado { get; set; }
        public string IdDelCargo { get; set; }
        public string IdDelProceso { get; set; }
        public string IdDeLaCompetencia { get; set; }
        //public string DescripcionCompetencia { get; set; }
        public string DescripcionDeLaCompetencia { get; set; }
        public string NotaSobreCien { get; set; }

        public ResultadoCompetenciaRDTO()
        {

        }

    }

    public class ProcesoXCompetenciasDTO
    {
        //public ProcesoEvaluacionDTO proceso;
        //public ICollection<CompetenciaXExamenDTO> resultadosCompetencias;

        public ProcesoEvaluacionDTO Proceso { get; set; }
        public ICollection<CompetenciaXExamenDTO> ResultadosCompetencias { get; set; }

    }

}