using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;
using System.IO;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    public class HistorialController : Controller
    {
        public HistorialController()
            : base()
        {
            ViewBag.Area = "Historial";
        }

        //
        // GET: /Organizacion/Historial/

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoDocumentos = context.TablaTiposDocumentos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.estadosColaborador = context.TablaEstadosColaboradores.All().Select(p => p.ToDTO()).ToList();
                ViewBag.pais = context.TablaPaises.All().Select(p => p.ToDTO()).ToList();
                ViewBag.gradoAcademico = context.TablaGradosAcademicos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(p => p.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                return View();
            }

        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ColaboradorDTO> colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).OrderBy(x => x.ID).ToList();
                return Json(colaboradores.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, FormCollection frm)
        {
            ColaboradorDTO colaborador = new ColaboradorDTO();

            using (DP2Context context = new DP2Context())
            {
                ColaboradorXPuesto cxp = new ColaboradorXPuesto();
                cxp.Colaborador=context.TablaColaboradores.One(z=>z.ID==Convert.ToInt16(frm[4]));
                cxp.ColaboradorID = Convert.ToInt16(frm[4]);
                cxp.Comentarios = "";
                cxp.FechaIngresoPuesto = new DateTime(Convert.ToInt16(frm["FechaIngresoPuesto"].Substring(0, 4)), Convert.ToInt16(frm["FechaIngresoPuesto"].Substring(5, 2)), Convert.ToInt16(frm["FechaIngresoPuesto"].Substring(8, 2)));
                cxp.FechaSalidaPuesto = new DateTime(Convert.ToInt16(frm["FechaSalidaPuesto"].Substring(0, 4)), Convert.ToInt16(frm["FechaSalidaPuesto"].Substring(5, 2)), Convert.ToInt16(frm["FechaSalidaPuesto"].Substring(8, 2)));
                cxp.Puesto = context.TablaPuestos.One(i => i.ID == Convert.ToInt16(frm["PuestoID"]));
                cxp.PuestoID = Convert.ToInt32(frm["PuestoID"]);
                cxp.Sueldo = Convert.ToInt32(frm["sueldo"]);

                context.TablaColaboradoresXPuestos.AddElement(cxp);

                return Json(context.TablaColaboradores.All().Select(i=>i.ToDTO()).ToDataSourceResult(request, ModelState));
            }
        }


        public ActionResult Linea(int ID)
        {
            using (DP2Context context = new DP2Context())
            {
                ColaboradorDTO C = context.TablaColaboradores.One(x=>x.ID==ID).ToDTO();
                List<ColaboradorXPuesto> CXP = context.TablaColaboradoresXPuestos.Where(X => X.ColaboradorID == C.ID).ToList();
                List<ColaboradorXPuesto> salida = new List<ColaboradorXPuesto>();
                foreach(ColaboradorXPuesto x in CXP)
                {
                    ColaboradorXPuesto aux = new ColaboradorXPuesto();
                    aux = x;
                    aux.Puesto = context.TablaPuestos.One(p => p.ID == x.PuestoID);
                    aux.Puesto.Area = context.TablaAreas.One(a => a.ID == aux.Puesto.AreaID);
                    salida.Add(aux);
                }
                ViewBag.ColaboradorXPuesto = salida; ;
                ViewBag.ColaboradorDTO = C;
                return View();
            }
        }

        public JsonResult _GetPuestos(int areaID)
        {
            using (DP2Context context = new DP2Context())
            {
                List<Puesto> p = new List<Puesto>();
                try
                {
                    p = context.TablaAreas.FindByID(areaID).Puestos.ToList();
                }
                catch (Exception) { }
                return Json(p.Select(x => x.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }



        //public ActionResult Detalles()
        //{
        //    using (DP2Context context = new DP2Context())
        //    {
        //        ColaboradorDTO DTO = context.TablaColaboradores.One(C => C.Username.Equals(User.Identity.Name)).ToDTO();
        //        ViewBag.ColaboradorDTO = DTO;
        //        ViewBag.Historial = context.TablaColaboradoresXPuestos.One(C => C.ColaboradorID == DTO.ID);
        //        return View();
        //    }
        //}

    }
}
