﻿using Kendo.Mvc.UI;
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
                    Evento c = new Evento().LoadFromDTO(evento);
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
                return View("ElegirInvitados", evento.ToDTO());

            }
        }

        //Para cargar y registrar invitados

        public ActionResult ReadInvitados([DataSourceRequest] DataSourceRequest request, int eventoID)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaInvitado.Where(x => x.EventoID == eventoID)
                    .Select(x => x.ToDTO()).ToDataSourceResult(request));
            }
        }

        // devuelve si se cambio a true ReferenciaDirecta
        private bool AddColaboradorToEvento(int colaboradorID, int eventoID, DP2Context context, bool esReferenciaDirecta)
        {
            var cruce = context.TablaInvitado.One(x => x.ColaboradorID == colaboradorID && x.EventoID == eventoID);
            if (cruce == null)
            { // nuevo
                context.TablaInvitado.AddElement(
                    new Invitado
                    {
                        ColaboradorID = colaboradorID,
                        EventoID= eventoID,
                        ReferenciaDirecta = esReferenciaDirecta,
                        ReferenciasPorAreas = esReferenciaDirecta ? 0 : 1
                    });
                return esReferenciaDirecta;
            }
            else if (!esReferenciaDirecta)
            {
                cruce.ReferenciasPorAreas++;
                context.TablaInvitado.ModifyElement(cruce);
                return false;
            }
            else
            { // no tenia referencia directa
                if (!cruce.ReferenciaDirecta)
                {
                    cruce.ReferenciaDirecta = true;
                    context.TablaInvitado.ModifyElement(cruce);
                    return true;
                }
                return false;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddInvitadosColaborador(int eventoID, int colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                bool isNuevaReferenciaDirecta = AddColaboradorToEvento(colaboradorID, eventoID, context, true);
                return Json(new { success = isNuevaReferenciaDirecta });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddInvitadosAreas(int eventoID, int areaID)
        {
            using (DP2Context context = new DP2Context())
            {
                if (context.TablaAreaXEvento.One(x => x.EventoID == eventoID && x.AreaID == areaID) != null)
                {
                    return Json(new { success = false });
                }
                context.TablaAreaXEvento.AddElement(new AreaXEvento
                {
                    Evento = context.TablaEvento.FindByID(eventoID),
                    Area = context.TablaAreas.FindByID(areaID)
                });
                context.TablaColaboradores.All().Select(c => c.ToDTO()).Where(c => areaID == c.AreaID).Each(c => AddColaboradorToEvento(c.ID, eventoID, context, false));
                return Json(new { success = true });
            }
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyInvitados([DataSourceRequest] DataSourceRequest request, Invitado cruce)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaInvitado.RemoveElementByID(cruce.ID, true);
                return Json(ModelState.ToDataSourceResult());
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyInvitadoDirecto(int eventoID, int colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                var cruce = context.TablaInvitado.One(x => x.EventoID == eventoID && x.ColaboradorID == colaboradorID);
                if (cruce != null)
                {
                    context.TablaInvitado.RemoveElementByID(cruce.ID, true);
                }
                return Json(new { sucess = true });
            }
        }

        public ActionResult GetInvitadosDirectos(int eventoID)
        {
            using (DP2Context context = new DP2Context())
            {
                var lista = context.TablaInvitado.Where(x => x.EventoID == eventoID && x.ReferenciaDirecta).ToList().Select(c => c.ToDTO()).ToList();
                return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAreas(int eventoID)
        {
            using (DP2Context context = new DP2Context())
            {
                var lista = context.TablaAreaXEvento.Where(x => x.EventoID == eventoID).ToList().Select(c => c.ToDTO()).ToList();
                return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyArea(int eventoID, int areaID)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaAreaXEvento.RemoveElementByID(context.TablaAreaXEvento.One(x => x.EventoID == eventoID && x.AreaID == areaID).ID);
                foreach (ColaboradorDTO c in context.TablaColaboradores.All().Select(c => c.ToDTO()).Where(c => areaID == (c.AreaID)).ToList())
                {
                    Invitado cruce = context.TablaInvitado.One(x => x.ColaboradorID == c.ID && x.EventoID == eventoID);
                    if (cruce != null)
                    {
                        cruce.ReferenciasPorAreas--;
                        context.TablaInvitado.ModifyElement(cruce);
                        if (cruce.ReferenciasPorAreas == 0 && !cruce.ReferenciaDirecta)
                        {
                            context.TablaInvitado.RemoveElementByID(cruce.ID, true);
                        }
                    }
                }
                return Json(new { success = true });
            }
        }

    }
}
