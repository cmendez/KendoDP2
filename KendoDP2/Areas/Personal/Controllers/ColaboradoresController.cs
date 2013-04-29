using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Personal.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Personal.Controllers
{
    public class ColaboradoresController : Controller
    {
        //
        // GET: /Personal/Colaborador/

        public ColaboradoresController()
        {
            ViewBag.Area = "Personal";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {

                ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoDocumentos = context.TablaTiposDocumentos.All().Select(p => p.ToDTO()).ToList();
                return View();
            }

        }

       // [AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Create([DataSourceRequest] DataSourceRequest request, ColaboradorDTO colaborador)
        //{
         //   using (DP2Context context = new DP2Context())
         //   {
         //       Colaborador c = new Colaborador(colaborador);
         //       context.TablaColaboradores.AddElement(c);
          //      return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState)); 
         //   }
       // }
    }
}
