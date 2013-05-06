using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    [Authorize()]
    public class PuestosController : Controller
    {
        public PuestosController()
        {
            ViewBag.Area = "Organizacion";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(p => p.ToDTO()).ToList();
                ViewBag.estadosPuesto = context.TablaEstadosPuestos.All().Select(p => p.ToDTO()).ToList();
                return View();
            }

        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<PuestoDTO> puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                return Json(puestos.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, PuestoDTO puesto)
        {
            using (DP2Context context = new DP2Context())
            {
                Puesto p = new Puesto(puesto);
                p.EstadoPuesto = context.TablaEstadosPuestos.One(x => x.Descripcion.Equals("Vacante"));
                context.TablaPuestos.AddElement(p);

                Area a = context.TablaAreas.FindByID(puesto.AreaID);
                PuestoXArea cruce = new PuestoXArea { Area = a, Puesto = p };
                
                //p.PuestosArea.Add(cruce);
              
                context.TablaPuestosXAreas.AddElement(cruce);

                return Json(new[] { p.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, PuestoDTO puesto)
        {
            using (DP2Context context = new DP2Context())
            {
                Puesto p = context.TablaPuestos.FindByID(puesto.ID).LoadFromDTO(puesto);
                context.TablaPuestos.ModifyElement(p);
                return Json(new[] { p.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, PuestoDTO puesto)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaPuestos.RemoveElementByID(puesto.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }
 
      

    }
}
