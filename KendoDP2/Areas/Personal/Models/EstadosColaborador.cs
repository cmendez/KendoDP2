using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Personal.Models
{
    public class EstadosColaborador: DBObject
    {
        public string Descripcion { get; set; }
        public virtual ICollection<Colaborador> ListaColaboradores { get; set; }


        public EstadosColaborador() { }

        public EstadosColaborador(string descripcion)
        {
            Descripcion = descripcion;
        }

         public EstadosColaborador(EstadosColaboradorDTO e)
        {
            LoadFromDTO(e);
        }

        public EstadosColaborador LoadFromDTO(EstadosColaboradorDTO e)
        {
            Descripcion = e.Descripcion;
            ID = e.ID;
            return this;
        }
        
        public EstadosColaboradorDTO ToDTO()
        {
            return new EstadosColaboradorDTO(this);
        }


    }

    public class EstadosColaboradorDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string Descripcion { get; set; }

        public EstadosColaboradorDTO() { }

        public EstadosColaboradorDTO(EstadosColaborador e)
        {
            ID = e.ID;
            Descripcion = e.Descripcion;
        }

    }
}