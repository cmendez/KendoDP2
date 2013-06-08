using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Configuracion.Models
{
    public class CentrosEstudio: DBObject
    {
        public string Nombre { get; set; }
        public virtual List<Persona> Persona { get; set; }

        public CentrosEstudio() { }

        public CentrosEstudio(CentrosEstudioDTO p)
        {
            LoadFromDTO(p);
        }

        public CentrosEstudio LoadFromDTO(CentrosEstudioDTO p)
        {
            Nombre = p.Nombre;
            ID = p.ID;
            return this;
        }
        
        public CentrosEstudioDTO ToDTO()
        {
            return new CentrosEstudioDTO(this);
        }
                      
    }

    public class CentrosEstudioDTO
    {
        
        [ScaffoldColumn(false)]
        public int ID { get; set; }


        public string Nombre { get; set; }

        public CentrosEstudioDTO() { }

        public CentrosEstudioDTO(CentrosEstudio c)
        {
            ID = c.ID;
            Nombre = c.Nombre;
        }
    
    }
}