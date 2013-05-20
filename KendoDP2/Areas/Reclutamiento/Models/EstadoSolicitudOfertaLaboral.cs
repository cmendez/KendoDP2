using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class EstadosSolicitudOfertaLaboral: DBObject
    {
        public string Descripcion { get; set; }
        public virtual ICollection<OfertaLaboral> ListaOfertasLaborales { get; set; }


        public EstadosSolicitudOfertaLaboral() { }

        public EstadosSolicitudOfertaLaboral(string descripcion)
        {
            Descripcion = descripcion;
        }

        public EstadosSolicitudOfertaLaboral(EstadosSolicitudOfertaLaboralDTO e)
        {
            LoadFromDTO(e);
        }

        public EstadosSolicitudOfertaLaboral LoadFromDTO(EstadosSolicitudOfertaLaboralDTO e)
        {
            Descripcion = e.Descripcion;
            ID = e.ID;
            return this;
        }

        public EstadosSolicitudOfertaLaboralDTO ToDTO()
        {
            return new EstadosSolicitudOfertaLaboralDTO(this);
        }


    }

    public class EstadosSolicitudOfertaLaboralDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string Descripcion { get; set; }

        public EstadosSolicitudOfertaLaboralDTO() { }

        public EstadosSolicitudOfertaLaboralDTO(EstadosSolicitudOfertaLaboral e)
        {
            ID = e.ID;
            Descripcion = e.Descripcion;
        }

    }
    
}