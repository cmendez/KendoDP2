using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Eventos.Models
{
    public class TipoEvento: DBObject
    {

        public string Descripcion { get; set; }
        public virtual ICollection<Evento> ListaEventos { get; set; }

        public TipoEvento() { }
        public TipoEvento(string descripcion)
        {
            Descripcion = descripcion;
        }
        public TipoEvento(TipoEventoDTO ev)
        {
            LoadFromDTO(ev);
        }
        public TipoEvento LoadFromDTO(TipoEventoDTO ev)
        {
            ID = ev.ID;
            Descripcion = ev.Descripcion;
            return this;
        }
        public TipoEventoDTO ToDTO()
        {
            return new TipoEventoDTO(this);
        }
    }

    public class TipoEventoDTO
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }

        public TipoEventoDTO(TipoEvento t)
        {
            ID = t.ID;
            Descripcion = t.Descripcion;
        }

    }
    
}