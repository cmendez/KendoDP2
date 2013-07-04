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


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, PuestoDTO puesto)
        {
            using (DP2Context context = new DP2Context())
            {
                if (puesto.PuestoSuperiorID == 0)
                {
                    ModelState.AddModelError("Puesto", "No se puede crear un puesto sin puesto superior.");
                    return Json(new[] { puesto }.ToDataSourceResult(request, ModelState));
                }

                Puesto p = new Puesto(puesto);
                p.EstadoPuesto = context.TablaEstadosPuestos.One(x => x.Descripcion.Equals("Vacante"));
                context.TablaPuestos.AddElement(p);
                if (p.PuestoSuperiorID.GetValueOrDefault() > 0)
                {
                    Puesto puestoPapa = context.TablaPuestos.FindByID(p.PuestoSuperiorID.GetValueOrDefault());
                    puestoPapa.ReparteObjetivosASubordinados(context);
                }
                Area a = context.TablaAreas.FindByID(puesto.AreaID);
                //PuestoXArea cruce = new PuestoXArea { Area = a, Puesto = p };
                
                //context.TablaPuestosXAreas.AddElement(cruce);

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
        public ActionResult Copy([DataSourceRequest] DataSourceRequest request, int puestoID)
        {
        //  Obtiene el nuevo nombre del puesto
            int id;
            using (DP2Context context = new DP2Context())
            {
                Puesto puestoBase = context.TablaPuestos.FindByID(puestoID);
                string sPuesto = "";

                if (puestoBase.Nombre.Contains("-"))
                {
                    int num = puestoBase.Nombre.IndexOf("-") + 2;
                    sPuesto = puestoBase.Nombre.Substring(0, num);
                }
                else
                {
                    sPuesto = puestoBase.Nombre + " - ";
                }

                Puesto puestoMax = context.TablaPuestos.Where(p => p.Nombre.StartsWith(sPuesto)).OrderBy(p => p.Nombre).LastOrDefault();


                if (puestoMax == null)
                {
                    sPuesto = sPuesto + "2";
                }
                else
                {
                    int num = puestoMax.Nombre.IndexOf("-") + 2;
                    string sNum = puestoMax.Nombre.Substring(num);
                    int next = int.Parse(sNum) + 1;
                    sPuesto = sPuesto + next;
                }
    
            //  Crea el nuevo puesto
                Puesto puestoCopia = new Puesto(puestoBase, sPuesto);
                id = context.TablaPuestos.AddElement(puestoCopia);
            }
            using(DP2Context context = new DP2Context()){
                Puesto puestoBase = context.TablaPuestos.FindByID(puestoID);
                Puesto puestoCopia = context.TablaPuestos.FindByID(id);
            //  Copia las funciones
                var funciones = context.TablaFunciones.Where(f => f.PuestoID == puestoBase.ID);
                foreach (Funcion funcion in funciones)
                {
                    context.TablaFunciones.AddElement(new Funcion { PuestoID = puestoCopia.ID, Nombre = funcion.Nombre });
                }

            //  Copia las competencias
                var competenciasxpuesto = context.TablaCompetenciaXPuesto.Where(c => c.PuestoID == puestoBase.ID);
                foreach (CompetenciaXPuesto cxp in competenciasxpuesto)
                {
                    context.TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto { PuestoID = puestoCopia.ID, CompetenciaID = cxp.CompetenciaID, NivelID = cxp.NivelID, Peso = cxp.Peso });
                }

            //  Hereda los objetivos del padre
                if (puestoCopia.PuestoSuperiorID.GetValueOrDefault() > 0)
                {
                    Puesto puestoPapa = context.TablaPuestos.FindByID(puestoCopia.PuestoSuperiorID.GetValueOrDefault());
                    puestoPapa.ReparteObjetivosASubordinados(context);
                }

            //  Se crea la base para la configuración de su evaluación:
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoCopia.ID, true, "El mismo", 1, 50));
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoCopia.ID, true, "Jefe", 1, 50));
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoCopia.ID, false, "Pares", 0, 0));
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoCopia.ID, false, "Subordinados", 0, 0));
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoCopia.ID, false, "Clientes", 0, 0));
                context.TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoCopia.ID, false, "Otros", 0, 0));

                return Json(new[] { puestoCopia.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }
       

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, PuestoDTO puesto)
        {
            using (DP2Context context = new DP2Context())
            {
                Puesto p = context.TablaPuestos.FindByID(puesto.ID).LoadFromDTO(puesto);
                if (puesto.PuestoSuperiorID == 0)
                {
                    ModelState.AddModelError("Puesto", "No se puede crear un puesto sin puesto superior.");
                    return Json(new[] { puesto }.ToDataSourceResult(request, ModelState));
                }
                context.TablaPuestos.ModifyElement(p);
                return Json(new[] { p.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, PuestoDTO puesto)
        {
            using (DP2Context context = new DP2Context())
            {
                Puesto p = context.TablaPuestos.FindByID(puesto.ID);

                if (p.ColaboradorPuestos.Any(c => c.FechaIngresoPuesto <= DateTime.Now && (c.FechaSalidaPuesto == null || c.FechaSalidaPuesto > DateTime.Today)))
                {
                    ModelState.AddModelError("Puesto", "No se puede eliminar un puesto con un colaborador activo.");
                    return Json(ModelState.IsValid ? new object() : ModelState.ToDataSourceResult());
                }

                if (p.Puestos.Any(i => !i.IsEliminado)){
                     ModelState.AddModelError("Puesto", "No se puede eliminar un puesto con puestos subordinados.");
                    return Json(ModelState.IsValid ? new object() : ModelState.ToDataSourceResult());
                }

                context.TablaPuestos.RemoveElementByID(puesto.ID);
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
