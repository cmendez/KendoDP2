using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KendoDP2.Models.Generic;
using ExtensionMethods;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    public class WSOfertaLaboralController : WSController
    {

        public JsonResult getOfertasLaborales()
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {

                }
                catch (Exception ex)
                {

                }
            }
            
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}
