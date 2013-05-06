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
                o.Creador = context.TablaColaboradores.One(c => c.Username.Equals(User.Identity.Name));
                context.TablaObjetivos.AddElement(o);
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo)
        {
            using (DP2Context context = new DP2Context())
            {
                Objetivo o = context.TablaObjetivos.FindByID(objetivo.ID).LoadFromDTO(objetivo);
                context.TablaObjetivos.ModifyElement(o);
                return Json(new[] { o.ToDTO() }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, ObjetivoDTO objetivo)
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
                List<ObjetivoDTO> ListaObjetivos = new List<ObjetivoDTO>();
                ObjetivoDTO ob1 = new ObjetivoDTO();
                ob1.Nombre = "Objetivo1";
                ob1.TipoObjetivoBSCID = 1;
                ob1.Peso = 50;
                ob1.ID = 1;
                ListaObjetivos.Add(ob1);

                ObjetivoDTO ob2 = new ObjetivoDTO();
                ob2.Nombre = "Objetivo2";
                ob2.TipoObjetivoBSCID = 1;
                ob2.Peso = 50;
                ob2.ID = 2;
                ListaObjetivos.Add(ob2);

                ObjetivoDTO ob3 = new ObjetivoDTO();
                ob3.Nombre = "Objetivo1.1";
                ob3.TipoObjetivoBSCID = 1;
                ob3.Peso = 50;
                ob3.ObjetivoPadreID = 1;
                ob3.ID=3;
                ListaObjetivos.Add(ob3);

                ObjetivoDTO ob4 = new ObjetivoDTO();
                ob4.Nombre = "Objetivo1.2";
                ob4.TipoObjetivoBSCID = 1;
                ob4.Peso = 50;
                ob4.ID=4;
                ob4.ObjetivoPadreID = 1;
                ListaObjetivos.Add(ob4);

                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, Creador = TablaColaboradores.FindByID(1) });
                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 2", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, Creador= TablaColaboradores.FindByID(1) });
                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1.1", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, ObjetivoPadreID = 1, Creador = TablaColaboradores.FindByID(1) });
                //TablaObjetivos.AddElement(new Objetivo { Nombre = "Objetivo Financiero 1.2", TipoObjetivoBSCID = 1, Peso = 50, FechaCreacion = DateTime.Now, ObjetivoPadreID = 1, Creador = TablaColaboradores.FindByID(1) });
        

                return Json(ListaObjetivos, JsonRequestBehavior.AllowGet);
                //return Json(ob.ToDTO(), JsonRequestBehavior.AllowGet);
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
