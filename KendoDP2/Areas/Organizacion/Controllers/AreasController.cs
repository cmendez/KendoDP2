using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    [Authorize()]
    public class AreasController : Controller
    {
        public AreasController() : base() {
            ViewBag.Area = "Organizacion";
        }
        
        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.PageSize = 8;
                ViewBag.Areas = context.TablaAreas.All().Select(a => a.ToDTO()).ToList();
                return View();
            }
        }

        public JsonResult AreasToTree(int? id)
        {
            using (DP2Context context = new DP2Context())
            {
                var areas = context.TablaAreas.Where(a => id.HasValue ? a.AreaSuperiorID == id : a.AreaSuperiorID == null).Select(a => a.ToTreeDTO()).OrderBy(a => a.Name);
                return Json(areas.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                var areas = context.TablaAreas.All().Select(a => a.ToDTO()).OrderBy(a => a.Nombre);
                return Json(areas.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, AreaDTO area)
        {
            using (DP2Context context = new DP2Context())
            {
                if (context.TablaAreas.One(x => x.Nombre.IsCaseInsensitiveEqual(area.Nombre)) != null)
                {
                    ModelState.AddModelError("Area", "Ya existe otra área con el mismo nombre.");
                    return Json(new[] { area }.ToDataSourceResult(request, ModelState));
                }
                Area a = new Area(area);
                a.ID = context.TablaAreas.AddElement(a);
                return Json(new[] { a.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, AreaDTO area)
        {
            using (DP2Context context = new DP2Context())
            {
                Area a = context.TablaAreas.FindByID(area.ID).LoadFromDTO(area);
                context.TablaAreas.ModifyElement(a);
                return Json(new[] { a.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, AreaDTO area)
        {
            using (DP2Context context = new DP2Context())
            {
                Area a = context.TablaAreas.FindByID(area.ID);
                if (!a.Puestos.Any(i => !i.IsEliminado) && !a.Areas.Any(i => !i.IsEliminado))
                    context.TablaAreas.RemoveElementByID(area.ID); 
                else
                    ModelState.AddModelError("Area", "No se puede eliminar un área con áreas o puestos subordinados.");
                return Json(ModelState.IsValid ? new object() : ModelState.ToDataSourceResult());
            }
        }
    }
}
