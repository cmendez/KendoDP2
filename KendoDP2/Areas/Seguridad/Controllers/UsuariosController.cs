using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;


namespace KendoDP2.Areas.Seguridad.Controllers
{
    public class UsuariosController : Controller
    {
        public UsuariosController()
            : base()
        {
            ViewBag.Area = "Seguridad";
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                var x = Json(context.TablaUsuarios.All().Select(p => p.ToDTO()).ToDataSourceResult(request));
                return x;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, UsuarioDTO User)
        {
            using (DP2Context context = new DP2Context())
            {
                Usuario u = context.TablaUsuarios.FindByID(User.ID).LoadFromDTO(User);
                context.TablaUsuarios.ModifyElement(u);
                return Json(new[] { u.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, UsuarioDTO User)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaUsuarios.RemoveElementByID(User.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }

    }
}