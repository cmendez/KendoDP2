using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Reclutamiento.Models;
using KendoDP2.Models.Seguridad;
using System.Globalization;

namespace KendoDP2.Controllers
{
    public class PublicController : Controller
    {
        //
        // GET: /Public/

        public PublicController()
        {
            ViewBag.Area = "Public";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.estadosSolicitudOferta = context.TablaEstadosSolicitudes.All().Select(e => e.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(a => a.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                return View();
            }
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<OfertaLaboralDTO> ofertasPosibles = context.TablaOfertaLaborales.All().Where(p => (p.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Aprobado")) && (p.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Pública"))).Select(p => p.ToDTO()).ToList();
                DateTime now = DateTime.Now;
                List<OfertaLaboralDTO> ofertasEnFecha = ofertasPosibles.Where(x => DateTime.ParseExact(x.FechaFinRequerimiento, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(now) >= 1).ToList();
                return Json(ofertasEnFecha.ToDataSourceResult(request));

            }
        }

    }
}
