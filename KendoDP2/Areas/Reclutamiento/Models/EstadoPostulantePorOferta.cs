using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class EstadoPostulantePorOferta: DBObject
    {
    
        public string Descripcion { get; set; }
        public virtual ICollection<OfertaLaboralXPostulante> ListaOfertasLaboralesPorPostulantes { get; set; }


        public EstadoPostulantePorOferta() { }

        public EstadoPostulantePorOferta(string descripcion)
        {
            Descripcion = descripcion;
        }

        public EstadoPostulantePorOferta(EstadoPostulantePorOfertaDTO e)
        {
            LoadFromDTO(e);
        }

        public EstadoPostulantePorOferta LoadFromDTO(EstadoPostulantePorOfertaDTO e)
        {
            Descripcion = e.Descripcion;
            ID = e.ID;
            return this;
        }

        public EstadoPostulantePorOfertaDTO ToDTO()
        {
            return new EstadoPostulantePorOfertaDTO(this);
        }


    }

    public class EstadoPostulantePorOfertaDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string Descripcion { get; set; }

        public EstadoPostulantePorOfertaDTO() { }

        public EstadoPostulantePorOfertaDTO(EstadoPostulantePorOferta e)
        {
            ID = e.ID;
            Descripcion = e.Descripcion;
        }
    
    }

}