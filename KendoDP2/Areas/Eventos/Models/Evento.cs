using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kendo.Mvc.Extensions;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace KendoDP2.Areas.Eventos.Models
{
    public class Evento : DBObject
    {
        public string Nombre { get; set; }

        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        
        public int EstadoID { get; set; }
        public virtual EstadoEvento Estado { get; set; }

        public int TipoEventoID { get; set; }
        public virtual TipoEvento TipoEvento { get; set; }

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
            TipoEventoID = e.TipoEventoID;
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
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        [DisplayName("Nombre Evento")]
        public string Nombre { get; set; }

        [DisplayName("Inicio Evento")]
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string Inicio { get; set; }

        [DisplayName("Fin Evento")]
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string Fin { get; set; }

        [DisplayName("Estado")]
        public int EstadoID { get; set; }
        public string Estado { get; set; }

        [DisplayName("Tipo Evento")]
        public int TipoEventoID { get; set; }
        public string TipoEvento { get; set; }

        [DisplayName("Creador Evento")]
        public int CreadorID { get; set; }
        
        [DisplayName("Creador")]
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
            Estado = e.Estado != null ? e.Estado.Descripcion : String.Empty;
            TipoEventoID = e.TipoEventoID;
            TipoEvento = e.TipoEvento != null ? e.TipoEvento.Descripcion : String.Empty;
            CreadorID = e.CreadorID;
            Creador = e.Creador.ToDTO().NombreCompleto;
            if (e.Invitados != null && e.Invitados.Count > 0)
            {
                Invitados = new List<ColaboradorDTO>();
                foreach (var invitado in e.Invitados)
                {
                    Invitados.Add(invitado.Asistente.ToDTO());
                }
            }
        }

    }
}