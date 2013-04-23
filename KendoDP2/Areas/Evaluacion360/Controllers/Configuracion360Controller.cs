using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Evaluacion360.Models;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    [Authorize()]
    public class Configuracion360Controller : Controller
    {
        public Configuracion360Controller()
            : base()
        {
            ViewBag.Area = "Evaluacion360";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaPerfiles.All().Select(p => p.ToDTO()).ToDataSourceResult(request));
            }
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, CompetenciaDTO competencia)
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        Competencia c = new Competencia(competencia);
        //        context.TablaCompetencias.AddElement(c);
        //        return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
        //    }
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, CompetenciaDTO competencia)
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        Competencia c = context.TablaCompetencias.FindByID(competencia.ID).LoadFromDTO(competencia);
        //        context.TablaCompetencias.ModifyElement(c);
        //        return json(new[] { c.todto() }.todatasourceresult(request, modelstate));
        //    }
        //}

        //[acceptverbs(httpverbs.post)]
        //public actionresult editinginline_destroy([datasourcerequest] datasourcerequest request, competenciadto competencia)
        //{
        //    using (dp2context context = new dp2context())
        //    {
        //        context.tablacompetencias.removeelementbyid(competencia.id);
        //        return json(modelstate.todatasourceresult());
        //    }
        //}

    }
}
