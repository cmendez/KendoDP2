using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace KendoDP2.Models.Generic
{

    public partial class DP2Context : DbContext
    {
        // Area Configuracion
        public DbSet<Periodo> InternalPeriodos { get; set; }

        public DBGenericRequester<Periodo> TablaPeriodos { get; set; }

        // Area Seguridad
        public DbSet<Rol> InternalRoles { get; set; }
        public DbSet<Usuario> InternalUsuarios { get; set; }

        public DBGenericRequester<Rol> TablaRoles { get; set; }
        public DBGenericRequester<Usuario> TablaUsuarios { get; set; }

        // Area Evaluacion360
        public DbSet<Competencia> InternalCompetencias { get; set; }
        public DbSet<Capacidad> InternalCapacidades { get; set; }
        public DbSet<NivelCapacidad> InternalNivelCapacidades { get; set; }

        public DBGenericRequester<Competencia> TablaCompetencias { get; set; }
        public DBGenericRequester<Capacidad> TablaCapacidades { get; set; }
        public DBGenericRequester<NivelCapacidad> TablaNivelCapacidades { get; set; }

        private void RegistrarTablas()
        {
            // Area Configuracion
            TablaPeriodos = new DBGenericRequester<Periodo>(this, InternalPeriodos);

            // Area Seguridad
            TablaRoles = new DBGenericRequester<Rol>(this, InternalRoles);
            TablaUsuarios = new DBGenericRequester<Usuario>(this, InternalUsuarios);

            // Area Evaluacion360
            TablaCompetencias = new DBGenericRequester<Competencia>(this, InternalCompetencias);
            TablaCapacidades = new DBGenericRequester<Capacidad>(this, InternalCapacidades);
            TablaNivelCapacidades = new DBGenericRequester<NivelCapacidad>(this, InternalNivelCapacidades);
        }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Seeds
        public void Seed()
        {
            // Area Configuracion
            SeedPeriodos();
            // Area Seguridad
            SeedRol();
            SeedUsuario();
            // Area Evaluacion360
            SeedCompetencias();
            SeedNivelCapacidad();
        }

        // Area Configuracion
        private void SeedPeriodos()
        {
            TablaPeriodos.AddElement(new Periodo("Periodo inicial", DateTime.Now));
        }

        // Area Seguridad
        private void SeedRol()
        {
            TablaRoles.AddElement(new Rol("Administrador"));
            TablaRoles.AddElement(new Rol("Invitado"));
        }

        private void SeedUsuario()
        {
            var administrador = TablaRoles.One(p => p.Nombre.Equals("Administrador"));
            var invitado = TablaRoles.One(p => p.Nombre.Equals("Invitado"));
            TablaUsuarios.AddElement(new Usuario("admin", "admin", administrador));
            TablaUsuarios.AddElement(new Usuario("anonimo", "anonimo", invitado));
        }

        // Area Evaluacion360

        private void SeedCompetencias()
        {
            TablaCompetencias.AddElement(new Competencia("Ser chiquito"));
            TablaCompetencias.AddElement(new Competencia("Ser grande"));
            TablaCompetencias.AddElement(new Competencia("Ser kiwi"));
        }

        private void SeedNivelCapacidad()
        {
            for (int i = 1; i <= 3; i++)
                TablaNivelCapacidades.AddElement(new NivelCapacidad(i));
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
}

