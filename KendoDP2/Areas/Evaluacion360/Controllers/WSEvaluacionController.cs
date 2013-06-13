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

        
    }
}
