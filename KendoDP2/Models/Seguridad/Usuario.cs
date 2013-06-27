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

        public Usuario(string username, string password, IList<Rol> roles)
        {
            Username = username;
            Password = password;
            Roles = new List<Rol>();
            foreach(Rol r in roles)
            {
                Roles.Add(r);
            }
        }

        public UsuarioDTO ToDTO()
        {
            return new UsuarioDTO(this);
        }

        public Usuario()
        {
        }

        public Usuario(UsuarioDTO dto)
        {
            LoadFromDTO(dto);
        }

        public Usuario LoadFromDTO(UsuarioDTO dto)
        {
            ID = dto.ID;
            IsEliminado = dto.IsEliminado;
            Password = dto.Password;
            Username = dto.Username;
            return this;
        }
    }

    public class UsuarioDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int ID { get; set; }
        public bool IsEliminado { get; set; }
        public List<RolDTO> Roles { get; set; }

        public UsuarioDTO() { }
        
        public UsuarioDTO(Usuario u)
        {
            Username = u.Username;
            Password = u.Password;
            IsEliminado=u.IsEliminado;
            Roles = new List<RolDTO>();
            foreach(Rol r in u.Roles)
            {
                RolDTO rr = new RolDTO(r);
                Roles.Add(rr);
            }
            ID = u.ID;
        }
    }
}