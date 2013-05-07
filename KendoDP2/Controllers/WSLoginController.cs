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
        //
        // GET: /WSLogin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string username, string password)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    UsuarioDTO usuario = context.TablaUsuarios.One(x => x.Username.Equals(username)).ToDTO();
                    return usuario.Password.Equals(password) ? 
                        JsonSuccessGet(new { usuario = usuario }) : 
                        JsonErrorGet("No existe dicho usuario y password");
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }
    }
}
