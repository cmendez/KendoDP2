using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class Perfil : DBObject
    {
        public string Nombre { get; set; }
        //public virtual ICollection<Capacidad> Capacidades { get; set; }

        public Perfil() { }

        public Perfil(string nombre)
        {
            Nombre = nombre;
        }

        public Perfil(PerfilDTO dto)
        {
            LoadFromDTO(dto);
        }

        public Perfil LoadFromDTO(PerfilDTO dto)
        {
            ID = dto.ID;
            Nombre = dto.Nombre;
            return this;
        }

        public PerfilDTO ToDTO()
        {
            return new PerfilDTO(this);
        }
    }

    public class PerfilDTO
    {
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public PerfilDTO(Perfil perfil)
        {
            Nombre = perfil.Nombre;
            ID = perfil.ID;
        }
        public PerfilDTO() { }
    }

}