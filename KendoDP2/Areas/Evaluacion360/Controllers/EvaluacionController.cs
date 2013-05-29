using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    [Authorize()]
    public class EvaluacionController : Controller
    {
        //
        // GET: /Evaluacion360/Evaluacion/
        public EvaluacionController()
            : base()
        {
            ViewBag.Area = "Evaluacion360";
            
        }

        public ActionResult Index()
        {
            // Recibir esta variable como parámetro
            int evaluadoID = 2;
            using (DP2Context context = new DP2Context())
            {
                ViewBag.evaluado = context.TablaColaboradores.One(c => c.ID == evaluadoID).ToDTO();
                    //context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                return View();
            }
        }

    }
}
