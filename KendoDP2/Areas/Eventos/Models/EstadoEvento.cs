using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Eventos.Models
{
    public class EstadoEvento : DBObject
    {
        public string Descripcion { get; set; }
        public virtual ICollection<Evento> EventosConEstado { get; set; }

        public EstadoEvento() { }
        public EstadoEvento(string descripcion)
        {
            Descripcion = descripcion;
        }
        public EstadoEvento(EstadoEventoDTO ev)
        {
            LoadFromDTO(ev);
        }
        public EstadoEvento LoadFromDTO(EstadoEventoDTO ev)
        {
            ID = ev.ID;
            Descripcion = ev.Descripcion;
            return this;
        }
        public EstadoEventoDTO ToDTO()
        {
            return new EstadoEventoDTO(this);
        }
    }

    public class EstadoEventoDTO
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }

        public EstadoEventoDTO(EstadoEvento ev)
        {
            ID = ev.ID;
            Descripcion = ev.Descripcion;
        }

    }
}