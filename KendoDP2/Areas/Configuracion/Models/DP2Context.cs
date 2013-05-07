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
        public DbSet<Periodo> InternalPeriodos { get; set; }
        public DbSet<Pais> InternalPaises { get; set; }

        public DBGenericRequester<Periodo> TablaPeriodos { get; set; }
        public DBGenericRequester<Pais> TablaPaises { get; set; }

        private void RegistrarTablasConfiguracion()
        {
            TablaPeriodos = new DBGenericRequester<Periodo>(this, InternalPeriodos);
            TablaPaises = new DBGenericRequester<Pais>(this, InternalPaises);
        }

        // Seeds

        private void SeedPeriodos()
        {
            CrearPeriodoConBSC("Período inicial", DateTime.Now);
        }

        private void SeedPaises()
        {
            TablaPaises.AddElement(new Pais { Nombre = "Perú" });
            TablaPaises.AddElement(new Pais { Nombre = "Estados Unidos" });
            TablaPaises.AddElement(new Pais { Nombre = "Argentina" });
            TablaPaises.AddElement(new Pais { Nombre = "España" });
            TablaPaises.AddElement(new Pais { Nombre = "Brazil" });
            TablaPaises.AddElement(new Pais { Nombre = "Canadá" });
        }

        // Otros metodos


        public Periodo CrearPeriodoConBSC(string nombrePeriodo, DateTime fecha)
        {
            Periodo p = new Periodo(nombrePeriodo, fecha);
            TablaPeriodos.AddElement(p);
            p.BSC = new BSC { PeriodoID = p.ID };
            TablaPeriodos.ModifyElement(p);
            return p;
        }
    }
}