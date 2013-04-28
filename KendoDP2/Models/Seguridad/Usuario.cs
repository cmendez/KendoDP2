using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Models.Seguridad
{
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
            //rol.Usuarios.Add(this);
        }

        public UsuarioDTO ToDTO()
        {
            return new UsuarioDTO(this);
        }

        public Usuario()
        {
        }
    }

    public class UsuarioDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        public UsuarioDTO() { }
        
        public UsuarioDTO(Usuario u)
        {
            Username = u.Username;
            Password = u.Password;
        }
    }
}