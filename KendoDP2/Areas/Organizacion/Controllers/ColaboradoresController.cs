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
using System.IO;

namespace KendoDP2.Areas.Organizacion.Controllers
{
    [Authorize()]
    public class ColaboradoresController : Controller
    {
        public ColaboradoresController()
        {
            ViewBag.Area = "Organizacion";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoDocumentos = context.TablaTiposDocumentos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.estadosColaborador = context.TablaEstadosColaboradores.All().Select(p => p.ToDTO()).ToList();
                ViewBag.pais = context.TablaPaises.All().Select(p => p.ToDTO()).ToList();
                ViewBag.gradoAcademico = context.TablaGradosAcademicos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(p => p.ToDTO()).ToList();
                ViewBag.puestos = context.TablaPuestos.All().Select(p => p.ToDTO()).ToList();
                return View();
            }

        }

        // Grid periodos
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ColaboradorDTO> colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).OrderBy(x => x.ID).ToList();
                return Json(colaboradores.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ColaboradorDTO colaborador)
        {
            using (DP2Context context = new DP2Context())
            {
                //en caso no funcione solo se sacan los if's y se deja tal como esta
                    Colaborador c = new Colaborador(colaborador);
                    if (ValidaColaboradores(c.TipoDocumentoID, c.NumeroDocumento) == 0)
                    {
                        c.EstadoColaborador = context.TablaEstadosColaboradores.One(x => x.Descripcion.Equals("Contratado"));
                        context.TablaColaboradores.AddElement(c);

                        Puesto p = context.TablaPuestos.FindByID(colaborador.PuestoID);
                        ColaboradorXPuesto cruce = new ColaboradorXPuesto { ColaboradorID = c.ID, PuestoID = p.ID, Sueldo = colaborador.Sueldo };

                        context.TablaColaboradoresXPuestos.AddElement(cruce);

                        return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
                    }
                    else
                    {
                        return Json(new { ok = false, error = "Error!!!!!! PELIGRO!!" },JsonRequestBehavior.AllowGet);
                        
                    }
                }
                     
            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, ColaboradorDTO colaborador)
        {
            using (DP2Context context = new DP2Context())
            {
                Colaborador c = context.TablaColaboradores.FindByID(colaborador.ID).LoadFromDTO(colaborador);
                context.TablaColaboradores.ModifyElement(c);
                ColaboradorDTO colaboradorBD = c.ToDTO(); // lee el ultimo puesto de la bd
                // crea un nuevo puesto en la tabla de cruce si algo cambio
                if (colaboradorBD.PuestoID != colaborador.PuestoID || colaboradorBD.Sueldo != colaborador.Sueldo)
                {
                    Puesto p = context.TablaPuestos.FindByID(colaborador.PuestoID);
                    ColaboradorXPuesto cruce = new ColaboradorXPuesto { ColaboradorID = c.ID, PuestoID = p.ID, Sueldo = colaborador.Sueldo };

                    context.TablaColaboradoresXPuestos.AddElement(cruce);
                }
                
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, ColaboradorDTO colaborador)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaColaboradores.RemoveElementByID(colaborador.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }

        public JsonResult _GetPuestos(int areaID)
        {
            using (DP2Context context = new DP2Context())
            {
                List<Puesto> p = new List<Puesto>();
                try
                {
                    p = context.TablaAreas.FindByID(areaID).Puestos.ToList();
                }
                catch (Exception) { }
                return Json(p.Select(x => x.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public int ValidaColaboradores(int tipoDocumentoID, string documento)
        {
            using (DP2Context context = new DP2Context())
            {
                IList<Colaborador> colaboradores = context.TablaColaboradores.All().Where(c => ((c.TipoDocumentoID == tipoDocumentoID) && (c.NumeroDocumento == documento))).ToList();
                if (colaboradores.Count() == 0)
                    return 0;
                else
                    return 1;
            }
        }

        [HttpPost]
        public ActionResult UploadImagen(IEnumerable<HttpPostedFileBase> ImagenColaborador, int ID)
        {
            // The Name of the Upload component is "files"
            if (ImagenColaborador != null && ID != 0)
            {
                foreach (var file in ImagenColaborador)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        file.InputStream.CopyTo(memoryStream);
                        using (DP2Context context = new DP2Context())
                        {
                            Colaborador c = context.TablaColaboradores.FindByID(ID);
                            c.ImagenColaborador = memoryStream.ToArray();
                            context.TablaColaboradores.ModifyElement(c);
                        }
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult ViewImageDeColaborador(int colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    byte[] bytes = context.TablaColaboradores.FindByID(colaboradorID).ImagenColaborador;

                    return File(bytes, "image/jpg");
                }
                catch
                {
                    var file = Server.MapPath("~/Images/snoopy-joecool-color.gif");
                    using (var stream = new FileStream(file, FileMode.Open))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            stream.CopyTo(memoryStream);
                            return File(memoryStream.ToArray(), "image/gif");
                        }
                    }
                    
                }
            }
            
        }
    }
}
