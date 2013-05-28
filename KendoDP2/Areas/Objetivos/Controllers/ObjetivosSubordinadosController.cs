﻿using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Areas.Objetivos.Models;

namespace KendoDP2.Areas.Objetivos.Controllers
{
    public class ObjetivosSubordinadosController : Controller
    {
        public ObjetivosSubordinadosController()
        {
            ViewBag.Area = "Objetivos";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                int colaboradorID = DP2MembershipProvider.GetPersonaID(this);
                int puestoID = context.TablaColaboradores.FindByID(colaboradorID).ToDTO().PuestoID;
                Puesto puesto = context.TablaPuestos.FindByID(puestoID);
                ViewBag.periodos = context.TablaPeriodos.All().Select(c => c.ToDTO()).ToList();
                ViewBag.objetivos = puesto.Objetivos.Select(c => c.ToDTO(context)).ToList();
                return View();
            }
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, int objetivoPadreID)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaObjetivos.Where(o => o.ObjetivoPadreID == objetivoPadreID && o.IsObjetivoIntermedio).Select(o => o.ToDTO(context)).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = new Objetivo(objetivo, context);
                o.IsObjetivoIntermedio = true;
                context.TablaObjetivos.AddElement(o);
                ReparteObjetivosASubordinados(context, context.TablaPuestos.FindByID(o.ObjetivoPadre.PuestoAsignadoID.GetValueOrDefault()));
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
                foreach (var o2 in o.ObjetivosHijos)
                {
                    o2.Nombre = o.Nombre;
                    context.TablaObjetivos.ModifyElement(o2);
                }
                return Json(new[] { o.ToDTO(context) }.ToDataSourceResult(request, ModelState));
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

        private void ReparteObjetivosASubordinados(DP2Context context, Puesto puesto)
        {
            foreach (var objetivoPadre in puesto.Objetivos)
                foreach (var objetivoIntermedio in objetivoPadre.ObjetivosHijos.Where(c => c.IsObjetivoIntermedio))
                    foreach (var puestoHijo in puesto.Puestos)
                        if(!puestoHijo.Objetivos.Any(x => x.ObjetivoPadreID == objetivoIntermedio.ID))
                        {
                            Objetivo nuevo = new Objetivo{Nombre = objetivoIntermedio.Nombre, ObjetivoPadre = objetivoIntermedio, PuestoAsignado = puestoHijo};
                            context.TablaObjetivos.AddElement(nuevo);
                        }
        }
    }
}