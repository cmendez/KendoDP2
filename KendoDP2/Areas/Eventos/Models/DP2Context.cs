﻿using System;
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
        public DbSet<TipoEvento> InternalTiposEvento { get; set; }
        public DbSet<Invitado> InternalInvitado { get; set; }

        public DBGenericRequester<Evento> TablaEvento { get; set; }
        public DBGenericRequester<EstadoEvento> TablaEstadoEvento { get; set; }
        public DBGenericRequester<TipoEvento> TablaTiposEvento { get; set; }
        public DBGenericRequester<Invitado> TablaInvitado { get; set; }

        private void RegistrarTablasEventos()
        {
            TablaEvento = new DBGenericRequester<Evento>(this, InternalEventos);
            TablaEstadoEvento = new DBGenericRequester<EstadoEvento>(this, InternalEstadoEvento);
            TablaTiposEvento = new DBGenericRequester<TipoEvento>(this, InternalTiposEvento);
            TablaInvitado = new DBGenericRequester<Invitado>(this, InternalInvitado);
        }

        private void SeedEstadoEvento()
        {
            TablaEstadoEvento.AddElement(new EstadoEvento { Descripcion = "Estado1" });
            TablaEstadoEvento.AddElement(new EstadoEvento { Descripcion = "Estado2" });
            TablaEstadoEvento.AddElement(new EstadoEvento { Descripcion = "Estado3" });
        }

        private void SeedTiposEventos()
        {
            TablaTiposEvento.AddElement(new TipoEvento { Descripcion = "Evento Empresa" });
            TablaTiposEvento.AddElement(new TipoEvento { Descripcion = "Evento Personal" });
            TablaTiposEvento.AddElement(new TipoEvento { Descripcion = "Evento Fechas Especiales" });

        }

        private void SeedEvento()
        {
            TablaEvento.AddElement(new Evento { Nombre = "Evento 1", Inicio = DateTime.ParseExact("29/05/2013 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("30/06/2013 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 1, TipoEventoID = 1, CreadorID = 2 });
            TablaEvento.AddElement(new Evento { Nombre = "Evento 1", Inicio = DateTime.ParseExact("29/05/2013 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("30/06/2013 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 1, TipoEventoID = 1, CreadorID = 2 });
            TablaEvento.AddElement(new Evento { Nombre = "Evento 1", Inicio = DateTime.ParseExact("29/05/2013 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("30/06/2013 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 1, TipoEventoID = 1, CreadorID = 2 });
            TablaEvento.AddElement(new Evento { Nombre = "Evento 1", Inicio = DateTime.ParseExact("29/05/2013 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("30/06/2013 23:59:59", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 1, TipoEventoID = 1, CreadorID = 2 });

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