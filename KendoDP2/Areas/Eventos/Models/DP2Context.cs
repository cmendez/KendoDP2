using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using KendoDP2.Areas.Eventos.Models;

namespace KendoDP2.Models.Generic
{
    public partial class DP2Context : DBObject
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

        private void SeedEvento()
        {

        }

        private void SeedEstadoEvento()
        {

        }

        private void SeedInvitado()
        {

        }

    }
}