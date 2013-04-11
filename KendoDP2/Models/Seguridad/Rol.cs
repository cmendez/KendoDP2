using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KendoDP2.Models.Seguridad
{
    [Table("Rol")]
    public class Rol : DBObject
    {
        public string Nombre { get; set; }

        public virtual List<Usuario> Usuarios { get; set; }

        public Rol(string nombre)
        {
            Nombre = nombre;
            Usuarios = new List<Usuario>();
        }

        public Rol()
        {
        }
    }
}