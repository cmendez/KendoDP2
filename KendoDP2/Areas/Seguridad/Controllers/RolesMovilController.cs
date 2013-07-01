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
    public class RolesMovilController : Controller
    {
        //
        // GET: /Seguridad/RolesMovil/
        public RolesMovilController()
        {
            ViewBag.Area = "Seguridad";
        }

        public ActionResult Index(int IDMOVIL)
        {
            Session["CAMBIARROLESAUSUARIOSMOVIL"] = IDMOVIL;
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
                using (DP2Context context = new DP2Context())
                {
                    // Roles de un usuario
                    Usuario usuario = context.TablaUsuarios.One(u => u.ID == (int)Session["CAMBIARROLESAUSUARIOSMOVIL"]);
                    List<RolDTO> salida = new List<RolDTO>();
                    foreach (Rol r in usuario.Roles)
                    {
                        if (r.EsWeb == false)
                        {
                            RolDTO aux = new RolDTO(r);
                            salida.Add(aux);
                        }
                    }
                    return Json(salida.ToDataSourceResult(request));
                }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, RolDTO rol)
        {
            using (DP2Context context = new DP2Context())
            {
                    Usuario usuario = context.TablaUsuarios.One(u => u.ID == (int)Session["CAMBIARROLESAUSUARIOSMOVIL"]);
                    List<Rol> nuevo = new List<Rol>();
                    foreach(Rol r in usuario.Roles)
                    {
                        Rol aux = new Rol();
                        aux.ID = r.ID;
                        aux.Nombre = r.Nombre;
                        aux.IsEliminado = r.IsEliminado;
                        aux.Area = r.Area;
                        aux.EsWeb = r.EsWeb;

                        if(r.Nombre==rol.Nombre)
                        {
                            aux.Permiso = rol.Permiso;
                        }else
                        {
                            aux.Permiso = r.Permiso;
                        }
                        nuevo.Add(aux);
                    }
                    usuario.Roles = nuevo;
                    context.TablaUsuarios.ModifyElement(usuario);
                    return View("Index");
                    //return Json(new[] { usuario.Roles }.ToDataSourceResult(request, ModelState));
            }
        }
    }
}
