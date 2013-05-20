using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class ModoSolicitudOfertaLaboral: DBObject
    {
        public string Descripcion { get; set; }
        public virtual ICollection<OfertaLaboral> ListaModoSolicitudOfertaLaboral { get; set; }


        public ModoSolicitudOfertaLaboral() { }

        public ModoSolicitudOfertaLaboral(string descripcion)
        {
            Descripcion = descripcion;
        }

        public ModoSolicitudOfertaLaboral(ModoSolicitudOfertaLaboralDTO e)
        {
            LoadFromDTO(e);
        }

        public ModoSolicitudOfertaLaboral LoadFromDTO(ModoSolicitudOfertaLaboralDTO e)
        {
            Descripcion = e.Descripcion;
            ID = e.ID;
            return this;
        }

        public ModoSolicitudOfertaLaboralDTO ToDTO()
        {
            return new ModoSolicitudOfertaLaboralDTO(this);
        }


    }

    public class ModoSolicitudOfertaLaboralDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string Descripcion { get; set; }

        public ModoSolicitudOfertaLaboralDTO() { }

        public ModoSolicitudOfertaLaboralDTO(ModoSolicitudOfertaLaboral e)
        {
            ID = e.ID;
            Descripcion = e.Descripcion;
        }

    }
}