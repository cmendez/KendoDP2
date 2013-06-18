using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ExtensionMethods;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Reclutamiento.Models;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    public class WSEvaluaController : WSController
    {
        // /WSEvaluacion/setRespuestasXEvaluacion
        [HttpPost]
        public JsonResult setRespuestasXEvaluacion(string idOfertaLaboral, string idPostulante,
                                                    string descripcionFase, List<RespuestaDTO> respuestas,
                                                    EvaluacionXFaseXPostulacionDTO evaluacion)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    // **************************** VALIDACIONES ****************************
                    Postulante p = context.TablaPostulante.FindByID(Convert.ToInt32(idPostulante));
                    if (p == null) return JsonErrorPost("No existe el Postulante con ID = " + idPostulante);

                    OfertaLaboral ol = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(idOfertaLaboral));
                    if (ol == null) return JsonErrorPost("No existe la Oferta Laboral con ID = " + idOfertaLaboral);

                    FasePostulacion fp = context.TablaFasePostulacion.One(x => x.Descripcion.Equals(descripcionFase));
                    if (fp == null) return JsonErrorPost("No existe la Fase de Postulacion cuya descripcion sea = " + descripcionFase);

                    //Buscar OfertaLaboralXPostulante
                    OfertaLaboralXPostulante olxp = context.TablaOfertaLaboralXPostulante
                        .One(x => x.OfertaLaboralID == ol.ID && x.PostulanteID == p.ID);
                    if (olxp == null) return JsonErrorPost("El postulante " + p.ToDTO().NombreCompleto + " no ha postulado a la Oferta Laboral : " + ol.Descripcion);

                    //Buscar FasePostulacionXOfertaLaboralXPostulante 
                    FasePostulacionXOfertaLaboralXPostulante fpxolxp = context.TablaFasePostulacionXOfertaLaboralXPostulante
                        .One(x => x.OfertaLaboralXPostulanteID == olxp.ID && x.FasePostulacionID == fp.ID);
                    if (fpxolxp == null)
                    {
                        if (olxp.EstadoPostulantePorOferta != null)
                        {
                            if (olxp.EstadoPostulantePorOferta.Descripcion == "Aprobado Fase 1" ||
                                olxp.EstadoPostulantePorOferta.Descripcion == "Aprobado Fase 3")
                            {
                                context.TablaFasePostulacionXOfertaLaboralXPostulante
                                .AddElement(
                                fpxolxp = new FasePostulacionXOfertaLaboralXPostulante
                                {
                                    FasePostulacionID = fp.ID,
                                    OfertaLaboralXPostulanteID = olxp.ID
                                });
                            }
                        }
                        else
                        {
                            return JsonErrorPost("La Oferta Laboral " + ol.Descripcion + " no ha llegado a la Fase de Postulacion : " + fp.Descripcion);
                        }
                    }

                    //Buscar EvaluacionXFaseXPostulacion
                    EvaluacionXFaseXPostulacion e = context.TablaEvaluacionXFaseXPostulacion.One(x => x.FasePostulacionXOfertaLaboralXPostulanteID == fpxolxp.ID);
                    if (e != null) return JsonErrorPost("La postulacion del postulante " + p.ToDTO().NombreCompleto +
                        " a la oferta laboral " + ol.Puesto.Nombre + " que " + ol.Descripcion + " que ha alcanzado la fase " +
                        descripcionFase + " ya tiene una evaluacion registrada");

                    if (respuestas == null || respuestas.Count == 0) return JsonErrorPost("No se puede ingresar una evaluacion sin respuestas");
                    // ******************************************************************

                    //Crear y cargar EvaluacionXFaseXPostulacion 
                    e = new EvaluacionXFaseXPostulacion();
                    e.FechaInicio = DateTime.ParseExact(evaluacion.FechaInicio, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                    e.FechaFin = DateTime.ParseExact(evaluacion.FechaFin, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                    e.Comentarios = evaluacion.Comentarios;
                    e.Observaciones = evaluacion.Observaciones;

                    //Asignar la evaluacion a la FasePostulacionXOfertaLaboralXPostulante 
                    e.FasePostulacionXOfertaLaboralXPostulanteID = fpxolxp.ID;
                    e.FasePostulacionXOfertaLaboralXPostulante = fpxolxp;

                    //Calcular el puntaje y asignarlo
                    double puntajeTotal = 0;
                    foreach (var obj in respuestas)
                    {
                        puntajeTotal += obj.Puntaje;
                    }
                    e.Puntaje = puntajeTotal;

                    // ********************************** COMO SE QUE APROBO, NO SE COMO ASIGNARLO AQUI Y NO SE SI ES EL MOMENTO ADECUADO **********************************
                    e.FlagAprobado = true; // ESTO DEBE CALCULARSE

                    //Guardar la evaluacion por fase por postulacion, es necesario reasignar el ID o ya se guarda
                    context.TablaEvaluacionXFaseXPostulacion.AddElement(e);

                    //El puntaje obtenido lo debo acumular el puntaje Total de la OfertaXPostulante
                    var ofertaLaboralXPostulante = e.FasePostulacionXOfertaLaboralXPostulante.OfertaLaboralXPostulante;
                    ofertaLaboralXPostulante.PuntajeTotal += (int)e.Puntaje;
                    context.TablaOfertaLaboralXPostulante.ModifyElement(ofertaLaboralXPostulante);

                    // *************** UPDATE FasePostulacionXOfertaLaboralXPostulante con la EvaluacionXFaseXPostulacion ***************
                    fpxolxp.EvaluacionXFaseXPostulacionID = e.ID;
                    context.TablaFasePostulacionXOfertaLaboralXPostulante.ModifyElement(fpxolxp);

                    //Guardar las respuesta, indicando la evaluacion a la que pertenecen
                    List<Respuesta> lstRespuesta = new List<Respuesta>();
                    foreach (var obj in respuestas)
                    {
                        Respuesta rAux = new Respuesta().LoadFromDTO(obj);
                        rAux.EvaluacionXFaseXPostulacionID = e.ID;
                        context.TablaRespuesta.AddElement(rAux);
                        lstRespuesta.Add(rAux);
                    }

                    return JsonSuccessPost(new { evaluacion = e.ToDTO(), respuestas = lstRespuesta.Select(x => x.ToDTO()).ToList() });
                }
                catch (Exception ex)
                {
                    return JsonErrorPost("Error en la BD: " + ex.Message + ex.InnerException);
                }
            }
        }


    }
}
