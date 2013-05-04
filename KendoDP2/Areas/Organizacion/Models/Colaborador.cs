using KendoDP2.Areas.Objetivos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Configuracion.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoDP2.Areas.Evaluacion360.Models;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int PaisID { get; set; }
        public virtual Pais Pais { get; set; }

        public byte[] ImagenColaborador { get; set; }

        [InverseProperty("Contacto")]
        public virtual ICollection<Contactos> EsContactoDe { get; set; }
        [InverseProperty("Colaborador")]
        public virtual ICollection<Contactos> Contactos { get; set; }


        public Colaborador() { }

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
            FechaNacimiento = c.FechaNacimiento;
            FechaIngresoEmpresa = c.FechaIngreso;
           
            return this;
        }


        new public ColaboradorDTO ToDTO()
        {
            return new ColaboradorDTO(this);
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
        
        [DisplayName("Apellido Paterno")]
        [StringLength(30)]
        public string ApellidoPaterno { get; set; }

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
        public byte[] CurriculumVitae { get; set; }

        [DisplayName("Imagen")]
        public byte[] ImagenColaborador { get; set; }

        [DisplayName("Centro de estudios")]
        public string CentroEstudios { get; set; }

        [DisplayName("Grado Académico")]
        public int GradoAcademicoID { get; set; }

        [DisplayName("Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        
        [DisplayName("Ingreso Empresa")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaIngreso { get; set; }

        [DisplayName("Área")]
        public int AreaID { get; set; }
        
        [DisplayName("Puesto")]
        public int PuestoID { get; set; }
        
        [DisplayName("Sueldo S/.")]
        public int Sueldo { get; set; }


        public ColaboradorDTO() { }

        public ColaboradorDTO(Colaborador c)
        {
            NombreCompleto = c.ApellidoPaterno + " " + c.ApellidoMaterno + ", " + c.Nombres;
            ID = c.ID;
            GradoAcademicoID = c.GradoAcademicoID.GetValueOrDefault();
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
            FechaNacimiento = c.FechaNacimiento;
            FechaIngreso = c.FechaIngresoEmpresa;

            try {
                ColaboradorXPuesto cruce = c.ColaboradoresPuesto.OrderByDescending(a => a.ID).First();
                AreaID = cruce.Puesto.AreaID;
                PuestoID = cruce.Puesto.ID;
                Sueldo = cruce.Sueldo;
            } catch(Exception){
                AreaID = 0;
                PuestoID = 0;
                Sueldo = 0;
            }


         }

        }
    }
