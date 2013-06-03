using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Seguridad;
using KendoDP2.Areas.Eventos.Models;



namespace KendoDP2.Areas.Eventos.Controllers
{
    public class EventosController : Controller
    {
        //
        // GET: /Eventos/Eventos/

        public EventosController()
        {
            ViewBag.Area = "Eventos";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.areas = context.TablaAreas.All().Select(p => p.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.estadosEventos = context.TablaEstadoEvento.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoEventos = context.TablaTiposEvento.All().Select(p => p.ToDTO()).ToList();
                return View();

            }

        }

        // Grid periodos
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<EventoDTO> colaboradores = context.TablaEvento.All().Select(p => p.ToDTO()).OrderBy(x => x.ID).ToList();
                return Json(colaboradores.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, EventoDTO evento)
        {
            using (DP2Context context = new DP2Context())
            {
                    Evento c = new Evento(evento);
                    int creadorID = DP2MembershipProvider.GetPersonaID(this);
                    c.CreadorID = creadorID;
                    c.Creador = context.TablaColaboradores.FindByID(creadorID);
                    context.TablaEvento.AddElement(c);
                    return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));                
                
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, EventoDTO evento)
        {
            using (DP2Context context = new DP2Context())
            {
                Evento p = context.TablaEvento.FindByID(evento.ID).LoadFromDTO(evento);
                context.TablaEvento.ModifyElement(p);
                return Json(new[] { p.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, EventoDTO evento)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaEvento.RemoveElementByID(evento.ID, true);
                return Json(ModelState.ToDataSourceResult());
            }
        }

        public ActionResult ListaInvitados(int eventoID)
        {
            using (DP2Context context = new DP2Context())
            {
                Evento evento = context.TablaEvento.FindByID(eventoID);
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
                ViewBag.estados = context.TablaEstadoColaboradorXProcesoEvaluaciones.All().Select(c => c.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
                return View("ElegirInvitados");

            }
        }
    }
}
