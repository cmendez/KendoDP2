using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ExtensionMethods;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Eventos.Models;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Eventos.Controllers
{
    public class WSEventoController : WSController
    {

        // /WSEvento/getEventos?colaboradorID=&fechaDesde=&fechaHasta=
        public JsonResult getEventos(string colaboradorID, string fechaDesde, string fechaHasta)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    Colaborador c = context.TablaColaboradores.FindByID(Convert.ToInt32(colaboradorID));
                    if (c == null) return JsonErrorGet("No existe el Colaborador con ID = " + colaboradorID);

                    DateTime inicio, fin;
                    try
                    {
                           inicio = DateTime.ParseExact(fechaDesde, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                    }
                    catch (Exception)
                    {
                        return JsonErrorGet("Formato de la fecha de inicio incorrecto");
                    }

                    try
                    {
                        fin = DateTime.ParseExact(fechaHasta, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                    }
                    catch (Exception)
                    {
                        return JsonErrorGet("Formato de la fecha de fin incorrecto");                        
                    }

                    if (DateTime.Compare(inicio, fin) >= 0) return JsonErrorGet("La fecha final no puede ser menor que la fecha inicial");

                    List<Evento> eventos;
                    try
                    {
                        eventos = context.TablaEvento.Where(x =>
                            (x.CreadorID == c.ID || x.Invitados.Select(y => y.Asistente).Select(z => z.ID).ToList().Contains(c.ID)) &&
                            DateTime.Compare(inicio, x.Inicio) <= 0 && DateTime.Compare(fin, x.Fin) >= 0);
                    }
                    catch (Exception ex)
                    {
                        return JsonErrorGet("Fallo el query de shettttt xD!");
                    }

                    List<EventoDTO> eventosDTO = eventos.Select(x => x.ToDTO()).ToList();
                    return JsonSuccessGet(new { eventos = eventosDTO });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
                }
            }
        }

    }
}
