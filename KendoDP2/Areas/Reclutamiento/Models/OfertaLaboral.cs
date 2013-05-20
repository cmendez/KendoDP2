using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class OfertaLaboral : DBObject
    {
        public int PuestoID {get;set;}
        public Puesto Puesto{get;set;}
        public int Estado { get; set; }
        public virtual ICollection<Postulante> Postulantes { get; set; }

        public OfertaLaboral(OfertaLaboralDTO o) : this()
        {
            LoadFromDTO(o);
        }

        //Constructor
        public OfertaLaboral()
        {
        }

        public OfertaLaboral LoadFromDTO(OfertaLaboralDTO o)
        {
            ID = o.ID;
            PuestoID = o.PuestoID;
            Estado = o.Estado;
            return this;
        }

        public OfertaLaboralDTO ToDTO()
        {
            return new OfertaLaboralDTO(this);
        }
    }

    public class OfertaLaboralDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [DisplayName("Puesto")]
        public int PuestoID { get; set; }
        public int Estado { get; set; }

        public OfertaLaboralDTO() { }

        public OfertaLaboralDTO(OfertaLaboral o)
        {
            ID = o.ID;
            PuestoID = o.PuestoID;
            Estado = o.Estado;
        }
    }
}