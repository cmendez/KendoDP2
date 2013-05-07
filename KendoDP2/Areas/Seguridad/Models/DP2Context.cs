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
        public DbSet<Rol> InternalRoles { get; set; }
        public DbSet<Usuario> InternalUsuarios { get; set; }
        public DbSet<SidebarOption> InternalSidebarNavigator { get; set; }

        public DBGenericRequester<Rol> TablaRoles { get; set; }
        public DBGenericRequester<Usuario> TablaUsuarios { get; set; }
        public DBGenericRequester<SidebarOption> TablaSidebarNavigator { get; set; }

        private void RegistrarTablasSeguridad()
        {
            TablaRoles = new DBGenericRequester<Rol>(this, InternalRoles);
            TablaUsuarios = new DBGenericRequester<Usuario>(this, InternalUsuarios);
            TablaSidebarNavigator = new DBGenericRequester<SidebarOption>(this, InternalSidebarNavigator);
        }

        private void SeedSidebarNavigator()
        {
            SidebarNavigator sn = new SidebarNavigator();
            SidebarOption sidebar;

            foreach (SidebarOption Lso in sn.Opciones)
            {
                if (Lso.Suboptions.Count > 0)
                {
                    List<SidebarSuboption> suboption = new List<SidebarSuboption>();

                    foreach (SidebarSuboption SSO in Lso.Suboptions)
                    {
                        SidebarSuboption aux = new SidebarSuboption(SSO.Title, SSO.Controller, SSO.Method, SSO.Icon);
                        suboption.Add(aux);
                    }
                    sidebar = new SidebarOption(Lso.Area, Lso.Title, Lso.Icon, suboption);
                }
                else
                {
                    sidebar = new SidebarOption(Lso.Area, Lso.Controller, Lso.Method, Lso.Title, Lso.Icon);
                }
                TablaSidebarNavigator.AddElement(sidebar);
            }

        }

        private void SeedRoles()
        {
            List<SidebarOption> sidebar = TablaSidebarNavigator.All();
            TablaRoles.AddElement(new Rol("Administrador", sidebar));
            TablaRoles.AddElement(new Rol("Invitado"));
        }

        private void SeedUsuarios()
        {
            var administrador = TablaRoles.One(p => p.Nombre.Equals("Administrador"));
            var invitado = TablaRoles.One(p => p.Nombre.Equals("Invitado"));
            TablaUsuarios.AddElement(new Usuario("anonimo", "anonimo", invitado));
        }
        
    }
}