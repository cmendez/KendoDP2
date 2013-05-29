using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;
using ExtensionMethods;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    public class WSColaboradorController : WSController
    {

        public JsonResult getColaborador(string id)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    ColaboradorDTO colaborador = context.TablaColaboradores.FindByID(Convert.ToInt32(id)).ToDTO();
                    PuestoDTO puesto = colaborador.PuestoID == 0 ? new PuestoDTO() : context.TablaPuestos.FindByID(colaborador.PuestoID).ToDTO();
                    AreaDTO area = colaborador.AreaID == 0 ? new AreaDTO() : context.TablaAreas.FindByID(colaborador.AreaID).ToDTO();
                    return JsonSuccessGet(new
                    {
                        colaborador = colaborador,
                        puesto = puesto,
                        area = area
                    });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }

        public JsonResult getContactos(string id)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    List<ColaboradorDTO> contactos = context.TablaColaboradores.FindByID(Convert.ToInt32(id)).Contactos
                        .Select(c => c.Contacto).Select(c => c.ToDTO()).ToList();
                    return JsonSuccessGet(new { contactos = contactos });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }

        public JsonResult ColaboradoresToList()
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    List<ColaboradorDTO> colaboradores = context.TablaColaboradores.All().Select(a => a.ToDTO()).OrderBy(a => a.NombreCompleto).ToList();
                    return JsonSuccessGet(new { colaboradores = colaboradores });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error: " + ex.Message);
                }
            }
            
        }

        public JsonResult tieneJefe(string colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    Puesto puestoUltimo = context.TablaColaboradoresXPuestos
                        .One(x =>   (x.ColaboradorID == Convert.ToInt32(colaboradorID)) && 
                                    (!x.FechaSalidaPuesto.HasValue))
                        .Puesto;
                    return puestoUltimo.PuestoSuperiorID.HasValue ? 
                        JsonSuccessGet(true) : JsonSuccessGet(false);
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }

        public JsonResult getEquipoTrabajo(string colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    //jalo a mi jefe
                    int puestoSuperiorID = context.TablaColaboradoresXPuestos
                        .One(x => x.ColaboradorID == Convert.ToInt32(colaboradorID) && !x.FechaSalidaPuesto.HasValue)
                        .Puesto.PuestoSuperiorID.GetValueOrDefault();
                    Colaborador jefe = context.TablaColaboradoresXPuestos
                        .One(x => x.PuestoID == puestoSuperiorID && !x.FechaSalidaPuesto.HasValue).Colaborador;
                    List<Puesto> puestosDebajoJefe = context.TablaPuestos.Where(x => x.PuestoSuperiorID == puestoSuperiorID);
                    List<ColaboradorDTO> lista = new List<ColaboradorDTO>();
                    foreach (var puesto in puestosDebajoJefe)
                    {
                        Colaborador colaboradorDebajoJefe = context.TablaColaboradoresXPuestos
                            .One(x => x.PuestoID == puesto.ID && !x.FechaSalidaPuesto.HasValue).Colaborador;
                        List<Puesto> puestosDebajoAmiwi = context.TablaPuestos.Where(x => x.PuestoSuperiorID == puesto.ID);
                        List<ColaboradorDTO> lista2 = new List<ColaboradorDTO>();
                        foreach (var puesto2 in puestosDebajoAmiwi)
                        {
                            Colaborador colaboradorDebajoAmiwi = context.TablaColaboradoresXPuestos
                                .One(x => x.PuestoID == puesto2.ID && !x.FechaSalidaPuesto.HasValue).Colaborador;
                            lista2.Add(new ColaboradorDTO(colaboradorDebajoAmiwi));
                        }
                        ColaboradorDTO aux2 = new ColaboradorDTO(colaboradorDebajoJefe, lista2);
                        lista.Add(aux2);
                    }
                    ColaboradorDTO aux = new ColaboradorDTO(jefe, lista);

                    return JsonSuccessGet(aux);
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }

        }
    }
}
