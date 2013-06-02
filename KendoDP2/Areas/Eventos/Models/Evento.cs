using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;
using System.ComponentModel.DataAnnotations.Schema;

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

        [InverseProperty("Evento")]
        public virtual ICollection<Invitado> Invitados { get; set; }

        public Evento() { }
        public Evento(EventoDTO e)
        {
            ID = e.ID;
            Nombre = e.Nombre;
            Inicio = DateTime.ParseExact(e.Inicio, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            Fin = DateTime.ParseExact(e.Fin, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            EstadoID = e.EstadoID;
            CreadorID = e.CreadorID;
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
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Inicio { get; set; }
        public string Fin { get; set; }

        public int EstadoID { get; set; }
        public string Estado { get; set; }

        //public string Tipo { get; set; }

        public int CreadorID { get; set; }
        public string Creador { get; set; }

        public List<ColaboradorDTO> Invitados { get; set; }

        public EventoDTO() { }
        public EventoDTO(Evento e)
        {
            ID = e.ID;
            Nombre = e.Nombre;
            Inicio = e.Inicio.ToString("dd/MM/yyyy HH:mm:ss");
            Fin = e.Fin.ToString("dd/MM/yyyy HH:mm:ss");
            EstadoID = e.EstadoID;
            Estado = e.Estado.Descripcion;
            CreadorID = e.CreadorID;
            Creador = e.Creador.ToDTO().NombreCompleto;

            Invitados = new List<ColaboradorDTO>();
            foreach (var invitado in e.Invitados)
            {
                Invitados.Add(invitado.Asistente.ToDTO());
            }
        }

    }
}