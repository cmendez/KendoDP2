using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Personal.Models
{
    public class Colaborador : Persona
    {
        public string Direccion { get; set; }
        public bool IsActivo { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public Colaborador() { }
        public Colaborador(string nombres, string apellidoPaterno, string apellidoMaterno)
        {
            Nombres = nombres;
            ApellidoMaterno = apellidoMaterno;
            ApellidoPaterno = apellidoPaterno;
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

        public ColaboradorDTO() { }

        public ColaboradorDTO(Colaborador c)
        {
            NombreCompleto = c.ApellidoPaterno + " " + c.ApellidoMaterno + ", " + c.Nombres;
            ID = c.ID;
        }
    }
}