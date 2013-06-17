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

        public string LugarEvento { get; set; }

        [InverseProperty("Evento")]
        public virtual ICollection<Invitado> Invitados { get; set; }

        //
        public string custom { get; set; }

        public Evento() { }
        public Evento(EventoDTO e)
        {
            ID = e.ID;
            Nombre = e.Nombre;
            
            if (e.Inicio.Contains(":"))
            {
                Inicio = DateTime.ParseExact(e.Inicio, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            else
            {
                Inicio = DateTime.ParseExact(e.Inicio, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }

            if (e.Fin.Contains(":"))
            {
                Fin = DateTime.ParseExact(e.Fin, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            else
            {
                Fin = DateTime.ParseExact(e.Fin, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }

            EstadoID = e.EstadoID;
            CreadorID = e.CreadorID;
            TipoEventoID = e.TipoEventoID;
        }
        public Evento LoadFromDTO(EventoDTO e)
        {
            ID = e.ID;
            EstadoID = e.EstadoID;
            CreadorID = e.CreadorID;
            TipoEventoID = e.TipoEventoID;
            Nombre = e.Nombre;
            LugarEvento = e.LugarEvento;
            Inicio = Convert.ToDateTime(FormatRawDateTimeForKendo(e.Inicio));
            Fin = Convert.ToDateTime(FormatRawDateTimeForKendo(e.Fin));
            return this;
        }
        private static string FormatRawDateTimeForKendo(string datetime)
        {
            //Tue Jun 04 2013 02:00:00 GMT-0500
            string right = datetime.Substring(datetime.IndexOf(' '));
            //Jun 04 2013 02:00:00 GMT-0500
            string left = right.Substring(0, datetime.LastIndexOf(':'));
            return left;
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

        [DisplayName("Fecha Inicio Evento")]
        [Required]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public string Inicio { get; set; }

        [DisplayName("Fecha Fin Evento")]
        [Required]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
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

        [DisplayName("Lugar del Evento")]
        [MaxLength(100)]
        public string LugarEvento { get; set; }

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
            Creador = e.Creador != null ? e.Creador.ToDTO().NombreCompleto : String.Empty;
            LugarEvento = e.LugarEvento;
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