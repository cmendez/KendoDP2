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
        // Area Configuracion
        public DbSet<Periodo> InternalPeriodos { get; set; }
        public DbSet<Pais> InternalPaises { get; set; }

        public DBGenericRequester<Periodo> TablaPeriodos { get; set; }
        public DBGenericRequester<Pais> TablaPaises { get; set; }

        // Area Organizacion
        public DbSet<Area> InternalAreas { get; set; }
        public DbSet<Puesto> InternalPuestos { get; set; }
        public DbSet<EstadosPuesto> InternalEstadosPuestos { get; set; }
        public DbSet<PuestoXArea> InternalPuestosXAreas { get; set; }
        public DbSet<Persona> InternalPersonas { get; set; }
        public DbSet<Colaborador> InternalColaboradores { get; set; }
        public DbSet<EstadosColaborador> InternalEstadosColaboradores { get; set; }
        public DbSet<TipoDocumento> InternalTiposDocumentos { get; set; }
        public DbSet<GradoAcademico> InternalGradosAcademicos { get; set; }
        public DbSet<ColaboradorXPuesto> InternalColaboradoresXPuestos { get; set; }

        public DBGenericRequester<Area> TablaAreas { get; set; }
        public DBGenericRequester<Puesto> TablaPuestos { get; set; }
        public DBGenericRequester<EstadosPuesto> TablaEstadosPuestos { get; set; }
        public DBGenericRequester<PuestoXArea> TablaPuestosXAreas { get; set; }
        public DBGenericRequester<Persona> TablaPersonas { get; set; }
        public DBGenericRequester<Colaborador> TablaColaboradores { get; set; }
        public DBGenericRequester<EstadosColaborador> TablaEstadosColaboradores { get; set; }
        public DBGenericRequester<TipoDocumento> TablaTiposDocumentos { get; set; }
        public DBGenericRequester<GradoAcademico> TablaGradosAcademicos { get; set; }
        public DBGenericRequester<ColaboradorXPuesto> TablaColaboradoresXPuestos { get; set; }

        // Area Seguridad
        public DbSet<Rol> InternalRoles { get; set; }
        public DbSet<Usuario> InternalUsuarios { get; set; }
        public DbSet<SidebarOption> InternalSidebarNavigator { get; set; }

        public DBGenericRequester<Rol> TablaRoles { get; set; }
        public DBGenericRequester<Usuario> TablaUsuarios { get; set; }
        public DBGenericRequester<SidebarOption> TablaSidebarNavigator { get; set; }

        // Area Evaluacion360
        public DbSet<Competencia> InternalCompetencias { get; set; }
        public DbSet<Capacidad> InternalCapacidades { get; set; }
        public DbSet<NivelCapacidad> InternalNivelCapacidades { get; set; }

        public DbSet<Evaluacion> InternalEvaluaciones { get; set; }
        public DbSet<Evaluador> InternalEvaluadores { get; set; }
        public DbSet<TipoEvaluador> InternalTipoEvaluadores { get; set; }

        public DbSet<ProcesoEvaluacion> InternalProcesoEvaluaciones { get; set; }
        public DbSet<ColaboradorXProcesoEvaluacion> InternalColaboradorXProcesoEvaluaciones { get; set; }
        public DbSet<EstadoColaboradorXProcesoEvaluacion> InternalEstadoColaboradorXProcesoEvaluaciones { get; set; }


        public DBGenericRequester<Competencia> TablaCompetencias { get; set; }
        public DBGenericRequester<Capacidad> TablaCapacidades { get; set; }
        public DBGenericRequester<NivelCapacidad> TablaNivelCapacidades { get; set; }

        public DBGenericRequester<Evaluacion> TablaEvaluaciones { get; set; }
        public DBGenericRequester<Evaluador> TablaEvaluadores { get; set; }
        public DBGenericRequester<TipoEvaluador> TablaTipoEvaluador { get; set; }
        

        public DBGenericRequester<ProcesoEvaluacion> TablaProcesoEvaluaciones { get; set; }
        public DBGenericRequester<ColaboradorXProcesoEvaluacion> TablaColaboradorXProcesoEvaluaciones { get; set; }
        public DBGenericRequester<EstadoColaboradorXProcesoEvaluacion> TablaEstadoColaboradorXProcesoEvaluaciones { get; set; }

        // Area Objetivos
        public DbSet<Objetivo> InternalObjetivos { get; set; }
        public DbSet<TipoObjetivoBSC> InternalTipoObjetivoBSC { get; set; }
        public DbSet<BSC> InternalBSC { get; set; }

        public DBGenericRequester<Objetivo> TablaObjetivos { get; set; }
        public DBGenericRequester<TipoObjetivoBSC> TablaTipoObjetivoBSC { get; set; }
        public DBGenericRequester<BSC> TablaBSC { get; set; }


        // Reclutamiento
        public DbSet<OfertaLaboral> InternalOfertaLaborals { get; set; }

        // Area Personal
        //public DbSet<Persona> InternalPersonas { get; set; }
        //public DbSet<Colaborador> InternalColaboradores { get; set; }
        //public DbSet<EstadosColaborador> InternalEstadosColaboradores { get; set; }
        //public DbSet<TipoDocumento> InternalTiposDocumentos { get; set; }
        //public DbSet<GradoAcademico> InternalGradosAcademicos { get; set; }
        //public DbSet<ColaboradorXPuesto> InternalColaboradoresXPuestos { get; set; }
  
        //public DBGenericRequester<Persona> TablaPersonas { get; set; }
        //public DBGenericRequester<Colaborador> TablaColaboradores { get; set; }
        //public DBGenericRequester<EstadosColaborador> TablaEstadosColaboradores { get; set; }
        //public DBGenericRequester<TipoDocumento> TablaTiposDocumentos { get; set; }
        //public DBGenericRequester<GradoAcademico> TablaGradosAcademicos { get; set; }
        //public DBGenericRequester<ColaboradorXPuesto> TablaColaboradoresXPuestos { get; set; }


        public DBGenericRequester<OfertaLaboral> TablaOfertaLaborals { get; set; }

        private void RegistrarTablas()
        {
            // Area Configuracion
            TablaPeriodos = new DBGenericRequester<Periodo>(this, InternalPeriodos);
            TablaPaises = new DBGenericRequester<Pais>(this, InternalPaises);

            // Area Organizacion
            TablaAreas = new DBGenericRequester<Area>(this, InternalAreas);
            TablaPuestos = new DBGenericRequester<Puesto>(this, InternalPuestos);
            TablaEstadosPuestos = new DBGenericRequester<EstadosPuesto>(this, InternalEstadosPuestos);
            TablaPuestosXAreas = new DBGenericRequester<PuestoXArea>(this, InternalPuestosXAreas);

            // Area Seguridad
            TablaRoles = new DBGenericRequester<Rol>(this, InternalRoles);
            TablaUsuarios = new DBGenericRequester<Usuario>(this, InternalUsuarios);
            TablaSidebarNavigator = new DBGenericRequester<SidebarOption>(this, InternalSidebarNavigator);

            // Area Organizacion
            TablaAreas = new DBGenericRequester<Area>(this, InternalAreas);
            TablaPuestos = new DBGenericRequester<Puesto>(this, InternalPuestos);
            TablaEstadosPuestos = new DBGenericRequester<EstadosPuesto>(this, InternalEstadosPuestos);
            TablaPersonas = new DBGenericRequester<Persona>(this, InternalPersonas);
            TablaColaboradores = new DBGenericRequester<Colaborador>(this, InternalColaboradores);
            TablaEstadosColaboradores = new DBGenericRequester<EstadosColaborador>(this, InternalEstadosColaboradores);
            TablaGradosAcademicos = new DBGenericRequester<GradoAcademico>(this, InternalGradosAcademicos);
            TablaTiposDocumentos = new DBGenericRequester<TipoDocumento>(this, InternalTiposDocumentos);
            TablaColaboradoresXPuestos = new DBGenericRequester<ColaboradorXPuesto>(this, InternalColaboradoresXPuestos);

            // Area Evaluacion360
            TablaCompetencias = new DBGenericRequester<Competencia>(this, InternalCompetencias);
            TablaCapacidades = new DBGenericRequester<Capacidad>(this, InternalCapacidades);
            TablaNivelCapacidades = new DBGenericRequester<NivelCapacidad>(this, InternalNivelCapacidades);

            TablaEvaluaciones = new DBGenericRequester<Evaluacion>(this, InternalEvaluaciones);
            TablaEvaluadores = new DBGenericRequester<Evaluador>(this, InternalEvaluadores);
            TablaTipoEvaluador = new DBGenericRequester<TipoEvaluador>(this, InternalTipoEvaluadores);            

            TablaProcesoEvaluaciones = new DBGenericRequester<ProcesoEvaluacion>(this, InternalProcesoEvaluaciones);
            TablaEstadoColaboradorXProcesoEvaluaciones = new DBGenericRequester<EstadoColaboradorXProcesoEvaluacion>(this, InternalEstadoColaboradorXProcesoEvaluaciones);
            TablaColaboradorXProcesoEvaluaciones = new DBGenericRequester<ColaboradorXProcesoEvaluacion>(this, InternalColaboradorXProcesoEvaluaciones);
        
            // Area Objetivos
            TablaBSC = new DBGenericRequester<BSC>(this, InternalBSC);
            TablaObjetivos = new DBGenericRequester<Objetivo>(this, InternalObjetivos);
            TablaTipoObjetivoBSC = new DBGenericRequester<TipoObjetivoBSC>(this, InternalTipoObjetivoBSC);


            // Area Personal
            //Todo esto ya estaba declarado en Organización
            /*
            TablaPersonas = new DBGenericRequester<Persona>(this, InternalPersonas);
            TablaColaboradores = new DBGenericRequester<Colaborador>(this, InternalColaboradores);
            TablaEstadosColaboradores = new DBGenericRequester<EstadosColaborador>(this, InternalEstadosColaboradores);
            TablaGradosAcademicos = new DBGenericRequester<GradoAcademico>(this, InternalGradosAcademicos);
            TablaTiposDocumentos = new DBGenericRequester<TipoDocumento>(this, InternalTiposDocumentos);
            TablaColaboradoresXPuestos = new DBGenericRequester<ColaboradorXPuesto>(this, InternalColaboradoresXPuestos);
             */

            //Reclutamiento
            TablaOfertaLaborals = new DBGenericRequester<OfertaLaboral>(this, InternalOfertaLaborals);
        }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Seeds
        public void Seed()
        {
            // Area Configuracion
            SeedPeriodos();
            SeedPaises();
            // Area Organizacion
            SeedAreas();
            SeedPuestos();
            // Area Seguridad
            SeedSidebarNavigator();
            SeedRoles();
            SeedUsuarios();
            // Area Evaluacion360
            SeedCompetencias();
            SeedNivelCapacidades();
            SeedEstadoPersonaXProcesoEvaluaciones();
            // Area Objetivos
            SeedTipoObjetivoBSC();
            // Area Organizacion (segunda parte)
            SeedTiposDocumentos();
            SeedEstadosColaborador();
            SeedGradosAcademicos();
            SeedColaboradores();

            //Reclutamiento
            SeedOfertaLaboral();

            SeedObjetivos();

        }

        // Area Configuracion

        

        public Periodo CrearPeriodoConBSC(string nombrePeriodo, DateTime fecha)
        {
            Periodo p = new Periodo(nombrePeriodo, fecha);
            TablaPeriodos.AddElement(p);
            p.BSC = new BSC { PeriodoID = p.ID };
            TablaPeriodos.ModifyElement(p);
            return p;
        }

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

        // Area Seguridad
        private void SeedSidebarNavigator()
        {
            SidebarNavigator sn = new SidebarNavigator();
            SidebarOption sidebar;
            
            foreach(SidebarOption Lso in sn.Opciones)
            {
                if(Lso.Suboptions.Count>0)
                {
                    List<SidebarSuboption> suboption = new List<SidebarSuboption>();

                    foreach(SidebarSuboption SSO in Lso.Suboptions)                        
                    {   
                        SidebarSuboption aux =new SidebarSuboption(SSO.Title,SSO.Controller,SSO.Method,SSO.Icon);
                        suboption.Add(aux);                    
                    }
                    sidebar = new SidebarOption(Lso.Area, Lso.Title, Lso.Icon, suboption);
                }else
                {
                    sidebar = new SidebarOption(Lso.Area, Lso.Controller, Lso.Method, Lso.Title, Lso.Icon);
                }
                TablaSidebarNavigator.AddElement(sidebar);
            }
            
        }

        private void SeedRoles()
        {
            List<SidebarOption> sidebar = TablaSidebarNavigator.All();
            TablaRoles.AddElement(new Rol("Administrador",sidebar));
            TablaRoles.AddElement(new Rol("Invitado"));
        }

        private void SeedUsuarios()
        {
            var administrador = TablaRoles.One(p => p.Nombre.Equals("Administrador"));
            var invitado = TablaRoles.One(p => p.Nombre.Equals("Invitado"));
            TablaUsuarios.AddElement(new Usuario("anonimo", "anonimo", invitado));
        }
        

        // Area Evaluacion360

        private void SeedCompetencias()
        {
            TablaCompetencias.AddElement(new Competencia("Ser chiquito"));
            TablaCompetencias.AddElement(new Competencia("Ser grande"));
            TablaCompetencias.AddElement(new Competencia("Ser kiwi"));
        }

        private void SeedNivelCapacidades()
        {
            for (int i = 1; i <= 3; i++)
                TablaNivelCapacidades.AddElement(new NivelCapacidad(i));
        }

        private void SeedEstadoPersonaXProcesoEvaluaciones()
        {
            TablaEstadoColaboradorXProcesoEvaluaciones.AddElement(new EstadoColaboradorXProcesoEvaluacion { Nombre = ConstantsEstadoColaboradorXProcesoEvaluacion.Pendiente });
            TablaEstadoColaboradorXProcesoEvaluaciones.AddElement(new EstadoColaboradorXProcesoEvaluacion { Nombre = ConstantsEstadoColaboradorXProcesoEvaluacion.Terminado });
        }

        // Area Objetivos
        
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

        // Area Personal

        private void SeedColaboradores()
        {
            // TODO(Modulo 1): mejorar seed o borrarlo
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Fortino Mario Alonso", ApellidoPaterno = "Moreno", ApellidoMaterno = "Reyes", Username = "admin", Password = "admin", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Walter Joao Carlos", ApellidoPaterno = "Mitta", ApellidoMaterno = "Tucto", Username = "wallace", Password = "wallace", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
        }

        private void SeedTiposDocumentos()
        {
            TablaTiposDocumentos.AddElement(new TipoDocumento { Descripcion = "Pasaporte" });
            TablaTiposDocumentos.AddElement(new TipoDocumento { Descripcion = "DNI" });
        }

        private void SeedEstadosColaborador()
        {
            TablaEstadosColaboradores.AddElement(new EstadosColaborador { Descripcion = "Contratado" });
            TablaEstadosColaboradores.AddElement(new EstadosColaborador { Descripcion = "Despedido" });
            TablaEstadosColaboradores.AddElement(new EstadosColaborador { Descripcion = "Inactivo" });
        }



        private void SeedGradosAcademicos()
        {
            TablaGradosAcademicos.AddElement(new GradoAcademico { Descripcion = "Bachiller" });
            TablaGradosAcademicos.AddElement(new GradoAcademico { Descripcion = "Técnico" });
            TablaGradosAcademicos.AddElement(new GradoAcademico { Descripcion = "Estudiante" });
            TablaGradosAcademicos.AddElement(new GradoAcademico { Descripcion = "Licenciado" });
            TablaGradosAcademicos.AddElement(new GradoAcademico { Descripcion = "Master" });
            TablaGradosAcademicos.AddElement(new GradoAcademico { Descripcion = "Doctor" });
        }

        // Area Organizacion

        private void SeedAreas()
        {
            Area area1 = new Area { Nombre = "La gran Área", Descripcion = "El área más grande" };
            TablaAreas.AddElement(area1);
            TablaAreas.AddElement(new Area { Nombre = "Gerencia general", Descripcion = "Debajo de la gran área", AreaSuperiorID = TablaAreas.One(a => a.Nombre.Equals("La gran Área")).ID});
        }

        private void SeedPuestos()
        {
            TablaPuestos.AddElement(new Puesto { Nombre = "Presidente", Descripcion = "Jefe de proyecto", AreaID = TablaAreas.One(a => a.Nombre.Equals("La gran Área")).ID });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente general", Descripcion = "Por ahí", AreaID = TablaAreas.One(a => a.Nombre.Equals("Gerencia general")).ID });
        }


        private void SeedEstadosPuesto()
        {
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Asignado" });
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Vacante" });
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Inactivo" });
        }

        // Area Reclutamiento
        private void SeedOfertaLaboral()
        {
            TablaOfertaLaborals.AddElement(new OfertaLaboral { Estado = 1, PuestoID = TablaPuestos.One(a=>a.Nombre.Equals("Presidente")).ID });
        }

        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // No tocar por nada del mundo las lineas de abajo, si no esterilizo a quien lo haga.
        // - Entendido.
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

