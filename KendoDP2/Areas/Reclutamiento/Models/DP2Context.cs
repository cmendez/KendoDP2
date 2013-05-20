using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Objetivos.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Reclutamiento.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using KendoDP2.Models.Helpers;
using KendoDP2.Models.Seguridad;

namespace KendoDP2.Models.Generic
{
    public partial class DP2Context : DbContext
    {
        public DbSet<OfertaLaboral> InternalOfertaLaboral { get; set; }
        public DbSet<Postulante> InternalPostulante { get; set; }

        public DBGenericRequester<OfertaLaboral> TablaOfertaLaboral { get; set; }
        public DBGenericRequester<Postulante> TablaPostulante { get; set; }

        private void RegistrarTablasReclutamiento()
        {
            TablaOfertaLaboral = new DBGenericRequester<OfertaLaboral>(this, InternalOfertaLaboral);
            TablaPostulante = new DBGenericRequester<Postulante>(this, InternalPostulante);
        }

        private void SeedOfertaLaboral()
        {
            //TablaOfertaLaboral.AddElement(new OfertaLaboral { EstadoSolicitudOfertaLaboralID = 1, PuestoID = TablaPuestos.One(a => a.Nombre.Equals("Presidente")).ID });
        }

        private void SeedPostulante()
        {
            //TablaPostulante.AddElement(new Postulante );
        }
    }
}