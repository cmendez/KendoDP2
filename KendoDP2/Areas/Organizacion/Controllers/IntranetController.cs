using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    [Authorize()]
    public class IntranetController : Controller
    {
        public IntranetController()
        {
            ViewBag.Area = "Organizacion";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
                ViewBag.ColaboradorDTO = context.TablaColaboradores.FindByID(ColaboradorID).ToDTO();
                ViewBag.tipoDocumentos = context.TablaTiposDocumentos.All().Select(c => c.ToDTO()).ToList();
                ViewBag.gradoAcademico = context.TablaGradosAcademicos.All().Select(c => c.ToDTO()).ToList();
                ViewBag.estadosColaborador = context.TablaEstadosColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.pais = context.TablaPaises.All().Select(c => c.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(c => c.ToDTO()).ToList();
                return View();
            }
            
        }

    }
}
