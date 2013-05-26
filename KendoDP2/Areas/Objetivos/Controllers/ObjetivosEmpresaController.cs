using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoDP2.Areas.Objetivos.Models;


namespace KendoDP2.Areas.Objetivos.Controllers
{
    
    public class ObjetivosEmpresaController : Controller
    {
        public ObjetivosEmpresaController()
        {
            ViewBag.Area = "Objetivos";
        }

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context()) {
                ViewBag.periodos = context.TablaPeriodos.All().Select(p => p.ToDTO()).ToList();
                ViewBag.tipoObjetivosBSC = context.TablaTipoObjetivoBSC.All();
                ViewBag.colaboradores = context.TablaColaboradores.All().Select(p => p.ToDTO()).ToList();
                return View();
            }
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request, int tipoObjetivoBSCID, int BSCID)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaObjetivos.Where(o => o.TipoObjetivoBSCID == tipoObjetivoBSCID && o.BSCID == BSCID).Select(o => o.ToDTO()).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = new Objetivo(objetivo);
                context.TablaObjetivos.AddElement(o);
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = context.TablaObjetivos.FindByID(objetivo.ID).LoadFromDTO(objetivo);
                context.TablaObjetivos.ModifyElement(o);
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                context.TablaObjetivos.RemoveElementByID(objetivo.ID);
                return Json(ModelState.ToDataSourceResult());
            }
        }

        public ActionResult ListarObjetivos(int idperiodo)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ObjetivoRDTO> ListaObjetivos = new List<ObjetivoRDTO>();

                ObjetivoRDTO ob1 = new ObjetivoRDTO();
                ob1.idObjetivo = 1;
                ob1.descripcion = "Objetivo1";                
                ListaObjetivos.Add(ob1);

                ObjetivoRDTO ob2 = new ObjetivoRDTO();
                ob2.idObjetivo = 1;
                ob2.descripcion = "Objetivo2";
                ListaObjetivos.Add(ob2);

                ObjetivoRDTO ob3 = new ObjetivoRDTO();
                ob3.idObjetivo = 3;
                ob3.descripcion = "Objetivo3";
                ListaObjetivos.Add(ob3);

                ObjetivoRDTO ob4 = new ObjetivoRDTO();
                ob4.idObjetivo = 4;
                ob4.descripcion = "Objetivo4";
                ListaObjetivos.Add(ob4);

                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, Creador = TablaColaboradores.FindByID(1) });
                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 2", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, Creador= TablaColaboradores.FindByID(1) });
                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1.1", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, ObjetivoPadreID = 1, Creador = TablaColaboradores.FindByID(1) });
                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1.2", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, ObjetivoPadreID = 1, Creador = TablaColaboradores.FindByID(1) });
        

                return Json(ListaObjetivos, JsonRequestBehavior.AllowGet);
                //return Json(ob.ToDTO(), JsonRequestBehavior.AllowGet);
            }    
        }

        public ActionResult ListarObjetivosXBSC(int BSCId,int idperiodo)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ObjetivoRDTO> ListaObjetivos = new List<ObjetivoRDTO>();

                ObjetivoRDTO ob1 = new ObjetivoRDTO();
                ob1.idObjetivo = 1;
                ob1.descripcion = "Objetivo1";
                ListaObjetivos.Add(ob1);

                ObjetivoRDTO ob2 = new ObjetivoRDTO();
                ob2.idObjetivo = 1;
                ob2.descripcion = "Objetivo2";
                ListaObjetivos.Add(ob2);

                ObjetivoRDTO ob3 = new ObjetivoRDTO();
                ob3.idObjetivo = 3;
                ob3.descripcion = "Objetivo3";
                ListaObjetivos.Add(ob3);

                ObjetivoRDTO ob4 = new ObjetivoRDTO();
                ob4.idObjetivo = 4;
                ob4.descripcion = "Objetivo4";
                ListaObjetivos.Add(ob4);

                List<ObjetivoRDTO> ListaObjetivos2 = new List<ObjetivoRDTO>();
                ListaObjetivos2 = context.TablaObjetivos.Where(o => o.TipoObjetivoBSCID == BSCId && o.BSCID==idperiodo && o.ObjetivoPadreID >0).Select(p => p.ToRDTO()).ToList();

                return Json(ListaObjetivos2, JsonRequestBehavior.AllowGet);
                //return Json(ListaObjetivos, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarObjetivosXPadre2(int PadreId)
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaObjetivos.Where(o => o.ObjetivoPadreID == PadreId).Select(p => p.ToRDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarObjetivosXPadre(int PadreId)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ObjetivoRDTO> ListaObjetivos = new List<ObjetivoRDTO>();

                ObjetivoRDTO ob1 = new ObjetivoRDTO();
                ob1.idObjetivo = 1;
                ob1.descripcion = "Objetivo1";
                ListaObjetivos.Add(ob1);

                ObjetivoRDTO ob2 = new ObjetivoRDTO();
                ob2.idObjetivo = 1;
                ob2.descripcion = "Objetivo2";
                ListaObjetivos.Add(ob2);

                ObjetivoRDTO ob3 = new ObjetivoRDTO();
                ob3.idObjetivo = 3;
                ob3.descripcion = "Objetivo3";
                ListaObjetivos.Add(ob3);

                ObjetivoRDTO ob4 = new ObjetivoRDTO();
                ob4.idObjetivo = 4;
                ob4.descripcion = "Objetivo4";
                ListaObjetivos.Add(ob4);
                
                //return Json(ListaObjetivos, JsonRequestBehavior.AllowGet);
                return Json(context.TablaObjetivos.Where(o => o.ObjetivoPadreID == PadreId).Select(p => p.ToRDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarObjetivosPrueba(int PadreId)
        {
            using (DP2Context context = new DP2Context())
            {
                List<ObjetivoDTO> ListaObjetivos2 = new List<ObjetivoDTO>();
                ListaObjetivos2 = context.TablaObjetivos.All().Select(p => p.ToDTO()).ToList();
                return Json(context.TablaObjetivos.All().Select(p => p.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListarPeriodos()
        {
            using (DP2Context context = new DP2Context())
            {
                return Json(context.TablaPeriodos.All().Select(p => p.ToDTO()).ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
