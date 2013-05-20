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
        public DbSet<OfertaLaboral> InternalOfertaLaborals { get; set; }

        public DBGenericRequester<OfertaLaboral> TablaOfertaLaborals { get; set; }

        private void RegistrarTablasReclutamiento()
        {
            TablaOfertaLaborals = new DBGenericRequester<OfertaLaboral>(this, InternalOfertaLaborals);
        }

        private void SeedOfertaLaboral()
        {
            TablaOfertaLaborals.AddElement(new OfertaLaboral { EstadoSolicitudOfertaLaboralID = 1, PuestoID = TablaPuestos.One(a => a.Nombre.Equals("Presidente")).ID });
        }
    }
}