using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KendoDP2.Models.Generic;
using ExtensionMethods;
using KendoDP2.Areas.Reclutamiento.Models;
using KendoDP2.Areas.Organizacion.Models;

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

        public JsonResult getFunciones(string idOfertaLaboral)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    List<FuncionDTO> funciones = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(idOfertaLaboral))
                        .Puesto.Funciones.Select(x => x.ToDTO()).ToList();
                    return JsonSuccessGet(new { funciones = funciones});
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }

        }

        public JsonResult getOfertasLaboralesXEstado(string estadoOfertaLaboral)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    EstadosSolicitudOfertaLaboral e = context.TablaEstadosSolicitudes.One(x => x.Descripcion.Equals(estadoOfertaLaboral));
                    List<OfertaLaboralDTO> ofertas = context.TablaOfertaLaborales
                        .Where(x => x.EstadoSolicitudOfertaLaboralID == e.ID).Select(x => x.ToDTO()).ToList();
                    return JsonSuccessGet(ofertas);
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }
    }
}
