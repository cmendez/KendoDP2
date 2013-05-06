using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    public class OrganizacionesController : Controller
    {
        //
        // GET: /Organizacion/Organizaciones/

         public OrganizacionesController()
        {
            ViewBag.Area = "Organizacion";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.Organizacion = context.TablaOrganizaciones.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoDocumentos = context.TablaTiposDocumentos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.pais = context.TablaPaises.All().Select(p => p.ToDTO()).ToList();
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();

                return View();
            }

        }

    }
}
