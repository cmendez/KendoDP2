using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Areas.Reclutamiento.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

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
                var ofertas1 = context.TablaOfertaLaborales.All().Select(p => p.ToMobilePostulanteDTO(userName)).ToList();
                var estado = context.TablaEstadosSolicitudes.One(a => a.Descripcion.Equals("Aprobado")).ID;
                //var ofertas2 = context.TablaOfertaLaborales.Where(a=>a.EstadoSolicitudOfertaLaboralID == estado).Select(p => p.ToMobilePostulanteDTO()).ToList();
                return Json(ofertas1, JsonRequestBehavior.AllowGet);
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
