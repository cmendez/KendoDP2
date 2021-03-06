﻿using Kendo.Mvc.UI;
using KendoDP2.Models.Generic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Seguridad;


namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class ListarProcesosXEvaluadorController : Controller
    {
        //
        // GET: /Evaluacion360/ListarProcesosXEvaluador/

                public ListarProcesosXEvaluadorController()
            : base() 
        {
            ViewBag.Area = "Evaluacion360";
        }



        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context())
            {
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();               
                ViewBag.estados = context.TablaEstadoProcesoEvaluacion.All().Select(e => e.ToDTO()).ToList();
                return View();

            }
        }

        internal List<ProcesoEvaluacion> _Read(int idUsuario, DP2Context context)
        {
            List<ProcesoEvaluacion> listaProcesos_ = new List<ProcesoEvaluacion>();
            List<int> listaExamenes = context.TablaEvaluadores.Where(x=>x.ElIDDelEvaluador==idUsuario).Select(a=>a.ProcesoEnElQueParticipanID).ToList();
           // List<int> evaluacionesPendientes = context.TablaColaboradorXProcesoEvaluaciones.Where(pxexe => listaExamenes.Contains(pxexe.ColaboradorID)).Select(e => e.ProcesoEvaluacionID).ToList();
            int estadoID = context.TablaEstadoProcesoEvaluacion.One(e=>e.Descripcion.Equals(ConstantsEstadoProcesoEvaluacion.EnProceso)).ID;
            listaProcesos_ = context.TablaProcesoEvaluaciones.Where(p => listaExamenes.Contains(p.ID) && p.EstadoProcesoEvaluacionID== estadoID).ToList();
            return listaProcesos_;
         /* List<ProcesoEvaluacion> listaProceso = new List<ProcesoEvaluacion>();
          IList<Evaluador> listaProcesosEvaluador = (context.TablaEvaluadores.Where(a => a.ElIDDelEvaluador == idUsuario));

          for (int i = 0; i < listaProcesosEvaluador.Count; i++)
          {
              ProcesoEvaluacion procesoauxiliar = context.TablaProcesoEvaluaciones.One(b => b.ID == (listaProcesosEvaluador.ElementAt(i).ProcesoEnElQueParticipanID) &&
                  b.EstadoProcesoEvaluacionID == context.TablaEstadoProcesoEvaluacion.One(e => e.Descripcion.Equals(ConstantsEstadoProcesoEvaluacion.EnProceso)).ID);

              if (procesoauxiliar != null)
              {
                  listaProceso.Add(procesoauxiliar);
              }
            
          }
          if (listaProceso.Count == 0)
              return listaProceso;
          else
              return listaProceso.GroupBy(x => x.ID).Select(y => y.FirstOrDefault()).ToList();*/
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (DP2Context context = new DP2Context())
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
                List<ProcesoEvaluacion> listaProceso = _Read(ColaboradorID, context);

                return Json(listaProceso.Select(x => x.ToDTO()).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult ListarEvaluados(int procesoEvaluacionID)
        {
            using (DP2Context context = new DP2Context())
            {
                ProcesoEvaluacion proceso = context.TablaProcesoEvaluaciones.FindByID(procesoEvaluacionID);
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();                
                ViewBag.estados = context.TablaEstadoColaboradorXProcesoEvaluaciones.All().Select(c => c.ToDTO()).ToList();
                ViewBag.areas = context.TablaAreas.All().Select(c => c.ToDTO()).ToList();
                return View(proceso);

            }
        }

        public List<Evaluador> _ReadEvaluados(int usuarioID, int procesoID, DP2Context context)
        {
            //return Json(context.TablaColaboradores.FindByID(ColaboradorID).OcurrenciasComoEvaluador.Where(x => x.Evaluado.ProcesoEvaluacionID == procesoID).ToList());

            IList<Evaluador> listaEvaluaciones = new List<Evaluador>();

            return (context.TablaEvaluadores.Where(y => y.ElIDDelEvaluador == usuarioID &&
                                                                               y.ProcesoEnElQueParticipanID == procesoID)).ToList();
            //IList<EvaluadorDTO> listaEvaluaciones2 = listaEvaluaciones.Select(x => x.ToDTO());

            //Json(listaEvaluaciones.Select(x=>x.ToDTO()).ToDataSourceResult(request));

        }
        public ActionResult ReadEvaluados([DataSourceRequest] DataSourceRequest request, int procesoID)
        {
            using (DP2Context context = new DP2Context())
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
                List<Evaluador> listaEvaluaciones = _ReadEvaluados(ColaboradorID, procesoID, context);
                return Json(listaEvaluaciones.Select(x => x.ToDTOEvaluacion(context)).ToDataSourceResult(request));

            }
        }

    }
}
