using Kendo.Mvc.UI;
using KendoDP2.Areas.Reclutamiento.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    [Authorize()]
    public class SolicitudOfertasLaboralesController : Controller
    {
        //
        // GET: /Reclutamiento/SolicitudOfertasLaborales/

        public SolicitudOfertasLaboralesController()
        {
            ViewBag.Area = "Reclutamiento";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.ofertasLaborales = context.TablaOfertaLaborales.All().Select(p => p.ToDTO()).ToList();
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                ViewBag.modosSolicitudOferta = context.TablaModosSolicitudes.All().Select(p => p.ToDTO()).ToList();
                ViewBag.estadosSolicitudOferta = context.TablaEstadosSolicitudes.All().Select(p => p.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(p => p.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                return View();
            }

        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<OfertaLaboralDTO> ofertas = context.TablaOfertaLaborales.All().Select(p => p.ToDTO()).OrderBy(x => x.ID).ToList();
                return Json(ofertas.ToDataSourceResult(request));
            
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, OfertaLaboralDTO oferta)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = new OfertaLaboral(oferta);
                context.TablaOfertaLaborales.AddElement(o);
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, OfertaLaboralDTO oferta)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaOfertaLaborales.RemoveElementByID(oferta.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, OfertaLaboralDTO oferta)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(oferta.ID).LoadFromDTO(oferta);
                context.TablaOfertaLaborales.ModifyElement(o);
                

                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }


        public ActionResult GetViewOferta(int id)
        {
            
            using (DP2Context context = new DP2Context())
            {
                ViewBag.estadosSolicitudOferta = context.TablaEstadosSolicitudes.All().Select(p => p.ToDTO()).ToList();
                var oferta = context.TablaOfertaLaborales.FindByID(id);
               return PartialView("ViewSolicitudOfertaLaboral", oferta);
            }
        }


    }



}
