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
        public string Nivel { get; set; }
        public string Subnivel { get; set; }
        public int Secuencia { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

        public Rol() { }

        public Rol(int sec, string Nivel, bool activo)
        {
            this.Secuencia = sec;
            this.Nivel = Nivel;
            this.Subnivel = String.Empty;
            this.IsEliminado = activo;
            Usuarios = new List<Usuario>();
        }

        public Rol(int id,int sec, string Nivel, bool activo)
        {
            this.ID = id;
            this.Secuencia = sec;
            this.Nivel = Nivel;
            this.Subnivel = String.Empty;
            this.IsEliminado = activo;
            Usuarios = new List<Usuario>();
        }

        public Rol(int sec, string Nivel, string Subnivel, bool activo)
        {
            this.Secuencia = sec;
            this.Nivel = Nivel;
            this.Subnivel = Subnivel;
            this.IsEliminado = activo;
            Usuarios = new List<Usuario>();
        }

        public Rol(int id,int sec, string Nivel, string Subnivel, bool activo)
        {
            this.ID = id;
            this.Secuencia = sec;
            this.Nivel = Nivel;
            this.Subnivel = Subnivel;
            this.IsEliminado = activo;
            Usuarios = new List<Usuario>();
        }

        public Rol(RolDTO dto)
        {
            LoadFromDTO(dto);
        }

        public Rol LoadFromDTO(RolDTO dto)
        {
            ID = dto.ID;
            Nivel = dto.Nivel;
            Subnivel = dto.Subnivel;
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

        public int ID { get; set; }
        public string Nivel { get; set; }
        public string Subnivel { get; set; }
        public bool IsEliminado { get; set; }
        public int Secuencia { get; set; }

        public RolDTO(Rol r)
        {
            this.Secuencia = r.Secuencia;
            this.ID = r.ID;
            this.Nivel = r.Nivel;
            this.Subnivel = r.Subnivel;
            this.IsEliminado = r.IsEliminado;
        }

        public RolDTO() { }
    }
}