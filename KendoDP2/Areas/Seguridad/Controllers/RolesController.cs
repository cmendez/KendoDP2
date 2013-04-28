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
    [Authorize()]
    public class RolesController : Controller
    {
        public RolesController()
        {
            ViewBag.Area = "Seguridad";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaRoles.All().Select(p => p.ToDTO()).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, RolDTO rol)
        {
            using (DP2Context context = new DP2Context())
            {
                Rol r = new Rol(rol.Nombre);
                context.TablaRoles.AddElement(r);
                return Json(new[] { r.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, RolDTO rol)
        {
            using (DP2Context context = new DP2Context())
            {
                Rol c = context.TablaRoles.FindByID(rol.ID).LoadFromDTO(rol);
                context.TablaRoles.ModifyElement(c);
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, RolDTO rol)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaRoles.RemoveElementByID(rol.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }


    }
}
