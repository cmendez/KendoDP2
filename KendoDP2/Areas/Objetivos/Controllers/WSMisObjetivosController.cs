using KendoDP2.Areas.Objetivos.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Objetivos.Controllers
{
    public class WSMisObjetivosController : Controller
    {

        public ActionResult GetAllMisObjetivos(int idUsuario, int idPeriodo)
        {
            using (DP2Context context = new DP2Context())
            {
                ColaboradorDTO col = context.TablaColaboradores.FindByID(idUsuario).ToDTO();
                int puestoID = col.PuestoID;
                Puesto puesto = context.TablaPuestos.FindByID(puestoID);
                ViewBag.periodos = context.TablaPeriodos.All().Select(c => c.ToDTO()).ToList();
                List<ObjetivoDTO> objetivos = puesto.Objetivos.Select(c => c.ToDTO(context)).ToList();
                List<ObjetivoDTO> ret = new List<ObjetivoDTO>();
                foreach(ObjetivoDTO objetivo in objetivos){
                    ret.AddRange(context.TablaObjetivos.Where(o => o.ObjetivoPadreID == objetivo.ID && o.PuestoAsignadoID == null).Select(o => o.ToDTO(context)));
                }
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
