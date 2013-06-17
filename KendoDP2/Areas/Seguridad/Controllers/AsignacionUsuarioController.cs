using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Seguridad.Controllers
{
    public class AsignacionUsuarioController : Controller
    {

        public AsignacionUsuarioController()
        {
            ViewBag.Area = "Seguridad";
        }
        //
        // GET: /Seguridad/AsignacionUsuario/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                var X=context.TablaColaboradores.Where(c => c.Username != "admin");
                return Json(X.Select(I => I.ToDTO()).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _Update([DataSourceRequest] DataSourceRequest request, ColaboradorDTO colaborador)
        {
            using (DP2Context context = new DP2Context())
            {
                Colaborador c = context.TablaColaboradores.One(o => o.ID == colaborador.ID);
                c.Username = colaborador.Usuario;
                c.Password = colaborador.Password;
                context.TablaColaboradores.ModifyElement(c);
                return Json(context.TablaColaboradores.Where(f => f.Username != "admin").Select(I => I.ToDTO()).ToDataSourceResult(request));
            }
        }

        
    }
}
