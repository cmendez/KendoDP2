using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ExtensionMethods;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Reclutamiento.Models;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    public class WSPostulanteController : WSController
    {
        // /WSPostulante/getPostulante
        public JsonResult getPostulante(string postulanteID = null)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    List<Postulante> lstPostulante = context.TablaPostulante.All();
                    if (lstPostulante.Count == 0) throw new Exception("No hay postulantes");

                    return JsonSuccessGet(new { postulantes = lstPostulante.Select(x => x.ToDTO()).ToList() });

                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
                }

            }
        }

    }
}
