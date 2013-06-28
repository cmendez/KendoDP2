using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Seguridad;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Helpers;
using ExtensionMethods;

namespace KendoDP2.Controllers
{
    public class WSLoginController : WSController
    {
        // /WSLogin/Login
        public ActionResult Login(string username, string password)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    var usuario = context.TablaUsuarios.One(x => x.Username != null && x.Username.Equals(username));
                    if (usuario == null) return JsonErrorGet("El usuario y/o la contraseña son invalidos");

                    return usuario.Password != null && usuario.Password.Equals(password) ?  
                        JsonSuccessGet(new { usuario = usuario.ToDTO() }) : 
                        JsonErrorGet("No existe dicho usuario y password");
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
                }
            }
        }
    }
}
