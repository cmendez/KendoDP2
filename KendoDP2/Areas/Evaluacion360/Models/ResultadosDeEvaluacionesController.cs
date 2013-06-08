using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExtensionMethods;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    //public class ResultadosDeEvaluacionesController : Controller
    public class ResultadosDeEvaluacionesController : WSController        
    {
        ////
        //// GET: /Evaluacion360/ResultadosDeEvaluaciones/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public JsonResult consultarDatosDelEmpleado(string conEsteIdentificador)
        public JsonResult consultarResultadosDeLasEvaluaciones(int idDelColaborador)
        {
            return null;
            //TODO: Realizar una prueba con datos ficticios
            ////TODO: Coordinar con el encargado de cálculos de resultados para cargar esta estructura de datos con 
            //TODO: Coordinar con el encargado de cálculos de resultados para integrar este método con la base de datos.
            //TODO: Invocar esta funcionalidad desde el móvil
        }

    }
}
