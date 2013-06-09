using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Areas.Reclutamiento.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System.Globalization;

namespace KendoDP2.Areas.Reclutamiento.Controllers
{
    public class ReclutamientoMobileController : Controller
    {
        //
        // GET: /Reclutamiento/ReclutamientoMobile/

        public ActionResult Index()
        {
            return View();
        }

        // GET: /Reclutamiento/ReclutamientoMobile/Colaborador?userName=admin

        public ActionResult Colaborador(string userName)
        {
            using (DP2Context context = new DP2Context()){
                //Solo se mostrarán aquellas ofertas aprobadas, internas y que aún esté vigente
                var fechaActual = DateTime.Now;
                var estado = context.TablaEstadosSolicitudes.One(a => a.Descripcion.Equals("Aprobado")).ID;
                var modo = context.TablaModosSolicitudes.One(a => a.Descripcion.Equals("Convocatoria Interna")).ID;
                var ofertas2 = context.TablaOfertaLaborales.Where(a=>a.EstadoSolicitudOfertaLaboralID == estado)
                    .Where(a=>a.ModoSolicitudOfertaLaboralID == modo)
                    .Where(a => DateTime.ParseExact(a.FechaFinVigenciaSolicitud, "dd/MM/yyyy", CultureInfo.CurrentCulture).CompareTo(fechaActual) >= 1)
                    .Select(p => p.ToMobilePostulanteDTO(userName)).ToList();
                return Json(ofertas2, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: /Reclutamiento/ReclutamientoMobile/Jefe?userName=admin
        public ActionResult Jefe(string userName)
        {
            using (DP2Context context = new DP2Context())
            {
                var ofertas1 = context.TablaOfertaLaborales.All().Select(p => p.ToMobileJefeDTO(userName)).ToList();
                var estado = context.TablaEstadosSolicitudes.One(a => a.Descripcion.Equals("Aprobado")).ID;
                //var ofertas2 = context.TablaOfertaLaborales.Where(a=>a.EstadoSolicitudOfertaLaboralID == estado).Select(p => p.ToMobilePostulanteDTO()).ToList();
                return Json(ofertas1, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
