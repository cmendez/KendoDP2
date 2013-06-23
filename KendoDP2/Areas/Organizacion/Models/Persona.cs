using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoDP2.Models.Seguridad;
using KendoDP2.Areas.Evaluacion360.Models;


namespace KendoDP2.Areas.Organizacion.Models
{
    public class Persona : Usuario
    {
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string CentroEstudios { get; set; }
        public string CorreoElectronico { get; set; }

        public int? GradoAcademicoID { get; set; }
        public virtual GradoAcademico GradoAcademico { get; set; }
       
        public int TipoDocumentoID { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        
        public int CurriculumVitaeID { get; set; }

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
        //public string Username { get; set; }
        //public string Password { get; set; }
        //public virtual List<Rol> Roles { get; set; }

        public PersonaDTO() {
            //Roles = new List<Rol>();
        }
        public PersonaDTO(Persona p)
        {
            ID = p.ID;
            NombreCompleto = p.ApellidoPaterno + " " + p.ApellidoMaterno + ", " + p.Nombres;
            //Username = p.Username;
            //Password = p.Password;
            //Roles = new List<Rol>();
            //Roles = p.Roles;
        }
    }
}