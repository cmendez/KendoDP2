using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using KendoDP2.Models.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class FasePostulacion : DBObject
    {
        public string Descripcion { get; set; }

        [InverseProperty("FasePostulacion")] //Cambiar el nombre por uno mas idoneo xD
        public virtual ICollection<FasePostulacionXOfertaLaboralXPostulante> Postulaciones_OfLabxPost { get; set; }

        public FasePostulacion() { }
        public FasePostulacion(string descripcion)
        {
            Descripcion = descripcion;
        }
        public FasePostulacion(FasePostulacionDTO fp)
        {
            LoadFromDTO(fp);
        }
        public FasePostulacion LoadFromDTO(FasePostulacionDTO fp)
        {
            ID = fp.ID;
            Descripcion = fp.Descripcion;
            return this;
        }
        public FasePostulacionDTO ToDTO(FasePostulacion fp)
        {
            return new FasePostulacionDTO(this);
        }
    }

    public class FasePostulacionDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string Descripcion { get; set; }

        public FasePostulacionDTO() { }
        public FasePostulacionDTO(FasePostulacion fp)
        {
            ID = fp.ID;
            Descripcion = fp.Descripcion;
        }
    }
}