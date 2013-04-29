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
        public string Nombre { get; set; }

        public virtual List<Usuario> Usuarios { get; set; }
        public virtual List<SidebarOption> Navegacion { get; set; }

        public Rol(string nombre)
        {
            Nombre = nombre;
            Usuarios = new List<Usuario>();
            Navegacion = new List<SidebarOption>();
        }

        public Rol(string nombre, List<SidebarOption> sidebaroption)
        {
            Nombre = nombre;
            Usuarios = new List<Usuario>();
            Navegacion = new List<SidebarOption>();
            Navegacion = sidebaroption;
        }

        public Rol()
        {
            Usuarios = new List<Usuario>();
            Navegacion = new List<SidebarOption>();
        }

        public Rol(RolDTO dto)
        {
            LoadFromDTO(dto);
        }

        public Rol LoadFromDTO(RolDTO dto)
        {
            ID = dto.ID;
            Nombre = dto.Nombre;
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

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        public RolDTO(Rol r)
        {
            Nombre = r.Nombre;
        }
        public RolDTO()
        {
            Nombre = String.Empty;
        }
    }
}