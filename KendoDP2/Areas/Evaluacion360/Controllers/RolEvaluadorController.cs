using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;


namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class RolEvaluadorController : Controller
    {
        //
        // GET: /Evaluacion360/RolEvaluador/

        public RolEvaluadorController()
            : base()
        {
            ViewBag.Area = "Evaluacion360";
        }


        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);

                ViewBag.listaprocesos = context.TablaColaboradores.FindByID(ColaboradorID).ColaboradorXProcesoEvaluaciones.Select(c => c.ProcesoEvaluacion.ToDTO()).ToList(); 

                return View();
            }
        }

    }
}
