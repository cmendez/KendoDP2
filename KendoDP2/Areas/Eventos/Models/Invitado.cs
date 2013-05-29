using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Eventos.Models;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}