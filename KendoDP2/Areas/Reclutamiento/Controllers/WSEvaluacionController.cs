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
                    //Buscar OfertaLaboralXPostulante
                    OfertaLaboralXPostulante olxp = context.TablaOfertaLaboralXPostulante
                        .One(x =>   (x.OfertaLaboralID == Convert.ToInt32(idOfertaLaboral)) &&
                                    (x.PostulanteID == Convert.ToInt32(idPostulante)));
                    //Buscar FasePostulacionXOfertaLaboralXPostulante 
                    FasePostulacion fp = context.TablaFasePostulacion.One(x => x.Descripcion.Equals(descripcionFase));
                    FasePostulacionXOfertaLaboralXPostulante fpxolxp = context.TablaFasePostulacionXOfertaLaboralXPostulante
                        .One(x =>   (x.OfertaLaboralXPostulanteID == olxp.ID) &&
                                    (x.FasePostulacionID == (fp != null ? fp.ID : 4)));
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
                    // COMO SE QUE APROBO, NO SE COMO ASIGNARLO AQUI Y NO SE SI ES EL MOMENTO ADECUADO
                    e.FlagAprobado = true; // ESTO DEBE CALCULARSE
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

                    //return JsonSuccessPost(new { id1 = idOfertaLaboral, id2 = idPostulante, id3 = descripcionFase, obj1 = respuestas,
                    //    obj2 = evaluacion, obj3 = olxp.ToDTO(), obj4 =  fpxolxp != null ? fpxolxp.ID : -1,
                    //    obj5 = fp != null ? fp.ID : -1, obj6 = e.ToDTO(), obj7 = lstRespuesta.Select(x => x.ToDTO()).ToList()
                    //});

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
