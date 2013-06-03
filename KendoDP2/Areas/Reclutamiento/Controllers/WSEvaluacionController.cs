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
                    //OfertaLaboralXPostulante olxp = context.TablaOfertaLaboralXPostulante
                    //    .One(x =>   (x.OfertaLaboralID == Convert.ToInt32(idOfertaLaboral)) && 
                    //                (x.PostulanteID == Convert.ToInt32(idPostulante)));
                    ////Buscar FasePostulacionXOfertaLaboralXPostulante 
                    //FasePostulacionXOfertaLaboralXPostulante fpxolxp = context.TablaFasePostulacionXOfertaLaboralXPostulante
                    //    .One(x =>   (x.OfertaLaboralXPostulanteID == olxp.ID) &&
                    //                (x.FasePostulacionID == context.TablaFasePostulacion
                    //                                        .One(a => a.Descripcion.Equals(descripcionFase)).ID));
                    ////Crear y cargar EvaluacionXFaseXPostulacion 
                    //EvaluacionXFaseXPostulacion e = new EvaluacionXFaseXPostulacion().LoadFromDTO(evaluacion);
                    ////Asignar la evaluacion a la FasePostulacionXOfertaLaboralXPostulante 
                    //e.FasePostulacionXOfertaLaboralXPostulanteID = fpxolxp.ID;
                    //e.FasePostulacionXOfertaLaboralXPostulante = context.TablaFasePostulacionXOfertaLaboralXPostulante.FindByID(fpxolxp.ID);
                    ////Calcular el puntaje y asignarlo
                    //int puntajeTotal = 0;
                    //foreach (var obj in respuestas)
                    //{
                    //    puntajeTotal += obj.Puntaje;
                    //}
                    //e.Puntaje = puntajeTotal;
                    //// COMO SE QUE APROBO, NO SE COMO ASIGNARLO AQUI Y NO SE SI ES EL MOMENTO ADECUADO
                    //e.FlagAprobado = true; // ESTO DEBE CALCULARSE
                    ////Guardar la evaluacion por fase por postulacion, es necesario reasignar el ID o ya se guarda
                    //context.TablaEvaluacionXFaseXPostulacion.AddElement(e);
                    ////Guardar las respuesta, indicando la evaluacion a la que pertenecen
                    //List<Respuesta> lstRespuesta = new List<Respuesta>();
                    //foreach (var obj in respuestas)
                    //{
                    //    Respuesta rAux = new Respuesta().LoadFromDTO(obj);
                    //    rAux.EvaluacionXFaseXPostulacionID = e.ID;
                    //    context.TablaRespuesta.AddElement(rAux);
                    //    lstRespuesta.Add(rAux);
                    //}

                    return JsonSuccessGet(new { id1 = idOfertaLaboral, id2 = idPostulante, obj1 = respuestas, obj2 = evaluacion  });

                    //return JsonSuccessPost(new { evaluacion = e.ToDTO(), respuestas = lstRespuesta.Select(x => x.ToDTO()).ToList() });
                }
                catch (Exception ex)
                {
                    return JsonErrorPost("Error en la BD: " + ex.Message);
                }
            }
        }

        [HttpPost]
        public JsonResult prueba(int id)
        {
            return JsonSuccessPost(id);
        }

        //public JsonResult setEvaluacion(string idOfertaLaboral, string idPostulante, string descripcionFase, 
        //                                string inicio, string fin, string comentarios = null, string observaciones = null)
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        try
        //        {
        //            //Buscar OfertaLaboralXPostulante
        //            OfertaLaboralXPostulante olxp = context.TablaOfertaLaboralXPostulante
        //                .One(x => (x.OfertaLaboralID == Convert.ToInt32(idOfertaLaboral)) &&
        //                            (x.PostulanteID == Convert.ToInt32(idPostulante)));
        //            //Buscar FasePostulacionXOfertaLaboralXPostulante 
        //            FasePostulacionXOfertaLaboralXPostulante fpxolxp = context.TablaFasePostulacionXOfertaLaboralXPostulante
        //                .One(x => (x.OfertaLaboralXPostulanteID == olxp.ID) &&
        //                            (x.FasePostulacionID == context.TablaFasePostulacion
        //                                                    .One(a => a.Descripcion.Equals(descripcionFase)).ID));
        //            //Crear Evaluacion y asignar todas sus propiedades
        //            EvaluacionXFaseXPostulacion e = new EvaluacionXFaseXPostulacion();
        //            e.FechaInicio = DateTime.ParseExact(inicio, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
        //            e.FechaFin = DateTime.ParseExact(fin, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
        //            e.Comentarios = comentarios;
        //            e.Observaciones = observaciones;
        //            e.FasePostulacionXOfertaLaboralXPostulanteID = fpxolxp.ID; //Asignar la evaluacion a la FasePostulacionXOfertaLaboralXPostulante 
        //            //Guardar la evaluacion por fase por postulacion, es necesario reasignar el ID o ya se guarda
        //            context.TablaEvaluacionXFaseXPostulacion.AddElement(e);

        //            return JsonSuccessGet(new { evaluacion = e });
        //        }
        //        catch (Exception ex)
        //        {
        //            return JsonErrorGet("Error en la BD: " + ex.Message);
        //        }

        //    }
        //}

        //public JsonResult setRespuesta(string idEvaluacion, string puntaje, string funcionID, string comentario = null)
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        try
        //        {
        //            Respuesta r = context.TablaRespuesta.One(x =>   x.EvaluacionXFaseXPostulacionID == Convert.ToInt32(idEvaluacion) && 
        //                                                            x.FuncionID == Convert.ToInt32(funcionID));
        //            if (r != null)
        //            {
        //                r.Puntaje = Convert.ToInt32(puntaje);
        //                r.Comentario = comentario;
        //                r.FuncionID = Convert.ToInt32(funcionID);
        //                context.TablaRespuesta.ModifyElement(r);
        //            }
        //            else
        //            {
        //                r = new Respuesta();
        //                r.Puntaje = Convert.ToInt32(puntaje);
        //                r.Comentario = comentario;
        //                r.FuncionID = Convert.ToInt32(funcionID);
        //                r.EvaluacionXFaseXPostulacionID = Convert.ToInt32(idEvaluacion);
        //                context.TablaRespuesta.AddElement(r);
        //            }

        //            return JsonSuccessGet(new { respuesta = r });
        //        }
        //        catch (Exception ex)
        //        {
        //            return JsonErrorGet("Error en la BD: " + ex.Message);
        //        }
        //    }
        //}

        //public JsonResult setPuntajeYFlagAEvaluacion(string idEvaluacion)
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        try
        //        {
        //            EvaluacionXFaseXPostulacion e = context.TablaEvaluacionXFaseXPostulacion.FindByID(Convert.ToInt32(idEvaluacion));
        //            if (e == null) throw new Exception();

        //            List<Respuesta> lstRespuesta = e.RespuestasDeLaEvaluacion.ToList();
        //            int puntaje = 0;
        //            foreach (var respuesta in lstRespuesta)
        //            {
        //                puntaje += respuesta.Puntaje;
        //            }
        //            e.Puntaje = puntaje;
        //            e.FlagAprobado = true; // Esto debe ser analizado a partir de algo NO DEBE QUEDAR ASI

        //            context.TablaEvaluacionXFaseXPostulacion.ModifyElement(e);

        //            return JsonSuccessGet(new { evaluacion = e });
        //        }
        //        catch (Exception ex)
        //        {
        //            return JsonErrorGet("Error en la BD: " + ex.Message);
        //        }
        //    }
        //}

    }
}
