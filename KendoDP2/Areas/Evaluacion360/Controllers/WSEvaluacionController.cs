using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Models.Generic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class WSEvaluacionController : Controller
    {
        public ActionResult ReadPreguntas(int idEvaluador, int idProcesoEvaluacion, int idColaboradorEvaluado)
        {
            using (DP2Context context = new DP2Context())
            {
                Evaluador e = context.TablaEvaluadores.One(x => x.ProcesoEnElQueParticipanID == idProcesoEvaluacion && x.ElEvaluado == idColaboradorEvaluado && x.ElIDDelEvaluador== idEvaluador);
                int tablaEvaluadoresID = e.ID;
                int puestoID = context.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == idColaboradorEvaluado && !x.IsEliminado && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto)).PuestoID;
                return Json(new ProcesoEvaluacionController()._Editing_ReadCapEvaluacion(puestoID, tablaEvaluadoresID, context), JsonRequestBehavior.AllowGet);
            }
        }

        
        public ActionResult ResponderPreguntas(string respuestas, int tablaEvaluadorID)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    List<Respuesta> resps = JsonConvert.DeserializeObject<List<Respuesta>>(respuestas);
                    var contr = new ProcesoEvaluacionController();
                    foreach (var resp in resps)
                        contr._GuardarPuntuacionPregunta(resp.PreguntaID, resp.Puntaje, context);
                    var cntrEvaluacion = new EvaluacionController();
                    cntrEvaluacion._GuardarEvaluacion_ws(tablaEvaluadorID, context);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        
    }
    public class Respuesta
    {
        public int PreguntaID { get; set; }
        public int Puntaje { get; set; }
        public Respuesta() { }
    }
}
