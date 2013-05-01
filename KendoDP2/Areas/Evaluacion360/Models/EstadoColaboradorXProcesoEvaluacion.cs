using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class EstadoColaboradorXProcesoEvaluacion : DBObject
    {
        public string Nombre { get; set; }
        public virtual ICollection<ColaboradorXProcesoEvaluacion> ColaboradorXProcesoEvaluaciones { get; set; }

        public EstadoColaboradorXProcesoEvaluacionDTO ToDTO()
        {
            return new EstadoColaboradorXProcesoEvaluacionDTO(this);
        }

    }

    public class EstadoColaboradorXProcesoEvaluacionDTO
    {
        public string Nombre { get; set; }
        public int ID { get; set; }

        public EstadoColaboradorXProcesoEvaluacionDTO() { }
        public EstadoColaboradorXProcesoEvaluacionDTO(EstadoColaboradorXProcesoEvaluacion x)
        {
            Nombre = x.Nombre;
            ID = x.ID;
        }
    }
    public class ConstantsEstadoColaboradorXProcesoEvaluacion
    {
        public static string Pendiente = "Pendiente";
        public static string Terminado = "Terminado";
    }
}
