using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;
using Kendo.Mvc.Extensions;
using KendoDP2.Models.Seguridad;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Seguridad.Controllers
{
    public class UsuariosMovilController : Controller
    {
        //
        // GET: /Seguridad/UsuariosMovil/

        public UsuariosMovilController()
        {
            ViewBag.Area = "Seguridad";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<UsuarioDTO> salida = new List<UsuarioDTO>();
                foreach (Usuario dto in context.TablaUsuarios.Where(p => ((p.Username != null) && (p.Username != User.Identity.Name)), true))
                {
                    if (dto.Username != "admin")
                    {
                        UsuarioDTO u = new UsuarioDTO(dto);
                        salida.Add(u);
                    }
                }
                return Json(salida.ToDataSourceResult(request));
            }
        }

        public ActionResult VerRoles([DataSourceRequest] DataSourceRequest request, int ID)
        {
            using (DP2Context context = new DP2Context())
            {
                Session["Usuario_Roles"] = context.TablaUsuarios.One(i => i.ID == ID);
            }
            return Redirect("../../RolesMovil/UsuarioRoles");
        }


    }
}
