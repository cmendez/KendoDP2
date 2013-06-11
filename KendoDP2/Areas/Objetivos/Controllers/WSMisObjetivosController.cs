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

        private List<ObjetivoDTO> _GetAllMisObjetivos(int idUsuario, int idPeriodo, DP2Context context)
        {
            ColaboradorDTO col = context.TablaColaboradores.FindByID(idUsuario).ToDTO();
            int puestoID = col.PuestoID;
            Puesto puesto = context.TablaPuestos.FindByID(puestoID);
            List<ObjetivoDTO> objetivos = puesto.Objetivos.Select(c => c.ToDTO(context)).ToList();
            List<ObjetivoDTO> ret = new List<ObjetivoDTO>();
            foreach (ObjetivoDTO objetivo in objetivos)
            {
                ret.AddRange(context.TablaObjetivos.Where(o => o.ObjetivoPadreID == objetivo.ID && o.PuestoAsignadoID == null).Select(o => o.ToDTO(context)));
            }
            return ret.Where(x => x.BSCID == idPeriodo).ToList();
        }
        public ActionResult GetAllMisObjetivos(int idUsuario, int idPeriodo)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(_GetAllMisObjetivos(idUsuario, idPeriodo, context), JsonRequestBehavior.AllowGet);
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


        public ActionResult Destroy(int objetivoID)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    context.TablaObjetivos.RemoveElementByID(objetivoID);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult RegistrarAvance(int idObjetivo, int alcance, string descripcion)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    AvanceObjetivo a = new AvanceObjetivo { FechaCreacion = DateTime.Now.ToString("dd/MM/yyyy"), Comentario = descripcion, ObjetivoID = idObjetivo, Valor = alcance };
                    context.TablaAvanceObjetivo.AddElement(a);
                    Objetivo o = context.TablaObjetivos.FindByID(idObjetivo);
                    o.AvanceFinal = a.Valor;
                    context.TablaObjetivos.ModifyElement(o);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

            }

        }
    }

}
