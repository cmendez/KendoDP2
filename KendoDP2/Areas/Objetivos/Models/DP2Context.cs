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

            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 1", 1, 50, 100));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 2", 1, 50, 100));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 1.1", 1, 50, 1));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 1.2", 1, 50, 1));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 1", 3, 50, 100));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 2", 3, 50, 100));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 1.1", 3, 50, 5));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 1.2", 3, 50, 5));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 1", 4, 50, 100));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 2", 4, 50, 100));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 1.1", 4, 50, 9));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 1.2", 4, 50, 9));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 1", 2, 50, 100));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 2", 2, 50, 100));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 1.1", 2, 50, 13));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 1.2", 2, 50, 13));

            /*TablaObjetivos.AddElement(new Objetivo ( "Objetivo Financiero 1", 1, 50, -1));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Financiero 2",1, 50, -1));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Financiero 1.1", 1, 50,  1 ));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Financiero 1.2", 1, 50,  1 ));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Cliente 1", 2, 50, -1 ));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Cliente 2", 2, 50, -1));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Cliente 1.1",  2, 50, 5 ));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Cliente 1.2", 2, 50, 5));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Interno 1",  3, 50, -1));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Interno 2", 3, 50, -1));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Interno 1.1", 3, 50, 9 ));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Interno 1.2",  3, 50,  9 ));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Formación 1",  4, 50, -1 ));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Formación 2", 4, 50, -1));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Formación 1.1", 4,  50, 13));
            TablaObjetivos.AddElement(new Objetivo ( "Objetivo Formación 1.2", 4, 50, 13 ));*/

           
        }
    }
}