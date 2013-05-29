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

        }

        private void SeedInvitado()
        {

        }

    }
}