using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class EstadosPuesto: DBObject
    {
        public string Descripcion { get; set; }
        public virtual ICollection<Puesto> ListaPuestos { get; set; }


        public EstadosPuesto() { }

        public EstadosPuesto(string descripcion)
        {
            Descripcion = descripcion;
        }

        public EstadosPuesto(EstadosPuestoDTO e)
        {
            LoadFromDTO(e);
        }

        public EstadosPuesto LoadFromDTO(EstadosPuestoDTO e)
        {
            Descripcion = e.Descripcion;
            ID = e.ID;
            return this;
        }

        public EstadosPuestoDTO ToDTO()
        {
            return new EstadosPuestoDTO(this);
        }


    }

    public class EstadosPuestoDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string Descripcion { get; set; }

        public EstadosPuestoDTO() { }

        public EstadosPuestoDTO(EstadosPuesto e)
        {
            ID = e.ID;
            Descripcion = e.Descripcion;
        }

    }
}