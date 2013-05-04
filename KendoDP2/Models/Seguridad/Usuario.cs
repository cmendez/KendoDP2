using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Models.Seguridad
{
    public class Usuario : DBObject
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Rol> Roles { get; set; }

        public Usuario(string username, string password)
        {
            Username = username;
            Password = password;
            Roles = new List<Rol>();
        }

        public Usuario(string username, string password, List<Rol> rol)
        {
            Username = username;
            Password = password;
            Roles = new List<Rol>();
            foreach(Rol r in rol)
            {
                Rol x = new Rol(r.ID,r.Secuencia,r.Nivel,r.Subnivel,r.IsEliminado);
                Roles.Add(x);
                r.Usuarios.Add(this);
            }
        }

        public UsuarioDTO ToDTO()
        {
            return new UsuarioDTO(this);
        }

        public Usuario() { }

        public Usuario LoadFromDTO(UsuarioDTO dto)
        {
            ID = dto.ID;
            Username = dto.UserName;
            Password = dto.password;
            return this;
        }
    }

    public class UsuarioDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string password { get; set; }
        public string UserName { get; set; }


        public UsuarioDTO() { }
        public UsuarioDTO(Usuario u)
        {
            ID = u.ID;
            UserName = u.Username;
            password = u.Password;
        }
    }
}