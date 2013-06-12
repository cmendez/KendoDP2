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
        // /WSColaborador/getColaborador?id=
        public JsonResult getColaborador(string id)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    Colaborador c = context.TablaColaboradores.FindByID(Convert.ToInt32(id));
                    if (c == null) throw new Exception("No existe colaborador con ID = " + id);
                    if (c.ColaboradoresPuesto == null || c.ColaboradoresPuesto.Count == 0) throw new Exception("El colaborador " + c.ToDTO().NombreCompleto + " no tiene puestos asignados");
                    ColaboradorDTO colaborador = c.ToDTO();


                    ColaboradorXPuesto actual = c.ColaboradoresPuesto.Single(x => !x.FechaSalidaPuesto.HasValue);
                    if (actual == null) throw new Exception("El colaborador " + colaborador.NombreCompleto + " no tiene un puesto actual determinado");

                    PuestoDTO puesto = actual.Puesto.ToDTO();
                    AreaDTO area = actual.Puesto.Area.ToDTO();
                    
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

        // /WSColaborador/getContactos?id=
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

        // /WSColaborador/tieneJefe?colaboradorID=
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

        // /WSColaborador/getEquipoTrabajo
        public JsonResult getEquipoTrabajo(string colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    Colaborador c = context.TablaColaboradores.FindByID(Convert.ToInt32(colaboradorID));
                    if (c == null) throw new Exception("No existe colaborador cuyo ID = " + colaboradorID);

                    // Obtengo el ID de mi jefe
                    var ColaboradorPuesto_Actual = context.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == c.ID && !x.FechaSalidaPuesto.HasValue);
                    if (ColaboradorPuesto_Actual == null) throw new Exception("El colaborador " + c.ToDTO().NombreCompleto + " con ID = " + colaboradorID + " no tiene asignado puesto alguno por el momento");

                    int puestoSuperiorID = ColaboradorPuesto_Actual.Puesto.PuestoSuperiorID.GetValueOrDefault();
                    var ColaboradorPuesto_Superior = context.TablaColaboradoresXPuestos.One(x => x.PuestoID == puestoSuperiorID && !x.FechaSalidaPuesto.HasValue);
                    //Aca no se valida la no existencia de ColaboradorPuesto_Superior, porque de suceder eso puesto se asume como puesto superior al puesto actual, osea al colaborador actual como eje de primer nivel

                    Colaborador colaboradorNivel1;
                    List<Puesto> puestosNivel2;
                    List<ColaboradorDTO> colaboradoresNivel2 = new List<ColaboradorDTO>();

                    if (ColaboradorPuesto_Superior != null) //Si el Jefe Existe
                    {
                        colaboradorNivel1 = ColaboradorPuesto_Superior.Colaborador;
                        puestosNivel2 = context.TablaPuestos.Where(x => x.PuestoSuperiorID == puestoSuperiorID);
                    }
                    else //Hago Nivel 1 al colaboradorID
                    {
                        colaboradorNivel1 = context.TablaColaboradores.FindByID(c.ID);
                        int puestoColaboradorID = ColaboradorPuesto_Actual.Puesto.ID;
                        //int puestoColaboradorID = context.TablaColaboradoresXPuestos
                        //    .One(x => x.ColaboradorID == c.ID && !x.FechaSalidaPuesto.HasValue)
                        //    .Puesto.ID;
                        puestosNivel2 = context.TablaPuestos.Where(x => x.PuestoSuperiorID == puestoColaboradorID);
                    }

                    foreach (var puesto in puestosNivel2)
                    {
                        ColaboradorXPuesto cxp = context.TablaColaboradoresXPuestos
                            .One(x => x.PuestoID == puesto.ID && !x.FechaSalidaPuesto.HasValue);
                        if (cxp == null) continue;
                        
                        Colaborador colaboradorNivel2 = cxp.Colaborador;
                        List<Puesto> puestosNivel3 = context.TablaPuestos.Where(x => x.PuestoSuperiorID == puesto.ID);
                        List<ColaboradorDTO> colaboradoresNivel3 = new List<ColaboradorDTO>();
                        
                        foreach (var puesto2 in puestosNivel3)
                        {
                            cxp = context.TablaColaboradoresXPuestos
                                .One(x => x.PuestoID == puesto2.ID && !x.FechaSalidaPuesto.HasValue);
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

        // /WSColaborador/getEventos?colaboradorID=
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
