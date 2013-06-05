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
        public DbSet<Area> InternalAreas { get; set; }
        public DbSet<Puesto> InternalPuestos { get; set; }
        public DbSet<EstadosPuesto> InternalEstadosPuestos { get; set; }
        public DbSet<PuestoXArea> InternalPuestosXAreas { get; set; }
        public DbSet<Persona> InternalPersonas { get; set; }
        public DbSet<Colaborador> InternalColaboradores { get; set; }
        public DbSet<Contactos> InternalContactos { get; set; }
         public DbSet<EstadosColaborador> InternalEstadosColaboradores { get; set; }
        public DbSet<TipoDocumento> InternalTiposDocumentos { get; set; }
        public DbSet<GradoAcademico> InternalGradosAcademicos { get; set; }
        public DbSet<ColaboradorXPuesto> InternalColaboradoresXPuestos { get; set; }
        public DbSet<Organizacion> InternalOrganizaciones { get; set; }
        public DbSet<Funcion> InternalFunciones { get; set; }
        public DbSet<FuncionXPuesto> InternalFuncionesXPuestos { get; set; }

        public DBGenericRequester<Area> TablaAreas { get; set; }
        public DBGenericRequester<Puesto> TablaPuestos { get; set; }
        public DBGenericRequester<EstadosPuesto> TablaEstadosPuestos { get; set; }
        public DBGenericRequester<PuestoXArea> TablaPuestosXAreas { get; set; }
        public DBGenericRequester<Persona> TablaPersonas { get; set; }
        public DBGenericRequester<Colaborador> TablaColaboradores { get; set; }
        public DBGenericRequester<Contactos> TablaContactos { get; set; }
        public DBGenericRequester<EstadosColaborador> TablaEstadosColaboradores { get; set; }
        public DBGenericRequester<TipoDocumento> TablaTiposDocumentos { get; set; }
        public DBGenericRequester<GradoAcademico> TablaGradosAcademicos { get; set; }
        public DBGenericRequester<ColaboradorXPuesto> TablaColaboradoresXPuestos { get; set; }
        public DBGenericRequester<Organizacion> TablaOrganizaciones { get; set; }
        public DBGenericRequester<Funcion> TablaFunciones { get; set; }
        public DBGenericRequester<FuncionXPuesto> TablaFuncionesXPuestos { get; set; }

        private void RegistrarTablasOrganizacion()
        {
            TablaAreas = new DBGenericRequester<Area>(this, InternalAreas);
            TablaPuestos = new DBGenericRequester<Puesto>(this, InternalPuestos);
            TablaEstadosPuestos = new DBGenericRequester<EstadosPuesto>(this, InternalEstadosPuestos);
            TablaPuestosXAreas = new DBGenericRequester<PuestoXArea>(this, InternalPuestosXAreas);
            TablaPersonas = new DBGenericRequester<Persona>(this, InternalPersonas);
            TablaColaboradores = new DBGenericRequester<Colaborador>(this, InternalColaboradores);
            TablaContactos = new DBGenericRequester<Contactos>(this, InternalContactos);
            TablaEstadosColaboradores = new DBGenericRequester<EstadosColaborador>(this, InternalEstadosColaboradores);
            TablaGradosAcademicos = new DBGenericRequester<GradoAcademico>(this, InternalGradosAcademicos);
            TablaTiposDocumentos = new DBGenericRequester<TipoDocumento>(this, InternalTiposDocumentos);
            TablaColaboradoresXPuestos = new DBGenericRequester<ColaboradorXPuesto>(this, InternalColaboradoresXPuestos);
            TablaOrganizaciones = new DBGenericRequester<Organizacion>(this, InternalOrganizaciones);
            TablaFunciones = new DBGenericRequester<Funcion>(this, InternalFunciones);
            TablaFuncionesXPuestos = new DBGenericRequester<FuncionXPuesto>(this, InternalFuncionesXPuestos);
        }

        private void SeedOrganizacion()
        {
            TablaOrganizaciones.AddElement(new Organizacion { RazonSocial = "Nueva organizacion" });
        }

        private void SeedColaboradores()
        {
            int idDNI = TablaTiposDocumentos.One(x => x.Descripcion.Equals("DNI")).ID;
            GradoAcademico gradoacademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller"));

            TablaColaboradores.AddElement(new Colaborador { Nombres = "Fortino Mario Alonso", ApellidoPaterno = "Moreno", ApellidoMaterno = "Reyes", Username = "admin", Password = "admin", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Miguel", ApellidoPaterno = "Vega", ApellidoMaterno = "Buendía", Username = "mvega", Password = "mvega", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Pako", ApellidoPaterno = "Sarmiento", ApellidoMaterno = "XXX", Username = "psarmiento", Password = "psarmiento", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Christian", ApellidoPaterno = "Mendez", ApellidoMaterno = "XXX", Username = "cmendez", Password = "cmendez", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Manuel", ApellidoPaterno = "Solorzano", ApellidoMaterno = "XXX", Username = "msolorzano", Password = "msolorzano", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Cesar", ApellidoPaterno = "Vasquez", ApellidoMaterno = "XXX", Username = "cvasquez", Password = "cvasquez", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Diana", ApellidoPaterno = "Lepage", ApellidoMaterno = "XXX", Username = "dlepage", Password = "dlepage", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Hans", ApellidoPaterno = "Espinoza", ApellidoMaterno = "XXX", Username = "hespinoza", Password = "hespinoza", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Pedro", ApellidoPaterno = "Curich", ApellidoMaterno = "XXX", Username = "pcurich", Password = "pcurich", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Andre", ApellidoPaterno = "Montoya", ApellidoMaterno = "XXX", Username = "amontoya", Password = "amontoya", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Daiana", ApellidoPaterno = "Castro", ApellidoMaterno = "XXX", Username = "dcastro", Password = "dcastro", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Juan", ApellidoPaterno = "Cahuin", ApellidoMaterno = "XXX", Username = "jcahuin", Password = "jcahuin", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Christian", ApellidoPaterno = "Perez", ApellidoMaterno = "XXX", Username = "cperez", Password = "cperez", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Ever", ApellidoPaterno = "Mitta", ApellidoMaterno = "XXX", Username = "emitta", Password = "emitta", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Walter", ApellidoPaterno = "Erquinigo", ApellidoMaterno = "XXX", Username = "werquinigo", Password = "werquinigo", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Katy", ApellidoPaterno = "Tucto", ApellidoMaterno = "XXX", Username = "ktucto", Password = "ktucto", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico, CorreoElectronico = "ktucto@gmail.com" });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Joao", ApellidoPaterno = "Chavez", ApellidoMaterno = "XXX", Username = "jchavez", Password = "jchavez", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico, CorreoElectronico = "ktucto+RH@gmail.com" });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Carlos", ApellidoPaterno = "Lengua", ApellidoMaterno = "XXX", Username = "clengua", Password = "clengua", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "Juan", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "jperez", Password = "jperez", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });

            Colaborador crios = new Colaborador { Nombres = "Carmen", ApellidoPaterno = "Rios", ApellidoMaterno = "De La Puente", Username = "crios", Password = "crios", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico };

            crios.Objetivos = new List<Objetivo> { 
                                        new Objetivo { Nombre = "Elaborar un plan de posgrado en administración", FechaFinalizacion = DateTime.Today.AddDays(7.0), Peso = 80, AvanceFinal = 10, TipoObjetivoBSCID = 1, BSCID = 1 },
                                        new Objetivo { Nombre = "Participar en talleres de gestión del talento", FechaFinalizacion = DateTime.Today.AddDays(7.0), Peso = 20, AvanceFinal = 10, TipoObjetivoBSCID = 1, BSCID = 1 }
                                        //new Objetivo { Nombre = "", FechaFinalizacion = DateTime.Today.AddDays(7.0), Peso = 10, AvanceFinal = 10 }
            };


            TablaColaboradores.AddElement(crios);

            Colaborador jquintana = new Colaborador { Nombres = "Jimena", ApellidoPaterno = "Quintana", ApellidoMaterno = "Alvarado", Username = "jquintana", Password = "jquintana", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1 };

            jquintana.Objetivos = new List<Objetivo> { 
                                        new Objetivo { Nombre = "Certificarme en gestión de proyectos", FechaFinalizacion = DateTime.Today.AddDays(7.0), Peso = 80, AvanceFinal = 10, TipoObjetivoBSCID = 1, BSCID = 1 },
                                        new Objetivo { Nombre = "Participar en talleres de gestión del talento", FechaFinalizacion = DateTime.Today.AddDays(7.0), Peso = 20, AvanceFinal = 10, TipoObjetivoBSCID = 1 , BSCID = 1}
            };

            TablaColaboradores.AddElement(jquintana);

            Colaborador rchavez = new Colaborador { Nombres = "Rodrigo", ApellidoPaterno = "Chavez", ApellidoMaterno = "Moreno", Username = "rchavez", Password = "rchavez", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1 };

            rchavez.Objetivos = new List<Objetivo>();

            //Objetivo aumentarLasVentas = new Objetivo("Aumentar las ventas", 1, 50, 100); //100 es "no tiene padre"
            Objetivo aumentarLasVentas = new Objetivo("Aumentar las ventas", 1, 50, this); //100 es "no tiene padre"
            aumentarLasVentas.FechaFinalizacion = DateTime.Today.AddDays(7.0);
            //aumentarLasVentas.IsAsignadoAPersona = true;
            aumentarLasVentas.AvanceFinal = 10; //10%

            //Objetivo reducirCostos = new Objetivo("Reducir los costos", 1, 25, 100);
            Objetivo reducirCostos = new Objetivo("Reducir los costos", 1, 25, this);
            reducirCostos.FechaFinalizacion = DateTime.Today.AddDays(7.0);
            //reducirCostos.IsAsignadoAPersona = true;
            reducirCostos.AvanceFinal = 10; //10%

            //Objetivo ganarClientes = new Objetivo("Ganar nuevos clientes", 1, 25, 100);
            Objetivo ganarClientes = new Objetivo("Ganar nuevos clientes", 1, 25, this);
            ganarClientes.FechaFinalizacion = DateTime.Today.AddDays(7.0);
            //ganarClientes.IsAsignadoAPersona = true;
            ganarClientes.AvanceFinal = 10; //10%

            rchavez.Objetivos.Add(aumentarLasVentas);
            rchavez.Objetivos.Add(reducirCostos);
            rchavez.Objetivos.Add(ganarClientes);

            TablaColaboradores.AddElement(rchavez);        
        
        }

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
            TablaAreas.AddElement(new Area { Nombre = "Responsabilidad social", Descripcion = "No hace nada", AreaSuperiorID = 2 });
            TablaAreas.AddElement(new Area { Nombre = "Administración", Descripcion = "Administra", AreaSuperiorID = 2 });
            TablaAreas.AddElement(new Area { Nombre = "Finanzas", Descripcion = "Financia", AreaSuperiorID = 10 });
            TablaAreas.AddElement(new Area { Nombre = "Recursos Humanos", Descripcion = "Molesta", AreaSuperiorID = 10 });
        }

        private void SeedPuestos()
        {
            TablaPuestos.AddElement(new Puesto { Nombre = "Presidente", Descripcion = "Jefe de proyecto", AreaID = TablaAreas.One(a => a.Nombre.Equals("Directorio")).ID, PuestoSuperiorID = null });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente general", Descripcion = "Por ahí 1", AreaID = TablaAreas.One(a => a.Nombre.Equals("Gerencia general")).ID, PuestoSuperiorID = 1 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de ventas", Descripcion = "Por ahí 2", AreaID = TablaAreas.One(a => a.Nombre.Equals("Ventas")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de TI", Descripcion = "Por ahí 3", AreaID = TablaAreas.One(a => a.Nombre.Equals("TI")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de márketing", Descripcion = "Por ahí 4", AreaID = TablaAreas.One(a => a.Nombre.Equals("Márketing")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de operaciones", Descripcion = "Por ahí 5", AreaID = TablaAreas.One(a => a.Nombre.Equals("Operaciones")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de responsabilidad social", Descripcion = "Por ahí 6", AreaID = TablaAreas.One(a => a.Nombre.Equals("Responsabilidad social")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente administrativo", Descripcion = "Por ahí 7", AreaID = TablaAreas.One(a => a.Nombre.Equals("Administración")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de logística", Descripcion = "Por ahí 9", AreaID = TablaAreas.One(a => a.Nombre.Equals("Logística")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de recursos humanos", Descripcion = "Por ahí 10", AreaID = TablaAreas.One(a => a.Nombre.Equals("Recursos Humanos")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de finanzas", Descripcion = "Por ahí 11", AreaID = TablaAreas.One(a => a.Nombre.Equals("Finanzas")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Auditor en Jefe", Descripcion = "Por ahí 12", AreaID = TablaAreas.One(a => a.Nombre.Equals("Auditoría")).ID, PuestoSuperiorID = 2 });
        }


        private void SeedEstadosPuesto()
        {
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Asignado" });
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Vacante" });
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Inactivo" });
        }

        private void SeedColaboradorXPuesto()
        {
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 1, ColaboradorID = 2, Sueldo = 2300, FechaIngresoPuesto = new DateTime(2010, 1, 1), FechaSalidaPuesto = null, Comentarios = "Hizo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 2, ColaboradorID = 3, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 3, ColaboradorID = 4, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 4, ColaboradorID = 5, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 5, ColaboradorID = 23, Sueldo = 2000, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 6, ColaboradorID = 6, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 7, ColaboradorID = 7, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 8, ColaboradorID = 8, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 9, ColaboradorID = 9, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 10, ColaboradorID = 10, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 11, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 12, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 13, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 14, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 15, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 16, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 17, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 18, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 19, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 20, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 21, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 22, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
        }


        private void SeedFunciones()
        {
            TablaFunciones.AddElement(new Funcion { Nombre = "Hacer muchas cosas", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Presidir", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Figurar", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gerenciar generalmente", PuestoID = 2 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestión de capital humano", PuestoID = 2 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Dar rumbo al negocio", PuestoID = 2 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Vender", PuestoID = 3 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Reducir gastos de venta", PuestoID = 3 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Cerrar negocios", PuestoID = 3 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gobernar su TI", PuestoID = 4 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Seguridad", PuestoID = 4 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestión de activos", PuestoID = 4 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ganar clientes", PuestoID = 5 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Desperdiciar dinero", PuestoID = 5 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Creerse importante", PuestoID = 5 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Hacer", PuestoID = 6 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Operar", PuestoID = 6 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Producir", PuestoID = 6 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Negociar con comunidades", PuestoID = 7 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Promover programas de desarrollo", PuestoID = 7 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Responsabilizar", PuestoID = 7 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Administrar", PuestoID = 8 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestionar", PuestoID = 8 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ordenar", PuestoID = 8 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Pedir", PuestoID = 9 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Proveer", PuestoID = 9 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Prever", PuestoID = 9 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Contratar", PuestoID = 10 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ascender", PuestoID = 10 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Botar", PuestoID = 10 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Invertir", PuestoID = 11 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Presupuestar", PuestoID = 11 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Contar", PuestoID = 11 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Molestar 1", PuestoID = 12 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Molestar 2", PuestoID = 12 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Creerse muy importante", PuestoID = 12 });
        }
    }
}