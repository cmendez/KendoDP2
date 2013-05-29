using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Eventos.Models
{
    public class Evento : DBObject
    {
        public string Nombre { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }

        public int EstadoID { get; set; }
        public virtual EstadoEvento Estado { get; set; }

        //public string Tipo { get; set; }

        public int CreadorID { get; set; }
        public virtual Colaborador Creador { get; set; }

        public virtual ICollection<Invitado> Invitados { get; set; }

        public Evento() { }
        public Evento(EventoDTO e)
        {
            Nombre = e.Nombre;
            Inicio = Convert.ToDateTime(e.Inicio);
            Fin = Convert.ToDateTime(e.Fin);
            EstadoID = e.EstadoID;

        }

        public Evento LoadFromDTO(EventoDTO e)
        {
            return this;
        }

        public EventoDTO ToDTO()
        {
            return new EventoDTO(this);
        }
    }

    public class EventoDTO
    {

        public string Nombre { get; set; }
        public string Inicio { get; set; }
        public string Fin { get; set; }

        public int EstadoID { get; set; }
        public string Estado { get; set; }

        //public string Tipo { get; set; }

        public int CreadorID { get; set; }
        public string Creador { get; set; }

        public List<ColaboradorDTO> Invitados { get; set; }

        public EventoDTO(Evento e)
        {
            Nombre = e.Nombre;
            Inicio = e.Inicio.ToString("dd/MM/yyyy");
            Fin = e.Fin.ToString("dd/MM/yyyy");
            EstadoID = e.EstadoID;
            Estado = e.Estado.Descripcion;
            CreadorID = e.CreadorID;
            Creador = e.Creador.ToDTO().NombreCompleto;

            Invitados = new List<ColaboradorDTO>();
            foreach (var invitado in e.Invitados)
            {
                //Invitados.Add(invitado.fiohadoihf.ToDTO());
            }
        }

    }
}