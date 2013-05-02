using KendoDP2.Areas.Personal.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class ProcesoEvaluacion: DBObject 
    {

        public string Nombre { get; set; }
        public DateTime? FechaCierre { get; set; }

        public int AutorizadorID { get; set; }
        public Colaborador Autorizador { get; set; }

        public ICollection<ColaboradorXProcesoEvaluacion> PersonaXProcesoEvaluaciones { get; set; }

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

        [DisplayName("Fecha de cierre a evaluadores")]
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaCierre { get; set; }

        [Required]
        [UIHint("GridForeignKey")]
        [DisplayName("Autorizado por")]
        public int AutorizadorID { get; set; }

        public ProcesoEvaluacionDTO() { }
        public ProcesoEvaluacionDTO(ProcesoEvaluacion p)
        {
            ID = p.ID;
            Nombre = p.Nombre;
            FechaCierre = p.FechaCierre.GetValueOrDefault().ToShortDateString();
            AutorizadorID = p.AutorizadorID;
        }
    }
}