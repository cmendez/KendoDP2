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
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult setRespuestasXEvaluacion(string idOfertaLaboral, string idPostulante, 
                                                    string descripcionFase, List<RespuestaDTO> respuestas,
                                                    EvaluacionXFaseXPostulacionDTO evaluacion)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    //Buscar OfertaLaboralXPostulante
                    OfertaLaboralXPostulante olxp = context.TablaOfertaLaboralXPostulante
                        .One(x =>   (x.OfertaLaboralID == Convert.ToInt32(idOfertaLaboral)) && 
                                    (x.PostulanteID == Convert.ToInt32(idPostulante)));
                    //Buscar FasePostulacionXOfertaLaboralXPostulante 
                    FasePostulacionXOfertaLaboralXPostulante fpxolxp = context.TablaFasePostulacionXOfertaLaboralXPostulante
                        .One(x =>   (x.OfertaLaboralXPostulanteID == olxp.ID) &&
                                    (x.FasePostulacionID == context.TablaFasePostulacion
                                                            .One(a => a.Descripcion.Equals(descripcionFase)).ID));
                    //Crear y cargar EvaluacionXFaseXPostulacion 
                    EvaluacionXFaseXPostulacion e = new EvaluacionXFaseXPostulacion().LoadFromDTO(evaluacion);
                    //Asignar la evaluacion a la FasePostulacionXOfertaLaboralXPostulante 
                    e.FasePostulacionXOfertaLaboralXPostulanteID = fpxolxp.ID;
                    //Calcular el puntaje y asignarlo
                    int puntajeTotal = 0;
                    foreach (var obj in respuestas)
                    {
                        puntajeTotal += obj.Puntaje;
                    }
                    e.Puntaje = puntajeTotal;
                    // COMO SE QUE APROBO, NO SE COMO ASIGNARLO AQUI Y NO SE SI ES EL MOMENTO ADECUADO
                    
                    //Guardar la evaluacion por fase por postulacion, es necesario reasignar el ID o ya se guarda
                    context.TablaEvaluacionXFaseXPostulacion.AddElement(e);
                    //Guardar las respuesta, indicando la evaluacion a la que pertenecen
                    List<Respuesta> lstRespuesta = new List<Respuesta>();
                    foreach (var obj in respuestas)
                    {
                        Respuesta rAux = new Respuesta().LoadFromDTO(obj);
                        rAux.EvaluacionXFaseXPostulacionID = e.ID;
                        context.TablaRespuesta.AddElement(rAux);
                        lstRespuesta.Add(rAux);
                    }

                    return JsonSuccessPost(new { evaluacion = e, respuestas = lstRespuesta.Select(x => x.ToDTO()).ToList() });
                }
                catch (Exception ex)
                {
                    return JsonErrorPost("Error en la BD: " + ex.Message);
                }
            }
        }

    }
}
