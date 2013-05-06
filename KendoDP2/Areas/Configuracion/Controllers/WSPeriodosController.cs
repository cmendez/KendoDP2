using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Configuracion.Controllers
{
    public class WSPeriodosController : Controller
    {
        public ActionResult ListarPeriodos()
        {
            using (DP2Context context = new DP2Context())
            {
                List<PeriodoDTO> periodos = context.TablaPeriodos.All().Select(p => p.ToDTO()).OrderByDescending(x => x.FechaInicio).ToList();
                return Json(periodos, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
