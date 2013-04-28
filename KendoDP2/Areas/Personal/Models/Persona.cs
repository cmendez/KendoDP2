using KendoDP2.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Personal.Models
{
    public class Persona : Usuario
    {
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }

        public Persona() { }

        public PersonaDTO ToDTO()
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