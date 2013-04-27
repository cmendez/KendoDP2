using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Objetivos.Controllers
{
    [Authorize()]
    public class ObjetivosEmpresaController : Controller
    {
        public ObjetivosEmpresaController()
        {
            ViewBag.Area = "Objetivos";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context()) {
                ViewBag.periodos = context.TablaPeriodos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoObjetivosBSC = context.TablaTipoObjetivoBSC.All();
                return View();
            }
        }

    }
}
