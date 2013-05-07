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
                    Usuario usuario = context.TablaUsuarios.One(x => x.Username.Equals(username));
                    if (usuario != null)
                    {
                        UsuarioDTO usuarioDTO = usuario.ToDTO();
                        if (usuarioDTO.Password.Equals(password))
                        {
                            return JsonSuccessGet(new { usuario = usuarioDTO } );
                        }
                        else
                        {
                            return JsonErrorGet("No existe dicho usuario y password");
                        }

                    }
                    else
                    {
                        return JsonErrorGet("No existe dicho usuario y password");
                    }
                }
                catch (Exception)
                {
                    return JsonErrorGet("Error en la BD");
                }
            }
        }
    }
}
