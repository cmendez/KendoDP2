using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Personal.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    [Authorize()]
    public class PuestosController : Controller
    {
        public PuestosController()
        {
            ViewBag.Area = "Organizacion";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(p => p.ToDTO()).ToList();
                return View();
            }

        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<PuestoDTO> puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                return Json(puestos.ToDataSourceResult(request));
            }
        }

 
        /*
         poifjvisohgdhsg
         sdihfosdihgfd}}
         voisdhfgpsdifdsfjgsd
         * sgoidhjgposi
         * dspogjposdjg
         * 
         posdjfpojhsd
         podsjfpsd
         */

    }
}
