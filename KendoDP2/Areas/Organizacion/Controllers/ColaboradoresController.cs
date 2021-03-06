﻿using System;
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
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ColaboradorDTO> colaboradores = context.TablaColaboradores.All(true).Where(x => !x.IsEliminado || x.IsEliminado && x.EstadoColaborador.Descripcion.Equals("Despedido")).Select(p => p.ToDTO()).OrderBy(x => x.ID).ToList();
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
                //cambio seguridad
                    c.Roles = context.SeedRolesGenerales();
                    if (ValidaColaboradores(c.TipoDocumentoID, c.NumeroDocumento)== 0)
                    {
                        if (PuestoEstaVacante(colaborador.PuestoID))
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
                            ModelState.AddModelError("Puesto", "El puesto está ocupado por otra persona, verificar");
                            return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Tipo Documento", "El Numero de documento es invalido");
                        return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
                        
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

                    if (PuestoEstaVacante(colaborador.PuestoID))
                    {
                        // asigno fecha fin al puesto
                        var ultimoCruce = context.TablaColaboradoresXPuestos.One(x => (x.FechaSalidaPuesto == null || x.FechaSalidaPuesto >= DateTime.Today) && x.ColaboradorID == c.ID);
                        if (ultimoCruce != null)
                        {
                            ultimoCruce.FechaSalidaPuesto = DateTime.Now.AddDays(-1);
                            context.TablaColaboradoresXPuestos.ModifyElement(ultimoCruce);
                        }

                        // se crea el nuevo puesto
                        DateTime fechaInicio;
                        try
                        {
                            fechaInicio = DateTime.ParseExact(colaborador.FechaIngreso, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                        }
                        catch
                        {
                            fechaInicio = DateTime.Now;
                        }
                        Puesto p = context.TablaPuestos.FindByID(colaborador.PuestoID);
                        ColaboradorXPuesto cruce = new ColaboradorXPuesto { ColaboradorID = c.ID, PuestoID = p.ID, Sueldo = colaborador.Sueldo, FechaIngresoPuesto = fechaInicio };

                        context.TablaColaboradoresXPuestos.AddElement(cruce);
                    }
                    else
                    {
                        ModelState.AddModelError("Puesto", "El puesto está ocupado por otra persona, verificar");
                        return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
                    }

                }
                else if (colaboradorBD.PuestoID == colaborador.PuestoID && colaboradorBD.Sueldo == colaborador.Sueldo)
                {
                    var ultimoCruce = context.TablaColaboradoresXPuestos.One(x => (x.FechaSalidaPuesto == null || x.FechaSalidaPuesto >= DateTime.Today) && x.ColaboradorID == c.ID);
                    try
                    {
                        var fechaInicio = DateTime.ParseExact(colaborador.FechaIngreso, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                        if (ultimoCruce != null)
                        {
                            ultimoCruce.FechaIngresoPuesto = fechaInicio;
                            context.TablaColaboradoresXPuestos.ModifyElement(ultimoCruce);
                        }
                    }
                    catch (Exception)
                    { }
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateImagen(int ID, int ImagenColaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                var colab = context.TablaColaboradores.FindByID(ID);
                colab.ImagenColaboradorID = ImagenColaboradorID;
                context.TablaColaboradores.ModifyElement(colab);
                return Json(new { success = true });
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

        public ActionResult BajarCurriculum(int curriculumVitaeID)
        {
            using (DP2Context context = new DP2Context())
            {
                var archivo = context.TablaArchivos.FindByID(curriculumVitaeID);
                return File(archivo.Data, archivo.Mime);
            }
        }
        public ActionResult ViewImageDeColaborador(int colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                if(colaboradorID != 0)
                {
                    var colaborador = context.TablaColaboradores.FindByID(colaboradorID);
                    if (colaborador.ImagenColaboradorID != 0)
                    {
                        var archivo = context.TablaArchivos.FindByID(colaborador.ImagenColaboradorID);
                        if (archivo.Data != null)
                            return File(archivo.Data, archivo.Mime);
                    }
                }
                var file = Server.MapPath("~/Images/unknown-person.jpg");
                using (var stream = new FileStream(file, FileMode.Open))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        return File(memoryStream.ToArray(), "image/jpg");
                    }
                }
            }
            
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Read2([DataSourceRequest] DataSourceRequest request, int colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
              //  return Json(context.TablaColaboradores.Where(c => c.Colaborador1ID == colaboradorID).Select(p => p.ToDTO()).ToDataSourceResult(request));
               //return Json( context.TablaColaboradores.FindByID(colaboradorID).Contactos.Where(x => x.ColaboradorID == colaboradorID).Select(x => x.Contacto).ToList());
                return Json(context.TablaColaboradores.FindByID(colaboradorID).Contactos.Where(x => x.ColaboradorID == colaboradorID).Select(x => x.ToDTO()).ToList().ToDataSourceResult(request)) ;
            }
        }

        public ActionResult AddEvaluadosColaborador(int colaboradorID, int contactoID)
        {
            using (DP2Context context = new DP2Context())
            {
                bool isNuevaReferenciaDirecta = AddColaboradorToProceso(colaboradorID, contactoID, context, true);
                return Json(new { success = isNuevaReferenciaDirecta });
            }
        }


        private bool AddColaboradorToProceso(int colaboradorID, int contactoID, DP2Context context, bool esReferenciaDirecta)
        {
            var cruce = context.TablaContactos.One(x => x.ColaboradorID == colaboradorID && x.ContactoID == contactoID);
            

            if (cruce == null)
            { // nuevo
                context.TablaContactos.AddElement(
                    new Contactos
                    {
                        ColaboradorID = colaboradorID,
                        ContactoID = contactoID,
                        Relacion = "Equipo de Área",
                        
                      
                    });
                return esReferenciaDirecta;
            }
            else if (!esReferenciaDirecta)
            {
                
                return false;
            }
            else
            { // no tenia referencia directa
              //  if (!cruce.ReferenciaDirecta)
               // {
                 //   cruce.ReferenciaDirecta = true;
                  //  context.TablaColaboradorXProcesoEvaluaciones.ModifyElement(cruce);
                   // return true;
               // }
                return false;
            }
        }


        public ActionResult GetViewContacto(int contactoID)
        {

            using (DP2Context context = new DP2Context())
            {

                Colaborador contacto = context.TablaColaboradores.FindByID(contactoID);

                ViewBag.tipoDocumento = contacto.TipoDocumento.ToDTO();
                ViewBag.gradoAcademico = contacto.GradoAcademico.ToDTO();
                ViewBag.contactoID = contactoID;
                return PartialView("ViewContacto", contacto.ToDTO());

            }
        }

        public ActionResult DespideColaborador([DataSourceRequest] DataSourceRequest request, int colaboradorID)
        {
            using (DP2Context context = new DP2Context())
            {
                Colaborador c = context.TablaColaboradores.FindByID(colaboradorID);
                c.EstadoColaborador = context.TablaEstadosColaboradores.All().Where(p => p.Descripcion.Equals("Despedido")).FirstOrDefault();
                context.TablaColaboradores.ModifyElement(c);
                ColaboradorDTO colaboradorBD = c.ToDTO(); // lee el ultimo puesto de la bd
                // crea un nuevo puesto en la tabla de cruce si algo cambio
                if (colaboradorBD.PuestoID != null)
                {
                    //asigno fecha fin al puesto
                    var ultimoCruce = context.TablaColaboradoresXPuestos.One(x => x.FechaSalidaPuesto == null && x.ColaboradorID == c.ID);
                    if (ultimoCruce != null)
                    {
                        ultimoCruce.FechaSalidaPuesto = DateTime.Now.AddDays(-1);
                        context.TablaColaboradoresXPuestos.ModifyElement(ultimoCruce);
                    }

                                    }
                context.TablaColaboradores.RemoveElementByID(colaboradorID);
                return Json(new[] { c.ToDTO() }.ToDataSourceResult(request, ModelState));
            }

        }

        public bool PuestoEstaVacante(int puestoID)
        {
            using (DP2Context context = new DP2Context())
            {
                ColaboradorXPuesto cruce = context.TablaColaboradoresXPuestos.One(x => (x.FechaSalidaPuesto == null || x.FechaSalidaPuesto >= DateTime.Today) && x.PuestoID == puestoID);
                return cruce == null;
            }
        }

    }
}
