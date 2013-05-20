using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class EstadosOfertaLaboralXPostulante : DBObject
    {
        public string Descripcion { get; set; }
        public virtual ICollection<OfertaLaboralXPostulante> ListaOfertaLaboralXPostulante { get; set; }

        public EstadosOfertaLaboralXPostulante() { }

        public EstadosOfertaLaboralXPostulante(string descripcion)
        {
            Descripcion = descripcion;
        }
        public EstadosOfertaLaboralXPostulante(EstadosOfertaLaboralXPostulanteDTO e)
        {
            LoadFromDTO(e);
        }
        public EstadosOfertaLaboralXPostulante LoadFromDTO(EstadosOfertaLaboralXPostulanteDTO e)
        {
            Descripcion = e.Descripcion;
            ID = e.ID;
            return this;
        }
        public EstadosOfertaLaboralXPostulanteDTO ToDTO()
        {
            return new EstadosOfertaLaboralXPostulanteDTO(this);
        }
    }

    public class EstadosOfertaLaboralXPostulanteDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string Descripcion { get; set; }

        public EstadosOfertaLaboralXPostulanteDTO() { }

        public EstadosOfertaLaboralXPostulanteDTO(EstadosOfertaLaboralXPostulante e)
        {
            ID = e.ID;
            Descripcion = e.Descripcion;
        }
    }
}