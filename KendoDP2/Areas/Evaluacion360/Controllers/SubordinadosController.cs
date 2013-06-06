using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class SubordinadosController : Controller
    {
        //
        // GET: /Evaluacion360/Subordinados/

        public ActionResult Index(int elProcesoSeleccionado, int elJefe)
        {
            ViewBag.Area = "";
            ViewBag.losProcesosDeEvaluacion = GestorServiciosPrivados.listaTodosLosProcesos();
            //ViewBag.losSubordinados = .consigueAlEmpleado();

            List<Colaborador> losSubordinados = GestorServiciosPrivados.consigueSusSubordinadosFicticios(elJefe);
            List<SupervisionASubordinadoDTO> elEstadoDeLasEvaluaciones = new List<SupervisionASubordinadoDTO>();

            foreach (Colaborador unEmpleado in losSubordinados)
            {
                //ProcesoXEvaluado laRelacionEntreElProcesoYSubordinado = GestorServiciosPrivados.devuelveDatosDeParticipacion(elProcesoSeleccionado, unEmpleado.ID);
                List<Evaluador> laRelacionEntreElProcesoYSubordinado = GestorServiciosPrivados.devuelveDatosDeParticipacion(elProcesoSeleccionado, unEmpleado.ID);

                //if (laRelacionEntreElProcesoYSubordinado != null)
                //{
                //    SupervisionSubordinadosDTO laRelacionEnFormatoParaElCliente = laRelacionEntreElProcesoYSubordinado.paraElCliente();

                //    elEstadoDeLasEvaluaciones.Add(laRelacionEnFormatoParaElCliente);
                //}

                SupervisionASubordinadoDTO unElementoDelConjunto = new SupervisionASubordinadoDTO();

                unElementoDelConjunto.PersonaID = unEmpleado.ID;
                unElementoDelConjunto.PersonaNombre = unEmpleado.Nombres;
                unElementoDelConjunto.CargoID = GestorServiciosPrivados.devolverPuestoVigente(unEmpleado).ID;
                unElementoDelConjunto.CargoNombre = GestorServiciosPrivados.devolverPuestoVigente(unEmpleado).Nombre;

                unElementoDelConjunto.Evaluadores = new List<EvaluadorDTO>();

                foreach (Evaluador relacionDeEvaluacion in laRelacionEntreElProcesoYSubordinado)
                {
                    //SupervisionSubordinadosDTO unaFilaDeLaVista = relacionDeEvaluacion.paraElCliente();
                    //elEstadoDeLasEvaluaciones.Add(unaFilaDeLaVista);
                    unElementoDelConjunto.Evaluadores.Add(relacionDeEvaluacion.paraElCliente());
                }

                elEstadoDeLasEvaluaciones.Add(unElementoDelConjunto);
            }

            ViewBag.losSubordinados = elEstadoDeLasEvaluaciones;


            return View();
        }

    }
}
