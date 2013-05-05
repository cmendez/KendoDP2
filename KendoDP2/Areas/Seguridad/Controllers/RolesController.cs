using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;

namespace KendoDP2.Areas.Seguridad.Controllers
{
    [Authorize()]
    public class RolesController : Controller
    {
        public RolesController()
        {
            ViewBag.Area = "Seguridad";
        }

        public ActionResult Index()
        {
            using(DP2Context context = new DP2Context())
            {
                ViewData["Usuario"] = null;
                ViewBag.Menu = context.TablaRoles.Where(u => u.Secuencia < 10).Select(u => u.ToDTO()).ToList();
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(FormCollection values)
        {
            // me reconoce el usuario al cual quiero darle permisos
            using (DP2Context context = new DP2Context())
            {
                Usuario usuario = new Usuario();
                usuario = context.TablaUsuarios.Where(u => u.ID == Convert.ToInt16(values["Usuarios"])).FirstOrDefault();
                ViewData["Usuario"] = usuario;
                ViewBag.Menu = context.TablaRoles.Where(u => u.Secuencia < 10).Select(u => u.ToDTO()).ToList();
            }
            return View();
        }

        public ActionResult GetUsuarios()
        {
            // me retorna los usuarios que estan en el sistema
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaUsuarios.All().Select(o => o.ToDTO()),JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, int RolID)
        {
            using (DP2Context context = new DP2Context())
            {
                Usuario u= (Usuario)ViewData["Usuario"]; // en base al usuario seleccionado, acceder a sus roles
                List<RolDTO> rol_Usuario = new List<RolDTO>();

                //List<RolDTO> Rol_Usuario=context.TablaRoles.Where()
                foreach(Rol r in u.Roles)
                {
                    RolDTO s = new RolDTO(r);
                    rol_Usuario.Add(s);
                }
                // para entender esta validacion vasta con ver la barra de navegacion
                List<RolDTO> x = rol_Usuario.Where(t => t.Secuencia > RolID * 10).Where(t => t.Secuencia < (RolID + 1) * 10).ToList();
                //un usuario es creado con todos los roles pero descativados, la idea es activarlos cuando se actualice cada row del grid
                return Json(x.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, RolDTO rol)
        {
            using (DP2Context context = new DP2Context())
            {
                // la idea es actualizar cada rol del usuario seleccionado
                Rol c = context.TablaRoles.FindByID(rol.ID,false).LoadFromDTO(rol);
                context.TablaRoles.ModifyElement(c);
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, RolDTO rol)
        {
            using (DP2Context context = new DP2Context())
            {
                // desactivar los persmisos pero no se va a borrar datos solo se desactivan esta de mas
                context.TablaRoles.RemoveElementByID(rol.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }


    }
}
