using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    public class WSColaboradorController : Controller
    {
        //
        // GET: /Organizacion/WSColaborador/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getColaborador(string id)
        {
            try
            {
                using (DP2Context context = new DP2Context())
                {
                    ColaboradorDTO colaborador = context.TablaColaboradores.FindByID(Convert.ToInt32(id)).ToDTO();
                    PuestoDTO puesto = colaborador.PuestoID == 0 ? new PuestoDTO() : context.TablaPuestos.FindByID(colaborador.PuestoID).ToDTO();
                    AreaDTO area = colaborador.AreaID == 0 ? new AreaDTO() : context.TablaAreas.FindByID(colaborador.AreaID).ToDTO();
                    return Json(new
                    {
                        sucess = true,
                        data = new
                        {
                            colaborador = colaborador,
                            puesto = puesto,
                            area = area
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new 
                { 
                    sucess = false,
                    message = "Error: " + ex.Message 
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ColaboradoresToList()
        {
            try
            {
                using (DP2Context context = new DP2Context())
                {
                    List<ColaboradorDTO> colaboradores = context.TablaColaboradores.All().Select(a => a.ToDTO()).OrderBy(a => a.NombreCompleto).ToList();
                    return Json(new
                    {
                        sucess = true,
                        data = colaboradores
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    sucess = false,
                    message = "Error: " + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
