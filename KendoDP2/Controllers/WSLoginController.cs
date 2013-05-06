using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Seguridad;
using KendoDP2.Models.Generic;

namespace KendoDP2.Controllers
{
    public class WSLoginController : Controller
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
                            return Json(new { respuesta = 1, usuario = usuarioDTO }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { respuesta = 0 }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        return Json(new { respuesta = 0 }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { respuesta = -1 }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
