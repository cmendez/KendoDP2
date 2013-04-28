using KendoDP2.Areas.Objetivos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Configuracion.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KendoDP2.Areas.Personal.Models
{
    public class Colaborador : Persona
    {
        public string Direccion { get; set; }
        public bool IsActivo { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaIngresoEmpresa {get; set;}
        public DateTime? FechaSalidaEmpresa { get; set; }

        public virtual ICollection<Objetivo> Objetivos { get; set; }
        
        //public virtual ICollection<ColaboradorXPuesto> ColaboradoresPuesto { get; set; }
        //public int ColaboradorXPuestoID { get; set; }
        
        public int EstadosColaboradorID { get; set; }
        public virtual EstadosColaborador EstadoColaborador { get; set; }

        public int PaisID { get; set; }
        public virtual Pais Pais { get; set; }

        public Byte[] ImagenColaborador { get; set; }
        
        public Colaborador() { }

        public Colaborador(string nombres, string apellidoPaterno, string apellidoMaterno, string direccion, DateTime fecha_nacimiento,
                           string telefono, DateTime fecha_ingreso, string correo_electronico, string numero_documento,
                           string centro_estudios, Byte[] imagen, Byte[] curriculum_vitae, int tipo_documentoID, int estado_colaboradorID,
                           int paisID, bool is_activo, int grado_academicoID, DateTime fecha_salida_empresa )
        {
            Nombres = nombres;
            ApellidoMaterno = apellidoMaterno;
            ApellidoPaterno = apellidoPaterno;
            Direccion = direccion;
            Telefono = telefono;
            FechaNacimiento = fecha_nacimiento;
            FechaIngresoEmpresa = fecha_ingreso;
            CurriculumVitae = curriculum_vitae;
            ImagenColaborador = imagen;
            TipoDocumentoID = tipo_documentoID;
            NumeroDocumento = numero_documento;
            CorreoElectronico = correo_electronico;
            EstadosColaboradorID = estado_colaboradorID;
            CentroEstudios = centro_estudios;
            GradoAcademicoID = grado_academicoID;
            PaisID = paisID;
            IsActivo = is_activo;
            FechaSalidaEmpresa = fecha_salida_empresa;
        
        }

        public Colaborador(ColaboradorDTO c)
        {
            LoadFromDTO(c);
        }

        public Colaborador LoadFromDTO(ColaboradorDTO c)
        {
            ID = c.ID;
            Nombres = c.Nombre;
            ApellidoPaterno = c.ApellidoPaterno;
            ApellidoMaterno = c.ApellidoMaterno;
            Direccion = c.Direccion;
            Telefono = c.Telefono;
            PaisID = c.PaisID;
            GradoAcademicoID = c.GradoAcademicoID;
            EstadosColaboradorID = c.EstadoColaboradorID;
            TipoDocumentoID = c.TipoDocumentoID;
            NumeroDocumento = c.NumeroDocumento;
            CorreoElectronico = c.CorreoElectronico;
            CentroEstudios = c.CentroEstudios;
            ImagenColaborador = c.ImagenColaborador;
            CurriculumVitae = c.CurriculumVitae;
            FechaNacimiento = DateTime.Parse(c.FechaNacimiento);
            FechaIngresoEmpresa = DateTime.Parse(c.FechaIngreso);
         
            return this;
        }


        public ColaboradorDTO ToDTO()
        {
            return new ColaboradorDTO(this);
        }
    }

    public class ColaboradorDTO
    {
       
        public string NombreCompleto { get; set; }
        public int ID { get; set; }
        
        [DisplayName("Nombre (*)")]
        [StringLength(40)]
        public string Nombre { get; set; }
        
        [DisplayName("Apellido Paterno (*)")]
        [StringLength(30)]
        public string ApellidoPaterno { get; set; }

        [DisplayName("Apellido Materno (*)")]
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
        public string FechaNacimiento { get; set; }

        [DisplayName("Tipo de Documento (*)")]
        public int TipoDocumentoID { get; set; }

        [DisplayName("Número de Documento (*)")]
        [StringLength(20)]
        public string NumeroDocumento { get; set; }

        [DisplayName("Usuario")]
        public string Usuario { get; set; }

        [DisplayName("Estado")]
        public int EstadoColaboradorID { get; set; }

        [DisplayName("CurriculumVitae")]
        public Byte[] CurriculumVitae { get; set; }

        [DisplayName("Imagen")]
        public Byte[] ImagenColaborador { get; set; }

        [DisplayName("Centro de estudios")]
        public string CentroEstudios { get; set; }

        [DisplayName("Grado Académico")]
        public int GradoAcademicoID { get; set; }

        [DisplayName("Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        
        [DisplayName("Ingreso Empresa")]
        public string FechaIngreso { get; set; }

        [DisplayName("Área")]
        public int AreaID { get; set; }
        
        [DisplayName("Puesto")]
        public int PuestoID { get; set; }
        
        [DisplayName("Sueldo")]
        public double sueldo { get; set; }


        public ColaboradorDTO() { }

        public ColaboradorDTO(Colaborador c)
        {
            NombreCompleto = c.ApellidoPaterno + " " + c.ApellidoMaterno + ", " + c.Nombres;
            ID = c.ID;
            GradoAcademicoID = c.GradoAcademicoID;
            PaisID = c.PaisID;
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
            CurriculumVitae = c.CurriculumVitae;
            ImagenColaborador = c.ImagenColaborador;
            FechaNacimiento = c.FechaNacimiento.GetValueOrDefault().ToShortDateString();
            FechaIngreso = c.FechaIngresoEmpresa.GetValueOrDefault().ToShortDateString();
            AreaID = 1;
            PuestoID = 1;
            sueldo = 3300;


         }

        }
    }
