using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;

namespace KendoDP2.Areas.Seguridad.Controllers
{
    public class AsignarCredencialesController : Controller
    {
        //
        // GET: /Seguridad/AsignarCredenciales/
        public AsignarCredencialesController()
        {
            ViewBag.Area = "Seguridad";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                var salida = context.TablaColaboradores.Where(xx=>xx.Username!="admin").Select(c=>c.ToDTO());
                return Json(salida.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _Update([DataSourceRequest] DataSourceRequest request, ColaboradorDTO colaborador)
        {
            using (DP2Context context = new DP2Context())
            {
                Colaborador col = context.TablaColaboradores.One(c=>c.ID==colaborador.ID);
                col.Username = colaborador.Usuario;
                col.Password = colaborador.Password;
                context.TablaColaboradores.ModifyElement(col);
                return View("Index");
            }
        }

    }
}
