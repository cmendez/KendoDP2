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
            Session["CAMBIARROLESAUSUARIOS"] = ID;
            return View();  
        }

        

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (Session["CAMBIARROLESAUSUARIOS"] == null)
            {
                    // Roles en general
                using (DP2Context context = new DP2Context())
                {
                    return Json(context.TablaRoles.All(true).Select(r => r.ToDTO()).ToDataSourceResult(request));
                }
            }
            else
            {
                using (DP2Context context = new DP2Context())
                {
                    // Roles de un usuario
                    Usuario usuario = context.TablaUsuarios.One(u => u.ID == (int)Session["CAMBIARROLESAUSUARIOS"], true);
                    List<RolDTO> salida = new List<RolDTO>();
                    foreach (Rol r in usuario.Roles)
                    {
                        RolDTO aux = new RolDTO(r);
                        salida.Add(aux);
                    }
                    return Json(salida.ToDataSourceResult(request));
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, RolDTO rol)
        {
            using (DP2Context context = new DP2Context())
            {
                if (Session["CAMBIARROLESAUSUARIOS"]==null)
                {
                    Rol r = context.TablaRoles.One(o => o.ID == rol.ID,true);
                    r.IsEliminado = rol.IsEliminado;
                    context.TablaRoles.ModifyElement(r);

                    foreach(Usuario u in context.TablaUsuarios.All(true))
                    {
                        //si elimino un rol entonces a todos mis colaboradores se le desactiva el rol
                        if(rol.IsEliminado==true && u.Username != null)
                        {
                            u.Roles.Where(i => i.ID == rol.ID).FirstOrDefault().Permiso = false;
                            context.TablaUsuarios.ModifyElement(u);
                        }

                    }
                    return View("Index");
                    //return Json(context.TablaRoles.All(true).Select(er => er.ToDTO()).ToDataSourceResult(request));

                }else{
                    Usuario usuario = context.TablaUsuarios.One(u => u.ID == (int)Session["CAMBIARROLESAUSUARIOS"], true);
                    List<Rol> nuevo = new List<Rol>();
                    foreach(Rol r in usuario.Roles)
                    {
                        Rol aux = new Rol();
                        aux.ID = r.ID;
                        aux.Nombre = r.Nombre;
                        aux.IsEliminado = r.IsEliminado;
                        aux.Area = r.Area;
                        aux.Usuarios = new List<Usuario>();
                            foreach(UsuarioDTO u in r.Usuarios.Select(EE=>EE.ToDTO()))
                            {
                                aux.Usuarios.Add(new Usuario(u));
                            }
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
}
