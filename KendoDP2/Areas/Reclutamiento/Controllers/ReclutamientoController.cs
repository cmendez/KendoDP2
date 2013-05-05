using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Reclutamiento.Models;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    [Authorize()]
    public class ReclutamientoController : Controller
    {
        //
        // GET: /Reclutamiento/Reclutamiento/

        public ActionResult Index()
        {
            return View();
        }

        // GET: /Reclutamiento/Postulando

        public ActionResult Postulando()
        {
            //Mostrar los puestos de cada oferta
            //Tener cargada junto con los puestos la descripcion y los requisitos
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaOfertaLaborals.Where(o => o.Estado == 1).Select(o => o.ToDTO()),JsonRequestBehavior.AllowGet);
            }

            //**Solo se deberían mostrar las ofertas vigentes
            //return View();
        }

        public ActionResult Candidatos()
        {
            return View();
        }

    }
}
