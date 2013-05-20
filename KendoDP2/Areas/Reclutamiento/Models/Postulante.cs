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
        public string Estado { get; set; }

        [InverseProperty("Postulante")]
        public virtual ICollection<OfertaLaboralXPostulante> OfertasPostuladas { get; set; }

        public Postulante() { }

        public Postulante(PostulanteDTO p)
        {
            LoadFromDTO(p);
        }

        public Postulante LoadFromDTO(PostulanteDTO p)
        {
            ID = p.ID;

            //CentroEstudios;
            //CorreoElectronico;
            //GradoAcademicoID;
            
            Estado = p.Estado;
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

        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [DisplayName("Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [DisplayName("Tipo de Documento")]
        public int TipoDocumentoID { get; set; }

        [DisplayName("Número de Documento")]
        public string NumeroDocumento { get; set; }

        [DisplayName("Centro de estudios")]
        public string CentroEstudios { get; set; }

        [DisplayName("Grado Académico")]
        public int GradoAcademicoID { get; set; }

        [DisplayName("Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [DisplayName("Estado del Postulante")]
        public string Estado { get; set; }
        
        public PostulanteDTO() { }
        public PostulanteDTO(Postulante p)
        {
            NombreCompleto = p.ApellidoPaterno + " " + p.ApellidoMaterno + ", " + p.Nombres;
            ID = p.ID;

            Nombre = p.Nombres;
            ApellidoMaterno = p.ApellidoMaterno;
            ApellidoPaterno = p.ApellidoPaterno;
            TipoDocumentoID = p.TipoDocumentoID;
            NumeroDocumento = p.NumeroDocumento;
            CentroEstudios = p.CentroEstudios;
            GradoAcademicoID = p.GradoAcademicoID.GetValueOrDefault();
            CorreoElectronico = p.CorreoElectronico;

            Estado = p.Estado;
            
        }
    }
}