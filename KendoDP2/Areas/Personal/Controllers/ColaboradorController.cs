using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Personal.Controllers
{
    public class ColaboradorController : Controller
    {
        //
        // GET: /Personal/Colaborador/

        public ColaboradorController()
        {
            ViewBag.Area = "Personal";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {

                ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                return View();
            }

        }
    }
}
