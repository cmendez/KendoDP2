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
    public class WSEvaluacionController : WSController
    {
        [HttpPost]
        public JsonResult setRespuestasXEvaluacion(string idOfertaLaboral, string idPostulante, 
                                                    string descripcionFase, List<RespuestaDTO> respuestas,
                                                    EvaluacionXFaseXPostulacionDTO evaluacion)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    Postulante p = context.TablaPostulante.FindByID(Convert.ToInt32(idPostulante));
                    if (p == null) throw new Exception("No existe el Postulante con ID = " + idPostulante);

                    OfertaLaboral ol = context.TablaOfertaLaborales.FindByID(Convert.ToInt32(idOfertaLaboral));
                    if (ol == null) throw new Exception("No existe la Oferta Laboral con ID = " + idOfertaLaboral);

                    FasePostulacion fp = context.TablaFasePostulacion.One(x => x.Descripcion.Equals(descripcionFase));
                    if (fp == null) throw new Exception("No existe la Fase de Postulacion cuya descripcion sea = " + descripcionFase);

                    //Buscar OfertaLaboralXPostulante
                    OfertaLaboralXPostulante olxp = context.TablaOfertaLaboralXPostulante
                        .One(x => x.OfertaLaboralID == ol.ID && x.PostulanteID == p.ID);
                    if (olxp == null) throw new Exception("El postulante " + p.ToDTO().NombreCompleto + " no ha postulado a la Oferta Laboral " + ol.Descripcion);

                    //Buscar FasePostulacionXOfertaLaboralXPostulante 
                    FasePostulacionXOfertaLaboralXPostulante fpxolxp = context.TablaFasePostulacionXOfertaLaboralXPostulante
                        .One(x => x.OfertaLaboralXPostulanteID == olxp.ID && x.FasePostulacionID == fp.ID);
                    if (fpxolxp == null) throw new Exception("La Oferta Laboral " + ol.Descripcion + " no ha llegado a la Fase de Postulacion " + fp.Descripcion);
                    
                    //Crear y cargar EvaluacionXFaseXPostulacion 
                    EvaluacionXFaseXPostulacion e = new EvaluacionXFaseXPostulacion().LoadFromDTO(evaluacion);
                    
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
                    var ofertaLaboralXPostulante = e.FasePostulacionXOfertaLaboralXPostulante.OfertaLaboralXPostulante;
                    ofertaLaboralXPostulante.PuntajeTotal += (int)e.Puntaje;
                    context.TablaOfertaLaboralXPostulante.ModifyElement(ofertaLaboralXPostulante);
                    
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
                    return JsonErrorPost("Error en la BD: " + ex.Message);
                }
            }
        }

    }
}
