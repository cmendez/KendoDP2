using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class EvaluadoresController : Controller
    {
        //
        // GET: /Evaluacion360/Evaluadores/

        public ActionResult Index(int idEvaluado)
        {


            using (DP2Context context = new DP2Context())
            {

                ViewBag.idEvaluado = idEvaluado * 100;
                ViewBag.Area = ""; //Solo es temporal
                ViewBag.susSubordinados = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                return View();
            }
        }

    }
}
