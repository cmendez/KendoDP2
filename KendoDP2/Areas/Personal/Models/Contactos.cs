using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Personal.Models
{
    public class Contactos : DBObject
    {
        public int ContactoID { get; set; }
        public int ColaboradorID { get; set; }

        [ForeignKey("ContactoID")]
        public virtual Colaborador Contacto { get; set; }
        [ForeignKey("ColaboradorID")]
        public virtual Colaborador Colaborador { get; set; }
    }
}