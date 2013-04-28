using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Seguridad.Controllers
{
    public class WSUsuariosController : Controller
    {
        
        public ActionResult GetAllUsuarios()
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaUsuarios.All().Select(u => u.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

    }
}
