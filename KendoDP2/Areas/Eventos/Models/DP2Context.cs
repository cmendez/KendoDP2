using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using KendoDP2.Areas.Eventos.Models;

namespace KendoDP2.Models.Generic
{
    public partial class DP2Context : DbContext
    {
        public DbSet<Evento> InternalEventos { get; set; }
        public DbSet<EstadoEvento> InternalEstadoEvento { get; set; }
        public DbSet<Invitado> InternalInvitado { get; set; }

        public DBGenericRequester<Evento> TablaEvento { get; set; }
        public DBGenericRequester<EstadoEvento> TablaEstadoEvento { get; set; }
        public DBGenericRequester<Invitado> TablaInvitado { get; set; }

        private void RegistrarTablasEventos()
        {
            TablaEvento = new DBGenericRequester<Evento>(this, InternalEventos);
            TablaEstadoEvento = new DBGenericRequester<EstadoEvento>(this, InternalEstadoEvento);
            TablaInvitado = new DBGenericRequester<Invitado>(this, InternalInvitado);
        }

        private void SeedEstadoEvento()
        {
            TablaEstadoEvento.AddElement(new EstadoEvento { Descripcion = "Estado1" });
            TablaEstadoEvento.AddElement(new EstadoEvento { Descripcion = "Estado2" });
            TablaEstadoEvento.AddElement(new EstadoEvento { Descripcion = "Estado3" });
        }

        private void SeedEvento()
        {
            TablaEvento.AddElement(new Evento { Nombre = "Evento 1", Inicio = DateTime.ParseExact("04/06/2013 15:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("04/06/2013 18:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 1, CreadorID = 2 });
            TablaEvento.AddElement(new Evento { Nombre = "Evento 1", Inicio = DateTime.ParseExact("05/06/2013 15:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("05/06/2013 18:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 1, CreadorID = 2 });
            TablaEvento.AddElement(new Evento { Nombre = "Evento 1", Inicio = DateTime.ParseExact("08/06/2013 15:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("08/06/2013 20:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 1, CreadorID = 2 });
            TablaEvento.AddElement(new Evento { Nombre = "Evento 1", Inicio = DateTime.ParseExact("10/06/2013 15:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("10/06/2013 21:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 1, CreadorID = 2 });
        }

        private void SeedInvitado()
        {
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 3, EventoID = 1 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 4, EventoID = 1 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 5, EventoID = 1 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 6, EventoID = 1 });

            TablaInvitado.AddElement(new Invitado { ColaboradorID = 3, EventoID = 2 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 4, EventoID = 2 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 5, EventoID = 2 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 6, EventoID = 2 });

            TablaInvitado.AddElement(new Invitado { ColaboradorID = 3, EventoID = 3 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 4, EventoID = 3 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 5, EventoID = 3 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 6, EventoID = 3 });

            TablaInvitado.AddElement(new Invitado { ColaboradorID = 3, EventoID = 4 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 4, EventoID = 4 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 5, EventoID = 4 });
            TablaInvitado.AddElement(new Invitado { ColaboradorID = 6, EventoID = 4 });
        }

    }
}