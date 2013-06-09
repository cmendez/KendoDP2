using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Objetivos.Controllers
{
    public class AcordionController : Controller
    {
        //
        // GET: /Evaluacion360/Acordion/

        public ActionResult Index()
        {
            using (DP2Context contexto = new DP2Context()) 
            {
                List<Colaborador> subordinadosBaseDeDatos = contexto.TablaColaboradores.All();

                foreach (Colaborador subordinado in subordinadosBaseDeDatos)
                {
                    contexto.Entry(subordinado).Collection(s => s.Objetivos).Load();
                    contexto.Entry(subordinado).Collection(s => s.ColaboradoresPuesto).Load();
                    contexto.Entry(subordinado).Collection(s => s.ColaboradorXProcesoEvaluaciones).Load();
                    contexto.Entry(subordinado).Reference(s => s.EstadoColaborador).Load();
                    contexto.Entry(subordinado).Reference(s => s.Pais).Load();
                    contexto.Entry(subordinado).Collection(s => s.EsContactoDe).Load();
                    contexto.Entry(subordinado).Collection(s => s.Contactos).Load();

                }

                List<ColaboradorDTO> subordinadosCliente = subordinadosBaseDeDatos.Where(s => s.ID == 21 || s.ID == 22 || s.ID == 23 || s.ID == 3 || s.ID == 4).Select(s => s.ToDTO()).ToList();

                ViewBag.Colaboradores = subordinadosCliente;
                ViewBag.Area = "";
                return View();
            }
        }

    }
}
