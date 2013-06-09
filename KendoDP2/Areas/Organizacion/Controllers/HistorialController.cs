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
                ViewBag.areas = context.TablaAreas.All().Select(p => p.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                return View();
            }

        }

        
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ColaboradorXPuestoDTO> salida = new List<ColaboradorXPuestoDTO>();
                List<ColaboradorXPuesto> colaboradores_X_Puesto = context.TablaColaboradoresXPuestos.All().OrderBy(x => x.ID).ToList();
                foreach (ColaboradorXPuesto c in colaboradores_X_Puesto)
                {
                    ColaboradorXPuestoDTO aux = new ColaboradorXPuestoDTO(c);
                    aux.FechaIngresoPuesto = new DateTime();
                    aux.FechaSalidaPuesto = new DateTime();
                    aux.Sueldo = 0;
                    aux.Comentarios = "";
                    salida.Add(aux);
                }

                return Json(salida.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, ColaboradorXPuestoDTO cxp)
        {
            ColaboradorXPuestoDTO cc= new ColaboradorXPuestoDTO();
            TryUpdateModel(cc);
            using (DP2Context context = new DP2Context())
            {
                Colaborador colab = context.TablaColaboradores.One(CL => CL.ID == cxp.Colaborador.ID);
                ColaboradorXPuesto C = new ColaboradorXPuesto();
                C.Colaborador = context.TablaColaboradores.One(CL => CL.ID == cxp.Colaborador.ID);
                C.ColaboradorID = cxp.Colaborador.ID;
                C.Comentarios = cxp.Comentarios;
                C.FechaIngresoPuesto = cxp.FechaIngresoPuesto;
                C.FechaSalidaPuesto = cxp.FechaSalidaPuesto;
                C.Sueldo = cxp.Sueldo;
                C.Puesto = context.TablaPuestos.One(CL => CL.ID == cxp.PuestoID);
                C.PuestoID = cxp.PuestoID;

                colab.ColaboradoresPuesto.Add(C);
                context.TablaColaboradoresXPuestos.AddElement(C);
                context.TablaColaboradores.ModifyElement(colab);
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

        public JsonResult _GetArea()
        {
            using (DP2Context context = new DP2Context())
            {
                List<AreaDTO> p = new List<AreaDTO>();
                try
                {
                    p = context.TablaAreas.All().Select(i => i.ToDTO()).ToList();
                }
                catch (Exception) { }
                return Json(p, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult _GetPuestos(int areaID)
        {
            using (DP2Context context = new DP2Context())
            {
                List<Puesto> p = new List<Puesto>();
                try
                {
                    p = context.TablaPuestos.Where(i=>i.AreaID==areaID).ToList();
                }
                catch (Exception) { }
                return Json(p.Select(x => x.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
