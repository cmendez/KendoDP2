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
                return Json(context.TablaObjetivos.Where(o => o.TipoObjetivoBSCID == tipoObjetivoBSCID && o.BSCID == BSCID).Select(o => o.ToDTO(context)), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllObjetivosEmpresa(int BSCID)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaObjetivos.Where(o => o.TipoObjetivoBSCID != null && o.BSCID == BSCID).Select(o => o.ToDTO(context)), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CrearObjetivoEmpresa(ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = new Objetivo(objetivo, context);
                context.TablaObjetivos.AddElement(o);
                return Json(new { success = true });
            }
        }

    }
}
