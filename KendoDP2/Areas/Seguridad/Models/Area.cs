using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Seguridad.Models
{
    public class MenuArea
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

        public MenuArea(int id, string nombre) 
        {
            ID = id;
            Nombre = nombre;
        }
    }
}