using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Eventos.Models
{
    public class AreaXEvento: DBObject
    {
        public int AreaID { get; set; }
        public virtual Area Area { get; set; }

        public int EventoID { get; set; }
        public virtual Evento Evento { get; set; }

        public AreaXEventoDTO ToDTO()
        {
            return new AreaXEventoDTO(this);
        }
    }

    public class AreaXEventoDTO
    {
        public int ID { get; set; }
        public AreaDTO AreaDTO { get; set; }

        public AreaXEventoDTO() { }

        public AreaXEventoDTO(AreaXEvento x)
        {
            ID = x.ID;
            AreaDTO = x.Area.ToDTO();
        }
    }
    
}