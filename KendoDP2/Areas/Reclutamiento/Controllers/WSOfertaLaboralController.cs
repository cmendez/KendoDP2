﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KendoDP2.Models.Generic;
using ExtensionMethods;
using KendoDP2.Areas.Reclutamiento.Models;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    public class WSOfertaLaboralController : WSController
    {

        public JsonResult getOfertasLaborales(string descripcionFase)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    List<OfertaLaboralXPostulanteWSDTO> listaOfertasLaboralesYPostulantes = new List<OfertaLaboralXPostulanteWSDTO>();
                    IEnumerable<OfertaLaboralXPostulante> listaPostulaciones =
                        context.TablaFasePostulacion.One(x => x.Descripcion.Equals(descripcionFase))
                        .PostulacionesDeLaFase.Select(x=>x.OfertaLaboralXPostulante).Distinct();
                    List<OfertaLaboral> listaOfertasLaborales = listaPostulaciones.Select(x => x.OfertaLaboral).Distinct().ToList();
                    foreach (OfertaLaboral oflab in listaOfertasLaborales)
                    {
                        List<Postulante> lstPostulante = listaPostulaciones.Where(x => x.OfertaLaboral.Equals(oflab))
                            .Select(x => x.Postulante).Distinct().ToList();
                        listaOfertasLaboralesYPostulantes.Add(new OfertaLaboralXPostulanteWSDTO(oflab, lstPostulante));
                    }
                    
                    return JsonSuccessGet(new { ofertasLaborales = listaOfertasLaboralesYPostulantes });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }
    }
}
