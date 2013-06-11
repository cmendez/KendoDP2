using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KendoDP2.Models.Generic;
using ExtensionMethods;
using KendoDP2.Areas.Reclutamiento.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Evaluacion360.Models;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    public class WSOfertaLaboralController : WSController
    {
        // /WSOfertaLaboral/getOfertaLaboral
        public JsonResult getOfertaLaboral(string ofertaLaboralID = null)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    List<OfertaLaboral> lstOfertaLaboral = context.TablaOfertaLaborales.All();
                    if (lstOfertaLaboral.Count == 0) throw new Exception("No hay Ofertas Laborales");

                    return JsonSuccessGet(new { ofertaLaboral = lstOfertaLaboral.Select(x => x.ToDTO()).ToList() });

                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
                }

            }
        }

        public JsonResult getOfertasLaborales(string colaboradorID, string descripcionFase)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    FasePostulacion fp = context.TablaFasePostulacion.One(x => x.Descripcion.Equals(descripcionFase));
                    if (fp == null) throw new Exception("No existe Fase de Postulacion cuya descripcion sea " + descripcionFase);
                    if (fp.PostulacionesDeLaFase == null || fp.PostulacionesDeLaFase.Count == 0)
                        throw new Exception("La Fase de Postulacion " + fp.Descripcion + " no tiene asignada ninguna postulacion");

                    Colaborador c = context.TablaColaboradores.FindByID(Convert.ToInt32(colaboradorID));
                    if (c == null) throw new Exception("No existe el Colaborador cuyo ID = " + colaboradorID);

                    var listaOfertasLaboralesYPostulantes = new List<OfertaLaboralXPostulanteWSDTO>();
                    var listaPostulaciones = fp.PostulacionesDeLaFase.Select(x => x.OfertaLaboralXPostulante).Distinct();
                    var listaOfertasLaborales = listaPostulaciones
                                                .Select(x => x.OfertaLaboral)
                                                .Distinct()
                                                .Where(x => x.ResponsableID == c.ID) //Ofertas laborales cuyo responsable sea colaboradorID
                                                .ToList();
                    
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
                    OfertaLaboral ol = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(idOfertaLaboral));
                    if (ol == null) throw new Exception("No existe la Oferta Laboral con ID = " + idOfertaLaboral);

                    List<FuncionDTO> funciones = ol.Puesto.Funciones.Select(x => x.ToDTO()).ToList();
                    return JsonSuccessGet(new { funciones = funciones});
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }

        // /WSOfertaLaboral/getCompetencias
        public JsonResult getCompetencias(string idOfertaLaboral)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    OfertaLaboral ol = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(idOfertaLaboral));
                    if (ol == null) throw new Exception("No existe la Oferta Laboral con ID = " + idOfertaLaboral);
                    if(ol.PuestoID == 0 || ol.Puesto == null) throw new Exception("La Oferta Laboral no tiene asignado un puesto");

                    var competenciaAux = context.TablaCompetenciaXPuesto.Where(x => x.PuestoID == ol.PuestoID);
                    if (competenciaAux.Count == 0) throw new Exception("No existen competencias asignadas al puesto : " + ol.Puesto.Nombre);
                    
                    List<CompetenciaDTO> competencias = competenciaAux.Select(x => x.Competencia).Select(x => x.ToDTO()).ToList();
                    return JsonSuccessGet(new { competencias = competencias });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
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
                    if (e == null) throw new Exception("No existe el estado " + estadoOfertaLaboral + " para una Solicitud de Oferta Laboral");

                    List<OfertaLaboral> ofertas = context.TablaOfertaLaborales.Where(x => x.EstadoSolicitudOfertaLaboralID == e.ID);
                    if (ofertas == null || ofertas.Count == 0) throw new Exception("No existen Ofertas Laborales con el estado " + estadoOfertaLaboral);
                    
                    List<OfertaLaboralDTO> lstOfertas = ofertas.Select(x => x.ToDTO()).ToList();
                    return JsonSuccessGet(new { ofertasLaborales = lstOfertas });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }

        public JsonResult setEstadoSolicitudOfertaLaboral(string ofertaLaboralID, string nuevoEstado, string comentarios = "")
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    OfertaLaboral ol = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(ofertaLaboralID));
                    if (ol == null) throw new Exception("No existe la Oferta Laboral con ID = " + ofertaLaboralID);

                    EstadosSolicitudOfertaLaboral esol = context.TablaEstadosSolicitudes.One(x => x.Descripcion.Equals(nuevoEstado));
                    if (esol == null) throw new Exception("No existe el estado " + nuevoEstado + " para una Solicitud de Oferta Laboral");

                    ol.EstadoSolicitudOfertaLaboralID = esol.ID;
                    ol.Comentarios = comentarios;
                    context.TablaOfertaLaborales.ModifyElement(ol);

                    return JsonSuccessGet(new { ofertalaboral = ol.ToDTO()});
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
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

                    //ColaboradorXPuesto actual = context.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == c.ID && !x.FechaSalidaPuesto.HasValue);
                    ColaboradorXPuesto actual = c.ColaboradoresPuesto.Single(x => !x.FechaSalidaPuesto.HasValue);
                    if (actual == null) throw new Exception("El colaborador " + c.ToDTO().NombreCompleto + " no tiene un puesto actual determinado");

                    int areaID = actual.Puesto.AreaID;
                    //List<OfertaLaboral> lstOL = context.TablaOfertaLaborales.All();
                    List<OfertaLaboral> lstOL = context.TablaOfertaLaborales.Where(
                            x => // x.AreaID == areaID && //De la misma area que el postulante (colaborador) //SI QUIERES FILTRAR POR AREA, ACTIVAS AQUI NO MAS
                            x.EstadoSolicitudOfertaLaboralID == esol.ID && //Que coincida con el estado de la oferta laboral que deseo
                            !x.Postulantes.Select(y => y.PostulanteID).ToList().Contains(p.ID) //No sea una oferta ya postulada
                    );
                    if (lstOL == null || lstOL.Count == 0) 
                        throw new Exception("No se encontraron ofertas laborales que cumplan los requisitos (Misma area que el postulante/Coincida con el estado/No hayan sido ya postuladas)");

                    List<OfertaLaboralDTO> lstOLDTO = lstOL.Select(x => x.ToDTO()).ToList();
                    return JsonSuccessGet(new { ofertasLaborales = lstOLDTO });

                    //IEnumerable<OfertaLaboral> lstOL = p.OfertasPostuladas.Select(x => x.OfertaLaboral).Where(x => x.EstadoSolicitudOfertaLaboralID == esol.ID);
                    //if (lstOL == null) throw new Exception("De las ofertas postuladas por el colaborador " + c.ToDTO().NombreCompleto + " , ninguna esta en estado " + estadoOfertaLaboral);
                    
                    //List<OfertaLaboralDTO> lstOLDTO = lstOL.ToList().Select(x => x.ToDTO()).ToList();
                    //return JsonSuccessGet(new { ofertasLaborales = lstOLDTO });
                    
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
                }
            }
        }
    }
}
