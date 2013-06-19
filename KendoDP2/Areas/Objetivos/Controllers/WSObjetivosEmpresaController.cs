using KendoDP2.Areas.Objetivos.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Objetivos.Controllers
{
    public class WSObjetivosEmpresaController : Controller
    {
        public ActionResult ListarObjetivosEmpresa(int tipoObjetivoBSCID, int BSCID)
        {
            using (DP2Context context = new DP2Context())
            {
                var objetivos = context.TablaObjetivos.Where(o => o.TipoObjetivoBSCID == tipoObjetivoBSCID && o.BSCID == BSCID);
                var res = objetivos.Select(c => c.ToDTO(context)).ToList();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllObjetivosEmpresa(int BSCID)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaObjetivos.Where(o => o.TipoObjetivoBSCID != null && o.BSCID == BSCID).Select(o => o.ToDTO(context)).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        /*
            Nombre:Owo
            Peso:50
            TipoObjetivoBSCID:1
            BSCID:1
        */
         public ActionResult CrearObjetivoEmpresa(ObjetivoDTO objetivo)
         {
             using (DP2Context context = new DP2Context())
             {
                 Objetivo o = new Objetivo(objetivo, context);
                 context.TablaObjetivos.AddElement(o);
                return Json(new { success = true, ID = o.ID }, JsonRequestBehavior.AllowGet);
            }
        }

        /*
            ID = 10
            Nombre:Owo
            Peso:50
            TipoObjetivoBSCID:1
            BSCID:1
        */
        public ActionResult UpdateObjetivoEmpresa(ObjetivoDTO objetivo)
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
                context.TablaObjetivos.RemoveElementByID(objetivoID);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
