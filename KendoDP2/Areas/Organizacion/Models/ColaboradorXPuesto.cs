using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class ColaboradorXPuesto: DBObject
    {
        public int PuestoID { get; set; }
        public int ColaboradorID { get; set; }
        public int Sueldo { get; set; }
        public DateTime? FechaIngresoPuesto { get; set; }
        public DateTime? FechaSalidaPuesto { get; set; }
        public string Comentarios { get; set; }

        public virtual Colaborador Colaborador { get; set; }
        public virtual Puesto Puesto { get; set; }

        public ColaboradorXPuestoDTO ToDTO()
        {
            return new ColaboradorXPuestoDTO(this);
        }
    }

    public class ColaboradorXPuestoDTO
    {
        public ColaboradorDTO ColaboradorDTO { get; set; }
        public int ID { get; set; }

        public ColaboradorXPuestoDTO(ColaboradorXPuesto x)
        {
            ColaboradorDTO = x.Colaborador.ToDTO();
            ID = x.ID;
        }

        public ColaboradorXPuestoDTO() { }
    }
}