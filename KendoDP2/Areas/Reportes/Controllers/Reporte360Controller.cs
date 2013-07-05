using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Reportes.Models;

namespace KendoDP2.Areas.Reportes.Controllers
{
    public class Reporte360Controller : Controller
    {
        //
        // GET: /Reportes/Reporte360/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReporteColaborador(string userName)
        {

            using (DP2Context context = new DP2Context())
            {
                Colaborador colaboradorActual = context.TablaColaboradores.One(a => a.Username.Equals(userName));
                int idEstado = context.TablaEstadoColaboradorXProcesoEvaluaciones.One(a => a.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Terminado)).ID;
                //Obtengo los procesos
                var procesos = context.TablaColaboradorXProcesoEvaluaciones.Where(a=>a.ColaboradorID == colaboradorActual.ID 
                    && a.EstadoColaboradorXProcesoEvaluacionID == idEstado).Select(a=>a.toProcesoReportadoDTO()).ToList();
                return Json(procesos, JsonRequestBehavior.AllowGet);
            }
        }
    
    }
}
