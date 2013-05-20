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
        private void RegistrarTablas()
        {
            RegistrarTablasConfiguracion();
            RegistrarTablasOrganizacion();
            RegistrarTablasSeguridad();
            RegistrarTablasEvaluacion360();
            RegistrarTablasObjetivos();
            RegistrarTablasReclutamiento();
        }

        //Seeds
        public void Seed()
        {
            // Area Configuracion
            SeedPeriodos();
            SeedPaises();
            // Area Organizacion
            SeedAreas();
            SeedPuestos();
            SeedOrganizacion();
            // Area Seguridad
            SeedSidebarNavigator();
            SeedRoles();
            SeedUsuarios();
            // Area Evaluacion360
            SeedPerfiles();
            SeedCompetencias();
            SeedNivelCapacidades();
            SeedEstadoPersonaXProcesoEvaluaciones();
            SeedPuestoXEvaluadores();
            // Area Objetivos
            SeedTipoObjetivoBSC();
            // Area Organizacion (segunda parte)
            SeedTiposDocumentos();
            SeedEstadosColaborador();
            SeedGradosAcademicos();
            SeedColaboradores();
            // Reclutamiento
            SeedOfertaLaboral();
            // Area Objetivos
            SeedObjetivos();

        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // No tocar por nada del mundo las lineas de abajo, si no esterilizo a quien lo haga.
        public DP2Context()
           : base(KendoDP2.MvcApplication.IsDebug ? "DebugDB" : KendoDP2.MvcApplication.ConnectionString)
        {
            RegistrarTablas();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToOneConstraintIntroductionConvention>();
        }
    }

    //public class DP2ContextInitializerDEBUG : DropCreateDatabaseAlways<DP2Context>
    public class DP2ContextInitializerDEBUG : DropCreateDatabaseIfModelChanges<DP2Context>
    {
        protected override void Seed(DP2Context context)
        {
            context.Seed();
        }
    }
    public class DP2ContextInitializerRELEASE : CreateDatabaseIfNotExists<DP2Context>
    {
        protected override void Seed(DP2Context context)
        {
            context.Seed();
        }
    }

    public class Configuration : DbMigrationsConfiguration<DP2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}

