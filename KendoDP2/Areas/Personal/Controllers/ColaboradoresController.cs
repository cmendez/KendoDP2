using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Personal.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Personal.Controllers
{
    public class ColaboradoresController : Controller
    {
        //
        // GET: /Personal/Colaborador/

        public ColaboradoresController()
        {
            ViewBag.Area = "Personal";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoDocumentos = context.TablaTiposDocumentos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.estadosColaborador = context.TablaEstadosColaboradores.All().Select(p => p.ToDTO()).ToList();
                ViewBag.pais = context.TablaPaises.All().Select(p => p.ToDTO()).ToList();
                ViewBag.gradoAcademico = context.TablaGradosAcademicos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(p => p.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                return View();
            }

        }

        // Grid periodos
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ColaboradorDTO> colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).OrderBy(x => x.ID).ToList();
                return Json(colaboradores.ToDataSourceResult(request));
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ColaboradorDTO colaborador)
        {
            using (DP2Context context = new DP2Context())
            {
                Colaborador c = new Colaborador(colaborador);
                c.EstadoColaborador = context.TablaEstadosColaboradores.One(x => x.Descripcion.Equals("Contratado"));
                context.TablaColaboradores.AddElement(c);
                
               /* Puesto p = context.TablaPuestos.FindByID(colaborador.PuestoID);
                ColaboradorXPuesto cruce = new ColaboradorXPuesto { ColaboradorID = c.ID, PuestoID = p.ID, Sueldo = colaborador.Sueldo };
                c.ColaboradoresPuesto.Add(cruce);
                p.ColaboradorPuestos.Add(cruce);
                context.TablaColaboradoresXPuestos.AddElement(cruce);
                */
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState)); 
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, ColaboradorDTO colaborador)
        {
            using (DP2Context context = new DP2Context())
            {
                Colaborador c = context.TablaColaboradores.FindByID(colaborador.ID).LoadFromDTO(colaborador);
                context.TablaColaboradores.ModifyElement(c);
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, ColaboradorDTO colaborador)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaColaboradores.RemoveElementByID(colaborador.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }

    }
}
