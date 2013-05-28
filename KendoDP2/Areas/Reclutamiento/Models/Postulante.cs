using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Areas.Organizacion.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class Postulante : Persona
    {
        //public string Estado { get; set; }

        public int? ColaboradorID { get; set; }
        public virtual Colaborador Colaborador { get; set; }

        [InverseProperty("Postulante")]
        public virtual ICollection<OfertaLaboralXPostulante> OfertasPostuladas { get; set; }

        public Postulante() { }

        public Postulante(PostulanteDTO p)
        {
            LoadFromDTO(p);
        }

        public Postulante(Organizacion.Models.Colaborador colaborador)
        {
            this.Colaborador = colaborador;
            this.GradoAcademico = colaborador.GradoAcademico;
            this.TipoDocumento = colaborador.TipoDocumento;
        }

        public Postulante LoadFromDTO(PostulanteDTO p)
        {
            ID = p.ID;

            Nombres = p.Nombres;
            ApellidoMaterno = p.ApellidoMaterno;
            ApellidoPaterno = p.ApellidoPaterno;
            CentroEstudios = p.CentroEstudios;
            CorreoElectronico = p.CorreoElectronico;

            GradoAcademicoID = p.GradoAcademicoID;
            TipoDocumentoID = p.TipoDocumentoID;
            NumeroDocumento = p.NumeroDocumento;
            CurriculumVitaeID = p.CurriculumVitaeID;

            return this;
        }

        new public PostulanteDTO ToDTO()
        {
            return new PostulanteDTO(this);
        }

    }

    public class PostulanteDTO
    {
        public string NombreCompleto { get; set; }

        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("Nombres")]
        public string Nombres { get; set; }

        [DisplayName("Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [DisplayName("Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [DisplayName("Centro de estudios")]
        public string CentroEstudios { get; set; }

        [DisplayName("Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        public int GradoAcademicoID { get; set; }
        [DisplayName("Grado Académico")]
        public string GradoAcademico { get; set; }

        public int TipoDocumentoID { get; set; }
        [DisplayName("Tipo de Documento")]
        public string TipoDocumento { get; set; }

        [DisplayName("Número de Documento")]
        public string NumeroDocumento { get; set; }

        [DisplayName("Curriculum Vitae")]
        public int CurriculumVitaeID { get; set; }

        public ColaboradorDTO Colaborador { get; set; }

        //[DisplayName("Estado del Postulante")]
        //public string Estado { get; set; }

        public PostulanteDTO() { }
        public PostulanteDTO(Postulante p)
        {
            NombreCompleto = p.ApellidoPaterno + " " + p.ApellidoMaterno + ", " + p.Nombres;
            ID = p.ID;

            Nombres = p.Nombres;
            ApellidoMaterno = p.ApellidoMaterno;
            ApellidoPaterno = p.ApellidoPaterno;
            CentroEstudios = p.CentroEstudios;
            CorreoElectronico = p.CorreoElectronico;
            GradoAcademicoID = p.GradoAcademicoID.GetValueOrDefault();
            GradoAcademico = p.GradoAcademicoID.HasValue ? p.GradoAcademico.Descripcion : String.Empty;
            TipoDocumentoID = p.TipoDocumentoID;
            TipoDocumento = p.TipoDocumento.Descripcion;
            NumeroDocumento = p.NumeroDocumento;
            Colaborador = p.Colaborador == null ? new ColaboradorDTO() : p.Colaborador.ToDTO();
        }
    }
}