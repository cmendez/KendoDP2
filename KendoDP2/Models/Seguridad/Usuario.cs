using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KendoDP2.Models.Seguridad
{
    [Table("Usuario")]
    public class Usuario : DBObject
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual List<Rol> Roles { get; set; }

        public Usuario(string username, string password, Rol rol)
        {
            Username = username;
            Password = password;
            Roles = new List<Rol>();
            Roles.Add(rol);
            rol.Usuarios.Add(this);
        }

        public Usuario()
        {
        }
    }
}