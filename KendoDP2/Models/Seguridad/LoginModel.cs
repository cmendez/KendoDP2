using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Models.Seguridad
{
    public class LoginModel
    {
        [Required]
        [DisplayName("Usuario")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Recordarme")]
        public bool RememberMe { get; set; }
    }
}