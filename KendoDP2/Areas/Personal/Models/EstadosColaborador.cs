using System;
using System.Collections.Generic;
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


    }
}