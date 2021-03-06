﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Reclutamiento.Models;
using KendoDP2.Models.Seguridad;
using System.Globalization;

namespace KendoDP2.Controllers
{
    public class PublicController : Controller
    {
        //
        // GET: /Public/

        public PublicController()
        {
            ViewBag.Area = "Public";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.estadosSolicitudOferta = context.TablaEstadosSolicitudes.All().Select(e => e.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(a => a.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                return View();
            }
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<OfertaLaboralDTO> ofertasPosibles = context.TablaOfertaLaborales.All().Where(p => (p.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Aprobado")) && (p.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Pública"))).Select(p => p.ToDTO()).ToList();
                DateTime now = DateTime.Now;
                List<OfertaLaboralDTO> ofertasEnFecha = ofertasPosibles.Where(x => DateTime.ParseExact(x.FechaFinRequerimiento, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(now) >= 1).ToList();
                return Json(ofertasEnFecha.ToDataSourceResult(request));

            }
        }

        public ActionResult GetViewOferta(int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                ViewBag.responsable = oferta.Responsable.ToDTO();
                ViewBag.modoSolicitudOferta = oferta.ModoSolicitudOfertaLaboralID >= 1 ? oferta.ModoSolicitudOfertaLaboral.ToDTO() : new ModoSolicitudOfertaLaboralDTO();
                ViewBag.estadoSolicitudOferta = oferta.EstadoSolicitudOfertaLaboral.ToDTO();
                ViewBag.area = oferta.Area.ToDTO();
                ViewBag.puesto = oferta.Puesto.ToDTO();
                ViewBag.funciones = oferta.Puesto.Funciones.Select(c => c.ToDTO()).ToList();
                ViewBag.capacidades = oferta.Puesto.GetCapacidadesAsociadas(context).Select(c => c.ToDTO()).ToList();
                return PartialView("VerOferta", oferta.ToDTO());
            }
        }

        public ActionResult GetViewPostulante(int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                ViewBag.ofertaID = ofertaID;
                ViewBag.tipodocumentos = context.TablaTiposDocumentos.All().Select(a => a.ToDTO()).ToList();
                ViewBag.gradosacademicos = context.TablaGradosAcademicos.All().Select(p => p.ToDTO()).ToList();

                PostulanteDTO postulante = new PostulanteDTO();
                return PartialView("AgregarPostulante", postulante);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Postular(int ofertaID, PostulanteDTO postulanteDTO = null)
        {
            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                {
                    Postulante postulante = new Postulante(postulanteDTO);
                    context.TablaPostulante.AddElement(postulante);
                    EstadoPostulantePorOferta estado = context.TablaEstadoPostulanteXOferta.FindByID(1);
                    OfertaLaboralXPostulante cruce = new OfertaLaboralXPostulante { Postulante = postulante, OfertaLaboral = oferta, EstadoPostulantePorOferta = estado };
                    context.TablaOfertaLaboralXPostulante.AddElement(cruce);

                    return Json(new { success = true });
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ValidarDoc(int ofertaID, int tipo = 0, string num = ""){
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                if (oferta.Postulantes.Any(p => p.Postulante.NumeroDocumento == num && p.Postulante.TipoDocumentoID == tipo))
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true });
            }
        }
    }
}
