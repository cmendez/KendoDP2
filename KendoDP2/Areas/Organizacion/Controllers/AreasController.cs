using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    //[Authorize()]
    public class AreasController : Controller
    {
        public AreasController() : base() {
            ViewBag.Area = "Organizacion";
        }
        
        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                //ViewBag.Areas = context.TablaAreas.All().Select(a => a.ToDTO()).ToList();
                return View();
            }
        }

        public JsonResult AreasToTree(int? id)
        {
            using (DP2Context context = new DP2Context())
            {
                var areas = context.TablaAreas.Where(a => id.HasValue ? a.AreaSuperiorID == id : a.AreaSuperiorID == null).Select(a => a.ToTreeDTO());
                return Json(areas.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
