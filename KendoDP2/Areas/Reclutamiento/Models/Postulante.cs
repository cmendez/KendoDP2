using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using System.ComponentModel.DataAnnotations;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class Postulante : Persona
    {
        public string Estado { get; set; }
        public virtual ICollection<OfertaLaboral> OfertasPostuladas { get; set; }

        public Postulante() { }

        public Postulante(PostulanteDTO p)
        {
            LoadFromDTO(p);
        }

        public Postulante LoadFromDTO(PostulanteDTO p)
        {
            ID = p.ID;
            Estado = p.Estado;
            return this;
        }

        public PostulanteDTO ToDTO()
        {
            return new PostulanteDTO(this);
        }

    }

    public class PostulanteDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        public string Estado { get; set; }
        public string NombreCompleto { get; set; }


        public PostulanteDTO() { }
        public PostulanteDTO(Postulante p)
        {
            ID = p.ID;
            Estado = p.Estado;
            NombreCompleto = p.ApellidoPaterno + " " + p.ApellidoMaterno + ", " + p.Nombres;
        }
    }
}