﻿using KendoDP2.Areas.Evaluacion360.Models;
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
        // Toquenlo
        // Area Seguridad
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DBGenericRequester<Rol> TablaRoles { get; set; }
        public DBGenericRequester<Usuario> TablaUsuarios { get; set; }

        // Area Evaluacion360
        public DbSet<Competencia> Competencias { get; set; }

        public DBGenericRequester<Competencia> TablaCompetencias { get; set; }

       

        private void RegistrarTablas()
        {
            // Area Seguridad
            TablaRoles = new DBGenericRequester<Rol>(this, Roles);
            TablaUsuarios = new DBGenericRequester<Usuario>(this, Usuarios);

            // Area Evaluacion360
            TablaCompetencias = new DBGenericRequester<Competencia>(this, Competencias);

        }

        // NO TOCAR
        
        public DP2Context()
            //: base("Server=9b60ba48-d1f0-4481-9114-a19d01035a96.sqlserver.sequelizer.com;Database=db9b60ba48d1f044819114a19d01035a96;User ID=nooadgkzovbzpkrr;Password=sRs7Ga3UmBzfcVpWN7DiiwWyZ8gJeVgYxmKSXyvENVWNXt4UHppM4FG542gH3rPy;")
        {
            RegistrarTablas();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        //SEEDS, si pueden tocar, pero no mucho

        private void SeedRol()
        {
            TablaRoles.AddElement(new Rol("Administrador"));
            TablaRoles.AddElement(new Rol("Invitado"));
        }

        private void SeedUsuario()
        {
            var administrador = TablaRoles.FindByAttributeStringAsSingle("Nombre", "Administrador");
            var invitado = TablaRoles.FindByAttributeStringAsSingle("Nombre", "Invitado");
            TablaUsuarios.AddElement(new Usuario("a", "b", administrador));
            TablaUsuarios.AddElement(new Usuario("c", "d", invitado));
        }

        // Colocar aqui todos los seeds.
        public void Seed(){
            SeedRol();
            SeedUsuario();
        }

    }

}