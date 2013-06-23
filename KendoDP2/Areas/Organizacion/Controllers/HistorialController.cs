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
            ViewBag.Area = "Organizacion";
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
        private List<ColaboradorXPuestoDTO> ObtenerUltimosPuestos()
        {
            List<ColaboradorXPuestoDTO> SALIDA = new List<ColaboradorXPuestoDTO>();
            using (DP2Context context = new DP2Context())
            {
                foreach (Colaborador c in context.TablaColaboradores.All())
                {
                    if (c.ID != 1)
                    {
                        int maximoID = (from cxp in context.TablaColaboradoresXPuestos.All()
                                        where cxp.ColaboradorID == c.ID
                                        select cxp.ID).Max();
                        ColaboradorXPuestoDTO SAL = new ColaboradorXPuestoDTO();
                        SAL = context.TablaColaboradoresXPuestos.Where(cc => (cc.ColaboradorID == c.ID && cc.ID == maximoID)).Select(D => D.ToDTO()).FirstOrDefault();
                        SALIDA.Add(SAL);                        
                    }

                }
            }
            return SALIDA;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<ColaboradorXPuestoDTO> SALIDA = new List<ColaboradorXPuestoDTO>();
            SALIDA = ObtenerUltimosPuestos();               
            return Json(SALIDA.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, ColaboradorXPuestoDTO cxp)
        {
            List<ColaboradorXPuestoDTO> SALIDA = new List<ColaboradorXPuestoDTO>();

            try
            {
                using (DP2Context context = new DP2Context())
                {
                    if ((cxp.AgregarPuesto) && (!cxp.ModificarPuesto))
                    {
                        ColaboradorXPuesto cc = context.TablaColaboradoresXPuestos.One(o => o.ID == cxp.ID);
                        ColaboradorXPuesto nuevo = new ColaboradorXPuesto();
                        nuevo.LoadFromDTO(cxp);
                        if (cxp.FechaIngresoPuesto != null)
                        {
                            nuevo.FechaIngresoPuesto = DateTime.Parse(cxp.FechaIngresoPuesto);
                        }

                        if ((cxp.FechaSalidaPuesto != null) && (!cxp.ContratoIndefinido))
                        {
                            nuevo.FechaSalidaPuesto = DateTime.Parse(cxp.FechaSalidaPuesto);
                        }

                        if ((!cxp.ContratoIndefinido))
                        {
                            if ((DateTime.Parse(cxp.FechaSalidaPuesto).Year * 10000 + DateTime.Parse(cxp.FechaSalidaPuesto).Month * 100 + DateTime.Parse(cxp.FechaSalidaPuesto).Day) - (DateTime.Parse(cxp.FechaIngresoPuesto).Year * 10000 + DateTime.Parse(cxp.FechaIngresoPuesto).Month * 100 + DateTime.Parse(cxp.FechaIngresoPuesto).Day) <= 0)
                            {
                                SALIDA = new List<ColaboradorXPuestoDTO>();
                                SALIDA = ObtenerUltimosPuestos();
                                return Json(SALIDA.ToDataSourceResult(request));
                            }
                        }
                        nuevo.Colaborador = cc.Colaborador;
                        nuevo.Puesto = context.TablaPuestos.One(i => i.ID == cxp.PuestoID);
                        if (cc.FechaSalidaPuesto == null)
                        {
                            cc.FechaSalidaPuesto = DateTime.Now;
                            context.TablaColaboradoresXPuestos.ModifyElement(cc);
                        }
                        context.TablaColaboradoresXPuestos.AddElement(nuevo);
                    }
                    else
                    {
                        if ((!cxp.AgregarPuesto) && (cxp.ModificarPuesto))
                        {
                            ColaboradorXPuesto cc = context.TablaColaboradoresXPuestos.One(o => o.ID == cxp.ID);
                            ColaboradorXPuesto actualizado = new ColaboradorXPuesto();
                            actualizado.LoadFromDTO(cxp);

                            if (cxp.FechaIngresoPuesto != null)
                            {
                                actualizado.FechaIngresoPuesto = DateTime.Parse(cxp.FechaIngresoPuesto);
                            }

                            if ((cxp.FechaSalidaPuesto != null) && (!cxp.ContratoIndefinido))
                            {
                                actualizado.FechaSalidaPuesto = DateTime.Parse(cxp.FechaSalidaPuesto);
                            }

                            if ((!cxp.ContratoIndefinido))
                            {
                                if ((DateTime.Parse(cxp.FechaSalidaPuesto).Year * 10000 + DateTime.Parse(cxp.FechaSalidaPuesto).Month * 100 + DateTime.Parse(cxp.FechaSalidaPuesto).Day) - (DateTime.Parse(cxp.FechaIngresoPuesto).Year * 10000 + DateTime.Parse(cxp.FechaIngresoPuesto).Month * 100 + DateTime.Parse(cxp.FechaIngresoPuesto).Day) <= 0)
                                {
                                    SALIDA = new List<ColaboradorXPuestoDTO>();
                                    SALIDA = ObtenerUltimosPuestos();
                                    return Json(SALIDA.ToDataSourceResult(request));
                                }
                            }
                            actualizado.Puesto = cc.Puesto;
                            actualizado.Colaborador = cc.Colaborador;
                            context.TablaColaboradoresXPuestos.RemoveElementByID(cc.ID);
                            context.TablaColaboradoresXPuestos.AddElement(actualizado);
                        }
                    }
                    SALIDA = ObtenerUltimosPuestos();
                    return Json(SALIDA.ToDataSourceResult(request));
                }
            }
            catch (Exception) {
                SALIDA = ObtenerUltimosPuestos();
                return Json(SALIDA.ToDataSourceResult(request));
            }
            }

        public ActionResult Linea(int ID)
        {
            using (DP2Context context = new DP2Context())
            {
                ColaboradorDTO C = context.TablaColaboradores.One(x=>x.ID==ID).ToDTO();
                C.Puesto = ObtenerUltimosPuestos().Where(ccc => ccc.Colaborador.ID == C.ID).FirstOrDefault().Puesto.Nombre;
                C.Sueldo = ObtenerUltimosPuestos().Where(ccc => ccc.Colaborador.ID == C.ID).FirstOrDefault().Sueldo;
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
                ViewBag.ColaboradorXPuesto = salida;
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
                    var query =
                                from puestos in context.TablaPuestos.Where(i=>i.AreaID==areaID).ToList()
                                where !(from o in ObtenerUltimosPuestos()
                                select o.PuestoID)
                                    .Contains(puestos.ID)
                                select puestos;

                    foreach(var q in query)
                    {
                        p.Add(q);
                    }
                }
                catch (Exception) { }
                return Json(p.Select(x => x.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
