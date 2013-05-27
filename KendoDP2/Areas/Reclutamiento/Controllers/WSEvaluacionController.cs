using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ExtensionMethods;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    public class WSEvaluacionController : WSController
    {
        public JsonResult getEvaluacionXPostulacion(string idOfertaLaboral, string idPostulante, string fase)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    //Por empezar este WS


                    return JsonSuccessGet("");
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }

    }
}
