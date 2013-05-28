using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Evaluacion360.Models;

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
                ViewBag.PageSize = 8;

                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(p => p.ToDTO()).ToList();
                ViewBag.estadosPuesto = context.TablaEstadosPuestos.All().Select(p => p.ToDTO()).ToList();
                return View();
            }

        }

        public JsonResult PuestosToTree(int? id)
        {
            using (DP2Context context = new DP2Context())
            {
                var areas = context.TablaAreas.Where(a => id.HasValue ? a.AreaSuperiorID == id : a.AreaSuperiorID == null).Select(a => a.ToTreeDTO()).OrderBy(a => a.Name);
                return Json(areas.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                var puestos = context.TablaPuestos.All().Select(a => a.ToDTO()).OrderBy(a => a.Nombre);
                return Json(puestos.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }


   //     public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
     //   {
       //     using (DP2Context context = new DP2Context())
         //   {
           //     List<PuestoDTO> puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
             //   return Json(puestos.ToDataSourceResult(request));
            //}
        //}

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

                //360:
                //Se crea la base para la configuración de su evaluación:
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(p.ID, true, "El mismo", 1, 50));
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(p.ID, true, "Jefe", 1, 50));
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(p.ID, false, "Pares", 0, 0));
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(p.ID, false, "Subordinados", 0, 0));
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(p.ID, false, "Clientes", 0, 0));
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(p.ID, false, "Otros", 0, 0));

                return Json(new[] { p.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }


       

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, AreaDTO area)
        {
            using (DP2Context context = new DP2Context())
            {
                Area a = context.TablaAreas.FindByID(area.ID).LoadFromDTO(area);
                context.TablaAreas.ModifyElement(a);
                return Json(new[] { a.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, AreaDTO area)
        {
            using (DP2Context context = new DP2Context())
            {
                Area a = context.TablaAreas.FindByID(area.ID);
                if (!a.Puestos.Any(i => !i.IsEliminado) && !a.Areas.Any(i => !i.IsEliminado))
                    context.TablaAreas.RemoveElementByID(area.ID);
                else
                    ModelState.AddModelError("Area", "No se puede eliminar un área con áreas o puestos subordinados.");
                return Json(ModelState.IsValid ? new object() : ModelState.ToDataSourceResult());
            }
        }

        //**********************************************************************************
        //**********************************************************************************
        //FUNCIONES


        //**********************************************************************************
        //**********************************************************************************
        


    }
}
