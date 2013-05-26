using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoDP2.Areas.Objetivos.Models;


namespace KendoDP2.Areas.Objetivos.Controllers
{
    
    public class ObjetivosEmpresaController : Controller
    {
        public ObjetivosEmpresaController()
        {
            ViewBag.Area = "Objetivos";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context()) {
                ViewBag.periodos = context.TablaPeriodos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoObjetivosBSC = context.TablaTipoObjetivoBSC.All();
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                return View();
            }
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, int tipoObjetivoBSCID, int BSCID)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaObjetivos.Where(o => o.TipoObjetivoBSCID == tipoObjetivoBSCID && o.BSCID == BSCID).Select(o => o.ToDTO()).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = new Objetivo(objetivo);
                context.TablaObjetivos.AddElement(o);
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = context.TablaObjetivos.FindByID(objetivo.ID).LoadFromDTO(objetivo);
                context.TablaObjetivos.ModifyElement(o);
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaObjetivos.RemoveElementByID(objetivo.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }


        public ActionResult ListarObjetivosPrueba(int PadreId)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ObjetivoDTO> ListaObjetivos2 = new List<ObjetivoDTO>();
                ListaObjetivos2 = context.TablaObjetivos.All().Select(p => p.ToDTO()).ToList();
                return Json(context.TablaObjetivos.All().Select(p => p.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarPeriodos()
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaPeriodos.All().Select(p => p.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
