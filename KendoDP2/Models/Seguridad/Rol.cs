using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Helpers;

namespace KendoDP2.Models.Seguridad
{
    public class Rol : DBObject
    {
        public string Area { get; set; }
        public string Nombre { get; set; }
        public bool Permiso { get; set; }

        public virtual List<Usuario> Usuarios { get; set; }

        public Rol(string nombre, string area)
        {
            Nombre = nombre;
            Permiso = true; // esto debe cambiarse xq todos deben inicar con false(ningun acceso a nada)
            IsEliminado = false;
            Area = area;
            Usuarios = new List<Usuario>();
        }

        public Rol()
        {
            Usuarios = new List<Usuario>();
            Permiso = true; // esto debe cambiarse xq todos deben inicar con false(ningun acceso a nada)
            IsEliminado = false;
        }

        public Rol(RolDTO dto)
        {
            LoadFromDTO(dto);
        }

        public Rol LoadFromDTO(RolDTO dto)
        {
            ID = dto.ID;
            Nombre = dto.Nombre;
            Permiso = dto.Permiso;
            Area = dto.Area;
            IsEliminado = dto.IsEliminado;
            return this;
        }

        public RolDTO ToDTO()
        {
            return new RolDTO(this);
        }
    }

    public class RolDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public bool IsEliminado { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        public bool Permiso { get; set; }
        public string Area { get; set; }

        public RolDTO(Rol r)
        {
            Nombre = r.Nombre;
            Permiso = r.Permiso;
            Area = r.Area;
            IsEliminado = r.IsEliminado;
        }
        public RolDTO()
        {
            Nombre = String.Empty;
            Area = String.Empty;
            Permiso = true; // esto debe cambiarse xq todos deben inicar con false(ningun acceso a nada)
            IsEliminado = false;
        }
    }
}