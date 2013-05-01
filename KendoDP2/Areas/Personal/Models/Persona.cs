using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoDP2.Models.Seguridad;


namespace KendoDP2.Areas.Personal.Models
{
    public class Persona : Usuario
    {
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NumeroDocumento { get; set; }
        public string CentroEstudios { get; set; }
        public string CorreoElectronico { get; set; }
        public int? GradoAcademicoID { get; set; }
        public virtual GradoAcademico GradoAcademico { get; set; }
       
        public int TipoDocumentoID { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        
        public byte[] CurriculumVitae { get; set; }

        public Persona() { }

        new public PersonaDTO ToDTO()
        {
            return new PersonaDTO(this);
        }
    }

    public class PersonaDTO
    {
        public int ID { get; set; }
        public string NombreCompleto { get; set; }

        public PersonaDTO() { }
        public PersonaDTO(Persona p)
        {
            ID = p.ID;
            NombreCompleto = p.ApellidoPaterno + " " + p.ApellidoMaterno + ", " + p.Nombres;
        }
    }
}