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
using KendoDP2.Controllers;

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

        public ActionResult Contratar([DataSourceRequest] DataSourceRequest request, int ofertaID, int postulanteXOfertaLaboralID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = oferta.Postulantes.Where(p => p.ID == postulanteXOfertaLaboralID).FirstOrDefault();
                MiscController controladorGeneral = new MiscController();
                var org = context.TablaOrganizaciones.All().FirstOrDefault();
                if (postulanteOferta.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 3"))
                {
                    // aca asignar el nuevo puesto
                    int puestoPadre = oferta.PuestoID;
                    List<PuestoDTO> puestos = BuscoPuestoPapaEHijos(puestoPadre);

                    if (puestos != null)
                    {
                        PuestoDTO puestoEncontrado = BuscaPuestoLibreAsignar(puestos);

                        if (puestoEncontrado != null)
                        {

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
                                //AreaID = oferta.AreaID,
                                //PuestoID = oferta.PuestoID,

                                Sueldo = oferta.SueldoTentativo

                            };

                            Colaborador c = new Colaborador(colaboradorDTO);
                            c.EstadoColaborador = context.TablaEstadosColaboradores.One(x => x.Descripcion.Equals("Contratado"));
                            context.TablaColaboradores.AddElement(c);
                            Puesto pe = context.TablaPuestos.FindByID(puestoEncontrado.ID);
                            //hasta mientras fecha de ingreso = datetime.now
                            ColaboradorXPuesto cruce = new ColaboradorXPuesto { ColaboradorID = c.ID, PuestoID = pe.ID, Sueldo = colaboradorDTO.Sueldo, FechaIngresoPuesto = DateTime.Now };
                            context.TablaColaboradoresXPuestos.AddElement(cruce);

                            postulanteOferta.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Contratado"));
                            context.TablaOfertaLaboralXPostulante.ModifyElement(postulanteOferta);

                            if (postulanteOferta.Postulante.CorreoElectronico != null)
                            {
                                controladorGeneral.SendEmail(postulanteOferta.Postulante.CorreoElectronico, "[" + org.RazonSocial + "] Aviso de Seleccion",
                                        RetornaMensajeContrato(postulanteOferta.Postulante.ToDTO().NombreCompleto, postulanteOferta.OfertaLaboral.Area.Nombre, postulanteOferta.OfertaLaboral.Puesto.Nombre));

                            }
                            else
                            {
                                ModelState.AddModelError("Alerta", "Se selecciona y cambia el puesto del postulante, pero no se envía la notificación. Revise los datos e intente comunicarse por otro medio");
                            }

                            return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Este puesto está ocupado, por favor revisar el contrato del colaborador en el puesto actual o si existe dentro de la estructura");
                            return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "No existe el puesto");
                    }
                    return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                }
                else
                { 
                    ModelState.AddModelError("", "El postulante ya fue contratado o rechazado para esta oferta laboral.");
                    return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                }
            }

        }

        public string RetornaMensajeContrato(string nombre, string area, string puesto)
        {

            string mensaje = "Estimado(a) " + nombre + ": \n\n" +
                             "Se le notifica que tras la evaluación de sus datos, información y aptitudes, ha aprobado " +
                             "todas las etapas del proceso y por lo tanto se le asigna el siguiente puesto dentro de nuestra organización: \n\n" +
                             "Puesto: " + puesto + "\n" +
                             "Area Correspondiente: " + area + "\n" +

                             "Solicitamos se aproxime a las oficinas de Recursos humanos para realizar las coordinaciones y diligencias necesarias. \n\n" +
                             "Felicitaciones \n" +
                             "Saludos cordiales \n\n" +
                             "Gerencia Recursos Humanos \n";
            return mensaje;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, OfertaLaboralXPostulanteDTO ofertaPostulante)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboralXPostulante o = context.TablaOfertaLaboralXPostulante.FindByID(ofertaPostulante.ID);
                if (o.PuntajeFase2 == 0)
                {
                    o.PuntajeFase2 = ofertaPostulante.PuntajeFase2;
                    o.PuntajeTotal = o.PuntajeTotal + ofertaPostulante.PuntajeFase2;
                }
                context.TablaOfertaLaboralXPostulante.ModifyElement(o);
                foreach (var modelValue in ModelState.Values)
                {
                    modelValue.Errors.Clear();
                }
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }


        public bool PuestoEstaVacante(int puestoID)
        {
            using (DP2Context context = new DP2Context())
            {
                ColaboradorXPuesto cruce = context.TablaColaboradoresXPuestos.One(x => (x.FechaSalidaPuesto == null || x.FechaSalidaPuesto >= DateTime.Today) && x.PuestoID == puestoID);
                return cruce == null;
            }
        }

        public List<PuestoDTO> BuscoPuestoPapaEHijos(int puestoID)
        {
            using (DP2Context context = new DP2Context())
            {
                Puesto p = context.TablaPuestos.FindByID(puestoID);

                List<PuestoDTO> lista = context.TablaPuestos.Where(x => x.Nombre.Equals(p.Nombre) || x.Nombre.StartsWith(p.Nombre)).Select(m => m.ToDTO()).ToList();
                return lista;

            }

        }

        public PuestoDTO BuscaPuestoLibreAsignar(List<PuestoDTO> todosPuestos)
        {
            foreach (PuestoDTO p in todosPuestos)
            {
                if (PuestoEstaVacante(p.ID))
                    return p;
            }

            return null;
        }

        

     }
}
