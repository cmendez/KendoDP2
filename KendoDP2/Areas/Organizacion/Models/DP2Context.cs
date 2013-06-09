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

        
        
        //Modulo 3 - No modificar
        //private void SeedColaboradoresModuloTres()
        private void SeedColaboradorYPuestosModuloTres()
        {
            TablaColaboradores.AddElement(new Colaborador { ID = 1,  Nombres = "Fortino Mario Alonso", ApellidoPaterno = "Moreno", ApellidoMaterno = "Reyes", Username = "admin", Password = "admin", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")),Roles=TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 2, Nombres = "Miguel", ApellidoPaterno = "Vega", ApellidoMaterno = "Buendía", Username = "mvega", Password = "mvega", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 3, Nombres = "Pako", ApellidoPaterno = "Sarmiento", ApellidoMaterno = "XXX", Username = "psarmiento", Password = "psarmiento", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 4, Nombres = "Christian", ApellidoPaterno = "Mendez", ApellidoMaterno = "XXX", Username = "cmendez", Password = "cmendez", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 5, Nombres = "Manuel", ApellidoPaterno = "Solorzano", ApellidoMaterno = "XXX", Username = "msolorzano", Password = "msolorzano", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 6, Nombres = "Cesar", ApellidoPaterno = "Vasquez", ApellidoMaterno = "XXX", Username = "cvasquez", Password = "cvasquez", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 7, Nombres = "Diana", ApellidoPaterno = "Lepage", ApellidoMaterno = "XXX", Username = "dlepage", Password = "dlepage", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 8, Nombres = "Hans", ApellidoPaterno = "Espinoza", ApellidoMaterno = "XXX", Username = "hespinoza", Password = "hespinoza", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 9, Nombres = "Pedro", ApellidoPaterno = "Curich", ApellidoMaterno = "XXX", Username = "pcurich", Password = "pcurich", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID =10, Nombres = "Andre", ApellidoPaterno = "Montoya", ApellidoMaterno = "XXX", Username = "amontoya", Password = "amontoya", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID =11, Nombres = "Daiana", ApellidoPaterno = "Castro", ApellidoMaterno = "XXX", Username = "dcastro", Password = "dcastro", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID =12, Nombres = "Juan", ApellidoPaterno = "Cahuin", ApellidoMaterno = "XXX", Username = "jcahuin", Password = "jcahuin", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 13, Nombres = "Christian", ApellidoPaterno = "Perez", ApellidoMaterno = "XXX", Username = "cperez", Password = "cperez", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 14, Nombres = "Ever", ApellidoPaterno = "Mitta", ApellidoMaterno = "XXX", Username = "emitta", Password = "emitta", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 15, Nombres = "Walter", ApellidoPaterno = "Erquinigo", ApellidoMaterno = "XXX", Username = "werquinigo", Password = "werquinigo", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 16, Nombres = "Katy", ApellidoPaterno = "Tucto", ApellidoMaterno = "XXX", Username = "ktucto", Password = "ktucto", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, CorreoElectronico = "ktucto@gmail.com", GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 17, Nombres = "Joao", ApellidoPaterno = "Chavez", ApellidoMaterno = "XXX", Username = "jchavez", Password = "jchavez", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, CorreoElectronico = "ktucto+RH@gmail.com", GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 18, Nombres = "Carlos", ApellidoPaterno = "Lengua", ApellidoMaterno = "XXX", Username = "clengua", Password = "clengua", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller")), Roles = TablaRoles.All() });
            TablaColaboradores.AddElement(new Colaborador { ID = 19, Nombres = "Juan", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "jperez", Password = "jperez", TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID, PaisID = 1, EstadosColaboradorID = 1 });

            //modulo 3  - no tocar

            //1er nivel
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = TablaPuestos.One(perfil => perfil.Nombre.CompareTo("Gerente general") == 0).ID, ColaboradorID = TablaColaboradores.One(e => e.Nombres.CompareTo("colaborador modulo tres a") == 0).ID, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });
            
            //2do nivel
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = TablaPuestos.One(perfil => perfil.Nombre.CompareTo("Gerente de ventas") == 0).ID, ColaboradorID = TablaColaboradores.One(e => e.Nombres.CompareTo("colaborador modulo tres b") == 0).ID, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = TablaPuestos.One(perfil => perfil.Nombre.CompareTo("Gerente de TI") == 0).ID, ColaboradorID = TablaColaboradores.One(e => e.Nombres.CompareTo("colaborador modulo tres c") == 0).ID, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = TablaPuestos.One(perfil => perfil.Nombre.CompareTo("Gerente de márketing") == 0).ID, ColaboradorID = TablaColaboradores.One(e => e.Nombres.CompareTo("colaborador modulo tres d") == 0).ID, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = TablaPuestos.One(perfil => perfil.Nombre.CompareTo("Gerente de operaciones") == 0).ID, ColaboradorID = TablaColaboradores.One(e => e.Nombres.CompareTo("colaborador modulo tres e") == 0).ID, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = TablaPuestos.One(perfil => perfil.Nombre.CompareTo("Gerente de responsabilidad social") == 0).ID, ColaboradorID = TablaColaboradores.One(e => e.Nombres.CompareTo("colaborador modulo tres f") == 0).ID, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });

            //3er nivel

            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = TablaPuestos.One(perfil => perfil.Nombre.CompareTo("Jefe de Ventas area 1") == 0).ID, ColaboradorID = TablaColaboradores.One(e => e.Nombres.CompareTo("colaborador modulo tres g") == 0).ID, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = TablaPuestos.One(perfil => perfil.Nombre.CompareTo("Jefe de Ventas area 2") == 0).ID, ColaboradorID = TablaColaboradores.One(e => e.Nombres.CompareTo("colaborador modulo tres f") == 0).ID, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = TablaPuestos.One(perfil => perfil.Nombre.CompareTo("Analista de Ventas") == 0).ID, ColaboradorID = TablaColaboradores.One(e => e.Nombres.CompareTo("colaborador modulo tres h") == 0).ID, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = TablaPuestos.One(perfil => perfil.Nombre.CompareTo("Analista de Ventas") == 0).ID, ColaboradorID = TablaColaboradores.One(e => e.Nombres.CompareTo("colaborador modulo tres i") == 0).ID, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });


        }

        //Modulo 3 - No modificar
        //private void SeedObjetivos()
        private void SeedObjetivosModulo3()
        {

            TablaObjetivos.AddElement(new Objetivo());
        
        
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
    
            //MAS SEEEDS DE COLABORADORES
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres a", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosa", Password = "Objetivosa", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres b", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosb", Password = "Objetivosb", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres c", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosc", Password = "Objetivosc", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres d", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosd", Password = "Objetivosd", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres e", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivose", Password = "Objetivose", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres f", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosf", Password = "Objetivosf", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres g", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosg", Password = "Objetivosg", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres h", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosh", Password = "Objetivosh", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres i", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosi", Password = "Objetivosi", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres j", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosj", Password = "Objetivosj", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres k", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosk", Password = "Objetivosk", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres l", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosl", Password = "Objetivosl", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres m", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosm", Password = "Objetivosm", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });
            TablaColaboradores.AddElement(new Colaborador { Nombres = "colaborador modulo tres n", ApellidoPaterno = "Perez", ApellidoMaterno = "Fernández", Username = "Objetivosn", Password = "Objetivosn", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = gradoacademico });




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
            //  modulo 3: puesto a usarse - NO MODIFICAR
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente general", Descripcion = "Por ahí 1", AreaID = TablaAreas.One(a => a.Nombre.Equals("Gerencia general")).ID, PuestoSuperiorID = 1 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de ventas", Descripcion = "Por ahí 2", AreaID = TablaAreas.One(a => a.Nombre.Equals("Ventas")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de TI", Descripcion = "Por ahí 3", AreaID = TablaAreas.One(a => a.Nombre.Equals("TI")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de márketing", Descripcion = "Por ahí 4", AreaID = TablaAreas.One(a => a.Nombre.Equals("Márketing")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de operaciones", Descripcion = "Por ahí 5", AreaID = TablaAreas.One(a => a.Nombre.Equals("Operaciones")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de responsabilidad social", Descripcion = "Por ahí 6", AreaID = TablaAreas.One(a => a.Nombre.Equals("Responsabilidad social")).ID, PuestoSuperiorID = 2 });
            // no interesa
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente administrativo", Descripcion = "Por ahí 7", AreaID = TablaAreas.One(a => a.Nombre.Equals("Administración")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de logística", Descripcion = "Por ahí 9", AreaID = TablaAreas.One(a => a.Nombre.Equals("Logística")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de recursos humanos", Descripcion = "Por ahí 10", AreaID = TablaAreas.One(a => a.Nombre.Equals("Recursos Humanos")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de finanzas", Descripcion = "Por ahí 11", AreaID = TablaAreas.One(a => a.Nombre.Equals("Finanzas")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Auditor en Jefe", Descripcion = "Por ahí 12", AreaID = TablaAreas.One(a => a.Nombre.Equals("Auditoría")).ID, PuestoSuperiorID = 2 });
            
            /**mODULO 3 - NO MODIFICAR**/
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de Ventas area 1", Descripcion = "Por ahí 13", AreaID = TablaAreas.One(a => a.Nombre.Equals("Ventas")).ID, PuestoSuperiorID = 3 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de Ventas area 2", Descripcion = "Por ahí 13", AreaID = TablaAreas.One(a => a.Nombre.Equals("Ventas")).ID, PuestoSuperiorID = 3 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Analista de Ventas", Descripcion = "Por ahí 13", AreaID = TablaAreas.One(a => a.Nombre.Equals("Ventas")).ID, PuestoSuperiorID = 3 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de Ventas area 3", Descripcion = "Por ahí 14", AreaID = TablaAreas.One(a => a.Nombre.Equals("Ventas")).ID, PuestoSuperiorID = 3 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de TI area 1", Descripcion = "Por ahí 15", AreaID = TablaAreas.One(a => a.Nombre.Equals("TI")).ID, PuestoSuperiorID = 4 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de TI area 2", Descripcion = "Por ahí 16", AreaID = TablaAreas.One(a => a.Nombre.Equals("TI")).ID, PuestoSuperiorID = 4 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de márketing area 1", Descripcion = "Por ahí 17", AreaID = TablaAreas.One(a => a.Nombre.Equals("Márketing")).ID, PuestoSuperiorID = 5 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de márketing area 2", Descripcion = "Por ahí 18", AreaID = TablaAreas.One(a => a.Nombre.Equals("Márketing")).ID, PuestoSuperiorID = 5 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de operaciones area 1", Descripcion = "Por ahí 19", AreaID = TablaAreas.One(a => a.Nombre.Equals("Operaciones")).ID, PuestoSuperiorID = 6 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de operaciones area 2", Descripcion = "Por ahí 20", AreaID = TablaAreas.One(a => a.Nombre.Equals("Operaciones")).ID, PuestoSuperiorID = 6 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de responsabilidad area 1", Descripcion = "Por ahí 21", AreaID = TablaAreas.One(a => a.Nombre.Equals("Responsabilidad social")).ID, PuestoSuperiorID = 7 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de responsabilidad area 2", Descripcion = "Por ahí 22", AreaID = TablaAreas.One(a => a.Nombre.Equals("Responsabilidad social")).ID, PuestoSuperiorID = 7 });
        }


        private void SeedEstadosPuesto()
        {
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Asignado" });
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Vacante" });
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Inactivo" });
        }

        private void SeedColaboradorXPuesto()
        {
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 1, Puesto=TablaPuestos.One(p=>p.ID==1), ColaboradorID = 2, Colaborador= TablaColaboradores.One(i=>i.ID==2), Sueldo = 2300, FechaIngresoPuesto = new DateTime(2010, 1, 1), FechaSalidaPuesto = DateTime.MaxValue, Comentarios = "Hizo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 5, Puesto=TablaPuestos.One(p=>p.ID==5), ColaboradorID = 22, Colaborador = TablaColaboradores.One(i => i.ID == 23), Sueldo = 2000, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = DateTime.MaxValue, Comentarios = "Ninguno", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 3, ColaboradorID = 4, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 4, ColaboradorID = 5, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 5, ColaboradorID = 23, Sueldo = 2000, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Ninguno", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 6, ColaboradorID = 6, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 7, ColaboradorID = 7, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 8, ColaboradorID = 8, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            /*TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 9, ColaboradorID = 9, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 10, ColaboradorID = 10, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 11, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 12, ColaboradorID = 12, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 13, ColaboradorID = 13, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 14, ColaboradorID = 14, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 15, ColaboradorID = 15, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 16, ColaboradorID = 16, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 17, ColaboradorID = 17, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 18, ColaboradorID = 18, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 19, ColaboradorID = 19, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 20, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            */
            ///modulo 3  - no tocar
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 13, ColaboradorID = 21, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 14, ColaboradorID = 22, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 15, ColaboradorID = 23, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 16, ColaboradorID = 24, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 17, ColaboradorID = 25, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 18, ColaboradorID = 26, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 19, ColaboradorID = 27, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 20, ColaboradorID = 28, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 21, ColaboradorID = 29, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 22, ColaboradorID = 30, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 23, ColaboradorID = 31, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 24, ColaboradorID = 32, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });        
        

            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 20, ColaboradorID = 33, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 21, ColaboradorID = 34, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 22, ColaboradorID = 14, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            //TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 23, ColaboradorID = 15, Sueldo = 2500, FechaIngresoPuesto = new DateTime(2011, 1, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
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