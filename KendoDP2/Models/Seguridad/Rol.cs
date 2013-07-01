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
        public bool EsWeb { get; set; }



        public Rol(string nombre, string area)
        {
            Nombre = nombre;
            Permiso = false; // esto debe cambiarse xq todos deben inicar con false(ningun acceso a nada)
            IsEliminado = false;
            Area = area;
            EsWeb = true;
        }

        public Rol()
        {
            Permiso = false; // esto debe cambiarse xq todos deben inicar con false(ningun acceso a nada)
            IsEliminado = false;
            EsWeb = true;
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

        public bool EsWeb { get; set; }
        
        [StringLength(200)]
        public string Nombre { get; set; }

        public bool Permiso { get; set; }
        public string Area { get; set; }

        public RolDTO(Rol r)
        {
            ID = r.ID;
            Nombre = r.Nombre;
            Permiso = r.Permiso;
            Area = r.Area;
            IsEliminado = r.IsEliminado;
            EsWeb = r.EsWeb;
        }
        public RolDTO()
        {
            Nombre = String.Empty;
            Area = String.Empty;
            Permiso = false; // esto debe cambiarse xq todos deben inicar con false(ningun acceso a nada)
            IsEliminado = false;
            EsWeb = true;
        }
    }
}