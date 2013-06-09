using KendoDP2.Areas.Objetivos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Configuracion.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using System.ComponentModel.DataAnnotations.Schema;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Eventos.Models;
using System.Reflection;


namespace KendoDP2.Areas.Organizacion.Models
{
    public class Colaborador : Persona
    {
        public string Direccion { get; set; }
        public bool IsActivo { get; set; }
        public string Telefono { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaIngresoEmpresa {get; set;}
        public string FechaSalidaEmpresa { get; set; }

        public virtual ICollection<Objetivo> Objetivos { get; set; }
        
        public virtual ICollection<ColaboradorXPuesto> ColaboradoresPuesto { get; set; }

        public virtual ICollection<ColaboradorXProcesoEvaluacion> ColaboradorXProcesoEvaluaciones { get; set; }

        public int EstadosColaboradorID { get; set; }
        public virtual EstadosColaborador EstadoColaborador { get; set; }

        public int? PaisID { get; set; }
        public virtual Pais Pais { get; set; }
      
        public int ImagenColaboradorID { get; set; }

        [InverseProperty("Contacto")]
        public virtual ICollection<Contactos> EsContactoDe { get; set; }
        [InverseProperty("Colaborador")]
        public virtual ICollection<Contactos> Contactos { get; set; }

        public string ResumenEjecutivo { get; set; }

        [InverseProperty("Asistente")]
        public virtual ICollection<Invitado> EventosInvitado { get; set; }

        public Colaborador() { }

        public Colaborador(ColaboradorDTO c)
        {
            LoadFromDTO(c);
        }

        public Colaborador(Colaborador participanteDeEvaluacion)
        {
            Type t = participanteDeEvaluacion.GetType();
            //foreach (FieldInfo fieldInf in t.GetFields())
            //{
            //    fieldInf.SetValue(this, fieldInf.GetValue(participanteDeEvaluacion));
            //}
            foreach (PropertyInfo propInf in t.GetProperties())
            {
                propInf.SetValue(this, propInf.GetValue(participanteDeEvaluacion));
            }
        }

        public Colaborador LoadFromDTO(ColaboradorDTO c)
        {
            ID = c.ID;
            Nombres = c.Nombre;
            ApellidoPaterno = c.ApellidoPaterno;
            ApellidoMaterno = c.ApellidoMaterno;
            Direccion = c.Direccion;
            Telefono = c.Telefono;
            if(c.PaisID > 0) PaisID = c.PaisID;
            GradoAcademicoID = c.GradoAcademicoID;
            EstadosColaboradorID = c.EstadoColaboradorID;
            TipoDocumentoID = c.TipoDocumentoID;
            NumeroDocumento = c.NumeroDocumento;
            CorreoElectronico = c.CorreoElectronico;
            CentroEstudios = c.CentroEstudios;
            FechaNacimiento = c.FechaNacimiento;
            FechaIngresoEmpresa = c.FechaIngreso;
            ResumenEjecutivo = c.ResumenEjecutivo;
            ImagenColaboradorID = c.ImagenColaboradorID;
            CurriculumVitaeID = c.CurriculumVitaeID;
            
            return this;
        }


        new public ColaboradorDTO ToDTO()
        {
            return new ColaboradorDTO(this);
        }

        public NodoOrganigramaDTO ToNodoOrganigramaDTO(DP2Context context)
        {
            return new NodoOrganigramaDTO(context, this);
        }

        public ColaboradorDTO paraObservacion360()
        {
            return new ColaboradorEvaluadorDTO(this);
        }
    }

    public class ColaboradorDTO
    {
       
        public string NombreCompleto { get; set; }
        [DisplayName("Código")]
        public int ID { get; set; }
        
        [Required]
        [DisplayName("Nombre")]
        [StringLength(40)]
        public string Nombre { get; set; }
        
        [Required]
        [DisplayName("Apellido Paterno")]
        [StringLength(30)]
        public string ApellidoPaterno { get; set; }

        [Required]
        [DisplayName("Apellido Materno")]
        [StringLength(30)]
        public string ApellidoMaterno { get; set; }

        [DisplayName("Teléfono")]
        [StringLength(28)]
        public string Telefono { get; set; }

        [DisplayName("Dirección")]
        [StringLength(60)]
        public string Direccion { get; set; }
        
        [DisplayName("País")]
        public int PaisID { get; set; }
                
        [DisplayName("Fecha de Nacimiento")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaNacimiento { get; set; }

        [DisplayName("Tipo de Documento")]
        public int TipoDocumentoID { get; set; }

        [DisplayName("Número de Documento")]
        [StringLength(20)]
        public string NumeroDocumento { get; set; }

        [DisplayName("Usuario")]
        public string Usuario { get; set; }

        [DisplayName("Estado")]
        public int EstadoColaboradorID { get; set; }

        [DisplayName("Curriculum Vitae")]
        public int CurriculumVitaeID { get; set; }

        [DisplayName("Imagen")]
        public int ImagenColaboradorID { get; set; }

        [DisplayName("Centro de estudios")]
        [StringLength(100)]
        [Required]
        public string CentroEstudios { get; set; }

        [DisplayName("Grado Académico")]
        public int GradoAcademicoID { get; set; }

        [DisplayName("Correo Electrónico")]
        [StringLength(80)]
        public string CorreoElectronico { get; set; }
        
        [DisplayName("Ingreso Empresa")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaIngreso { get; set; }

        [DisplayName("Área")]
        [UIHint("GridForeignKey")]
        public int AreaID { get; set; }
        
        [DisplayName("Área")]
        public string Area { get; set; }
        
        [DisplayName("Puesto")]
        [UIHint("GridForeignKey")]
        public int PuestoID { get; set; }

        [DisplayName("Puesto")]
        public string Puesto { get; set; }

        [DisplayName("Sueldo S/.")]
        public int Sueldo { get; set; }

        [DisplayName("Sobre Mí")]
        [StringLength(350)]
        public string ResumenEjecutivo { get; set; }

        [DisplayName("Contraseña actual")]
        public string Contrasenha { get; set; }

        [DisplayName("Nueva Contraseña")]
        public string NuevaContrasenha { get; set; }

        public List<ObjetivoDTO> Objetivos { get; set; }

        public List<ColaboradorDTO> Subordinados { get; set; }

        public List<ContactosDTO> Contactos { get; set; }

        public ColaboradorDTO() { }

        public ColaboradorDTO(Colaborador c, List<ColaboradorDTO> listac = null)
        {
            NombreCompleto = c.ApellidoPaterno + " " + c.ApellidoMaterno + ", " + c.Nombres;
            ID = c.ID;
            GradoAcademicoID = c.GradoAcademicoID.GetValueOrDefault();
            PaisID = c.PaisID.GetValueOrDefault();
            Nombre = c.Nombres;
            ApellidoPaterno = c.ApellidoPaterno;
            ApellidoMaterno = c.ApellidoMaterno;
            TipoDocumentoID = c.TipoDocumentoID;
            CorreoElectronico = c.CorreoElectronico;
            CentroEstudios = c.CentroEstudios;
            EstadoColaboradorID = c.EstadosColaboradorID;
            NumeroDocumento = c.NumeroDocumento;
            Direccion = c.Direccion;
            Telefono = c.Telefono;
            CurriculumVitaeID = c.CurriculumVitaeID;
            ImagenColaboradorID = c.ImagenColaboradorID;
            FechaNacimiento = c.FechaNacimiento;
            FechaIngreso = c.FechaIngresoEmpresa;
            ResumenEjecutivo = c.ResumenEjecutivo;

            Usuario = c.Username;

            Subordinados = listac;

            try {
                ColaboradorXPuesto cruce = c.ColaboradoresPuesto.OrderByDescending(a => a.ID).First();
                AreaID = cruce.Puesto.AreaID;
                Area = cruce.Puesto.Area.Nombre;
                PuestoID = cruce.Puesto.ID;
                Puesto = cruce.Puesto.Nombre;
                Sueldo = cruce.Sueldo;
            } catch(Exception){
                AreaID = 0;
                PuestoID = 0;
                Sueldo = 0;
                Puesto = "";
                Sueldo = 0;
            }

            try
            {
                //Objetivos = c.Objetivos.Select(o => o.ToDTO()).ToList();
                Objetivos = c.Objetivos.Select(o => o.ToDTO(new DP2Context())).ToList();
                Contactos = c.Contactos.Select(o => o.ToDTO()).ToList();
            }
            catch (Exception)
            {
                //Objetivos no se han cargado
                Objetivos = new List<ObjetivoDTO>();
                Contactos = new List<ContactosDTO>();
            }


         }

        }

    public class ColaboradorDocumentosDTO
    {
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }

        public ColaboradorDocumentosDTO() { }

        public ColaboradorDocumentosDTO(Colaborador o)
        {
            if (o != null)
            {
                TipoDocumento = o.TipoDocumento.Descripcion;
                Documento = o.NumeroDocumento;
            }
        }
    }

    public class ColaboradorEvaluadorDTO : ColaboradorDTO
    {
        String FaseDeSuEvaluacion { get; set; }

        public ColaboradorEvaluadorDTO(Colaborador empleado) : base(empleado)
        {
        }

    }

    
}
