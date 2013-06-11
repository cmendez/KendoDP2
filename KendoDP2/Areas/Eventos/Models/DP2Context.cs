using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using KendoDP2.Areas.Eventos.Models;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Models.Generic
{
    public partial class DP2Context : DbContext
    {
        public DbSet<Evento> InternalEventos { get; set; }
        public DbSet<EstadoEvento> InternalEstadoEvento { get; set; }
        public DbSet<TipoEvento> InternalTiposEvento { get; set; }
        public DbSet<Invitado> InternalInvitado { get; set; }
        public DbSet<AreaXEvento> InternalAreaXEvento { get; set; }

        public DBGenericRequester<Evento> TablaEvento { get; set; }
        public DBGenericRequester<EstadoEvento> TablaEstadoEvento { get; set; }
        public DBGenericRequester<TipoEvento> TablaTiposEvento { get; set; }
        public DBGenericRequester<Invitado> TablaInvitado { get; set; }
        public DBGenericRequester<AreaXEvento> TablaAreaXEvento { get; set; }

        private void RegistrarTablasEventos()
        {
            TablaEvento = new DBGenericRequester<Evento>(this, InternalEventos);
            TablaEstadoEvento = new DBGenericRequester<EstadoEvento>(this, InternalEstadoEvento);
            TablaTiposEvento = new DBGenericRequester<TipoEvento>(this, InternalTiposEvento);
            TablaInvitado = new DBGenericRequester<Invitado>(this, InternalInvitado);
            TablaAreaXEvento = new DBGenericRequester<AreaXEvento>(this, InternalAreaXEvento);
        }

        private void SeedEstadoEvento()
        {
            TablaEstadoEvento.AddElement(new EstadoEvento { Descripcion = "Estado 1" });
            TablaEstadoEvento.AddElement(new EstadoEvento { Descripcion = "Estado 2" });
            TablaEstadoEvento.AddElement(new EstadoEvento { Descripcion = "Estado 3" });
        }

        private void SeedTiposEventos()
        {
            TablaTiposEvento.AddElement(new TipoEvento { Descripcion = "Evento Empresa" });
            TablaTiposEvento.AddElement(new TipoEvento { Descripcion = "Evento Personal" });
            TablaTiposEvento.AddElement(new TipoEvento { Descripcion = "Evento Fechas Especiales" });

        }

        private void SeedEvento()
        {
            List<Colaborador> lstC = TablaColaboradores.All();

            for(int i = 0; i < lstC.Count && i < 6; i++)
            {
                var colaborador = lstC[i];
                TablaEvento.AddElement(new Evento { Nombre = "Evento A", Inicio = DateTime.ParseExact("03/06/2013 07:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("03/06/2013 10:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 1, TipoEventoID = 1, CreadorID = colaborador.ID });
                TablaEvento.AddElement(new Evento { Nombre = "Evento B", Inicio = DateTime.ParseExact("04/06/2013 07:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("04/06/2013 10:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 1, TipoEventoID = 2, CreadorID = colaborador.ID });
                TablaEvento.AddElement(new Evento { Nombre = "Evento C", Inicio = DateTime.ParseExact("05/06/2013 07:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("05/06/2013 10:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 1, TipoEventoID = 3, CreadorID = colaborador.ID });

                TablaEvento.AddElement(new Evento { Nombre = "Evento A", Inicio = DateTime.ParseExact("03/06/2013 11:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("03/06/2013 14:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 2, TipoEventoID = 1, CreadorID = colaborador.ID });
                TablaEvento.AddElement(new Evento { Nombre = "Evento B", Inicio = DateTime.ParseExact("04/06/2013 11:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("04/06/2013 14:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 2, TipoEventoID = 2, CreadorID = colaborador.ID });
                TablaEvento.AddElement(new Evento { Nombre = "Evento C", Inicio = DateTime.ParseExact("05/06/2013 11:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("05/06/2013 14:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 2, TipoEventoID = 3, CreadorID = colaborador.ID });

                TablaEvento.AddElement(new Evento { Nombre = "Evento A", Inicio = DateTime.ParseExact("03/06/2013 15:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("03/06/2013 18:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 3, TipoEventoID = 1, CreadorID = colaborador.ID });
                TablaEvento.AddElement(new Evento { Nombre = "Evento B", Inicio = DateTime.ParseExact("04/06/2013 15:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("04/06/2013 18:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 3, TipoEventoID = 2, CreadorID = colaborador.ID });
                TablaEvento.AddElement(new Evento { Nombre = "Evento C", Inicio = DateTime.ParseExact("05/06/2013 15:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), Fin = DateTime.ParseExact("05/06/2013 18:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture), EstadoID = 3, TipoEventoID = 3, CreadorID = colaborador.ID });
            }

        }

        private void SeedInvitado()
        {
            List<Evento> lstE = TablaEvento.All();
            List<Colaborador> lstC = TablaColaboradores.All();

            foreach (var evento in lstE)
            {
                var colaboradorHOSTID = evento.CreadorID;
                foreach (var colaborador in lstC)
                {
                    if (colaborador.ID != colaboradorHOSTID)
                        TablaInvitado.AddElement(new Invitado { ColaboradorID = colaborador.ID, EventoID = evento.ID });
                }
            }
            
        }

    }
}