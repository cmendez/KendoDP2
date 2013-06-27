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

            TablaRoles.AddElement(new Rol { Area = "Mi informacion", Nombre = "Informacion personal", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Mi informacion", Nombre = "Mi equipo de trabajo", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Mi informacion", Nombre = "Mis contactos", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Mi informacion", Nombre = "Agenda", EsWeb = false, Permiso = true });
            
            TablaRoles.AddElement(new Rol { Area = "Reclutamiento", Nombre = "Aprobar solicitudes de oferta laboral", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Reclutamiento", Nombre = "Evaluar postulantes 1ra fase", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Reclutamiento", Nombre = "Evaluar postulantes 3ra fase", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Reclutamiento", Nombre = "Postular a oferta laboral", EsWeb = false, Permiso = true });
            
            TablaRoles.AddElement(new Rol { Area = "Evaluacion 360", Nombre = "Mis Pendientes", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Evaluacion 360", Nombre = "Rol de Evaluado", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Evaluacion 360", Nombre = "Mis subordinados", EsWeb = false, Permiso = true });
            
            TablaRoles.AddElement(new Rol { Area = "Objetivos", Nombre = "Objetivos Empresariales", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Objetivos", Nombre = "Objetivos Propios", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Objetivos", Nombre = "Objetivos para Equipo", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Objetivos", Nombre = "Registrar Avance", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Objetivos", Nombre = "Visualizar Avances", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Objetivos", Nombre = "Monitoreo", EsWeb = false, Permiso = true });

            TablaRoles.AddElement(new Rol { Area = "LineaDeCarrera", Nombre = "Comparar capacidades", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "LineaDeCarrera", Nombre = "Candidatos por puesto", EsWeb = false, Permiso = true });

            TablaRoles.AddElement(new Rol { Area = "Reportes", Nombre = "Reporte de Evaluacion 360", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Reportes", Nombre = "Reporte historico de Objetivos", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Reportes", Nombre = "Reporte de Cubrimiento de Puestos", EsWeb = false, Permiso = true });
            TablaRoles.AddElement(new Rol { Area = "Reportes", Nombre = "Reporte de Objetivos BSC", EsWeb = false, Permiso = true });

        }
    }
}