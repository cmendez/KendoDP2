using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class AColor : DBObject
    {
        [DisplayName("Color")]
        public string Color { get; set; }
        public string Text { get; set; }
    }
}