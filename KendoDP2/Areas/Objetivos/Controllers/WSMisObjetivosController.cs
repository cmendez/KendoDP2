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
                List<ObjetivoDTO> objetivos = puesto.Objetivos.Select(c => c.ToDTO(context)).ToList();
                List<ObjetivoDTO> ret = new List<ObjetivoDTO>();
                foreach(ObjetivoDTO objetivo in objetivos){
                    ret.AddRange(context.TablaObjetivos.Where(o => o.ObjetivoPadreID == objetivo.ID && o.PuestoAsignadoID == null).Select(o => o.ToDTO(context)));
                }
                return Json(ret.Where(x => x.BSCID == idPeriodo).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllMisObjetivosSuperiores(int idUsuario, int idPeriodo)
        {
            using (DP2Context context = new DP2Context())
            {
                ColaboradorDTO col = context.TablaColaboradores.FindByID(idUsuario).ToDTO();
                int puestoID = col.PuestoID;
                Puesto puesto = context.TablaPuestos.FindByID(puestoID);
                List<ObjetivoDTO> objetivos = puesto.Objetivos.Select(c => c.ToDTO(context)).ToList();
                return Json(objetivos.Where(x => x.BSCID == idPeriodo).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        /*
         * Nombre
         * Peso
         * ObjetivoPadreID : debe ser un objetivo obtenido por una llamada a GetAllMisObjetivosSuperiores 
         */
        public ActionResult Create(ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = new Objetivo(objetivo, context);
                context.TablaObjetivos.AddElement(o);
                return Json(new { idObjetivo = o.ID } , JsonRequestBehavior.AllowGet);
            }
        }

        /*
         * ID
         * Nombre
         * Peso
         * ObjetivoPadreID : debe ser un objetivo obtenido por una llamada a GetAllMisObjetivosSuperiores 
         */
        public ActionResult Update(ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = context.TablaObjetivos.FindByID(objetivo.ID).LoadFromDTO(objetivo, context);
                context.TablaObjetivos.ModifyElement(o);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
