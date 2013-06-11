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

        public ActionResult Index()
        {

            ViewBag.Area = "";
            return View();
        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {

            using (DP2Context contexto = new DP2Context())
            {
                int elUsuarioQueInicioSesion = DP2MembershipProvider.GetPersonaID(this);

                List<int> susSubordinados = GestorServiciosPrivados.consigueSusSubordinados(elUsuarioQueInicioSesion, contexto).Select(e => e.ID).ToList();
                return Json(contexto.TablaEvaluadores.Where(pxexe => susSubordinados.Contains(pxexe.ElEvaluado)).Select(examen => examen.enFormatoParaElClienteVistaSubordinados()).ToDataSourceResult(request));

            }
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