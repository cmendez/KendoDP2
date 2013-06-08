﻿using System;
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
                        .One(x => (x.ColaboradorID == Convert.ToInt32(colaboradorID)) &&
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
                    // Obtengo el ID de mi jefe
                    int puestoSuperiorID = context.TablaColaboradoresXPuestos
                        .One(x => x.ColaboradorID == Convert.ToInt32(colaboradorID) &&
                                    !x.FechaSalidaPuesto.HasValue)
                        .Puesto.PuestoSuperiorID.GetValueOrDefault();
                    ColaboradorXPuesto cxpInicial = context.TablaColaboradoresXPuestos
                        .One(x => x.PuestoID == puestoSuperiorID &&
                                    !x.FechaSalidaPuesto.HasValue);
                    Colaborador colaboradorNivel1;
                    List<Puesto> puestosNivel2;
                    List<ColaboradorDTO> colaboradoresNivel2 = new List<ColaboradorDTO>();

                    if (cxpInicial != null) //Si el Jefe Existe
                    {
                        colaboradorNivel1 = cxpInicial.Colaborador;
                        puestosNivel2 = context.TablaPuestos.Where(x => x.PuestoSuperiorID == puestoSuperiorID);
                    }
                    else //Hago Nivel 1 al colaboradorID
                    {
                        colaboradorNivel1 = context.TablaColaboradores.FindByID(Convert.ToInt32(colaboradorID));
                        int puestoColaboradorID = context.TablaColaboradoresXPuestos
                            .One(x => x.ColaboradorID == Convert.ToInt32(colaboradorID) &&
                                        !x.FechaSalidaPuesto.HasValue)
                            .Puesto.ID;
                        puestosNivel2 = context.TablaPuestos.Where(x => x.PuestoSuperiorID == puestoColaboradorID);
                    }

                    foreach (var puesto in puestosNivel2)
                    {
                        ColaboradorXPuesto cxp = context.TablaColaboradoresXPuestos
                            .One(x => x.PuestoID == puesto.ID &&
                                        !x.FechaSalidaPuesto.HasValue);
                        if (cxp == null) continue;
                        Colaborador colaboradorNivel2 = cxp.Colaborador;
                        List<Puesto> puestosNivel3 = context.TablaPuestos.Where(x => x.PuestoSuperiorID == puesto.ID);
                        List<ColaboradorDTO> colaboradoresNivel3 = new List<ColaboradorDTO>();
                        foreach (var puesto2 in puestosNivel3)
                        {
                            cxp = context.TablaColaboradoresXPuestos
                                .One(x => x.PuestoID == puesto2.ID &&
                                            !x.FechaSalidaPuesto.HasValue);
                            if (cxp == null) continue;
                            Colaborador colaboradorNivel3 = cxp.Colaborador;
                            ColaboradorDTO colaboradorNivel3DTO = new ColaboradorDTO(colaboradorNivel3);
                            colaboradoresNivel3.Add(colaboradorNivel3DTO);
                        }
                        ColaboradorDTO colaboradorNivel2DTO = new ColaboradorDTO(colaboradorNivel2, colaboradoresNivel3);
                        colaboradoresNivel2.Add(colaboradorNivel2DTO);
                    }
                    ColaboradorDTO colaboradorNivel1DTO = new ColaboradorDTO(colaboradorNivel1, colaboradoresNivel2);

                    return JsonSuccessGet(new { jefe = colaboradorNivel1DTO });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }

        }

        public JsonResult getEventos(string colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {

                    return JsonSuccessGet();
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }

            }
        }
    }
}
