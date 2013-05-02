using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Personal.Models
{
    public class TipoDocumento: DBObject
    {
        public string Descripcion { get; set; }
        public virtual ICollection<Persona> ListaPersonas { get; set; }

        public TipoDocumento() { }

        public TipoDocumento(string descripcion)
        {
            Descripcion = descripcion;
        }

        public TipoDocumento(TipoDocumentoDTO t)
        {
            LoadFromDTO(t);
        }

        public TipoDocumento LoadFromDTO(TipoDocumentoDTO t)
        {
            Descripcion = t.Descripcion;
            ID = t.ID;
            return this;
        }
        
        public TipoDocumentoDTO ToDTO()
        {
            return new TipoDocumentoDTO(this);
        }
        
    }

    public class TipoDocumentoDTO
    {
        [ScaffoldColumn(false)]
        public int ID {get; set;}
        
        public string Descripcion { get; set; }

        public TipoDocumentoDTO() { }

        public TipoDocumentoDTO(TipoDocumento t)
        {
            ID = t.ID;
            Descripcion = t.Descripcion;
        }
    }
}