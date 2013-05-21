using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;
using System;
using System.Collections.Generic;
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

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
                ViewBag.ColaboradorDTO = context.TablaColaboradores.FindByID(ColaboradorID).ToDTO();
                ViewBag.tipoDocumentos = context.TablaTiposDocumentos.All().Select(c => c.ToDTO()).ToList();
                ViewBag.gradoAcademico = context.TablaGradosAcademicos.All().Select(c => c.ToDTO()).ToList();
                ViewBag.estadosColaborador = context.TablaEstadosColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.pais = context.TablaPaises.All().Select(c => c.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(c => c.ToDTO()).ToList();
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
                


                //
                context.TablaColaboradores.ModifyElement(c);
                return Json(new { success = true });
            }
        }
    }
}
