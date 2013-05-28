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
    public class OfertasLaboralesInternasController : Controller
    {
        //
        // GET: /Reclutamiento/OfertasLaboralesInternas/

        public OfertasLaboralesInternasController()
        {
            ViewBag.Area = "Reclutamiento";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.ofertasLaborales = context.TablaOfertaLaborales.All().Where(p => (p.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Aprobado")) && (p.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Interna"))).Select(p => p.ToDTO()).ToList();
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
                List<OfertaLaboralDTO> ofertasPosibles = context.TablaOfertaLaborales.All().Where(p => (p.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Aprobado")) && (p.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Interna"))).Select(p => p.ToDTO()).ToList();
                return Json(ofertasPosibles.ToDataSourceResult(request));
            
            }
        }

        public ActionResult VerPostulantes(int ofertaLaboralID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaLaboralID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.ToList();
                ViewBag.postulantesOferta = postulantesOferta.Select(p => p.ToDTO());
                ViewBag.ofertaID = ofertaLaboralID;

                return View("AprobarPorFasePostulantesOfertaLaboral");
            }
        }

        
        public ActionResult VerPostulantesFase1(int ofertaLaboralID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaLaboralID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.ToList();
                ViewBag.postulantesOferta = postulantesOferta.Select(p => p.ToDTO());
                ViewBag.ofertaID = ofertaLaboralID;

                return View("PostulantesFase1AprobarFase2");
            }
        }

        public ActionResult VerPostulantesFase2(int ofertaLaboralID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaLaboralID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.ToList();
                ViewBag.postulantesOferta = postulantesOferta.Select(p => p.ToDTO());
                ViewBag.ofertaID = ofertaLaboralID;

                return View("PostulantesFase2AprobarFase3");
            }
        }

        public ActionResult VerPostulantesFase3(int ofertaLaboralID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaLaboralID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.ToList();
                ViewBag.postulantesOferta = postulantesOferta.Select(p => p.ToDTO());
                ViewBag.ofertaID = ofertaLaboralID;

                return View("PostulantesFase3Contratar");
            }
        }

        public ActionResult ReadListaPostulantes([DataSourceRequest] DataSourceRequest request, int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.ToList();

                return Json(postulantesOferta.Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }

        public ActionResult ReadListaPostulantesFase1([DataSourceRequest] DataSourceRequest request, int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.Where(p => p.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 1")).ToList();

                return Json(postulantesOferta.Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }

        public ActionResult ReadListaPostulantesFase2([DataSourceRequest] DataSourceRequest request, int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.Where(p => p.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 2")).ToList();

                return Json(postulantesOferta.Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }

        public ActionResult ReadListaPostulantesFase3([DataSourceRequest] DataSourceRequest request, int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.Where(p => p.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 3")).ToList();

                return Json(postulantesOferta.Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AprobarFase1([DataSourceRequest] DataSourceRequest request, int ofertaID, int postulanteID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = o.Postulantes.Where(p => p.ID == postulanteID).FirstOrDefault();


                postulanteOferta.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Aprobado Fase 1"));
                              
                context.TablaOfertaLaboralXPostulante.ModifyElement(postulanteOferta);

                return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AprobarFase2([DataSourceRequest] DataSourceRequest request, int ofertaID, int postulanteID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = o.Postulantes.Where(p => p.ID == postulanteID).FirstOrDefault();

                postulanteOferta.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Aprobado Fase 2"));
                
                context.TablaOfertaLaboralXPostulante.ModifyElement(postulanteOferta);

                return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AprobarFase3([DataSourceRequest] DataSourceRequest request, int ofertaID, int postulanteID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = o.Postulantes.Where(p => p.ID == postulanteID).FirstOrDefault();

                postulanteOferta.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Aprobado Fase 3"));

                context.TablaOfertaLaboralXPostulante.ModifyElement(postulanteOferta);

                return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
            }

        }


        public ActionResult GetViewMotivosRechazo(int ofertaID, int postulanteID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = oferta.Postulantes.Where(p => p.ID == postulanteID).FirstOrDefault();
                ViewBag.cruce = postulanteOferta.ID;
                ViewBag.ofertaID = ofertaID; ViewBag.area = oferta.Area.ToDTO();
                ViewBag.puesto = oferta.Puesto.ToDTO();

                return PartialView("EditorMotivoRechazo",postulanteOferta.ToDTO());
            }

        }

        //ahora creo que no tiene sentido....
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RechazarPostulante([DataSourceRequest] DataSourceRequest request, int ofertaID, int postulanteID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante op = o.Postulantes.Where(p => p.ID == postulanteID).FirstOrDefault();
                if (op.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 1"))
                {
                    op.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Rechazado Fase 1"));
                }
                if (op.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 2"))
                {
                    op.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Rechazado Fase 2"));
                }
                if (op.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 3"))
                {
                    op.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Rechazado Fase 3"));
                }
                context.TablaOfertaLaboralXPostulante.ModifyElement(op); 

                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }

        }


        

    }
}
