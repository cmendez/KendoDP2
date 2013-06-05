using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Seguridad;


namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class ListarProcesosXEvaluadorController : Controller
    {
        //
        // GET: /Evaluacion360/ListarProcesosXEvaluador/

                public ListarProcesosXEvaluadorController()
            : base()
        {
            ViewBag.Area = "Evaluacion360";
        }



        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();               
                ViewBag.estados = context.TablaEstadoProcesoEvaluacion.All().Select(e => e.ToDTO()).ToList();
                return View();

            }
        }


        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
                //var ret = context.TablaColaboradores.FindByID(ColaboradorID).ColaboradorXProcesoEvaluaciones.Select(x => x.ProcesoEvaluacion.ToDTO()).ToList();

                // context.TablaColaboradores.FindByID(ColaboradorID).Evaluadores.where(x => x.ProcesoEvaluacionXColaborador.ProcesoEvaluacionID == procesoID).toList();
                List<ProcesoEvaluacion> listaProceso = new List<ProcesoEvaluacion>();

                IList<Evaluador> listaProcesosEvaluador = (context.TablaEvaluadores.Where(a => a.ElIDDelEvaluador == ColaboradorID));

                for (int i = 0; i < listaProcesosEvaluador.Count; i++)
                {
                    listaProceso.Add(context.TablaProcesoEvaluaciones.FindByID(listaProcesosEvaluador.ElementAt(i).ProcesoEnElQueParticipanID));
                }

                return Json(listaProceso.Select(x=>x.ToDTO()).ToDataSourceResult(request));

            }
        }



        public ActionResult ListarEvaluados(int procesoEvaluacionID)
        {
            using (DP2Context context = new DP2Context())
            {
                ProcesoEvaluacion proceso = context.TablaProcesoEvaluaciones.FindByID(procesoEvaluacionID);
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();                
                ViewBag.estados = context.TablaEstadoColaboradorXProcesoEvaluaciones.All().Select(c => c.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
                return View(proceso);

            }
        }


        public ActionResult ReadEvaluados([DataSourceRequest] DataSourceRequest request, int procesoID)
        {
            using (DP2Context context = new DP2Context())
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
                //return Json(context.TablaColaboradores.FindByID(ColaboradorID).OcurrenciasComoEvaluador.Where(x => x.Evaluado.ProcesoEvaluacionID == procesoID).ToList());

                IList<Evaluador> listaEvaluaciones = new List<Evaluador>();

                listaEvaluaciones = (context.TablaEvaluadores.Where(y => y.ElIDDelEvaluador == ColaboradorID &&
                                                                                   y.ProcesoEnElQueParticipanID == procesoID));
                //IList<EvaluadorDTO> listaEvaluaciones2 = listaEvaluaciones.Select(x => x.ToDTO());

                //Json(listaEvaluaciones.Select(x=>x.ToDTO()).ToDataSourceResult(request));

                return Json(listaEvaluaciones.Select(x => x.ToDTOEvaluacion()).ToDataSourceResult(request));

            }
        }

    }
}
