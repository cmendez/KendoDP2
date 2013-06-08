using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;
using Kendo.Mvc.Extensions;
using KendoDP2.Models.Seguridad;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Seguridad.Controllers
{
    public class UsuariosController : Controller
    {
        public UsuariosController()
        {
            ViewBag.Area = "Usuarios";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaUsuarios.All().Select(i=>i.ToDTO()).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _Update([DataSourceRequest] DataSourceRequest request, UsuarioDTO usuario)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaUsuarios.AddElement(new Usuario(usuario));
                return Json(context.TablaUsuarios.All().Select(i => i.ToDTO()).ToDataSourceResult(request));
            }
        }

        public ActionResult VerRoles([DataSourceRequest] DataSourceRequest request, int ID)
        {
            using (DP2Context context = new DP2Context())
            {
                Session["Usuario_Roles"] = context.TablaUsuarios.One(i => i.ID == ID);
            }
            return Redirect("../../Roles/UsuarioRoles");
        }

        
    }
}
