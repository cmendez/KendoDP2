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
        // /WSOfertaLaboral/getOfertaLaboral?ofertaLaboralID=
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

        // /WSOfertaLaboral/getOfertasLaborales?colaboradorID=&descripcionFase=
        public JsonResult getOfertasLaborales(string colaboradorID, string descripcionFase)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    FasePostulacion fp = context.TablaFasePostulacion.One(x => x.Descripcion.Equals(descripcionFase));
                    if (fp == null) return JsonErrorGet("No existe Fase de Postulacion cuya descripcion sea " + descripcionFase);
                    if (fp.PostulacionesDeLaFase == null || fp.PostulacionesDeLaFase.Count == 0)
                        return JsonErrorGet("La Fase de Postulacion " + fp.Descripcion + " no tiene asignada ninguna postulacion");

                    Colaborador responsable = context.TablaColaboradores.FindByID(Convert.ToInt32(colaboradorID));
                    if (responsable == null) return JsonErrorGet("No existe el Colaborador cuyo ID = " + colaboradorID);

                    EstadoPostulantePorOferta estado = null;
                    if (descripcionFase.Equals("Registrado"))
                        estado = context.TablaEstadoPostulanteXOferta.One(x => x.Descripcion.Equals("Aprobado Fase 1"));
                    else if (descripcionFase.Equals("Aprobado Externo"))
                        estado = context.TablaEstadoPostulanteXOferta.One(x => x.Descripcion.Equals("Aprobado Fase 2"));
                    else if (descripcionFase.Equals("Aprobado RRHH"))
                        estado = context.TablaEstadoPostulanteXOferta.One(x => x.Descripcion.Equals("Aprobado Fase 3"));

                    if (estado == null) JsonErrorGet("La fase de postulacion " + fp.Descripcion + " no tiene asignada ninguna postulacion");

                    var listaOfertasLaboralesYPostulantes = new List<OfertaLaboralXPostulanteWSDTO>();
                    
                    // Obtengo las postulaciones que SEAN DE esa fase
                    //var lstPostulacionesDeLaFase = context.TablaOfertaLaboralXPostulante.All();
                    var lstPostulacionesDeLaFase = context.TablaOfertaLaboralXPostulante.Where(x => x.EstadoPostulantePorOferta == estado);
                    // Obtengo las postulaciones que HAN PASADO por esa fase
                    //var lstPostulacionesDeLaFase = fp.PostulacionesDeLaFase.Select(x => x.OfertaLaboralXPostulante).Distinct().ToList();
                    if (lstPostulacionesDeLaFase == null || lstPostulacionesDeLaFase.Count == 0)
                        return JsonErrorGet("No existe postulaciones que hayan llegado a la fase " + descripcionFase);

                    //De esas postulaciones filtro por aquellas cuyo responsable es "colaboradorID"
                    var lstOfertasLaboralesResponsable = lstPostulacionesDeLaFase
                                                        .Select(x => x.OfertaLaboral).Distinct()
                                                        .Where(x => x.ResponsableID == responsable.ID) //Ofertas laborales cuyo responsable sea colaboradorID
                                                        .ToList();

                    foreach (OfertaLaboral oflab in lstOfertasLaboralesResponsable)
                    {
                        var lstPostulante = oflab.Postulantes.Select(x => x.Postulante).ToList();
                        listaOfertasLaboralesYPostulantes.Add(new OfertaLaboralXPostulanteWSDTO(oflab, lstPostulante));
                    }
                    
                    return JsonSuccessGet(new { ofertasLaborales = listaOfertasLaboralesYPostulantes });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
                }
            }
        }

        // /WSOfertaLaboral/getFunciones?idOfertaLaboral=
        public JsonResult getFunciones(string idOfertaLaboral)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    OfertaLaboral ol = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(idOfertaLaboral));
                    if (ol == null) return JsonErrorGet("No existe la Oferta Laboral con ID = " + idOfertaLaboral);
                    if (ol.Puesto == null) return JsonErrorGet("ERROR LOGICO -> La oferta laboral no tiene puesto asignado");
                    if (ol.Puesto.Funciones == null || ol.Puesto.Funciones.Count() == 0) return JsonErrorGet("ERROR LOGICO -> El puesto de la oferta laboral no tiene funciones asignadas");

                    List<FuncionDTOWS> funciones = ol.Puesto.Funciones.Select(x => x.ToDTOWS()).ToList();
                    return JsonSuccessGet(new { funciones = funciones});
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
                }
            }
        }

        // /WSOfertaLaboral/getCompetencias
        // /WSOfertaLaboral/getCompetencias?idOfertaLaboral=
        public JsonResult getCompetencias(string idOfertaLaboral = null)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    if (idOfertaLaboral == null) // Envio TODAS las competencias
                    {
                        List<CompetenciaDTO> competencias = context.TablaCompetencias.All().Select(x => x.ToDTO()).ToList();
                        return JsonSuccessGet(new { competencias = competencias });
                    }
                    else
                    {
                        OfertaLaboral ol = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(idOfertaLaboral));
                        if (ol == null) throw new Exception("No existe la Oferta Laboral con ID = " + idOfertaLaboral);
                        if (ol.PuestoID == 0 || ol.Puesto == null) throw new Exception("La Oferta Laboral no tiene asignado un puesto");

                        var competenciaAux = context.TablaCompetenciaXPuesto.Where(x => x.PuestoID == ol.PuestoID);
                        if (competenciaAux.Count == 0) throw new Exception("No existen competencias asignadas al puesto : " + ol.Puesto.Nombre);

                        List<CompetenciaDTO> competencias = competenciaAux.Select(x => x.Competencia).Select(x => x.ToDTO()).ToList();
                        return JsonSuccessGet(new { competencias = competencias });
                    }
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
                }
            }

        }

        // /WSOfertaLaboral/getOfertasLaboralesXEstado?estadoOfertaLaboral=
        public JsonResult getOfertasLaboralesXEstado(string estadoOfertaLaboral)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    EstadosSolicitudOfertaLaboral e = context.TablaEstadosSolicitudes.One(x => x.Descripcion.Equals(estadoOfertaLaboral));
                    if (e == null) return JsonErrorGet("No existe el estado " + estadoOfertaLaboral + " para una Solicitud de Oferta Laboral");

                    List<OfertaLaboral> ofertas = context.TablaOfertaLaborales.Where(x => x.EstadoSolicitudOfertaLaboralID == e.ID);
                    if (ofertas == null || ofertas.Count == 0) return JsonErrorGet("No existen Ofertas Laborales con el estado " + estadoOfertaLaboral);
                    
                    List<OfertaLaboralDTO> lstOfertas = ofertas.Select(x => x.ToDTO()).ToList();
                    return JsonSuccessGet(new { ofertasLaborales = lstOfertas });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }

        // /WSOfertaLaboral/setEstadoSolicitudOfertaLaboral?ofertaLaboralID=&nuevoEstado=
        // /WSOfertaLaboral/setEstadoSolicitudOfertaLaboral?ofertaLaboralID=&nuevoEstado=&comentarios=
        // /WSOfertaLaboral/setEstadoSolicitudOfertaLaboral?ofertaLaboralID=&nuevoEstado=&comentarios=&colaboradorID=
        public JsonResult setEstadoSolicitudOfertaLaboral(string ofertaLaboralID, string nuevoEstado, string comentarios = "", string colaboradorID = "")
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    //Colaborador c;
                    //if (colaboradorID != "")
                    //{
                    //    c = context.TablaColaboradores.FindByID(Convert.ToInt32(colaboradorID));
                    //    if (c == null) return JsonErrorGet("No Existe el Colaborador con ID = " + colaboradorID);
                    //}

                    OfertaLaboral ol = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(ofertaLaboralID));
                    if (ol == null) return JsonErrorGet("No existe la Oferta Laboral con ID = " + ofertaLaboralID);

                    EstadosSolicitudOfertaLaboral esol = context.TablaEstadosSolicitudes.One(x => x.Descripcion.Equals(nuevoEstado));
                    if (esol == null) return JsonErrorGet("No existe el estado " + nuevoEstado + " para una Solicitud de Oferta Laboral");

                    //if (ol.ResponsableID == Convert.ToInt32(colaboradorID)) ;

                    ol.EstadoSolicitudOfertaLaboralID = esol.ID;
                    if (comentarios != "") ol.Comentarios = comentarios;
                    ol.FechaPublicacion = DateTime.Now.ToString("dd/MM/yyyy");

                    context.TablaOfertaLaborales.ModifyElement(ol);

                    return JsonSuccessGet(new { ofertalaboral = ol.ToDTO()});
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException);
                }
            }

        }

        // /WSOfertaLaboral/registrarPostulacion?colaboradorID=&ofertaLaboralID=
        public JsonResult registrarPostulacion(string colaboradorID, string ofertaLaboralID)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    Colaborador c = context.TablaColaboradores.FindByID(Convert.ToInt32(colaboradorID));
                    if (c == null) return JsonErrorGet("No existe Colaborador con ID = " + colaboradorID);

                    OfertaLaboral ol = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(ofertaLaboralID));
                    if (ol == null) return JsonErrorGet("No existe Oferta Laboral con ID = " + ofertaLaboralID);
                    
                    //Crear el postulante a partir de ese colaborador
                    Postulante p = context.TablaPostulante.One(x => x.ColaboradorID == Convert.ToInt32(colaboradorID));
                    // Si no encuentro al colaborador como postulante, creo el postulante
                    if (p == null) context.TablaPostulante.AddElement(p = new Postulante(c));

                    OfertaLaboralXPostulante ofxp = new OfertaLaboralXPostulante();
                    ofxp.OfertaLaboralID = ol.ID;
                    ofxp.PostulanteID = p.ID;
                    ofxp.EstadoPostulantePorOfertaID = context.TablaEstadoPostulanteXOferta.One(x => x.Descripcion.Equals("Inscrito")).ID;
                    ofxp.FechaPostulacion = DateTime.Now.ToString("dd/MM/yyyy");
                    context.TablaOfertaLaboralXPostulante.AddElement(ofxp);

                    FasePostulacionXOfertaLaboralXPostulante fpxolxp = new FasePostulacionXOfertaLaboralXPostulante();
                    fpxolxp.FasePostulacionID = context.TablaFasePostulacion.One(x => x.Descripcion.Equals("Registrado")).ID;
                    fpxolxp.OfertaLaboralXPostulanteID = ofxp.ID;
                    context.TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(fpxolxp);

                    return JsonSuccessGet(new { postulacion = ofxp.ToDTO(), fasePostulacionID = fpxolxp.ID });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message + ex.InnerException+ ex.StackTrace);
                }
            }

        }

        // /WSOfertaLaboral/getOfertasLaboralesColaborador?colaboradorID=&estadoOfertaLaboral=
        public JsonResult getOfertasLaboralesColaborador(string colaboradorID, string estadoOfertaLaboral)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    EstadosSolicitudOfertaLaboral esol = context.TablaEstadosSolicitudes.One(x => x.Descripcion.Equals(estadoOfertaLaboral));
                    if (esol == null) return JsonErrorGet("No existe el estado " + estadoOfertaLaboral + " para una Solicitud de Oferta Laboral");

                    Colaborador c = context.TablaColaboradores.FindByID(Convert.ToInt32(colaboradorID));
                    if (c == null) return JsonErrorGet("No existe el Colaborador con ID = " + colaboradorID);

                    Postulante p = context.TablaPostulante.One(x => x.ColaboradorID == c.ID);
                    //if (p == null) throw new Exception("El colaborador " + c.ToDTO().NombreCompleto + " no existe como postulante");
                    if (p == null) p = new Postulante { ID = 0 };
                    //if (p.OfertasPostuladas == null || p.OfertasPostuladas.Count == 0) throw new Exception("El colaborador " + c.ToDTO().NombreCompleto + " no ha postulado a nada");

                    //ColaboradorXPuesto actual = context.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == c.ID && !x.FechaSalidaPuesto.HasValue);
                    ColaboradorXPuesto actual = c.ColaboradoresPuesto.Single(x => !x.FechaSalidaPuesto.HasValue);
                    if (actual == null) return JsonErrorGet("El colaborador " + c.ToDTO().NombreCompleto + " no tiene un puesto actual determinado");

                    int areaID = actual.Puesto.AreaID;
                    //List<OfertaLaboral> lstOL = context.TablaOfertaLaborales.All();
                    List<OfertaLaboral> lstOL = context.TablaOfertaLaborales.Where(
                            x =>  x.AreaID == areaID && //De la misma area que el postulante (colaborador) //SI QUIERES FILTRAR POR AREA, ACTIVAS AQUI NO MAS
                            x.EstadoSolicitudOfertaLaboralID == esol.ID && //Que coincida con el estado de la oferta laboral que deseo
                            !x.Postulantes.Select(y => y.PostulanteID).ToList().Contains(p.ID) //No sea una oferta ya postulada
                    );
                    if (lstOL == null || lstOL.Count == 0)
                        return JsonErrorGet("No se encontraron ofertas laborales que cumplan los requisitos (Misma area que el postulante/Coincida con el estado/No hayan sido ya postuladas)");

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
