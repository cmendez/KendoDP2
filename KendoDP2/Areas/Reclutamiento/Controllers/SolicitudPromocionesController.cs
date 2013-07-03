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
    public class SolicitudPromocionesController : Controller
    {
        //
        // GET: /Reclutamiento/SolicitudPromociones/

        public SolicitudPromocionesController()
        {
            ViewBag.Area = "Reclutamiento";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                //ViewBag.ofertasLaborales = context.TablaOfertaLaborales.All().Select(p => p.ToDTO()).ToList();
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
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
                List<SolicitudPromocionDTO> promociones = context.TablaSolicitudPromociones.All().Select(p => p.ToDTO()).OrderBy(x => x.ID).ToList();
                return Json(promociones.ToDataSourceResult(request));
            
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, SolicitudPromocionDTO solicitud)
        {
            int id;
            using (DP2Context context = new DP2Context())
            {
                solicitud.EstadoSolicitudOfertaLaboralID = context.TablaEstadosSolicitudes.One(x => x.Descripcion.Equals("Pendiente")).ID;
                solicitud.FechaRequerimiento = ParseoFecha(solicitud.FechaRequerimiento);
                solicitud.FechaFinRequerimiento = ParseoFecha(solicitud.FechaFinRequerimiento);
                SolicitudPromocion o = new SolicitudPromocion(solicitud);
                
        
                id = context.TablaSolicitudPromociones.AddElement(o);
            }
            using (DP2Context context = new DP2Context())
            {
                var o = context.TablaSolicitudPromociones.FindByID(id);
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, SolicitudPromocionDTO solicitud)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaSolicitudPromociones.RemoveElementByID(solicitud.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, SolicitudPromocionDTO solicitud)
        {
            using (DP2Context context = new DP2Context())
            {
                SolicitudPromocion o = context.TablaSolicitudPromociones.FindByID(solicitud.ID).LoadFromDTO(solicitud);
                context.TablaSolicitudPromociones.ModifyElement(o);
                

                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }


        public ActionResult GetViewSolicitud(int solicitudID)
        {
            
            using (DP2Context context = new DP2Context())
            {
                SolicitudPromocion solicitud = context.TablaSolicitudPromociones.FindByID(solicitudID);
                ViewBag.yaValido = YaValido(solicitud);
                ViewBag.responsable = solicitud.Responsable.ToDTO();
                ViewBag.estadoSolicitudOferta = solicitud.EstadoSolicitudOfertaLaboral.ToDTO();
                ViewBag.area = solicitud.Area.ToDTO();
                ViewBag.puesto = solicitud.Puesto.ToDTO();
                ViewBag.promovido = solicitud.Ascendido.ToDTO();
                ViewBag.funciones = solicitud.Puesto.Funciones.Select(c => c.ToDTO()).ToList();
                ViewBag.capacidades = solicitud.Puesto.GetCapacidadesAsociadas(context).Select(c => c.ToDTO()).ToList();

                return PartialView("ViewSolicitudPromocion", solicitud.ToDTO());
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CambiaEstadoSolicitudAprobada([DataSourceRequest] DataSourceRequest request, int solicitudID)
        {
            using (DP2Context context = new DP2Context())
            {
                SolicitudPromocion o = context.TablaSolicitudPromociones.FindByID(solicitudID);
                if (o.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Pendiente"))
                {
                    if (PuestoEstaVacante(o.PuestoID))
                    {
                        o.EstadoSolicitudOfertaLaboral = context.TablaEstadosSolicitudes.One(p => p.Descripcion.Equals("Aprobado"));
                        DateTime ahora = DateTime.Now;
                        o.FechaAprobacion = ahora.ToString("dd/MM/yyyy");
                        var ultimoCruce = context.TablaColaboradoresXPuestos.One(x => x.FechaSalidaPuesto == null && x.ColaboradorID == o.AscendidoID);
                        if (ultimoCruce != null)
                        {
                            ultimoCruce.FechaSalidaPuesto = DateTime.Now.AddDays(-1);
                            context.TablaColaboradoresXPuestos.ModifyElement(ultimoCruce);
                        }
                        ColaboradorXPuesto cruce = new ColaboradorXPuesto { ColaboradorID = o.AscendidoID, PuestoID = o.PuestoID, Sueldo = o.SueldoTentativo, FechaIngresoPuesto = ahora };

                        context.TablaColaboradoresXPuestos.AddElement(cruce);
                        context.TablaSolicitudPromociones.ModifyElement(o);

                        return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
                    }
                    else
                    {
                        ModelState.AddModelError("Puesto", "El puesto está ocupado por otra persona. Verificar. De lo contrario Rechazar la solicitud");
                        return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
                    }
                }

   //             ModelState.AddModelError("Puesto", "No es posible aprobar esta solicitud");
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));


            }
        }

       [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CambiaEstadoSolicitudRechazada([DataSourceRequest] DataSourceRequest request, int solicitudID)
        {
            using (DP2Context context = new DP2Context())
            {
                SolicitudPromocion o = context.TablaSolicitudPromociones.FindByID(solicitudID);
                if (o.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Pendiente"))
                {
                    o.EstadoSolicitudOfertaLaboral = context.TablaEstadosSolicitudes.One(p=> p.Descripcion.Equals("Rechazado"));
                }
                context.TablaSolicitudPromociones.ModifyElement(o);

                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }

        }

       public string ParseoFecha(string fecha)
       {

           string anho = fecha.Substring(0,4);
           string mes = fecha.Substring(5,2);
           string dia= fecha.Substring(8,2);
           string fechanueva = dia + "/" + mes + "/" + anho;
           return fechanueva;

       }


       public bool YaValido(SolicitudPromocion solicitud)
       {
           return (!(solicitud.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Pendiente")));
       }

       public bool PuestoEstaVacante(int puestoID)
       {
           using (DP2Context context = new DP2Context())
           {
               ColaboradorXPuesto cruce = context.TablaColaboradoresXPuestos.One(x => (x.FechaSalidaPuesto == null || x.FechaSalidaPuesto >= DateTime.Today) && x.PuestoID == puestoID);
               return cruce == null;
           }
       }
    }
}
