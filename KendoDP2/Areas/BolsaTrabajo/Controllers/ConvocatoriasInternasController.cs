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
using KendoDP2.Models.Seguridad;


namespace KendoDP2.Areas.BolsaTrabajo.Controllers
{
    public class ConvocatoriasInternasController : Controller
    {
        //
        // GET: /Reclutamiento/ConvocatoriaInterna/

        public ConvocatoriasInternasController()
        {
            ViewBag.Area = "BolsaTrabajo";
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
                List<OfertaLaboralDTO> ofertas = context.TablaOfertaLaborales.All().Where(p => (p.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Aprobado")) && (p.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Interna"))).Select(p => p.ToDTO()).ToList();
                return Json(ofertas.ToDataSourceResult(request));
            
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
                //ViewBag.funciones = oferta.Puesto. 
                return PartialView("ViewOfertaLaboralInterna", oferta.ToDTO());
            }
        }

        //Falta completar
        public ActionResult GetViewPostulante(int ofertaID)
        {

            using (DP2Context context = new DP2Context())
            {
                
                int colaboradorID = DP2MembershipProvider.GetPersonaID(this);
                Colaborador colaborador = context.TablaColaboradores.FindByID(colaboradorID);
                ViewBag.tipoDocumento = colaborador.TipoDocumento.ToDTO();
                ViewBag.gradoAcademico = colaborador.GradoAcademico.ToDTO();
                ViewBag.ofertaID = ofertaID;
                return PartialView("PostularOfertaLaboral", colaborador.ToDTO());
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Postular(int ofertaID)
        {
            using (DP2Context context = new DP2Context())
            {

                int colaboradorID = DP2MembershipProvider.GetPersonaID(this);
                Colaborador colaborador = context.TablaColaboradores.FindByID(colaboradorID);
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);

                //falta corregir la validacion
                if (ValidaPostulantePorOferta(oferta, colaborador) == 0)
                {
                    Postulante postulante = new Postulante(colaborador);
                    context.TablaPostulante.AddElement(postulante);
                    OfertaLaboralXPostulante cruce = new OfertaLaboralXPostulante { Postulante = postulante, OfertaLaboral = oferta };
                    context.TablaOfertaLaboralXPostulante.AddElement(cruce);

                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
        }


        public int ValidaPostulantePorOferta(OfertaLaboral oferta, Colaborador Colaborador)
        {
            ICollection<OfertaLaboralXPostulante> ListaPostulantes = oferta.Postulantes;

            foreach (var p in ListaPostulantes)
            {
                if (p.Postulante.ColaboradorID == Colaborador.ID)
                {
                    return 1;
                }
            }
            
            return 0;

        }
    }
}
