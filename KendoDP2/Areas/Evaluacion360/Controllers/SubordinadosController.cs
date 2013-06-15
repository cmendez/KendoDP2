using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class SubordinadosController : Controller
    {
        //
        // GET: /Evaluacion360/Subordinados/
        public SubordinadosController()
            : base() 
        {
            ViewBag.Area = "Evaluacion360";
        }

        public ActionResult Index()
        {

            using (DP2Context context = new DP2Context())
            {
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
                ViewBag.estados = context.TablaEstadoProcesoEvaluacion.All().Select(e => e.ToDTO()).ToList();
                // Identificar si el usuario loggeado tiene permisos para modificar procesos
                //ViewBag.esAdmin = EsAdmin(DP2MembershipProvider.GetPersonaID(this), context);

                return View();
            }
        }

        public ActionResult Read_ExamenesSubordinados([DataSourceRequest] DataSourceRequest request, int procesoID, int evaluadoID) {
            using (DP2Context context = new DP2Context()) {
                return Json(context.TablaEvaluadores.Where(x=>x.ElEvaluado== evaluadoID && x.ProcesoEnElQueParticipanID==procesoID).Select(p=>p.enFormatoParaElClienteVistaSubordinados()).ToDataSourceResult(request));
            }
        }

        public ActionResult Read_SubordinadosEvaluados([DataSourceRequest] DataSourceRequest request, int procesoID)
        {
            using (DP2Context context = new DP2Context())
            {
                int usuarioLoggeadoID = DP2MembershipProvider.GetPersonaID(this);

                List<int> susSubordinados = GestorServiciosPrivados.consigueSusSubordinados(usuarioLoggeadoID, context).Select(e => e.ID).ToList();
                //List<ProcesoXEvaluadoXEvaluadorDTO> evaluacionesSubordinados = context.TablaEvaluadores.Where(ev => susSubordinados.Contains(ev.ElEvaluado) && ev.ProcesoEnElQueParticipanID == procesoID).Select(e => e.enFormatoParaElClienteVistaSubordinados()).ToList();
                List<ColaboradorXProcesoEvaluacionDTO> subordinadosEvaluados = context.TablaColaboradorXProcesoEvaluaciones.Where(e => susSubordinados.Contains(e.ColaboradorID) && e.ProcesoEvaluacionID==procesoID).Select(p=>p.ToDTO()).ToList();
                return Json(subordinadosEvaluados.ToDataSourceResult(request));
            }
        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {

            using (DP2Context contexto = new DP2Context())
            {
                int elUsuarioQueInicioSesion = DP2MembershipProvider.GetPersonaID(this);
                using (DP2Context context = new DP2Context())
                {
                    IEnumerable<ProcesoEvaluacionDTO> listaProcesos= new List<ProcesoEvaluacionDTO>();
                    //Obtener la persona loggeada y su puesto
                    int idUsuario = DP2MembershipProvider.GetPersonaID(this);
                    Colaborador c = context.TablaColaboradores.FindByID(idUsuario);
                    ColaboradorXPuesto cxp;
                    try
                    {
                        cxp = context.TablaColaboradoresXPuestos.Where(x => x.ColaboradorID == c.ID && !x.IsEliminado && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto)).OrderByDescending(a => a.PuestoID).First();
                    }
                    catch (Exception excep)
                    {  // no tiene puesto asociado, se muestran todos los procesos
                        return Json(listaProcesos.ToDataSourceResult(request));
                    }
                    // no tiene puesto asociado, se muestran todos los procesos
                    if (cxp == null)
                    {
                        return Json(listaProcesos.ToDataSourceResult(request));
                    }
                    // tiene asignado un puesto
                    else
                    {
                        Puesto puesto = context.TablaPuestos.FindByID(cxp.PuestoID);
                        // No es presidente, admin 
                        if (puesto != null && puesto.PuestoSuperiorID != null)
                        {
                            IList<ProcesoEvaluacionDTO> listaProcesos_ = Read_(puesto, elUsuarioQueInicioSesion, context);
                            return Json(listaProcesos_.ToDataSourceResult(request));
                        }
                    }

                    return Json(listaProcesos.ToDataSourceResult(request));
                }
                //List<int> susSubordinados = GestorServiciosPrivados.consigueSusSubordinados(elUsuarioQueInicioSesion, contexto).Select(e => e.ID).ToList();
                //return Json(contexto.TablaEvaluadores.Where(pxexe => susSubordinados.Contains(pxexe.ElEvaluado)).Select(examen => examen.enFormatoParaElClienteVistaSubordinados()).ToDataSourceResult(request));

            }
        }

        public IList<ProcesoEvaluacionDTO> Read_(Puesto puesto, int elUsuarioQueInicioSesion, DP2Context context) {
            IList<ProcesoEvaluacionDTO> listaProcesos_ = new List<ProcesoEvaluacionDTO>();
            List<int> susSubordinados = GestorServiciosPrivados.consigueSusSubordinados(elUsuarioQueInicioSesion, context).Select(e => e.ID).ToList();
            List<int> evaluacionesSubordinados = context.TablaEvaluadores.Where(pxexe => susSubordinados.Contains(pxexe.ElEvaluado)).Select(e=>e.ProcesoEnElQueParticipanID).ToList();
            return context.TablaProcesoEvaluaciones.Where(p => evaluacionesSubordinados.Contains(p.ID)).Select(x=>x.ToDTO()).ToList();
        }

        public ActionResult VerNotasSubordinados(int procesoEvaluacionID)
        {
            int usuarioLoggeadoID = DP2MembershipProvider.GetPersonaID(this);
           
            using (DP2Context context = new DP2Context())
            {
                ViewBag.estados = context.TablaEstadoColaboradorXProcesoEvaluaciones.All().Select(e => e.ToDTO()).ToList();
                ProcesoEvaluacion proceso = context.TablaProcesoEvaluaciones.FindByID(procesoEvaluacionID);
                return View(proceso);

            }
        }

        public ActionResult VerExamenesSubordinado(int ProcesoID, int EvaluadoID) { 
            using(DP2Context context = new DP2Context()){
                ViewBag.estados = context.TablaEstadoColaboradorXProcesoEvaluaciones.All().Select(e => e.ToDTO()).ToList();
                ColaboradorXProcesoEvaluacionDTO  cxp= context.TablaColaboradorXProcesoEvaluaciones.One(x=>x.ProcesoEvaluacionID==ProcesoID && x.ColaboradorID== EvaluadoID).ToDTO();
                return View(cxp);
            }
        }

        private Colaborador consigueSuJefe(int idEvaluado, DP2Context context)
        {
            return GestorServiciosPrivados.consigueElJefe(idEvaluado, context);
        }
      }     
 }

        //public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        //{

        //    using (DP2Context contexto = new DP2Context())
        //    {

        //        //int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
        //        int elUsuarioQueInicioSesion = DP2MembershipProvider.GetPersonaID(this);



        //        //List<Colaborador> susSubordinados = GestorServiciosPrivados.consigueSusSubordinados(elUsuarioQueInicioSesion, contexto);
        //        List<int> susSubordinados = GestorServiciosPrivados.consigueSusSubordinados(elUsuarioQueInicioSesion, contexto).Select(e => e.ID).ToList();

        //        //return Json(contexto.TablaEvaluadores.All().Select(examen => examen.enFormatoParaElClienteVistaSubordinados()).ToDataSourceResult(request));
        //        return Json(contexto.TablaEvaluadores.Where(pxexe => susSubordinados.Contains(pxexe.ElEvaluado)).Select(examen => examen.enFormatoParaElClienteVistaSubordinados()).ToDataSourceResult(request));

        //    }
        //}


















        //public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        //{

        //    //List<EvaluadorSubordinadosDTO> procesoXEvaluadorXEvaluadoDTO;


        //    //using (DP2Context context = new DP2Context())
        //    using (DP2Context contexto = new DP2Context())
        //    {
        //        //return Json(context.TablaCompetencias.All().Select(p => p.ToDTO()).ToDataSourceResult(request));

        //        //procesoXEvaluadorXEvaluadoDTO = contexto.TablaEvaluadores.All().Select(examen => examen.enFormatoParaElClienteVistaSubordinados()).ToList();
        //        //procesoXEvaluadorXEvaluadoDTO = contexto.TablaEvaluadores.All().Select(examen => examen.enFormatoParaElClienteVistaSubordinados()).ToList();

        //        //ViewBag.Area = "";
        //        //ViewBag.evaluados = procesoXEvaluadorXEvaluadoDTO;

        //        return Json(contexto.TablaEvaluadores.All().Select(examen => examen.enFormatoParaElClienteVistaSubordinados()).ToDataSourceResult(request));

        //    }
        //}







        //public ActionResult Index()
        //{
        //    //using()
        //    List<EvaluadorSubordinadosDTO> procesoXEvaluadorXEvaluadoDTO;

        //    using (DP2Context contexto = new DP2Context())
        //    {

        //        //contexto.TablaEvaluadores.All().Select(pxexe => pxexe.)
        //        //List<EvaluadorSubordinadosDTO> procesoXEvaluadorXEvaluadoDTO = contexto.TablaEvaluadores.All().Select(examen => examen.enFormatoParaElClienteVistaSubordinados()).ToList();
        //        procesoXEvaluadorXEvaluadoDTO = contexto.TablaEvaluadores.All().Select(examen => examen.enFormatoParaElClienteVistaSubordinados()).ToList();

        //        ViewBag.Area = "";
        //        ViewBag.evaluados = procesoXEvaluadorXEvaluadoDTO;

        //    }

        //    //ViewBag.Area = "";
        //    ////ViewBag.
        //    //ViewBag.evaluados = procesoXEvaluadorXEvaluadoDTO;


        //    //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(subordinadosCliente.GetType());
        //    //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(procesoXEvaluadorXEvaluadoDTO.GetType());
        //    //StringWriter escritor = new StringWriter();


        //    //x.Serialize(escritor, procesoXEvaluadorXEvaluadoDTO);

        //    //System.Diagnostics.Debug.WriteLine(escritor.ToString());

        //    return View();
        //}












//public ActionResult Index(int elProcesoSeleccionado, int elJefe)
//{
//    ViewBag.Area = "";
//    ViewBag.losProcesosDeEvaluacion = GestorServiciosPrivados.listaTodosLosProcesos();
//    //ViewBag.losSubordinados = .consigueAlEmpleado();

//    List<Colaborador> losSubordinados = GestorServiciosPrivados.consigueSusSubordinadosFicticios(elJefe);
//    List<SupervisionASubordinadoDTO> elEstadoDeLasEvaluaciones = new List<SupervisionASubordinadoDTO>();

//    foreach (Colaborador unEmpleado in losSubordinados)
//    {
//        //ProcesoXEvaluado laRelacionEntreElProcesoYSubordinado = GestorServiciosPrivados.devuelveDatosDeParticipacion(elProcesoSeleccionado, unEmpleado.ID);
//        List<Evaluador> laRelacionEntreElProcesoYSubordinado = GestorServiciosPrivados.devuelveDatosDeParticipacion(elProcesoSeleccionado, unEmpleado.ID);

//        //if (laRelacionEntreElProcesoYSubordinado != null)
//        //{
//        //    SupervisionSubordinadosDTO laRelacionEnFormatoParaElCliente = laRelacionEntreElProcesoYSubordinado.paraElCliente();

//        //    elEstadoDeLasEvaluaciones.Add(laRelacionEnFormatoParaElCliente);
//        //}

//        SupervisionASubordinadoDTO unElementoDelConjunto = new SupervisionASubordinadoDTO();

//        unElementoDelConjunto.PersonaID = unEmpleado.ID;
//        unElementoDelConjunto.PersonaNombre = unEmpleado.Nombres;
//        unElementoDelConjunto.CargoID = GestorServiciosPrivados.devolverPuestoVigente(unEmpleado).ID;
//        unElementoDelConjunto.CargoNombre = GestorServiciosPrivados.devolverPuestoVigente(unEmpleado).Nombre;

//        unElementoDelConjunto.Evaluadores = new List<EvaluadorDTO>();

//        foreach (Evaluador relacionDeEvaluacion in laRelacionEntreElProcesoYSubordinado)
//        {
//            //SupervisionSubordinadosDTO unaFilaDeLaVista = relacionDeEvaluacion.paraElCliente();
//            //elEstadoDeLasEvaluaciones.Add(unaFilaDeLaVista);
//            unElementoDelConjunto.Evaluadores.Add(relacionDeEvaluacion.paraElCliente());
//        }

//        elEstadoDeLasEvaluaciones.Add(unElementoDelConjunto);
//    }

//    ViewBag.losSubordinados = elEstadoDeLasEvaluaciones;


//    return View();
//}