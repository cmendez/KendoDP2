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
            Usuarios = dto.Usuarios;
            Navegacion = dto.Navegacion;
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
        [StringLength(200, ErrorMessage = "Solo se permite una longitud de 200 caracteres para un Rol")]
        [DataType(DataType.Text, ErrorMessage = "Solo se admite caracteres")]
        public string Nombre { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public List<SidebarOption> Navegacion { get; set; }

        public RolDTO(Rol r)
        {
            Nombre = r.Nombre;
            Usuarios = r.Usuarios;
            Navegacion = r.Navegacion;
        }
        public RolDTO()
        {
            Nombre = String.Empty;
            Usuarios = new List<Usuario>();
            Navegacion = new List<SidebarOption>();
        }
    }
}