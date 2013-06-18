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
    public class WSObjetivosSubordinadosController : Controller
    {

        public ActionResult ListarObjetivosDeSubordinados(int idColaborador, int idPeriodo)
        {
            using (DP2Context context = new DP2Context())
            {
                int puestoID = context.TablaColaboradores.FindByID(idColaborador).ToDTO().PuestoID;
                Puesto puesto = context.TablaPuestos.FindByID(puestoID);
                List<Objetivo> objetivosPuesto = puesto.Objetivos.Where(x => x.GetBSCIDRaiz(context) == idPeriodo).ToList();
                List<Objetivo> objetivosHijos = new List<Objetivo>();
                objetivosPuesto.ForEach(x => objetivosHijos.AddRange(x.ObjetivosHijos(context)));
                List<Objetivo> objetivos = new List<Objetivo>();
                objetivosHijos.ForEach(x => objetivos.AddRange(x.ObjetivosHijos(context).Where(a => a.IsObjetivoIntermedio).ToList()));
                return Json(objetivos.Select(c => c.ToDTO(context)).ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        
        /*
         *   Nombre
         *   Peso
         *   ObjetivoPadreID : debe ser un objetivo obtenido al leer mis objetivos
         */
        public ActionResult Create(ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = new Objetivo(objetivo, context);
                o.IsObjetivoIntermedio = true;
                context.TablaObjetivos.AddElement(o);
                Objetivo padre1 = context.TablaObjetivos.FindByID(o.ObjetivoPadreID);
                Objetivo padre2 = context.TablaObjetivos.FindByID(padre1.ObjetivoPadreID);
                Puesto puesto = context.TablaPuestos.FindByID(padre2.PuestoAsignadoID.GetValueOrDefault());
                puesto.ReparteObjetivosASubordinados(context);
                return Json(new { success = true ,ID= o.ID }, JsonRequestBehavior.AllowGet);
            }
        }

        /*
         *   ID
         *   Nombre
         *   Peso
         *   ObjetivoPadreID : debe ser un objetivo obtenido al leer mis objetivos
         */
        public ActionResult Update(ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = context.TablaObjetivos.FindByID(objetivo.ID).LoadFromDTO(objetivo, context);
                context.TablaObjetivos.ModifyElement(o);
                foreach (var o2 in o.ObjetivosHijos(context))
                {
                    o2.Nombre = o.Nombre;
                    context.TablaObjetivos.ModifyElement(o2);
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Destroy(int objetivoID)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    context.TablaObjetivos.RemoveElementByID(objetivoID, true);
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