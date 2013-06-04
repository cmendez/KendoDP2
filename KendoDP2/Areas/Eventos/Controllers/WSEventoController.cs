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
                    DateTime inicio = DateTime.ParseExact(fechaDesde, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                    DateTime fin = DateTime.ParseExact(fechaHasta, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture); ;
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
