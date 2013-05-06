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
                    Colaborador colaborador = context.TablaColaboradores.FindByID(Convert.ToInt32(id));
                    if (colaborador != null)
                    {
                        return Json(new
                        {
                            nombres = colaborador.Nombres,
                            apellidos = colaborador.ApellidoPaterno + " " + colaborador.ApellidoMaterno,
                            area = "",
                            puesto = "",
                            email = colaborador.CorreoElectronico,
                            anexo = colaborador.Telefono,
                            fecha_ingreso = colaborador.FechaIngresoEmpresa
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { mensaje = "Colaborador de " + id + " no existe" }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception)
            {
                return Json(new { mensaje = "Sucedio un error en el WS" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ColaboradoresToList()
        {
            using (DP2Context context = new DP2Context())
            {
                var colaboradores = context.TablaColaboradores.All().Select(a => a.ToDTO()).OrderBy(a => a.NombreCompleto);
                return Json(colaboradores, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
