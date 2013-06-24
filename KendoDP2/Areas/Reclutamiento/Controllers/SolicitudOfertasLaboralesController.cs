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
    [Authorize()]
    public class SolicitudOfertasLaboralesController : Controller
    {
        //
        // GET: /Reclutamiento/SolicitudOfertasLaborales/

        public SolicitudOfertasLaboralesController()
        {
            ViewBag.Area = "Reclutamiento";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.ofertasLaborales = context.TablaOfertaLaborales.All().Select(p => p.ToDTO()).ToList();
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
                List<OfertaLaboralDTO> ofertas = context.TablaOfertaLaborales.All().Select(p => p.ToDTO()).OrderBy(x => x.ID).ToList();
                return Json(ofertas.ToDataSourceResult(request));
            
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, OfertaLaboralDTO oferta)
        {
            int ID = 0;
            using (DP2Context context = new DP2Context())
            {
                oferta.EstadoSolicitudOfertaLaboralID = context.TablaEstadosSolicitudes.One(x => x.Descripcion.Equals("Pendiente")).ID;
                oferta.FechaRequerimiento = ParseoFecha(oferta.FechaRequerimiento);
                oferta.FechaFinRequerimiento = ParseoFecha(oferta.FechaFinRequerimiento);
                OfertaLaboral o = new OfertaLaboral(oferta);
                
                //agregafunciones segun el puesto de trabajo
                /*
                Funcion f = new Funcion();
                ICollection<Funcion> funciones = context.TablaFunciones.All().ToList();

                foreach (Funcion fun in funciones)
                {
                    if (fun.PuestoID == oferta.PuestoID)
                    {
                        f = fun;
                        o.ListaFuncionesPuesto.Add(f);
                    }
                }
                   */
                ID = context.TablaOfertaLaborales.AddElement(o);
            }
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(ID);
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, OfertaLaboralDTO oferta)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaOfertaLaborales.RemoveElementByID(oferta.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, OfertaLaboralDTO oferta)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(oferta.ID).LoadFromDTO(oferta);
                context.TablaOfertaLaborales.ModifyElement(o);
                

                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }


        public ActionResult GetViewOferta(int ofertaID)
        {
            
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral oferta = context.TablaOfertaLaborales.FindByID(ofertaID);
                ViewBag.yaValido = YaValido(oferta);
                ViewBag.responsable = oferta.Responsable.ToDTO();
                ViewBag.modoSolicitudOferta = oferta.ModoSolicitudOfertaLaboralID >= 1 ? oferta.ModoSolicitudOfertaLaboral.ToDTO() : new ModoSolicitudOfertaLaboralDTO();
                ViewBag.estadoSolicitudOferta = oferta.EstadoSolicitudOfertaLaboral.ToDTO();
                ViewBag.area = oferta.Area.ToDTO();
                ViewBag.puesto = oferta.Puesto.ToDTO();
                ViewBag.funciones = oferta.Puesto.Funciones.Select(c => c.ToDTO()).ToList();
                ViewBag.capacidades = oferta.Puesto.GetCapacidadesAsociadas(context).Select(c => c.ToDTO()).ToList();
               return PartialView("ViewSolicitudOfertaLaboral", oferta.ToDTO());
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CambiaEstadoSolicitudAprobada([DataSourceRequest] DataSourceRequest request, int OfertaID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(OfertaID);
                if (o.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Pendiente"))
                {
                    o.EstadoSolicitudOfertaLaboral = context.TablaEstadosSolicitudes.One(p=> p.Descripcion.Equals("Aprobado"));
                    o.FechaPublicacion = DateTime.Now.ToShortDateString();
                }
                context.TablaOfertaLaborales.ModifyElement(o);

                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }

        }

       [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CambiaEstadoSolicitudRechazada([DataSourceRequest] DataSourceRequest request, int OfertaID)
        {
            using (DP2Context context = new DP2Context())
            {
                OfertaLaboral o = context.TablaOfertaLaborales.FindByID(OfertaID);
                if (o.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Pendiente"))
                {
                    o.EstadoSolicitudOfertaLaboral = context.TablaEstadosSolicitudes.One(p=> p.Descripcion.Equals("Rechazado"));
                }
                context.TablaOfertaLaborales.ModifyElement(o);

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


       public bool YaValido(OfertaLaboral oferta)
       {
           return (!(oferta.EstadoSolicitudOfertaLaboral.Descripcion.Equals("Pendiente")));
       }


       public JsonResult _GetColaboradores(int areaID)
       {
           using (DP2Context context = new DP2Context())
           {
               AreaDTO areaRRHH = context.TablaAreas.All().Where(a => a.IsRRHH).FirstOrDefault().ToDTO();

               List<ColaboradorDTO> p = new List<ColaboradorDTO>();
               try
               {
                   p = context.TablaColaboradores.All().Select(m => m.ToDTO()).Where(n => n.AreaID == areaID || n.AreaID == areaRRHH.ID ).ToList();
               }
               catch (Exception) { }
               return Json(p, JsonRequestBehavior.AllowGet);
           }
       }
    
    }

     

}
