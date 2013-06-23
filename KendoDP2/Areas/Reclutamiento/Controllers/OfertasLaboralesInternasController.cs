using Kendo.Mvc.UI;
using KendoDP2.Areas.Reclutamiento.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using KendoDP2.Models.Seguridad;
using KendoDP2.Controllers;
using KendoDP2.Areas.Organizacion.Models;


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
                List<OfertaLaboralXPostulanteDTO> postulantesOferta = oferta.Postulantes.Select(c => c.ToDTO()).ToList();

                return Json(postulantesOferta.ToDataSourceResult(request));
            }
        }

        public ActionResult ReadListaPostulantesFase1([DataSourceRequest] DataSourceRequest request, int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.Where(p => (p.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 1")) || (p.EstadoPostulantePorOferta.Descripcion.Equals("Rechazado Fase 1"))).ToList();

                return Json(postulantesOferta.Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }

        public ActionResult ReadListaPostulantesFase2([DataSourceRequest] DataSourceRequest request, int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.Where(p => (p.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 2")) || (p.EstadoPostulantePorOferta.Descripcion.Equals("Rechazado Fase 2"))).ToList();

                return Json(postulantesOferta.Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }

        public ActionResult ReadListaPostulantesFase3([DataSourceRequest] DataSourceRequest request, int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                List<OfertaLaboralXPostulante> postulantesOferta = oferta.Postulantes.Where(p => (p.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 3")) || (p.EstadoPostulantePorOferta.Descripcion.Equals("Rechazado Fase 3"))).ToList();

                return Json(postulantesOferta.Select(x => x.ToDTO()).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AprobarFase1([DataSourceRequest] DataSourceRequest request, int ofertaID, int postulanteXOfertaID, string fecha)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = o.Postulantes.Where(p => p.ID == postulanteXOfertaID).FirstOrDefault();
                MiscController controladorGeneral = new MiscController();
                //FasePostulacionXOfertaLaboralXPostulante fase = context.TablaFasePostulacionXOfertaLaboralXPostulante.All().FirstOrDefault();
                var org = context.TablaOrganizaciones.All().FirstOrDefault();

                if (postulanteOferta.EstadoPostulantePorOferta.Descripcion.Equals("Inscrito"))
                {
                    postulanteOferta.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Aprobado Fase 1"));
                    postulanteOferta.FechaEvaluacionPrimeraFase = fecha;
                    context.TablaOfertaLaboralXPostulante.ModifyElement(postulanteOferta);

                    int fpID = context.TablaFasePostulacion.One(x => x.Descripcion.Equals("Registrado")).ID;
                    var aux = context.TablaFasePostulacionXOfertaLaboralXPostulante
                        .One(x =>   x.FasePostulacionID == fpID && 
                                    x.OfertaLaboralXPostulanteID == postulanteXOfertaID);
                    if (aux == null) //Si dicho registro no existe
                    {//Entonces lo agrego
                        context.TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante
                        {
                            FasePostulacionID = fpID,
                            OfertaLaboralXPostulanteID = postulanteXOfertaID,

                        });
                    }

                    if (postulanteOferta.OfertaLaboral.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Interna"))
                    {
                        if(postulanteOferta.Postulante.Colaborador.CorreoElectronico != null)
                        {
                            controladorGeneral.SendEmail(postulanteOferta.Postulante.Colaborador.CorreoElectronico, "["+org.RazonSocial+"] Entrevista General",
                                    RetornaMensajeCorreoFase1(postulanteOferta.Postulante.Colaborador.ToDTO().NombreCompleto, fecha,org.Direccion, postulanteOferta.OfertaLaboral.Area.Nombre,postulanteOferta.OfertaLaboral.Puesto.Nombre));                            
                           
                        }
                        else{
                            ModelState.AddModelError("Alerta", "Se aprueba el pase del postulante, pero no se envía la notificación. Revise los datos e intente comunicarse por otro medio");
                            return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        if (postulanteOferta.Postulante.CorreoElectronico != null)
                        {
                            controladorGeneral.SendEmail(postulanteOferta.Postulante.CorreoElectronico, "[" + org.RazonSocial + "] Entrevista General",
                                RetornaMensajeCorreoFase1(postulanteOferta.Postulante.Nombres, fecha, org.Direccion, postulanteOferta.OfertaLaboral.Area.Nombre, postulanteOferta.OfertaLaboral.Puesto.Nombre));
                        }
                        else
                        {
                            ModelState.AddModelError("Alerta", "Se aprueba el pase del postulante, pero no se envía la notificación. Revise los datos e intente comunicarse por otro medio");
                            return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
                    
                        }
                    }
                    return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
                }
                else
                {
                    ModelState.AddModelError("", "El postulante ya fue aprobado o rechazado para otra fase.");
                    return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
                }

            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AprobarFase2([DataSourceRequest] DataSourceRequest request, int ofertaID, int postulanteXOfertaID, string fecha)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = o.Postulantes.Where(p => p.ID == postulanteXOfertaID).FirstOrDefault();
                MiscController controladorGeneral = new MiscController();
                var org = context.TablaOrganizaciones.All().FirstOrDefault();

                if (postulanteOferta.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 1"))
                {
                    postulanteOferta.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Aprobado Fase 2"));
                    postulanteOferta.FechaEvaluacionSegundaFase = fecha;
                    context.TablaOfertaLaboralXPostulante.ModifyElement(postulanteOferta);

                    int fpID = context.TablaFasePostulacion.One(x => x.Descripcion.Equals("Aprobado Externo")).ID;
                    var aux = context.TablaFasePostulacionXOfertaLaboralXPostulante
                        .One(x => x.FasePostulacionID == fpID &&
                                    x.OfertaLaboralXPostulanteID == postulanteXOfertaID);
                    if (aux == null) //Si dicho registro no existe
                    {//Entonces lo agrego
                        context.TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante
                        {
                            FasePostulacionID = fpID,
                            OfertaLaboralXPostulanteID = postulanteXOfertaID,

                        });
                    }

                    if (postulanteOferta.OfertaLaboral.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Interna"))
                    {
                        if (postulanteOferta.Postulante.Colaborador.CorreoElectronico != null)
                        {
                            controladorGeneral.SendEmail(postulanteOferta.Postulante.Colaborador.CorreoElectronico, "[" + org.RazonSocial + "] Entrevista Fase 2",
                                    RetornaMensajeCorreoFase2(postulanteOferta.Postulante.Colaborador.ToDTO().NombreCompleto, fecha, org.Direccion, postulanteOferta.OfertaLaboral.Area.Nombre, postulanteOferta.OfertaLaboral.Puesto.Nombre));

                        }
                        else
                        {
                            ModelState.AddModelError("Alerta", "Se aprueba el pase del postulante, pero no se envía la notificación. Revise los datos e intente comunicarse por otro medio");
                            return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        if (postulanteOferta.Postulante.CorreoElectronico != null)
                        {
                            controladorGeneral.SendEmail(postulanteOferta.Postulante.CorreoElectronico, "[" + org.RazonSocial + "] Entrevista Fase 2",
                                RetornaMensajeCorreoFase2(postulanteOferta.Postulante.Nombres, fecha, org.Direccion, postulanteOferta.OfertaLaboral.Area.Nombre, postulanteOferta.OfertaLaboral.Puesto.Nombre));
                        }
                        else
                        {
                            ModelState.AddModelError("Alerta", "Se aprueba el pase del postulante, pero no se envía la notificación. Revise los datos e intente comunicarse por otro medio");
                            return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));

                        }
                    }
                    
                    
                    return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
                }
                else
                {
                    ModelState.AddModelError("", "El postulante ya fue rechazado para otra fase.");
                    return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
                }
                
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AprobarFase3([DataSourceRequest] DataSourceRequest request, int ofertaID, int postulanteXOfertaID, string fecha)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = o.Postulantes.Where(p => p.ID == postulanteXOfertaID).FirstOrDefault();
                MiscController controladorGeneral = new MiscController();
                var org = context.TablaOrganizaciones.All().FirstOrDefault();

                if (postulanteOferta.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 2"))
                {
                    postulanteOferta.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Aprobado Fase 3"));
                    postulanteOferta.FechaEvaluacionTerceraFase = fecha;
                    context.TablaOfertaLaboralXPostulante.ModifyElement(postulanteOferta);

                    int fpID = context.TablaFasePostulacion.One(x => x.Descripcion.Equals("Aprobado RRHH")).ID;
                    var aux = context.TablaFasePostulacionXOfertaLaboralXPostulante
                        .One(x => x.FasePostulacionID == fpID &&
                                    x.OfertaLaboralXPostulanteID == postulanteXOfertaID);
                    if (aux == null) //Si dicho registro no existe
                    {//Entonces lo agrego
                        context.TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante
                        {
                            FasePostulacionID = fpID,
                            OfertaLaboralXPostulanteID = postulanteXOfertaID,

                        });
                    }

                    if (postulanteOferta.OfertaLaboral.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Interna"))
                    {
                        if (postulanteOferta.Postulante.Colaborador.CorreoElectronico != null)
                        {
                            controladorGeneral.SendEmail(postulanteOferta.Postulante.Colaborador.CorreoElectronico, "[" + org.RazonSocial + "] Entrevista Final",
                                    RetornaMensajeCorreoFase3(postulanteOferta.Postulante.Colaborador.ToDTO().NombreCompleto, fecha, org.Direccion, postulanteOferta.OfertaLaboral.Area.Nombre, postulanteOferta.OfertaLaboral.Puesto.Nombre));

                        }
                        else
                        {
                            ModelState.AddModelError("Alerta", "Se aprueba el pase del postulante, pero no se envía la notificación. Revise los datos e intente comunicarse por otro medio");
                            return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        if (postulanteOferta.Postulante.CorreoElectronico != null)
                        {
                            controladorGeneral.SendEmail(postulanteOferta.Postulante.CorreoElectronico, "[" + org.RazonSocial + "] Entrevista Final",
                                RetornaMensajeCorreoFase3(postulanteOferta.Postulante.Nombres, fecha, org.Direccion, postulanteOferta.OfertaLaboral.Area.Nombre, postulanteOferta.OfertaLaboral.Puesto.Nombre));
                        }
                        else
                        {
                            ModelState.AddModelError("Alerta", "Se aprueba el pase del postulante, pero no se envía la notificación. Revise los datos e intente comunicarse por otro medio");
                            return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));

                        }
                    }



                    return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
                }
                else
                {
                    ModelState.AddModelError("", "El postulante ya fue rechazado para otra fase.");
                    return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState));
                }
                
                
            }

        }


        public ActionResult GetViewMotivosRechazo(int ofertaID, int postulanteXOfertaID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = oferta.Postulantes.Where(p => p.ID == postulanteXOfertaID).FirstOrDefault();
                ViewBag.cruce = postulanteOferta.ID;
                ViewBag.ofertaID = ofertaID; ViewBag.area = oferta.Area.ToDTO();
                ViewBag.puesto = oferta.Puesto.ToDTO();
                ViewBag.yaRechazadoAprobado =  postulanteOferta.EstadoPostulantePorOferta.Descripcion.StartsWith("Rechaz");
                if (oferta.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Interna"))
                {
                    return PartialView("EditorMotivoRechazo", postulanteOferta.ToDTO());
                }
                else
                    return PartialView("EditorMotivoRechazoExterno", postulanteOferta.ToDTO());
            }

        }

        public ActionResult GetViewSeleccionFechaFase1(int ofertaID, int postulanteXOfertaID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = oferta.Postulantes.Where(p => p.ID == postulanteXOfertaID).FirstOrDefault();
                ViewBag.cruce = postulanteOferta.ID;
                ViewBag.ofertaID = ofertaID; 
                ViewBag.yaAprobado = postulanteOferta.EstadoPostulantePorOferta.Descripcion.StartsWith("Aprobado");
               
                    return PartialView("SeleccionarFechaEvaluacionFase1", postulanteOferta.ToDTO());
                
            }

        }

        public ActionResult GetViewSeleccionFechaFase2(int ofertaID, int postulanteXOfertaID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = oferta.Postulantes.Where(p => p.ID == postulanteXOfertaID).FirstOrDefault();
                ViewBag.cruce = postulanteOferta.ID;
                ViewBag.ofertaID = ofertaID;
                ViewBag.yaAprobado = false;

                return PartialView("SeleccionarFechaEvaluacionFase2", postulanteOferta.ToDTO());

            }

        }

        public ActionResult GetViewSeleccionFechaFase3(int ofertaID, int postulanteXOfertaID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = oferta.Postulantes.Where(p => p.ID == postulanteXOfertaID).FirstOrDefault();
                ViewBag.cruce = postulanteOferta.ID;
                ViewBag.ofertaID = ofertaID;
                ViewBag.yaAprobado =false;

                return PartialView("SeleccionarFechaEvaluacionFase3", postulanteOferta.ToDTO());

            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RechazarPostulante(int postulanteXOFertaID, int ofertaID, string observaciones, string motivoRechazo)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante op = o.Postulantes.Where(p => p.ID == postulanteXOFertaID).FirstOrDefault();
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
                if (op.EstadoPostulantePorOferta.Descripcion.Equals("Inscrito"))
                {
                    op.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Rechazado"));

                }
                
                op.MotivoRechazo = motivoRechazo;
                context.TablaOfertaLaboralXPostulante.ModifyElement(op);
                // aqui mandar correo 
                return Json(new { success = true });
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

                // aca asignar el nuevo puesto
                if (postulanteOferta.EstadoPostulantePorOferta.Descripcion.Equals("Aprobado Fase 3"))
                {
                    // se cambia el estado de la postulacion
                    postulanteOferta.EstadoPostulantePorOferta = context.TablaEstadoPostulanteXOferta.One(p => p.Descripcion.Equals("Contratado"));
                    context.TablaOfertaLaboralXPostulante.ModifyElement(postulanteOferta);

                    // asigno fecha fin al puesto
                    var todos = context.TablaColaboradoresXPuestos.All();
                    ColaboradorXPuesto u = null;
                    foreach (var crucex in todos)
                        if (crucex.ColaboradorID == postulanteOferta.Postulante.ColaboradorID)
                            if (crucex.FechaSalidaPuesto == null)
                                u = crucex;
                    if(u != null)
                    {
                        u.FechaSalidaPuesto = DateTime.Now.AddDays(-1);
                        context.TablaColaboradoresXPuestos.ModifyElement(u);
                    }


                    // se crea el nuevo puesto
                    ColaboradorXPuesto cruce = new ColaboradorXPuesto { ColaboradorID = postulanteOferta.Postulante.Colaborador.ID, PuestoID = oferta.PuestoID, Sueldo = oferta.SueldoTentativo, FechaIngresoPuesto = DateTime.Now};

                    context.TablaColaboradoresXPuestos.AddElement(cruce);

                    // se manda el correo
                    if (postulanteOferta.Postulante.Colaborador.CorreoElectronico != null)
                    {
                        controladorGeneral.SendEmail(postulanteOferta.Postulante.Colaborador.CorreoElectronico, "[" + org.RazonSocial + "] Aviso de Seleccion",
                                RetornaMensajeCorreoCambioPuesto(postulanteOferta.Postulante.Colaborador.ToDTO().NombreCompleto, postulanteOferta.OfertaLaboral.Area.Nombre, postulanteOferta.OfertaLaboral.Puesto.Nombre));
                    }
                    else
                    {
                        ModelState.AddModelError("Alerta", "La creación del puesto fue satisfactoria. Sin embargo: Se selecciona y cambia el puesto del postulante, pero no se envía la notificación. Revise los datos e intente comunicarse por otro medio");
                    }

                   //return RedirectToAction("Linea", "Historial", new { Area = "Organizacion", ID = DP2MembershipProvider.GetPersonaID(this) });
                }
                else
                {
                    ModelState.AddModelError("", "El postulante ya fue contratado o rechazado para esta oferta laboral.");
                }
                return Json(new[] { postulanteOferta.ToDTO() }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
                    
            }

        }

        public ActionResult GetViewPostulanteInterno(int ofertaID, int postulanteXOfertaID)
        {

            using (DP2Context context = new DP2Context())
            {

                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                OfertaLaboralXPostulante postulanteOferta = oferta.Postulantes.Where(p => p.ID == postulanteXOfertaID).FirstOrDefault();

                ViewBag.tipoDocumento = postulanteOferta.Postulante.Colaborador.TipoDocumento.ToDTO();
                ViewBag.gradoAcademico = postulanteOferta.Postulante.Colaborador.GradoAcademico.ToDTO();
                ViewBag.ofertaID = ofertaID;
                return PartialView("ViewPostulanteInterno", postulanteOferta.ToDTO());
               
            }
        }

        public string RetornaMensajeCorreoFase1(string nombre, string fecha, string lugar, string area, string puesto)
        {

            string mensaje = "Estimado(a) " + nombre + ": \n\n" +
                             "Se le notifica que tras la evaluación de sus datos presentados ha sido aceptado " +
                             "para la primera fase de reclutamiento. Por esta razón se hace la respectiva citación: \n\n" +
                             "Puesto al cual Postula: " + puesto + "\n" +
                             "Area Correspondiente: " + area + "\n" +
                             "Día: " + fecha + "\n" +
                             "Lugar: " + lugar + "\n\n" +
                             "Para la entrevista y evaluación respectiva. \n\n" +
                             "Saludos cordiales \n\n" + 
                             "Gerencia Recursos Humanos \n";
            return mensaje;
        }

        public string RetornaMensajeCorreoFase2(string nombre, string fecha, string lugar, string area, string puesto)
        {

            string mensaje = "Estimado(a) " + nombre + ": \n\n" +
                             "Se le notifica que tras la evaluación de sus datos presentados ha sido aceptado " +
                             "para la segunda fase de reclutamiento, que corresponde a la evaluación psicotécnica. Por esta razón se hace la respectiva citación: \n\n" +
                             "Puesto al cual Postula: " + puesto + "\n" +
                             "Area Correspondiente: " + area + "\n" +
                             "Día: " + fecha + "\n" +
                             "Lugar: " + lugar + "\n\n" +
                             "Para la entrevista y evaluación respectiva. \n\n" +
                             "Saludos cordiales \n\n" +
                             "Gerencia Recursos Humanos \n";
            return mensaje;
        }

        public string RetornaMensajeCorreoFase3(string nombre, string fecha, string lugar, string area, string puesto)
        {

            string mensaje = "Estimado(a) " + nombre + ": \n\n" +
                             "Se le notifica que tras la evaluación de sus datos presentados ha sido aceptado " +
                             "para la tercera fase de reclutamiento. Por esta razón se hace la respectiva citación: \n\n" +
                             "Puesto al cual Postula: " + puesto + "\n" +
                             "Area Correspondiente: " + area + "\n" +
                             "Día: " + fecha + "\n" +
                             "Lugar: " + lugar + "\n\n" +
                             "Para la entrevista y evaluación respectiva. \n\n" +
                             "Saludos cordiales \n\n" +
                             "Gerencia Recursos Humanos \n";
            return mensaje;
        }

        public string RetornaMensajeCorreoRechazaPase(string nombre, string area, string puesto)
        {

            string mensaje = "Estimado(a) " + nombre + ": \n\n" +
                             "Se le notifica que tras la evaluación de sus datos, información y aptitudes, no continuará " +
                             "dentro del proceso de reclutamiento para el siguiente puesto: \n\n" +
                             "Puesto: " + puesto + "\n" +
                             "Area Correspondiente: " + area + "\n" +
                             
                             "Solicitamos las disculpas del caso y le recordamos que puede participar en otra de nuestras convocatorias. \n\n" +
                             "Saludos cordiales \n\n" +
                             "Gerencia Recursos Humanos \n";
            return mensaje;
        }

        public string RetornaMensajeCorreoCambioPuesto(string nombre, string area, string puesto)
        {

            string mensaje = "Estimado(a) " + nombre + ": \n\n" +
                             "Se le notifica que tras la evaluación de sus datos, información y aptitudes, ha aprobado " +
                             "todas las etapas del proceso y por lo tanto se le asigna el siguiente puesto: \n\n" +
                             "Puesto: " + puesto + "\n" +
                             "Area Correspondiente: " + area + "\n" +

                             "Solicitamos se aproxime a las oficinas de Recursos humanos para realizar las coordinaciones y diligencias necesarias. \n\n" +
                             "Felicitaciones \n"+
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
                o.PuntajeTotal = ofertaPostulante.PuntajeTotal;
                context.TablaOfertaLaboralXPostulante.ModifyElement(o);
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        }
      

    
}
