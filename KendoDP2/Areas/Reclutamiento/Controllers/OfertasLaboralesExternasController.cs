using Kendo.Mvc.UI;
using KendoDP2.Areas.Reclutamiento.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    public class OfertasLaboralesExternasController : Controller
    {
        //
        // GET: /Reclutamiento/OfertasLaboralesExternas/

        public OfertasLaboralesExternasController()
        {
            ViewBag.Area = "Reclutamiento";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.ofertasLaborales = context.TablaOfertaLaborales.All().Where(p => (p.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Aprobado")) && (p.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Pública"))).Select(p => p.ToDTO()).ToList();
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
                List<OfertaLaboralDTO> ofertasPosibles = context.TablaOfertaLaborales.All().Where(p => (p.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Aprobado")) && (p.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Pública"))).Select(p => p.ToDTO()).ToList();
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

                return View("ListaPostulantesExternos");
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
                return View("PostulantesExternosFase1");
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
                return View("PostulantesExternosFase2");

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
                return View("PostulantesExternosFase3");

            }
        }

        //prueba reusar metodos controllerOfertaLaboralInterna

        public ActionResult ReadListaPostulantesExternos([DataSourceRequest] DataSourceRequest request, int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.ToList();

                return Json(postulantesOferta.Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }

        public ActionResult ReadListaPostulantesExternosFase1([DataSourceRequest] DataSourceRequest request, int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.Where(p => (p.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 1")) || (p.EstadoPostulantePorOferta.Descripcion.Equals("Rechazado Fase 1"))).ToList();

                return Json(postulantesOferta.Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }


        public ActionResult GetViewPostulanteExterno(int ofertaID, int postulanteXOfertaID)
        {

            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = oferta.Postulantes.Where(p => p.ID == postulanteXOfertaID).FirstOrDefault();

                ViewBag.tipoDocumento = postulanteOferta.Postulante.TipoDocumento.ToDTO();
                ViewBag.gradoAcademico = postulanteOferta.Postulante.GradoAcademico.ToDTO();
                ViewBag.ofertaID = ofertaID;
                return PartialView("ViewPostulanteExterno", postulanteOferta.ToDTO());

            }
        }

        public ActionResult Contratar(int ofertaID, int postulanteXOfertaLaboralID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = oferta.Postulantes.Where(p => p.ID == postulanteXOfertaLaboralID).FirstOrDefault();
                // aca asignar el nuevo puesto
                ColaboradorDTO colaboradorDTO = new ColaboradorDTO
                {
                    Nombre = postulanteOferta.Postulante.Nombres,
                    ApellidoPaterno = postulanteOferta.Postulante.ApellidoPaterno,
                    ApellidoMaterno = postulanteOferta.Postulante.ApellidoMaterno,
                    TipoDocumentoID = postulanteOferta.Postulante.TipoDocumentoID,
                    NumeroDocumento = postulanteOferta.Postulante.NumeroDocumento,
                    CorreoElectronico = postulanteOferta.Postulante.CorreoElectronico,
                    CentroEstudios = postulanteOferta.Postulante.CentroEstudios,
                    GradoAcademicoID = (int)postulanteOferta.Postulante.GradoAcademicoID,
                    CurriculumVitaeID = postulanteOferta.Postulante.CurriculumVitaeID,
                    AreaID = oferta.AreaID,
                    PuestoID = oferta.PuestoID,
                    Sueldo = oferta.SueldoTentativo
                    
                };

                Colaborador c = new Colaborador(colaboradorDTO);
                c.EstadoColaborador = context.TablaEstadosColaboradores.One(x => x.Descripcion.Equals("Contratado"));
                context.TablaColaboradores.AddElement(c);
                Puesto pe = context.TablaPuestos.FindByID(colaboradorDTO.PuestoID);
                ColaboradorXPuesto cruce = new ColaboradorXPuesto { ColaboradorID = c.ID, PuestoID = pe.ID, Sueldo = colaboradorDTO.Sueldo };
                context.TablaColaboradoresXPuestos.AddElement(cruce);

                postulanteOferta.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Contratado"));
                context.TablaOfertaLaboralXPostulante.ModifyElement(postulanteOferta);
                
                return RedirectToAction("Index", "Colaboradores", new { Area = "Organizacion" });
            }

        }

      
    }
}
