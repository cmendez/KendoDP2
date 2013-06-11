using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;
using KendoDP2.Areas.Seguridad.Models;

namespace KendoDP2.Areas.Seguridad.Controllers
{
    [Authorize()]
    public class RolesController : Controller
    {
        public RolesController()
        {
            ViewBag.Area = "Seguridad";
        }

        public ActionResult Index(int? ID)
        {
            if (ID == null)
            {
                Session["CAMBIARROLESAUSUARIOS"] = ID;
            }
            else
            {
                Session["CAMBIARROLESAUSUARIOS"] = ID;
            }
                using (DP2Context context = new DP2Context())
                {
                    List<MenuArea> AreasMenu = new List<MenuArea>();
                    int c = 0;
                    foreach (var s in context.TablaRoles.All().Select(i => i.Area).Distinct().ToArray())
                    {
                        c++;
                        AreasMenu.Add(new MenuArea(c, s));
                    }
                    ViewBag.Areas = AreasMenu;
                    return View();
                }
            
            
        }


        public ActionResult Read([DataSourceRequest] DataSourceRequest request, string areaNombre = "")
        {
            if (Session["CAMBIARROLESAUSUARIOS"]==null)
            {
                using (DP2Context context = new DP2Context())
                {
                    // Roles en general
                    var x = Json(context.TablaRoles.Where(p => p.Area == areaNombre).Select(p => p.ToDTO()).Distinct().ToDataSourceResult(request));
                    return x;
                }    
            }
            else
            {
                using (DP2Context context = new DP2Context())
                {
                    // Roles de un usuario
                    Usuario usuario= context.TablaUsuarios.One(u => u.ID == (int)Session["CAMBIARROLESAUSUARIOS"]);
                    var roles = usuario.Roles.Where(r => r.Area == areaNombre).Select(r=>r.ToDTO());

                    var x = Json(roles.ToDataSourceResult(request));
                    return x;
                }
            }
            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, RolDTO rol)
        {
            using (DP2Context context = new DP2Context())
            {
                Rol r = new Rol(rol.Nombre, rol.Area);
                context.TablaRoles.AddElement(r);
                return Json(new[] { r.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, RolDTO rol)
        {
            using (DP2Context context = new DP2Context())
            {
                Rol c = context.TablaRoles.FindByID(rol.ID).LoadFromDTO(rol);
                context.TablaRoles.ModifyElement(c);
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, RolDTO rol)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaRoles.RemoveElementByID(rol.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }

        DP2Context context = new DP2Context();

        public ActionResult Read_U([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                var usuario = (Usuario)Session["Usuario_Roles"];
                return Json(usuario.Roles, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update_U([DataSourceRequest] DataSourceRequest request, RolDTO rol)
        {
            using (DP2Context context = new DP2Context())
            {
                Rol c = context.TablaRoles.FindByID(rol.ID).LoadFromDTO(rol);
                context.TablaRoles.ModifyElement(c);
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }
    }
}
