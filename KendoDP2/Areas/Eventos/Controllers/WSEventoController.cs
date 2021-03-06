﻿using System;
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

                    List<Evento> eventosCreador = context.TablaEvento
                        .Where(x => x.CreadorID == c.ID && 
                            DateTime.Compare(inicio, x.Inicio) <= 0 && DateTime.Compare(fin, x.Fin) >= 0);
                    
                    List<Evento> eventosInvitado = c.EventosInvitado
                        .Select(x => x.Evento)
                        .Where(x => DateTime.Compare(inicio, x.Inicio) <= 0 && DateTime.Compare(fin, x.Fin) >= 0)
                        .ToList();

                    List<Evento> eventos = new List<Evento>(eventosCreador);
                    foreach (var item in eventosInvitado)
                    {
                        if (!eventos.Contains(item)) eventos.Add(item);
                    }
                    eventos = eventos.OrderBy(x => x.Nombre).ToList();

                    if (eventos == null || eventos.Count == 0) return JsonSuccessGet(new { eventos = new List<EventoDTO>() });

                    List<EventoDTO> eventosDTO = eventos.Select(x => x.ToDTO()).ToList();
                    return JsonSuccessGet(new { eventos = eventosDTO });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException + ex.Source + ex.StackTrace);
                }
            }
        }

    }
}
