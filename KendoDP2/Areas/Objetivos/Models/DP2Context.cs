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

        private void SeedBSC()
        {
            TablaBSC.AddElement(new BSC(2));
        }

        private void SeedObjetivos()
        {
            Puesto presidente = TablaPuestos.One(p => p.Nombre.Equals("Presidente"));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 1", 1, 1, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 2", 1, 1, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 1.1", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Financiero 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 1.2", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Financiero 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 1", 1, 3, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 2", 1, 3, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 1.1", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Cliente 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 1.2", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Cliente 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 1", 1, 4, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 2", 1, 4, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 1.1", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Interno 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 1.2", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Interno 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 1", 1, 2, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 2", 1, 2, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 1.1", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Formación 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 1.2", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Formación 1")).ID, 50, this));
           
       
        }
    }
}