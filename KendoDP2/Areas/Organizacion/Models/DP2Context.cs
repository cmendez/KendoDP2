﻿using KendoDP2.Areas.Configuracion.Models;
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
        public DbSet<Organizacion> InternalOrganizaciones { get; set; }

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
        public DBGenericRequester<Organizacion> TablaOrganizaciones { get; set; }


        private void RegistrarTablasOrganizacion()
        {
            TablaAreas = new DBGenericRequester<Area>(this, InternalAreas);
            TablaPuestos = new DBGenericRequester<Puesto>(this, InternalPuestos);
            TablaEstadosPuestos = new DBGenericRequester<EstadosPuesto>(this, InternalEstadosPuestos);
            TablaPuestosXAreas = new DBGenericRequester<PuestoXArea>(this, InternalPuestosXAreas);
            TablaPersonas = new DBGenericRequester<Persona>(this, InternalPersonas);
            TablaColaboradores = new DBGenericRequester<Colaborador>(this, InternalColaboradores);
            TablaEstadosColaboradores = new DBGenericRequester<EstadosColaborador>(this, InternalEstadosColaboradores);
            TablaGradosAcademicos = new DBGenericRequester<GradoAcademico>(this, InternalGradosAcademicos);
            TablaTiposDocumentos = new DBGenericRequester<TipoDocumento>(this, InternalTiposDocumentos);
            TablaColaboradoresXPuestos = new DBGenericRequester<ColaboradorXPuesto>(this, InternalColaboradoresXPuestos);
            TablaOrganizaciones = new DBGenericRequester<Organizacion>(this, InternalOrganizaciones);
        }

        private void SeedOrganizacion()
        {
            TablaOrganizaciones.AddElement(new Organizacion { RazonSocial = "Nueva organizacion" });
        }

        private void SeedColaboradores()
        {
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Fortino Mario Alonso", ApellidoPaterno = "Moreno", ApellidoMaterno = "Reyes", Username = "admin", Password = "admin", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Miguel", ApellidoPaterno = "Vega", ApellidoMaterno = "XXX", Username = "mvega", Password = "mvega", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Pako", ApellidoPaterno = "Sarmiento", ApellidoMaterno = "XXX", Username = "psarmiento", Password = "psarmiento", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Christian", ApellidoPaterno = "Mendez", ApellidoMaterno = "XXX", Username = "cmendez", Password = "cmendez", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Manuel", ApellidoPaterno = "Solorzano", ApellidoMaterno = "XXX", Username = "msolorzano", Password = "msolorzano", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Cesar", ApellidoPaterno = "Vasquez", ApellidoMaterno = "XXX", Username = "cvasquez", Password = "cvasquez", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Diana", ApellidoPaterno = "Lepage", ApellidoMaterno = "XXX", Username = "dlepage", Password = "dlepage", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Hans", ApellidoPaterno = "Espinoza", ApellidoMaterno = "XXX", Username = "hespinoza", Password = "hespinoza", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Pedro", ApellidoPaterno = "Curich", ApellidoMaterno = "XXX", Username = "pcurich", Password = "pcurich", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Andre", ApellidoPaterno = "Montoya", ApellidoMaterno = "XXX", Username = "amontoya", Password = "amontoya", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Daiana", ApellidoPaterno = "Castro", ApellidoMaterno = "XXX", Username = "dcastro", Password = "dcastro", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Juan", ApellidoPaterno = "Cahuin", ApellidoMaterno = "XXX", Username = "jcahuin", Password = "jcahuin", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Christian", ApellidoPaterno = "Perez", ApellidoMaterno = "XXX", Username = "cperez", Password = "cperez", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Ever", ApellidoPaterno = "Mitta", ApellidoMaterno = "XXX", Username = "emitta", Password = "emitta", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Walter", ApellidoPaterno = "Erquinigo", ApellidoMaterno = "XXX", Username = "werquinigo", Password = "werquinigo", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Katy", ApellidoPaterno = "Tucto", ApellidoMaterno = "XXX", Username = "ktucto", Password = "ktucto", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Joao", ApellidoPaterno = "Chavez", ApellidoMaterno = "XXX", Username = "jchavez", Password = "jchavez", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Carlos", ApellidoPaterno = "Lengua", ApellidoMaterno = "XXX", Username = "clengua", Password = "clengua", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });        }

        private void SeedContactos()
        {
            List<Colaborador> colaboradores = TablaColaboradores.All();
            foreach (Colaborador c in colaboradores)
            {
                if (c.Contactos == null) c.Contactos = new List<Contactos>();
                foreach (Colaborador c2 in colaboradores.Where(x => !x.ID.Equals(c.ID)))
                {
                    c.Contactos.Add(new Contactos { ColaboradorID = c.ID, ContactoID = c2.ID });
                }
                TablaColaboradores.ModifyElement(c);
            }
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
            TablaAreas.AddElement(new Area { Nombre = "Directorio", Descripcion = "El área más grande" });
            TablaAreas.AddElement(new Area { Nombre = "Gerencia general", Descripcion = "Debajo de la gran área", AreaSuperiorID = 1 });
            TablaAreas.AddElement(new Area { Nombre = "Auditoría", Descripcion = "Innecesaria", AreaSuperiorID = 1 });
            TablaAreas.AddElement(new Area { Nombre = "Ventas", Descripcion = "Algo útil por lo menos...", AreaSuperiorID = 2 });
            TablaAreas.AddElement(new Area { Nombre = "TI", Descripcion = "Para creernos importantes", AreaSuperiorID = 2 });
            TablaAreas.AddElement(new Area { Nombre = "Márketing", Descripcion = "Propaganda", AreaSuperiorID = 2 });
            TablaAreas.AddElement(new Area { Nombre = "Operaciones", Descripcion = "Vendemos pan", AreaSuperiorID = 2 });
            TablaAreas.AddElement(new Area { Nombre = "Logística", Descripcion = "Trae la masa", AreaSuperiorID = 7 });
            TablaAreas.AddElement(new Area { Nombre = "Responsabilidad Social", Descripcion = "No hace nada", AreaSuperiorID = 2 });
            TablaAreas.AddElement(new Area { Nombre = "Administración", Descripcion = "Administra", AreaSuperiorID = 2 });
            TablaAreas.AddElement(new Area { Nombre = "Finanzas", Descripcion = "Financia", AreaSuperiorID = 10 });
            TablaAreas.AddElement(new Area { Nombre = "Recursos humanos", Descripcion = "Molesta", AreaSuperiorID = 10 });
        }

        private void SeedPuestos()
        {
            TablaPuestos.AddElement(new Puesto { Nombre = "Presidente", Descripcion = "Jefe de proyecto", AreaID = TablaAreas.One(a => a.Nombre.Equals("Directorio")).ID });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente general", Descripcion = "Por ahí", AreaID = TablaAreas.One(a => a.Nombre.Equals("Gerencia general")).ID });

        }


        private void SeedEstadosPuesto()
        {
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Asignado" });
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Vacante" });
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Inactivo" });
        }
    }
}