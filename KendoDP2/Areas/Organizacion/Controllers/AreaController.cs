using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    [Authorize()]
    public class AreaController : Controller
    {
        public AreaController() : base() {
            ViewBag.Area = "Organizacion";
        }
        
        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.Areas = context.TablaAreas.All().Select(a => a.ToDTO()).ToList();
                return View();
            }
        }
    }
}
