using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class WSListarProcesosXEvaluadorController : Controller
    {
        public ActionResult Read(int idUsuario)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ProcesoEvaluacion> listaProceso = new ListarProcesosXEvaluadorController()._Read(idUsuario, context);
                return Json(listaProceso.Select(x => x.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        /*
         * Esto devuelve tambien el colaborador en cada proceso, ahi estan sus datos.
         */
        public ActionResult ReadEvaluados(int idUsuario)
        {
            using (DP2Context context = new DP2Context())
            {
                var controller = new ListarProcesosXEvaluadorController();
                List<ProcesoEvaluacion> procesos = controller._Read(idUsuario, context);
                List<Evaluador> evaluadores = new List<Evaluador>();
                procesos.ForEach(x => evaluadores.AddRange(controller._ReadEvaluados(idUsuario, x.ID, context)));
                return Json(evaluadores.Select(c => c.ToDTOEvaluacion()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

    }
}
