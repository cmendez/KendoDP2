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
    public class PuestosController : Controller
    {
        //
        // GET: /Organizacion/Puesto/
        public PuestosController()
        {
            ViewBag.Area = "Organizacion";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
          /*      ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoDocumentos = context.TablaTiposDocumentos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.estadosColaborador = context.TablaEstadosColaboradores.All().Select(p => p.ToDTO()).ToList();
                ViewBag.pais = context.TablaPaises.All().Select(p => p.ToDTO()).ToList();
                ViewBag.gradoAcademico = context.TablaGradosAcademicos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(p => p.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
            */    return View();
            }

        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<PuestoDTO> puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).OrderBy(x => x.ID).ToList();
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
