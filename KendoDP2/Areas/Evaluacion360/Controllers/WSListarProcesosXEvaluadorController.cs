﻿using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class WSListarProcesosXEvaluadorController : Controller
    {
        public ActionResult Read(int idUsuario)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ProcesoEvaluacion> listaProceso = new ListarProcesosXEvaluadorController()._Read(idUsuario, context);
                return Json(listaProceso.Select(x => x.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

    }
}
