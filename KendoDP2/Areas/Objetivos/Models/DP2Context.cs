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
        public DbSet<Objetivo> InternalObjetivos { get; set; }
        public DbSet<TipoObjetivoBSC> InternalTipoObjetivoBSC { get; set; }
        public DbSet<BSC> InternalBSC { get; set; }

        public DBGenericRequester<Objetivo> TablaObjetivos { get; set; }
        public DBGenericRequester<TipoObjetivoBSC> TablaTipoObjetivoBSC { get; set; }
        public DBGenericRequester<BSC> TablaBSC { get; set; }

        private void RegistrarTablasObjetivos()
        {
            TablaBSC = new DBGenericRequester<BSC>(this, InternalBSC);
            TablaObjetivos = new DBGenericRequester<Objetivo>(this, InternalObjetivos);
            TablaTipoObjetivoBSC = new DBGenericRequester<TipoObjetivoBSC>(this, InternalTipoObjetivoBSC);
        }

        private void SeedTipoObjetivoBSC()
        {
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.Financiero));
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.AprendizajeCrecimiento));
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.Cliente));
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.ProcesosInternos));

        }

        public void SeedObjetivos()
        {

            //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, Creador = TablaColaboradores.FindByID(1) });
            //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 2", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, Creador= TablaColaboradores.FindByID(1) });
            //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1.1", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, ObjetivoPadreID = 1, Creador = TablaColaboradores.FindByID(1) });
            //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1.2", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, ObjetivoPadreID = 1, Creador = TablaColaboradores.FindByID(1) });

        }
    }
}