using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ExtensionMethods;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Eventos.Models;

namespace KendoDP2.Areas.Eventos.Controllers
{
    public class WSEventoController : WSController
    {

        public JsonResult getEventos(string colaboradorID, string fechaDesde, string fechaHasta)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    DateTime inicio = Convert.ToDateTime(fechaDesde);
                    DateTime fin = Convert.ToDateTime(fechaHasta);
                    List<EventoDTO> eventos = context.TablaEvento
                        .Where(x => x.CreadorID == Convert.ToInt32(colaboradorID) &&
                                    DateTime.Compare(inicio,x.Inicio) <= 0 &&
                                    DateTime.Compare(fin, x.Fin) >= 0)
                        .Select(x => x.ToDTO()).ToList();
                    return JsonSuccessGet(new { eventos = eventos });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }

    }
}
