using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    public class OrganizacionesController : Controller
    {
        //
        // GET: /Organizacion/Organizaciones/

         public OrganizacionesController()
        {
            ViewBag.Area = "Organizacion";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.Organizacion = context.TablaOrganizaciones.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoDocumentos = context.TablaTiposDocumentos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.pais = context.TablaPaises.All().Select(p => p.ToDTO()).ToList();
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();

                return View(context.TablaOrganizaciones.FindByID(1).ToDTO());
            }

        }


        [HttpPost]
        public ActionResult  UpdateOrganizacion(OrganizacionDTO org) {
            using (DP2Context context = new DP2Context())
            {
                var o = context.TablaOrganizaciones.FindByID(1);
                o.LoadFromDTO(org);
                context.TablaOrganizaciones.ModifyElement(o);
                return RedirectToAction("Index");
            }
        }

        public ActionResult CargaDatosRepresentante(int colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                var colab = context.TablaColaboradores.FindByID(colaboradorID);
                return PartialView("DatosResponsableResultados", new ColaboradorDocumentosDTO(colab));
            }
            
        }

        public ActionResult CargaModal(int orgID)
        {
            using (DP2Context context = new DP2Context())
            {
                var colab = context.TablaOrganizaciones.FindByID(orgID);
                return PartialView("EditorOrganizacion");
            }
        }

    }
}
