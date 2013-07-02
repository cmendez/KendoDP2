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

        public List<Rol> SeedRolesGenerales()
        {
            List<Rol> tablaRoles = new List<Rol>();
            int x = TablaRoles.All().Count;
            tablaRoles.Add(new Rol { Titulo = "Competencias", ID = x + 1, Area = "Evaluacion360", Nombre = "Competencias", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Capacidades", ID = x + 2, Area = "Evaluacion360", Nombre = "Capacidades", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Evaluación de puestos de trabajo", ID = x + 3, Area = "Evaluacion360", Nombre = "PuestosEvaluacion", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Procesos de evaluación", ID = x + 4, Area = "Evaluacion360", Nombre = "ProcesoEvaluacion", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Mis evaluaciones", ID = x + 5, Area = "Evaluacion360", Nombre = "ListarProcesosXEvaluado", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Mis pendientes", ID = x + 6, Area = "Evaluacion360", Nombre = "ListarProcesosXEvaluador", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Mis colaboradores", ID = x + 7, Area = "Evaluacion360", Nombre = "Subordinados", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Objetivos de la empresa", ID = x + 8, Area = "Objetivos", Nombre = "Objetivosempresa", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Monitoreo en mi equipo de trabajo", ID = x + 9, Area = "Objetivos", Nombre = "Acordion", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Mis objetivos", ID = x + 10, Area = "Objetivos", Nombre = "Misobjetivos", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Objetivos de subordinados", ID = x + 11, Area = "Objetivos", Nombre = "Objetivossubordinados", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Periodos", ID = x + 12, Area = "Configuracion", Nombre = "Periodos", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Permisos a Web", ID = x + 13, Area = "Seguridad", Nombre = "Usuarios", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Permisos a Movil", ID = x + 14, Area = "Seguridad", Nombre = "UsuariosMovil", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Asignar Usario", ID = x + 15, Area = "Seguridad", Nombre = "AsignarCredenciales", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Nuevos Usuarios", ID = x + 16, Area = "Seguridad", Nombre = "CrearUsuario", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Orgazacion", ID = x + 17, Area = "Organizacion", Nombre = "Organizaciones", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Organigrama", ID = x + 18, Area = "Organizacion", Nombre = "Organigrama", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Areas", ID = x + 19, Area = "Organizacion", Nombre = "Areas", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Colaboradores", ID = x + 20, Area = "Organizacion", Nombre = "Colaboradores", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Funciones", ID = x + 21, Area = "Organizacion", Nombre = "Funciones", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Puestos", ID = x + 22, Area = "Organizacion", Nombre = "Puestos", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Linea de carrera", ID = x + 23, Area = "Organizacion", Nombre = "Historial", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Pagina persona", ID = x + 24, Area = "Organizacion", Nombre = "Intranet", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Ofertas laborales", ID = x + 25, Area = "Reclutamiento", Nombre = "SolicitudOfertasLaborales", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Solicitud de promocion", ID = x + 26, Area = "Reclutamiento", Nombre = "SolicitudPromociones", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Administrar ofertas laborales internas", ID = x + 27, Area = "Reclutamiento", Nombre = "OfertasLaboralesInternas", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Administrar ofertas laborales externas", ID = x + 28, Area = "Reclutamiento", Nombre = "OfertasLaboralesExternas", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Convocatoria interna", ID = x + 29, Area = "BolsaTrabajo", Nombre = "ConvocatoriasInternas", EsWeb = true, Permiso = false });
            tablaRoles.Add(new Rol { Titulo = "Eventos", ID = x + 30, Area = "Eventos", Nombre = "Eventos", EsWeb = true, Permiso = false });


            tablaRoles.Add(new Rol { ID = x + 31, Area = "Mi informacion", Nombre = "Informacion personal", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 32, Area = "Mi informacion", Nombre = "Mi equipo de trabajo", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 33, Area = "Mi informacion", Nombre = "Mis contactos", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 34, Area = "Mi informacion", Nombre = "Agenda", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 35, Area = "Reclutamiento", Nombre = "Aprobar solicitudes de oferta laboral", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 36, Area = "Reclutamiento", Nombre = "Evaluar postulantes 1ra fase", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 37, Area = "Reclutamiento", Nombre = "Evaluar postulantes 3ra fase", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 38, Area = "Reclutamiento", Nombre = "Postular a oferta laboral", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 39, Area = "Evaluacion 360", Nombre = "Mis Pendientes", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 40, Area = "Evaluacion 360", Nombre = "Rol de Evaluado", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 41, Area = "Evaluacion 360", Nombre = "Mis subordinados", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 42, Area = "Objetivos", Nombre = "Objetivos Empresariales", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 43, Area = "Objetivos", Nombre = "Objetivos Propios", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 44, Area = "Objetivos", Nombre = "Objetivos para Equipo", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 45, Area = "Objetivos", Nombre = "Registrar Avance", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 46, Area = "Objetivos", Nombre = "Visualizar Avances", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 47, Area = "Objetivos", Nombre = "Monitoreo", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 48, Area = "LineaDeCarrera", Nombre = "Comparar capacidades", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 49, Area = "LineaDeCarrera", Nombre = "Candidatos por puesto", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 50, Area = "Reportes", Nombre = "Reporte de Evaluacion 360", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 51, Area = "Reportes", Nombre = "Reporte historico de Objetivos", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 52, Area = "Reportes", Nombre = "Reporte de Cubrimiento de Puestos", EsWeb = false, Permiso = false });
            tablaRoles.Add(new Rol { ID = x + 53, Area = "Reportes", Nombre = "Reporte de Objetivos BSC", EsWeb = false, Permiso = false });

            foreach (Rol r in tablaRoles)
            {
                TablaRoles.AddElement(r);
            }

            return tablaRoles;
        }

        public  List<Rol> SeedRolesAdmin()
        {
            List<Rol> tablaRoles = new List<Rol>();
            int x = TablaRoles.All().Count;
            tablaRoles.Add(new Rol { Titulo = "Competencias", ID = x + 1, Area = "Evaluacion360", Nombre = "Competencias",  EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Capacidades", ID = x + 2, Area = "Evaluacion360", Nombre = "Capacidades", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Evaluación de puestos de trabajo", ID = x + 3, Area = "Evaluacion360", Nombre = "PuestosEvaluacion", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Procesos de evaluación", ID = x + 4, Area = "Evaluacion360", Nombre = "ProcesoEvaluacion", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Mis evaluaciones", ID = x + 5, Area = "Evaluacion360", Nombre = "ListarProcesosXEvaluado", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Mis pendientes", ID = x + 6, Area = "Evaluacion360", Nombre = "ListarProcesosXEvaluador", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Mis colaboradores", ID = x + 7, Area = "Evaluacion360", Nombre = "Subordinados", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Objetivos de la empresa",  ID = x + 8, Area = "Objetivos", Nombre = "Objetivosempresa", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Monitoreo en mi equipo de trabajo",ID = x + 9, Area = "Objetivos", Nombre = "Acordion", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Mis objetivos", ID = x + 10, Area = "Objetivos", Nombre = "Misobjetivos", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Objetivos de subordinados", ID = x + 11, Area = "Objetivos", Nombre = "Objetivossubordinados", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Periodos", ID = x + 12, Area = "Configuracion", Nombre = "Periodos", EsWeb = true, Permiso = true });
            
            tablaRoles.Add(new Rol { Titulo = "Permisos a Web", ID = x + 13, Area = "Seguridad", Nombre = "Usuarios", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Permisos a Movil",  ID = x + 14, Area = "Seguridad", Nombre = "UsuariosMovil", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Asignar Usario", ID = x + 15, Area = "Seguridad", Nombre = "AsignarCredenciales", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Nuevos Usuarios", ID = x + 16, Area = "Seguridad", Nombre = "CrearUsuario", EsWeb = true, Permiso = true });

            tablaRoles.Add(new Rol { Titulo = "Orgazacion", ID = x + 17, Area = "Organizacion", Nombre = "Organizaciones", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Organigrama", ID = x + 18, Area = "Organizacion", Nombre = "Organigrama", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Areas", ID = x + 19, Area = "Organizacion", Nombre = "Areas", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Colaboradores", ID = x + 20, Area = "Organizacion", Nombre = "Colaboradores", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Funciones", ID = x + 21, Area = "Organizacion", Nombre = "Funciones", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Puestos", ID = x + 22, Area = "Organizacion", Nombre = "Puestos", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Linea de carrera", ID = x + 23, Area = "Organizacion", Nombre = "Historial", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Pagina persona", ID = x + 24, Area = "Organizacion", Nombre = "Intranet", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Ofertas laborales", ID = x + 25, Area = "Reclutamiento", Nombre = "SolicitudOfertasLaborales", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Solicitud de promocion", ID = x + 26, Area = "Reclutamiento", Nombre = "SolicitudPromociones", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Administrar ofertas laborales internas", ID = x + 27, Area = "Reclutamiento", Nombre = "OfertasLaboralesInternas", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Administrar ofertas laborales externas", ID = x + 28, Area = "Reclutamiento", Nombre = "OfertasLaboralesExternas", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Convocatoria interna", ID = x + 29, Area = "BolsaTrabajo", Nombre = "ConvocatoriasInternas", EsWeb = true, Permiso = true });
            tablaRoles.Add(new Rol { Titulo = "Eventos", ID = x + 30, Area = "Eventos", Nombre = "Eventos", EsWeb = true, Permiso = true });


            tablaRoles.Add(new Rol { ID = x + 31, Area = "Mi informacion", Nombre = "Informacion personal", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 32, Area = "Mi informacion", Nombre = "Mi equipo de trabajo", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 33, Area = "Mi informacion", Nombre = "Mis contactos", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 34, Area = "Mi informacion", Nombre = "Agenda", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 35, Area = "Reclutamiento", Nombre = "Aprobar solicitudes de oferta laboral", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 36, Area = "Reclutamiento", Nombre = "Evaluar postulantes 1ra fase", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 37, Area = "Reclutamiento", Nombre = "Evaluar postulantes 3ra fase", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 38, Area = "Reclutamiento", Nombre = "Postular a oferta laboral", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 39, Area = "Evaluacion 360", Nombre = "Mis Pendientes", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 40, Area = "Evaluacion 360", Nombre = "Rol de Evaluado", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 41, Area = "Evaluacion 360", Nombre = "Mis subordinados", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 42, Area = "Objetivos", Nombre = "Objetivos Empresariales", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 43, Area = "Objetivos", Nombre = "Objetivos Propios", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 44, Area = "Objetivos", Nombre = "Objetivos para Equipo", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 45, Area = "Objetivos", Nombre = "Registrar Avance", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 46, Area = "Objetivos", Nombre = "Visualizar Avances", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 47, Area = "Objetivos", Nombre = "Monitoreo", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 48, Area = "LineaDeCarrera", Nombre = "Comparar capacidades", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 49, Area = "LineaDeCarrera", Nombre = "Candidatos por puesto", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 50, Area = "Reportes", Nombre = "Reporte de Evaluacion 360", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 51, Area = "Reportes", Nombre = "Reporte historico de Objetivos", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 52, Area = "Reportes", Nombre = "Reporte de Cubrimiento de Puestos", EsWeb = false, Permiso = true });
            tablaRoles.Add(new Rol { ID = x + 53, Area = "Reportes", Nombre = "Reporte de Objetivos BSC", EsWeb = false, Permiso = true });
           
            foreach(Rol r in tablaRoles)
            {
                TablaRoles.AddElement(r);    
            }

            return tablaRoles;
        }
    }
}
