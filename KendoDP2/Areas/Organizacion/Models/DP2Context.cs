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
using System.IO;
using System.Web.Hosting;

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
        //public DbSet<FuncionXPuesto> InternalFuncionesXPuestos { get; set; }
        public DbSet<AColor> InternalColores { get; set; }

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
        //public DBGenericRequester<FuncionXPuesto> TablaFuncionesXPuestos { get; set; }
        public DBGenericRequester<AColor> TablaColores { get; set; }

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
            //TablaFuncionesXPuestos = new DBGenericRequester<FuncionXPuesto>(this, InternalFuncionesXPuestos);
            TablaColores = new DBGenericRequester<AColor>(this, InternalColores);
        }

        private void SeedImagenes()
        {
            string path;
            string[] imagenes = { "cantinflas", "miguel", "paco", "christian", "manuel", "cesar", "diana", "hans", "pedro", "dios", "daiana", "cahuin", "mono", "ever", "walter", "katherine", "joao", "lengua" };
            
            foreach(string filename in imagenes){
                path = HostingEnvironment.ApplicationPhysicalPath + "Images\\rhpic\\" + filename + ".jpg";
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                Archivo a = new Archivo { Data = bytes, Nombre = filename, Mime = "image/jpg" };
                TablaArchivos.AddElement(a);
            }
        }

        private void SeedOrganizacion()
        {
            TablaOrganizaciones.AddElement(new Organizacion { RazonSocial = "Nueva organizacion" });
        }

        //Modulo 3 - No modificar
        //private void SeedObjetivos()
        private void SeedObjetivosModulo3()
        {

            TablaObjetivos.AddElement(new Objetivo());
        }

        private void SeedColaboradores()
        {
            //Los colaboradores x default tienen todos los roles asignados pero inactivos
            //el modulo de seguridad es el encargado de actualizar y brindarles permisos para que se cree los menus x roles
            // responsable Pedro Curich
            int idDNI = TablaTiposDocumentos.One(x => x.Descripcion.Equals("DNI")).ID;
            var doctor = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Doctor"));
            var master = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Master"));
            var licenciado = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Licenciado"));
            var bachiller = TablaGradosAcademicos.One(x => x.Descripcion.Equals("Bachiller"));
            List<Rol> allRoles = TablaRoles.All();

            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 1, Nombres = "Fortino Mario Alonso", ApellidoPaterno = "Moreno", ApellidoMaterno = "Reyes", Username = "admin", Password = "admin", CorreoElectronico = "cantinflas@rh.com", Telefono = "4456362", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = doctor, CentroEstudios = "PUCP", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 2, Nombres = "Miguel", ApellidoPaterno = "Vega", ApellidoMaterno = "Buendía", Username = "mvega", Password = "mvega", CorreoElectronico = "miguel@rh.com", Telefono = "4567898", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = master, CentroEstudios = "PUCP", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 3, Nombres = "Franscisco", ApellidoPaterno = "Sarmiento", ApellidoMaterno = "Cumpa", Username = "psarmiento", Password = "psarmiento", CorreoElectronico = "francisco@rh.com", Telefono = "5367859", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = bachiller, CentroEstudios = "UNI", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 4, Nombres = "Christian", ApellidoPaterno = "Mendez", ApellidoMaterno = "Anchante", Username = "cmendez", Password = "cmendez", CorreoElectronico = "christian@rh.com", Telefono = "1345678", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = bachiller, CentroEstudios = "UNI", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 5, Nombres = "Manuel", ApellidoPaterno = "Solorzano", ApellidoMaterno = "Cabeza", Username = "msolorzano", Password = "msolorzano", CorreoElectronico = "manolin@rh.com", Telefono = "5674245", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = bachiller, CentroEstudios = "PUCP", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 6, Nombres = "Cesar", ApellidoPaterno = "Vasquez", ApellidoMaterno = "Flores", Username = "cvasquez", Password = "cvasquez", CorreoElectronico = "cesarin@rh.com", Telefono = "5674896", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = bachiller, CentroEstudios = "UNMS", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 7, Nombres = "Diana", ApellidoPaterno = "Lepage", ApellidoMaterno = "Hoces", Username = "dlepage", Password = "dlepage", CorreoElectronico = "diana@rh.com", Telefono = "8643456", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = bachiller, CentroEstudios = "UNMS", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 8, Nombres = "Hans", ApellidoPaterno = "Espinoza", ApellidoMaterno = "Aguinaga", Username = "hespinoza", Password = "hespinoza", CorreoElectronico = "hans@rh.com", Telefono = "54678969", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = bachiller, CentroEstudios = "UNI", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 9, Nombres = "Pedro", ApellidoPaterno = "Curich", ApellidoMaterno = "Gonzales", Username = "pcurich", Password = "pcurich", CorreoElectronico = "pedro@rh.com", Telefono = "76575767", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = bachiller, CentroEstudios = "PUCP", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 10, Nombres = "Andre", ApellidoPaterno = "Montoya", ApellidoMaterno = "Del Pino", Username = "amontoya", Password = "amontoya", CorreoElectronico = "dios@rh.com", Telefono = "7657567", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = bachiller, CentroEstudios = "UNI", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 11, Nombres = "Daiana", ApellidoPaterno = "Castro", ApellidoMaterno = "Marquina", Username = "dcastro", Password = "dcastro", CorreoElectronico = "daiana@rh.com", Telefono = "34566585", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = bachiller, CentroEstudios = "U. DE LIMA", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 12, Nombres = "Juan", ApellidoPaterno = "Cahuin", ApellidoMaterno = "Medina", Username = "jcahuin", Password = "jcahuin", CorreoElectronico = "cahuin@rh.com", Telefono = "36756585", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = bachiller, CentroEstudios = "PUCP", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 13, Nombres = "Christian", ApellidoPaterno = "Perez", ApellidoMaterno = "Ortiz", Username = "cperez", Password = "cperez", CorreoElectronico = "mono@rh.com", Telefono = "67567576", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = licenciado, CentroEstudios = "U. DE LIMA", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 14, Nombres = "Ever", ApellidoPaterno = "Mitta", ApellidoMaterno = "Flores", Username = "emitta", Password = "emitta", CorreoElectronico = "ever@rh.com", Telefono = "6756753", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = licenciado, CentroEstudios = "U. DE LIMA", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 15, Nombres = "Walter", ApellidoPaterno = "Erquinigo", ApellidoMaterno = "Pezo", Username = "werquinigo", Password = "werquinigo", CorreoElectronico = "walter@rh.com", Telefono = "97645585", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = licenciado, CentroEstudios = "U. DE LIMA", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 16, Nombres = "Katy", ApellidoPaterno = "Tucto", ApellidoMaterno = "Romero", Username = "ktucto", Password = "ktucto", CorreoElectronico = "ktucto@gmail.com", Telefono = "34243244", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = licenciado, CentroEstudios = "U. DE LIMA", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 17, Nombres = "Joao", ApellidoPaterno = "Chavez", ApellidoMaterno = "Yrigoyen", Username = "jchavez", Password = "jchavez", CorreoElectronico = "ktucto+RH@gmail.com", Telefono = "34234234", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = licenciado, CentroEstudios = "U. DE LIMA", Roles = allRoles });
            TablaColaboradores.AddElement(new Colaborador { ImagenColaboradorID = 18, Nombres = "Carlos", ApellidoPaterno = "Lengua", ApellidoMaterno = "Lafosse", Username = "clengua", Password = "clengua", CorreoElectronico = "lengua@rh.com", Telefono = "34234234", TipoDocumentoID = idDNI, PaisID = 1, EstadosColaboradorID = 1, GradoAcademico = licenciado, CentroEstudios = "U. DE LIMA", Roles = allRoles });
    
        }

        private void SeedContactos()
        {
            List<Colaborador> colaboradores = TablaColaboradores.All();
            foreach (Colaborador c in colaboradores)
            {
                if (c.Contactos == null) c.Contactos = new List<Contactos>();
                foreach (Colaborador c2 in colaboradores.Where(x => !x.ID.Equals(c.ID) && x.ID<5 ))
                {
                    c.Contactos.Add(new Contactos { ColaboradorID = c.ID, ContactoID = c2.ID, Relacion="Organización" });
                    
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
            TablaColores.AddElement(new AColor { ID = 1, Color = "Amarillo", Text = "yellow" });
            TablaColores.AddElement(new AColor { ID = 2, Color = "Rojo", Text = "red" });
            TablaColores.AddElement(new AColor { ID = 3, Color = "Negro", Text = "black" });
            TablaColores.AddElement(new AColor { ID = 4, Color = "Verde", Text = "green" });
            TablaColores.AddElement(new AColor { ID = 5, Color = "Azul", Text = "blue" });
            TablaColores.AddElement(new AColor { ID = 6, Color = "Naranja", Text = "orange" });
            TablaColores.AddElement(new AColor { ID = 7, Color = "Rosado", Text = "pink" });

            TablaAreas.AddElement(new Area { Nombre = "Directorio", Descripcion = "El área más grande", ColorID = 4 });

            TablaAreas.AddElement(new Area { Nombre = "Gerencia general", Descripcion = "Debajo de la gran área", AreaSuperiorID = 1, ColorID = 4 });
            TablaAreas.AddElement(new Area { Nombre = "Auditoría", Descripcion = "Innecesaria", AreaSuperiorID = 1, ColorID = 2, IsAudit = true });

            TablaAreas.AddElement(new Area { Nombre = "Ventas", Descripcion = "Algo útil por lo menos...", AreaSuperiorID = 2, ColorID = 5 });
            TablaAreas.AddElement(new Area { Nombre = "TI", Descripcion = "Para creernos importantes", AreaSuperiorID = 2, ColorID = 4 });
            TablaAreas.AddElement(new Area { Nombre = "Márketing", Descripcion = "Propaganda", AreaSuperiorID = 2, ColorID = 4 });
            TablaAreas.AddElement(new Area { Nombre = "Operaciones", Descripcion = "Vendemos pan", AreaSuperiorID = 2, ColorID = 3 });
            TablaAreas.AddElement(new Area { Nombre = "Logística", Descripcion = "Trae la masa", AreaSuperiorID = 2, ColorID = 4 });
            TablaAreas.AddElement(new Area { Nombre = "RRSS", Descripcion = "No hace nada", AreaSuperiorID = 2, ColorID = 5 });
            TablaAreas.AddElement(new Area { Nombre = "Administración", Descripcion = "Administra", AreaSuperiorID = 2, ColorID = 3 });
            TablaAreas.AddElement(new Area { Nombre = "Finanzas", Descripcion = "Financia", AreaSuperiorID = 2, ColorID = 5 });
            TablaAreas.AddElement(new Area { Nombre = "RRHH", Descripcion = "Molesta", AreaSuperiorID = 2, ColorID = 3 });

        }

        private void SeedPuestos()
        {
            TablaPuestos.AddElement(new Puesto { Nombre = "Presidente", Descripcion = "Jefe de proyecto", AreaID = TablaAreas.One(a => a.Nombre.Equals("Directorio")).ID, PuestoSuperiorID = null });
            
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente general", Descripcion = "Por ahí 1", AreaID = TablaAreas.One(a => a.Nombre.Equals("Gerencia general")).ID, PuestoSuperiorID = 1 });
            
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de ventas", Descripcion = "Por ahí 2", AreaID = TablaAreas.One(a => a.Nombre.Equals("Ventas")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de TI", Descripcion = "Por ahí 3", AreaID = TablaAreas.One(a => a.Nombre.Equals("TI")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de márketing", Descripcion = "Por ahí 4", AreaID = TablaAreas.One(a => a.Nombre.Equals("Márketing")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de operaciones", Descripcion = "Por ahí 5", AreaID = TablaAreas.One(a => a.Nombre.Equals("Operaciones")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de RRSS", Descripcion = "Por ahí 6", AreaID = TablaAreas.One(a => a.Nombre.Equals("RRSS")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente administrativo", Descripcion = "Por ahí 7", AreaID = TablaAreas.One(a => a.Nombre.Equals("Administración")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de logística", Descripcion = "Por ahí 9", AreaID = TablaAreas.One(a => a.Nombre.Equals("Logística")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de RRHH", Descripcion = "Por ahí 10", AreaID = TablaAreas.One(a => a.Nombre.Equals("RRHH")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Gerente de finanzas", Descripcion = "Por ahí 11", AreaID = TablaAreas.One(a => a.Nombre.Equals("Finanzas")).ID, PuestoSuperiorID = 2 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Auditor Jefe", Descripcion = "Por ahí 12", AreaID = TablaAreas.One(a => a.Nombre.Equals("Auditoría")).ID, PuestoSuperiorID = 2 });
            
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de Ventas", Descripcion = "Por ahí 13", AreaID = TablaAreas.One(a => a.Nombre.Equals("Ventas")).ID, PuestoSuperiorID = 3 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de TI", Descripcion = "Por ahí 15", AreaID = TablaAreas.One(a => a.Nombre.Equals("TI")).ID, PuestoSuperiorID = 4 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de Márketing", Descripcion = "Por ahí 17", AreaID = TablaAreas.One(a => a.Nombre.Equals("Márketing")).ID, PuestoSuperiorID = 5 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de Operaciones", Descripcion = "Por ahí 19", AreaID = TablaAreas.One(a => a.Nombre.Equals("Operaciones")).ID, PuestoSuperiorID = 6 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de RRSS", Descripcion = "Por ahí 22", AreaID = TablaAreas.One(a => a.Nombre.Equals("RRSS")).ID, PuestoSuperiorID = 7 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe Administrativo", Descripcion = "Por ahí 24", AreaID = TablaAreas.One(a => a.Nombre.Equals("Administración")).ID, PuestoSuperiorID = 8 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de Logística", Descripcion = "Por ahí 25", AreaID = TablaAreas.One(a => a.Nombre.Equals("Logística")).ID, PuestoSuperiorID = 9 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de RRHH", Descripcion = "Por ahí 28", AreaID = TablaAreas.One(a => a.Nombre.Equals("RRHH")).ID, PuestoSuperiorID = 10 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe de Finanzas", Descripcion = "Por ahí 30", AreaID = TablaAreas.One(a => a.Nombre.Equals("Finanzas")).ID, PuestoSuperiorID = 11 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Jefe Auditor", Descripcion = "Por ahí 32", AreaID = TablaAreas.One(a => a.Nombre.Equals("Auditoría")).ID, PuestoSuperiorID = 12 });

            TablaPuestos.AddElement(new Puesto { Nombre = "Practicante de Ventas", Descripcion = "Por ahí 13", AreaID = TablaAreas.One(a => a.Nombre.Equals("Ventas")).ID, PuestoSuperiorID = 13 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Practicante de TI", Descripcion = "Por ahí 15", AreaID = TablaAreas.One(a => a.Nombre.Equals("TI")).ID, PuestoSuperiorID = 14 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Practicante de Márketing", Descripcion = "Por ahí 17", AreaID = TablaAreas.One(a => a.Nombre.Equals("Márketing")).ID, PuestoSuperiorID = 15 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Practicante de Operaciones", Descripcion = "Por ahí 19", AreaID = TablaAreas.One(a => a.Nombre.Equals("Operaciones")).ID, PuestoSuperiorID = 16 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Practicante de RRSS", Descripcion = "Por ahí 22", AreaID = TablaAreas.One(a => a.Nombre.Equals("RRSS")).ID, PuestoSuperiorID = 17 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Practicante de Administración", Descripcion = "Por ahí 24", AreaID = TablaAreas.One(a => a.Nombre.Equals("Administración")).ID, PuestoSuperiorID = 18 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Practicante de Logística", Descripcion = "Por ahí 25", AreaID = TablaAreas.One(a => a.Nombre.Equals("Logística")).ID, PuestoSuperiorID = 19 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Practicante de RRHH", Descripcion = "Por ahí 28", AreaID = TablaAreas.One(a => a.Nombre.Equals("RRHH")).ID, PuestoSuperiorID = 20 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Practicante de Finanzas", Descripcion = "Por ahí 30", AreaID = TablaAreas.One(a => a.Nombre.Equals("Finanzas")).ID, PuestoSuperiorID = 21 });
            TablaPuestos.AddElement(new Puesto { Nombre = "Practicante de Auditoría", Descripcion = "Por ahí 32", AreaID = TablaAreas.One(a => a.Nombre.Equals("Auditoría")).ID, PuestoSuperiorID = 22 });
        }


        private void SeedEstadosPuesto()
        {
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Asignado" });
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Vacante" });
            TablaEstadosPuestos.AddElement(new EstadosPuesto { Descripcion = "Inactivo" });
        }

        private void SeedColaboradorXPuesto()
        {
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 1, ColaboradorID = 1, Sueldo = 20000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 2, ColaboradorID = 2, Sueldo = 18000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 3, ColaboradorID = 3, Sueldo = 11000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 4, ColaboradorID = 4, Sueldo = 11000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 5, ColaboradorID = 5, Sueldo = 11000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 6, ColaboradorID = 6, Sueldo = 11000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 7, ColaboradorID = 7, Sueldo = 11000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 8, ColaboradorID = 8, Sueldo = 11000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 9, ColaboradorID = 9, Sueldo = 11000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 10, ColaboradorID = 10, Sueldo = 11000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 11, ColaboradorID = 11, Sueldo = 11000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 12, ColaboradorID = 12, Sueldo = 11000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 13, ColaboradorID = 13, Sueldo = 7000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 14, ColaboradorID = 14, Sueldo = 7000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 15, ColaboradorID = 15, Sueldo = 7000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 16, ColaboradorID = 16, Sueldo = 7000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 17, ColaboradorID = 17, Sueldo = 7000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            TablaColaboradoresXPuestos.AddElement(new ColaboradorXPuesto { PuestoID = 18, ColaboradorID = 18, Sueldo = 7000, FechaIngresoPuesto = new DateTime(2013, 3, 1), FechaSalidaPuesto = null, Comentarios = "Continua haciendo una gran labor", IsEliminado = false });
            
        }


        private void SeedFunciones()
        {
            TablaFunciones.AddElement(new Funcion { Nombre = "Hacer muchas cosas", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Presidir", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Figurar", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestionar los recursos", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Liderar al equipo", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Motivar a la organización", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Dirigir a la junta directiva", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ganar mucho dinero", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Delegar", PuestoID = 1 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Empoderar", PuestoID = 1 });

            TablaFunciones.AddElement(new Funcion { Nombre = "Gerenciar generalmente", PuestoID = 2 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestión de capital humano", PuestoID = 2 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Dar rumbo al negocio", PuestoID = 2 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestionar los recursos", PuestoID = 2 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Motivar al equipo", PuestoID = 2 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ganar mucho dinero", PuestoID = 2 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Delegar", PuestoID = 2 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Empoderar", PuestoID = 2 });


            TablaFunciones.AddElement(new Funcion { Nombre = "Vender", PuestoID = 3 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Reducir gastos de venta", PuestoID = 3 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Cerrar negocios", PuestoID = 3 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Motivar al equipo", PuestoID = 3 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ganar mucho dinero", PuestoID = 3 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Delegar", PuestoID = 3 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Empoderar", PuestoID = 3});
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestionar los recursos", PuestoID = 3 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Liderar al equipo", PuestoID = 3 });

            TablaFunciones.AddElement(new Funcion { Nombre = "Gobernar su TI", PuestoID = 4 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Seguridad", PuestoID = 4 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestión de activos", PuestoID = 4 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Motivar al equipo", PuestoID = 4 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ganar mucho dinero", PuestoID = 4 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Delegar", PuestoID = 4 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Empoderar", PuestoID = 4 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestionar los recursos", PuestoID = 4 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Liderar al equipo", PuestoID = 4 });

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

            TablaFunciones.AddElement(new Funcion { Nombre = "Hacer muchas cosas", PuestoID = 13 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Presidir", PuestoID = 13 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Figurar", PuestoID = 13 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gerenciar generalmente", PuestoID = 14 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestión de capital humano", PuestoID = 14 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Dar rumbo al negocio", PuestoID = 14 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Vender", PuestoID = 15 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Reducir gastos de venta", PuestoID = 15 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Cerrar negocios", PuestoID = 15 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gobernar su TI", PuestoID = 16 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Seguridad", PuestoID = 16 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestión de activos", PuestoID = 16 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ganar clientes", PuestoID = 17 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Desperdiciar dinero", PuestoID = 17 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Creerse importante", PuestoID = 17 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Hacer", PuestoID = 18 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Operar", PuestoID = 18 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Producir", PuestoID = 18 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Negociar con comunidades", PuestoID = 19 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Promover programas de desarrollo", PuestoID = 19 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Responsabilizar", PuestoID = 19 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Administrar", PuestoID = 20 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestionar", PuestoID = 20 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ordenar", PuestoID = 20 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Pedir", PuestoID = 21 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Proveer", PuestoID = 21 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Prever", PuestoID = 21 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Contratar", PuestoID = 22 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ascender", PuestoID = 22 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Botar", PuestoID = 22 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Invertir", PuestoID = 23 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Presupuestar", PuestoID = 23 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Contar", PuestoID = 23 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Molestar 1", PuestoID = 24 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Molestar 2", PuestoID = 24 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Creerse muy importante", PuestoID = 24 });

            TablaFunciones.AddElement(new Funcion { Nombre = "Hacer muchas cosas", PuestoID = 25 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Presidir", PuestoID = 25 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Figurar", PuestoID = 25 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gerenciar generalmente", PuestoID = 26 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestión de capital humano", PuestoID = 26 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Dar rumbo al negocio", PuestoID = 26 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Vender", PuestoID = 27 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Reducir gastos de venta", PuestoID = 27 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Cerrar negocios", PuestoID = 27 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gobernar su TI", PuestoID = 28 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Seguridad", PuestoID = 28 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestión de activos", PuestoID = 28 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ganar clientes", PuestoID = 29 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Desperdiciar dinero", PuestoID = 29 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Creerse importante", PuestoID = 29 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Hacer", PuestoID = 30 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Operar", PuestoID = 30 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Producir", PuestoID = 30 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Negociar con comunidades", PuestoID = 31 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Promover programas de desarrollo", PuestoID = 31 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Responsabilizar", PuestoID = 31 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Administrar", PuestoID = 32 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Gestionar", PuestoID = 32 });
            TablaFunciones.AddElement(new Funcion { Nombre = "Ordenar", PuestoID = 32 });
            
        }
    }
}