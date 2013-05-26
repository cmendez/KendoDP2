using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Objetivos.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Objetivos.Controllers
{
    public class MisObjetivosController : Controller
    {
        public MisObjetivosController()
        {
            ViewBag.Area = "Objetivos";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                int colaboradorID = DP2MembershipProvider.GetPersonaID(this);
                int puestoID = context.TablaColaboradores.FindByID(colaboradorID).ToDTO().PuestoID;
                Puesto puesto = context.TablaPuestos.FindByID(puestoID);
                ViewBag.periodos = context.TablaPeriodos.All().Select(c => c.ToDTO()).ToList();
                ViewBag.objetivos = puesto.Objetivos.Select(c => c.ToDTO()).ToList();
                return View();
            }
        }

    }
}
