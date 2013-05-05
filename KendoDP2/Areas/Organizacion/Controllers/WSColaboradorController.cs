using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    public class WSColaboradorController : Controller
    {
        //
        // GET: /Organizacion/WSColaborador/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getColaborador(string username)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(new { 
                    nombres = "algo",
                    apellidos = "algo",
                    area = "algo",
                    puesto = "algo",
                    email = "algo",
                    anexo = "algo",
                    fecha_ingreso = "algo"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ColaboradoresToList()
        {
            using (DP2Context context = new DP2Context())
            {
                var colaboradores = context.TablaColaboradores.All().Select(a => a.ToDTO()).OrderBy(a => a.NombreCompleto);
                return Json(colaboradores, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
