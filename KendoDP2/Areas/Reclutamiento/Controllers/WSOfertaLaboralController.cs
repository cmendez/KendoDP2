using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    public class WSOfertaLaboralController : Controller
    {
        //
        // GET: /Reclutamiento/WSOfertaLaboral/getOfertasLaborales

        public JsonResult getOfertasLaborales()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}
