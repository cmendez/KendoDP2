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
                List<Objetivo> objetivos = new List<Objetivo>();
                objetivosPuesto.ForEach(x => objetivos.AddRange(x.ObjetivosHijos.ToList()));
                return Json(objetivos.Select(c => c.ToDTO(context)).ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
