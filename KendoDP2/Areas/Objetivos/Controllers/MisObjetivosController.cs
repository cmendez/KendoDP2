using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Objetivos.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Objetivos.Controllers
{
    public class MisObjetivosController : Controller
    {
        public MisObjetivosController()
        {
            ViewBag.Area = "Objetivos";
        }

        public ActionResult Index(int? colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                int colaboradorLogueadoID = DP2MembershipProvider.GetPersonaID(this);
                colaboradorID = colaboradorID ?? colaboradorLogueadoID;
                ViewBag.puedeCrear = ViewBag.puedeEditar = colaboradorID == colaboradorLogueadoID; 
                ViewBag.periodos = context.TablaPeriodos.All().Select(c => c.ToDTO()).ToList();
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                int puestoID = context.TablaColaboradores.FindByID(colaboradorID.GetValueOrDefault()).ToDTO().PuestoID;
                if (puestoID > 0)
                {
                    Puesto puesto = context.TablaPuestos.FindByID(puestoID);
                    ViewBag.objetivos = puesto.Objetivos.Select(c => c.ToDTO(context)).ToList();
                }
                else
                {
                    ViewBag.objetivos = new List<ObjetivoDTO>();
                }
                ViewBag.colaboradorID = colaboradorID;
                return View();
            }
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, int objetivoPadreID)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaObjetivos.Where(o => o.ObjetivoPadreID == objetivoPadreID && o.PuestoAsignadoID == null).Select(o => o.ToDTO(context)).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo, int elUsuarioQueInicioSesion)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = new Objetivo(objetivo, context);
                o.Dueño = context.TablaColaboradores.FindByID(elUsuarioQueInicioSesion);
                context.TablaObjetivos.AddElement(o);
                return Json(new[] { o.ToDTO(context) }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = context.TablaObjetivos.FindByID(objetivo.ID).LoadFromDTO(objetivo, context);
                context.TablaObjetivos.ModifyElement(o);
                return Json(new[] { o.ToDTO(context) }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    context.TablaObjetivos.RemoveElementByID(objetivo.ID);
                    return Json(ModelState.ToDataSourceResult());
                }
                catch (Exception)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

    }
}
