using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;


namespace KendoDP2.Areas.Seguridad.Controllers
{
    public class CrearUsuarioController : Controller
    {
        //
        // GET: /Seguridad/CrearUsuario/
        public CrearUsuarioController()
        {
            ViewBag.Area = "Seguridad";
        }
        public ActionResult Index()
        {
            return View();
        }

        private List<UsuarioDTO> ObtenerUsuarios()
        {
            using (DP2Context context = new DP2Context())
            {
                var query = from usuarios in context.TablaUsuarios.All()
                            where !(from o in context.TablaColaboradores.All()
                                    select o.Username)
                                .Contains(usuarios.Username)
                            &&
                            !(from o in context.TablaPostulante.All()
                              select o.Username)
                                .Contains(usuarios.Username)
                            select usuarios;
                List<UsuarioDTO> salida = new List<UsuarioDTO>();

                foreach (Usuario US in query)
                {
                    UsuarioDTO aux = new UsuarioDTO();
                    aux.Password = US.ToDTO().Password;
                    aux.Username = US.ToDTO().Username;
                    aux.ID = US.ToDTO().ID;
                    aux.IsEliminado = US.ToDTO().IsEliminado;
                    aux.Roles = US.ToDTO().Roles;
                    salida.Add(aux);
                }
                return salida;
            }
        }

        public ActionResult _Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(ObtenerUsuarios().Where(c => c.Username != "admin").Distinct().ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _Create([DataSourceRequest] DataSourceRequest request, UsuarioDTO usuario)
        {
            using (DP2Context context = new DP2Context())
            {
                Usuario u = new Usuario(usuario);
                u.Username = usuario.Username;
                u.Password = usuario.Password;
                u.Roles = new List<Rol>();
                u.Roles = context.SeedRolesAdmin();
                context.TablaUsuarios.AddElement(u);
                return View("Index");
            }
        }
        

    }
}
