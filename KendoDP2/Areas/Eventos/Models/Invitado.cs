using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Eventos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace KendoDP2.Areas.Eventos.Models
{
    public class Invitado : DBObject
    {
        public int ColaboradorID { get; set; }
        public int EventoID { get; set; }

        [ForeignKey("ColaboradorID")]
        public virtual Colaborador Asistente { get; set; }
        [ForeignKey("EventoID")]
        public virtual Evento Evento { get; set; }

        public int ReferenciasPorAreas { get; set; }
        public bool ReferenciaDirecta { get; set; }

             public InvitadoDTO ToDTO()
        {
            return new InvitadoDTO(this);
        }
    }

    public class InvitadoDTO
    {
        public ColaboradorDTO ColaboradorDTO { get; set; }
        public int ID { get; set; }

        [DisplayName("Evento")]
        public int EventoID { get; set; }

        public int ColaboradorID { get; set; }


        public InvitadoDTO(Invitado x)
        {
            ColaboradorDTO = x.Asistente.ToDTO();
            ID = x.ID;
            EventoID = x.EventoID;
            ColaboradorID = x.ColaboradorID;
        }

        public InvitadoDTO() { }
    }

}
    
