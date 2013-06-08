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

        public DBGenericRequester<Rol> TablaRoles { get; set; }
        public DBGenericRequester<Usuario> TablaUsuarios { get; set; }

        private void RegistrarTablasSeguridad()
        {
            TablaRoles = new DBGenericRequester<Rol>(this, InternalRoles);
            TablaUsuarios = new DBGenericRequester<Usuario>(this, InternalUsuarios);
        }

        private void SeedRoles()
        {
            SidebarNavigator sn = new SidebarNavigator();

            foreach (SidebarOption Lso in sn.Opciones)
            {
                if (Lso.Suboptions.Count>0)
                {
                    List<SidebarSuboption> suboption = new List<SidebarSuboption>();
                    foreach (SidebarSuboption SSO in Lso.Suboptions)
                    {
                        Rol r = new Rol(SSO.Controller, Lso.Area);
                        TablaRoles.AddElement(r);
                    }
                }
                else
                {
                    Rol r = new Rol(Lso.Controller, Lso.Area);
                    TablaRoles.AddElement(r);
                }
            }
        }
    }
}