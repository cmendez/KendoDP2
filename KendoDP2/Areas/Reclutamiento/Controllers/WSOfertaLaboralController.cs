﻿using System;
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
                    return JsonSuccessGet(new { ofertasLaborales = ofertas });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult aprobarSolicitudOfertaLaboral(string ofertaLaboralID, string nuevoEstado, string comentarios)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    OfertaLaboral ol = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(ofertaLaboralID));
                    ol.EstadoSolicitudOfertaLaboralID = context.TablaEstadosSolicitudes.One(x => x.Descripcion.Equals(nuevoEstado)).ID;
                    ol.Comentarios = comentarios;
                    context.TablaOfertaLaborales.ModifyElement(ol);
                    return JsonSuccessPost(new { ofertalaboral = ol.ToDTO()});
                }
                catch (Exception ex)
                {
                    return JsonErrorPost("Error en la BD: " + ex.Message);
                }
            }

        }

        public JsonResult registrarPostulacion(string colaboradorID, string ofertaLaboralID)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    Colaborador c = context.TablaColaboradores.FindByID(Convert.ToInt32(colaboradorID));
                    if (c == null) throw new Exception("No existe Colaborador con ID = " + colaboradorID);

                    OfertaLaboral ol = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(ofertaLaboralID));
                    if (ol == null) throw new Exception("No existe Oferta Laboral con ID = " + ofertaLaboralID);


                    OfertaLaboralXPostulante ofxp = new OfertaLaboralXPostulante();
                    ofxp.OfertaLaboralID = ol.ID;
                    
                    //Crear el postulante a partir de ese colaborador
                    Postulante p = context.TablaPostulante.One(x => x.ColaboradorID == Convert.ToInt32(colaboradorID));
                    if (p == null) // Si no encuentro al colaborador como postulante, creo el postulante
                    {
                        context.TablaPostulante.AddElement(p = new Postulante(c));
                    }

                    ofxp.PostulanteID = p.ID;
                    ofxp.EstadoPostulantePorOfertaID = context.TablaEstadoPostulanteXOferta.One(x => x.Descripcion.Equals("Inscrito")).ID;
                    context.TablaOfertaLaboralXPostulante.AddElement(ofxp);

                    FasePostulacionXOfertaLaboralXPostulante fpxolxp = new FasePostulacionXOfertaLaboralXPostulante();
                    fpxolxp.FasePostulacionID = context.TablaFasePostulacion.One(x => x.Descripcion.Equals("Registrado")).ID;
                    fpxolxp.OfertaLaboralXPostulanteID = ofxp.ID;
                    context.TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(fpxolxp);

                    return JsonSuccessGet(new { postulacion = ofxp.ToDTO(), fasePostulacionID = fpxolxp.ID });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
                }
            }

        }

        public JsonResult getOfertasLaboralesColaborador(string colaboradorID, string estadoOfertaLaboral)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    EstadosSolicitudOfertaLaboral esol = context.TablaEstadosSolicitudes.One(x => x.Descripcion.Equals(estadoOfertaLaboral));
                    if (esol == null) throw new Exception("No existe el estado " + estadoOfertaLaboral + " para una Solicitud de Oferta Laboral");

                    Colaborador c = context.TablaColaboradores.FindByID(Convert.ToInt32(colaboradorID));
                    if (c == null) throw new Exception("No existe el Colaborador con ID = " + colaboradorID);

                    Postulante p = context.TablaPostulante.One(x => x.ColaboradorID == c.ID);
                    if (p == null) throw new Exception("El colaborador " + c.ToDTO().NombreCompleto + " no existe como postulante");
                    if (p.OfertasPostuladas == null || p.OfertasPostuladas.Count == 0) throw new Exception("El colaborador " + c.ToDTO().NombreCompleto + " no ha postulado a nada");

                    //context.TablaOfertaLaboralXPostulante.Where( x=> x.PostulanteID == p.ID && 

                    return JsonSuccessGet();
                    
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
                }
            }
        }
    }
}
