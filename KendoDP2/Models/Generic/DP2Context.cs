﻿using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Objetivos.Models;
using KendoDP2.Areas.Personal.Models;
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

        public DBGenericRequester<Periodo> TablaPeriodos { get; set; }

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
        public DbSet<ProcesoEvaluacion> InternalProcesoEvaluaciones { get; set; }

        public DBGenericRequester<Competencia> TablaCompetencias { get; set; }
        public DBGenericRequester<Capacidad> TablaCapacidades { get; set; }
        public DBGenericRequester<NivelCapacidad> TablaNivelCapacidades { get; set; }
        public DBGenericRequester<ProcesoEvaluacion> TablaProcesoEvaluaciones { get; set; }

        // Area Objetivos
        public DbSet<Objetivo> InternalObjetivos { get; set; }
        public DbSet<TipoObjetivoBSC> InternalTipoObjetivoBSC { get; set; }
        public DbSet<BSC> InternalBSC { get; set; }

        public DBGenericRequester<Objetivo> TablaObjetivos { get; set; }
        public DBGenericRequester<TipoObjetivoBSC> TablaTipoObjetivoBSC { get; set; }
        public DBGenericRequester<BSC> TablaBSC { get; set; }

        // Area Personal
        public DbSet<Persona> InternalPersonas { get; set; }
        public DbSet<Colaborador> InternalColaboradores { get; set; }

        public DBGenericRequester<Persona> TablaPersonas { get; set; }
        public DBGenericRequester<Colaborador> TablaColaboradores { get; set; }

        private void RegistrarTablas()
        {
            // Area Configuracion
            TablaPeriodos = new DBGenericRequester<Periodo>(this, InternalPeriodos);

            // Area Seguridad
            TablaRoles = new DBGenericRequester<Rol>(this, InternalRoles);
            TablaUsuarios = new DBGenericRequester<Usuario>(this, InternalUsuarios);
            TablaSidebarNavigator = new DBGenericRequester<SidebarOption>(this, InternalSidebarNavigator);

            // Area Evaluacion360
            TablaCompetencias = new DBGenericRequester<Competencia>(this, InternalCompetencias);
            TablaCapacidades = new DBGenericRequester<Capacidad>(this, InternalCapacidades);
            TablaNivelCapacidades = new DBGenericRequester<NivelCapacidad>(this, InternalNivelCapacidades);
            TablaProcesoEvaluaciones = new DBGenericRequester<ProcesoEvaluacion>(this, InternalProcesoEvaluaciones);
        
            // Area Objetivos
            TablaBSC = new DBGenericRequester<BSC>(this, InternalBSC);
            TablaObjetivos = new DBGenericRequester<Objetivo>(this, InternalObjetivos);
            TablaTipoObjetivoBSC = new DBGenericRequester<TipoObjetivoBSC>(this, InternalTipoObjetivoBSC);

            // Area Personal
            TablaPersonas = new DBGenericRequester<Persona>(this, InternalPersonas);
            TablaColaboradores = new DBGenericRequester<Colaborador>(this, InternalColaboradores);
        }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Seeds
        public void Seed()
        {
            // Area Configuracion
            SeedPeriodos();
            // Area Seguridad
            SeedSidebarNavigator();
            SeedRoles();
            SeedUsuarios();
            // Area Evaluacion360
            SeedCompetencias();
            SeedNivelCapacidades();
            // Area Objetivos
            SeedTipoObjetivoBSC();
            // Area Personal
            SeedColaboradores();
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

        // Area Objetivos
        
        private void SeedTipoObjetivoBSC()
        {
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.Financiero));
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.AprendizajeCrecimiento));
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.Cliente));
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.ProcesosInternos));
        }

        // Area Personal

        private void SeedColaboradores()
        {
            // TODO(Modulo 1): mejorar seed o borrarlo
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Fortino Mario Alonso", ApellidoPaterno = "Moreno", ApellidoMaterno = "Reyes", Username = "admin", Password = "admin" });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Walter Joao Carlos", ApellidoPaterno = "Mitta", ApellidoMaterno = "Tucto", Username = "wallace", Password = "wallace" });
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

