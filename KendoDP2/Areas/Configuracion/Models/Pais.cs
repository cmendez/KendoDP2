using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoDP2.Models.Generic;


namespace KendoDP2.Areas.Configuracion.Models
{
    public class Pais : DBObject
    {
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }

        public Pais() { }

        public Pais(string nombre)
        {
            Nombre = nombre;
        }
    }
}