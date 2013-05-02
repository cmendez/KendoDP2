using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    [Authorize()]
    public class PuestosEvaluacionController : Controller
    {
        public PuestosEvaluacionController()
        {
            ViewBag.Area = "Evaluacion360";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.puestos = context.TablaPuestos.All().Select(x => x.ToDTO()).ToList();
                ViewBag.niveles = context.TablaNivelCapacidades.All().Select(x => x.ToDTO()).ToList();
                ViewBag.competencias = context.TablaCompetencias.All().Select(x => x.ToDTO()).ToList();
                return View();
            }

        }

    }
}
