using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class EstadoProcesoEvaluacion: DBObject
    {
        public string Descripcion { get; set; }
        public virtual ICollection<ProcesoEvaluacion> ListaProcesoEvaluacion{ get; set; }

        public EstadoProcesoEvaluacion() { }

        public EstadoProcesoEvaluacion(string descripcion)
        {
            Descripcion = descripcion;
        }

         public EstadoProcesoEvaluacion(EstadoProcesoEvaluacionDTO e)
        {
            LoadFromDTO(e);
        }

        public EstadoProcesoEvaluacion LoadFromDTO(EstadoProcesoEvaluacionDTO e)
        {
            Descripcion = e.Descripcion;
            ID = e.ID;
            return this;
        }

        public EstadoProcesoEvaluacionDTO ToDTO()
        {
            return new EstadoProcesoEvaluacionDTO(this);
        }
    }

    public class EstadoProcesoEvaluacionDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string Descripcion { get; set; }

        public EstadoProcesoEvaluacionDTO() { }

        public EstadoProcesoEvaluacionDTO(EstadoProcesoEvaluacion e)
        {
            ID = e.ID;
            Descripcion = e.Descripcion;
        }

    }

    public class ConstantsEstadoProcesoEvaluacion
    {
        public static string Iniciado = "Iniciado";
        public static string Creado = "Creado";
        public static string EnProceso = "En proceso";
        public static string Terminado = "Terminado";
    }
}