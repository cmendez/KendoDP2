using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Reclutamiento.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoDP2.Areas.Objetivos.Models;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    
    public class ReclutamientoController : Controller
    {
        //
        // GET: /Reclutamiento/Reclutamiento/
        public ReclutamientoController()
        {
            ViewBag.Area = "Reclutamiento";
        }


        public ActionResult Index()
        {
            return View();
        }

        // GET: /Reclutamiento/Reclutamiento/Postulando

        public ActionResult Postulando()
        {
            //Mostrar los puestos de cada oferta
            //Tener cargada junto con los puestos la descripcion y los requisitos
            using (DP2Context context = new DP2Context())
            {
                //return View();
                return Json(context.TablaPeriodos.All().Select(o => o.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
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
