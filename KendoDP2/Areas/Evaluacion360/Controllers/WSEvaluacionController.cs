using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class WSEvaluacionController : Controller
    {
        public ActionResult ReadPreguntas(int idEvaluado, int idProcesoEvaluacion)
        {
            using (DP2Context context = new DP2Context())
            {
                int tablaEvaluadoresID = context.TablaEvaluadores.One(x => x.ElProceso.ID == idProcesoEvaluacion && x.ElEvaluado == idEvaluado).ID;
                int puestoID = context.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == idEvaluado && !x.IsEliminado && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto)).PuestoID;
                return Json(new ProcesoEvaluacionController()._Editing_ReadCapEvaluacion(puestoID, tablaEvaluadoresID, context), JsonRequestBehavior.AllowGet);
            }
        }

        class Respuesta
        {
            public int PreguntaID {get; set;}
            public int Puntaje {get; set;}
            public Respuesta(){}
        }
        public ActionResult ResponderPreguntas(List<Respuesta> respuestas)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    var contr = new ProcesoEvaluacionController();
                    foreach (var resp in respuestas)
                        contr._GuardarPuntuacionPregunta(resp.PreguntaID, resp.Puntaje, context);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        
    }
}
