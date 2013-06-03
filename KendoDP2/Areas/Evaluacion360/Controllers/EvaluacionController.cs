using Kendo.Mvc.UI;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    [Authorize()]
    public class EvaluacionController : Controller
    {
        //
        // GET: /Evaluacion360/Evaluacion/
        public EvaluacionController()
            : base()
        {
            ViewBag.Area = "Evaluacion360";
            
        }

        public ActionResult Index()
        {
            // Recibir esta variable como parámetro
            int evaluadoID = 2;
            using (DP2Context context = new DP2Context())
            {
                ColaboradorDTO  evaluado = context.TablaColaboradores.One(c => c.ID == evaluadoID).ToDTO();
           /*     CompetenciaXPuesto competenciaPuesto = context.TablaCompetenciaXPuesto.One(x => x.PuestoID == evaluado.PuestoID);
                IList<Capacidad> capacidades = context.TablaCapacidades.Where(x => x.NivelCapacidadID==competenciaPuesto.NivelID && x.CompetenciaID == competenciaPuesto.CompetenciaID).ToList();
                IList<CompetenciaXPuesto> competencias = context.TablaCompetenciaXPuesto.Where(x => x.PuestoID == evaluado.PuestoID);
              */
                ViewBag.evaluado = evaluado;
                return View();
            }
        }

        public ActionResult GuardarEvaluacion() {
            return View();
        }
    }
}
