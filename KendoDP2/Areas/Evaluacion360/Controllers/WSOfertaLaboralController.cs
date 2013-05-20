using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ExtensionMethods;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class WSOfertaLaboralController : WSController
    {
        //
        // GET: /Evaluacion360/WSOfertaLaboral/

        public JsonResult getOfertasLaborales(string fase)
        {
            //using (DP2Context context = new DP2Context())
            //{

            //}
            
            
            
            
            return Json("",JsonRequestBehavior.AllowGet);
        }

    }
}
