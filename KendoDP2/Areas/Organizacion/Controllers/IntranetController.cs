using KendoDP2.Areas.Eventos.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Reclutamiento.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    [Authorize()]
    public class IntranetController : Controller
    {
        public IntranetController()
        {
            ViewBag.Area = "Organizacion";
        }

        private List<OfertaLaboralDTO> GetOfertasRecientes(DP2Context context)
        {
            List<OfertaLaboralDTO> ofertasPosibles = context.TablaOfertaLaborales.Where(p => (p.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Aprobado")) && (p.ModoSolicitudOfertaLaboral.Descripcion.Equals("Convocatoria Interna"))).Select(p => p.ToDTO()).ToList();
            DateTime now = DateTime.Now;
            List<OfertaLaboralDTO> ofertasEnFecha = ofertasPosibles.Where(x => DateTime.ParseExact(x.FechaFinRequerimiento, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(now) >= 1).ToList();
            int indice = 1;
            List<OfertaLaboralDTO> res = new List<OfertaLaboralDTO>();
            if (ofertasEnFecha != null)
                for (int i = 0; i < 3 && i < ofertasEnFecha.Count; i++)
                    res.Add(ofertasEnFecha[i]);    
            
            while(ofertasEnFecha.Count < 0)
                ofertasEnFecha.Add( new OfertaLaboralDTO { Area = "-", Puesto = "-", SueldoTentativo = 0 } );
            return res;
        }
        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);

                string nombreE = "No hay eventos programados";
                string lugarE = "No hay eventos programados";
                
                
                ICollection<Invitado> i = context.TablaInvitado.Where(m => m.ColaboradorID == ColaboradorID).ToList();
                ICollection<Invitado> invReciente = i.Where(p => p.Evento.Inicio >= DateTime.Today).OrderBy(x => x.Evento.Inicio).ToList();
                
                if (invReciente != null)
                {
                        var evActual = invReciente.FirstOrDefault();
                        if (evActual != null)
                        {
                            nombreE = evActual.Evento.Nombre;
                            lugarE = evActual.Evento.LugarEvento;
                        }
                }
                
                

              
                ViewBag.ColaboradorDTO = context.TablaColaboradores.FindByID(ColaboradorID).ToDTO();
                ViewBag.tipoDocumentos = context.TablaTiposDocumentos.All().Select(c => c.ToDTO()).ToList();
                ViewBag.gradoAcademico = context.TablaGradosAcademicos.All().Select(c => c.ToDTO()).ToList();
                ViewBag.estadosColaborador = context.TablaEstadosColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.pais = context.TablaPaises.All().Select(c => c.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(c => c.ToDTO()).ToList();
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.colaboradorLogueado = DP2MembershipProvider.GetPersonaID(this);
                ViewBag.estadosEventos = context.TablaEstadoEvento.All().Select(c => c.ToDTO()).ToList();
                ViewBag.nombreEvento = nombreE;
                ViewBag.lugarEvento = lugarE;
                ViewBag.ofertas = GetOfertasRecientes(context);

                
                return View();
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateSimple(ColaboradorDTO colaborador)
        {
            using (DP2Context context = new DP2Context())
            {
                Colaborador c = context.TablaColaboradores.FindByID(colaborador.ID);
                //Aqui cargas a mano cada campo, porque no se modifican todos
                //c.Nombres = colaborador.Nombre;
                //c.TipoDocumentoID = colaborador.TipoDocumentoID;
                c.ResumenEjecutivo = colaborador.ResumenEjecutivo;
                c.Direccion = colaborador.Direccion;
                c.CorreoElectronico = colaborador.CorreoElectronico;
                c.CentroEstudios = colaborador.CentroEstudios;
                c.GradoAcademicoID = colaborador.GradoAcademicoID;
                c.GradoAcademico = context.TablaGradosAcademicos.FindByID(colaborador.GradoAcademicoID);
                c.Telefono = colaborador.Telefono;
                c.CurriculumVitaeID = colaborador.CurriculumVitaeID;

                //
                context.TablaColaboradores.ModifyElement(c);
                return Json(new { success = true });
            }
        }


        public int ValidarCambioContrasenha(ColaboradorDTO colaborador, string contrasenhaActual, string nuevaContrasenha)
        {
            if (colaborador.Contrasenha.Equals(contrasenhaActual))
            {
                colaborador.Contrasenha = nuevaContrasenha;
                return 1;
            }

            return 0;

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CambiarContrasenha(int colaboradorID, string pass1, string pass2)
        {
            using (DP2Context context = new DP2Context())
            {
                Colaborador c = context.TablaColaboradores.FindByID(colaboradorID);
                if (pass2.Length == 0)
                {
                    return Json(new { mensaje = "La contrasenha nueva no puede ser vacia." });
                }
                if (c.Password.Equals(pass1))
                {
                    c.Password = pass2;
                    context.TablaColaboradores.ModifyElement(c);
                    return Json(new { mensaje = "El cambio fue satisfactorio" });
                }
                else
                {
                    return Json(new { mensaje = "La contrasenha antigua no coincide" });
                }
            }
        }

        public JsonResult GetEventosPersonales(int colaboradorID)
        {
            List<Evento> Eventos = null;
            List<Evento> EventosXColaborador = new List<Evento>();
            using (DP2Context context = new DP2Context())
            {
                Eventos = context.TablaEvento.All().ToList();
               foreach (Evento e in Eventos)
                {
                    Invitado invitado = e.Invitados.Where(p => p.ColaboradorID == colaboradorID).FirstOrDefault();
                   //|| e.creadorID = colaboradorID 
                   if (invitado != null)
                    {
                        EventosXColaborador.Add(e);
                    }
                }

               var eventList = from e in EventosXColaborador
                                select new
                                {
                                    id = e.ID,
                                    title = e.Nombre,
                                    start = e.Inicio.ToString("s"),
                                    end = e.Fin.ToString("s"),
                                    allDay = false,
                                    className = e.custom
                                };

                var rows = eventList.ToArray();
                return Json(rows, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult ShowDetalleEvento(int eventoID)
        {
            using (DP2Context context = new DP2Context())
            {
                EventoDTO eventoDTO = context.TablaEvento.FindByID(eventoID).ToDTO();
                ViewBag.invitados = eventoDTO.Invitados.ToList();

                return PartialView("DetalleEvento", eventoDTO);
            }
        }

        

        public ActionResult RedireccionEventos()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.areas = context.TablaAreas.All().Select(p => p.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.estadosEventos = context.TablaEstadoEvento.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoEventos = context.TablaTiposEvento.All().Select(p => p.ToDTO()).ToList();

                return View("Index", new {Area= "Eventos"});
            }
        }
    }
}