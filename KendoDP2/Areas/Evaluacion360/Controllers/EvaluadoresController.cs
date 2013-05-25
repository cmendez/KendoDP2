using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class EvaluadoresController : Controller
    {
        //
        // GET: /Evaluacion360/Evaluadores/

        public ActionResult Index(int idEvaluado)
        {


            using (DP2Context context = new DP2Context())
            {

                ViewBag.idEvaluado = idEvaluado * 100;
                Colaborador colaborador = consigueAlEmpleado(idEvaluado);
                Puesto suPerfil = consigueSuPerfil(idEvaluado);
                List<PuestoXEvaluadores> puestoXEvaluadores = consigueSusEvaluadores(idEvaluado);

                //foreach (PuestoXEvaluadores puestoXEvaluador in puestoXEvaluadores) {
                //    puestoXEvaluador.Cantidad = 1;
                
                //}
                ViewBag.elEvaluado = colaborador.ToDTO();

                ViewBag.puestoXEvaluadores = puestoXEvaluadores.Select(p => p.ToDTO()).ToList();

                List<ColaboradorDTO> elJefe = new List<ColaboradorDTO>();
                elJefe.Add(consigueSuJefe(idEvaluado).ToDTO());
                ViewBag.suJefe = elJefe;
                List<ColaboradorDTO> elMismo = new List<ColaboradorDTO>();
                elMismo.Add(colaborador.ToDTO());
                ViewBag.elMismo = elMismo;
                ViewBag.susSubordinados = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.susPares = consigueSusPares(idEvaluado).Select(p => p.ToDTO()).ToList();
                ViewBag.otros = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();


                ViewBag.Area = ""; //Solo es temporal
                //ViewBag.susSubordinados = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                return View();
            }
        }

        public ActionResult enviar_evaluaciones(int evaluadoId, FormCollection form)
        {
            System.Collections.Specialized.NameObjectCollectionBase.KeysCollection llaves = form.Keys;
            int controles = form.Keys.Count;
            //string nombreControl = "ComboPares12";
            //Regex patron = new Regex(@"Combo(<claseEntorno>)");
            //Match coincidencias = patron.Match(nombreControl);
            //string claseEntorno = coincidencias.Groups["claseEntorno"].Value;

            ColaboradorXEvaluadores evaluadores = new ColaboradorXEvaluadores();

            evaluadores.evaluadoID = evaluadoId;
            evaluadores.evaluadoresID = new List<int>();

            for (int i = 0; i < llaves.Count / 2; i++)
            {
                string nombreControl = llaves[i*2 + 1];
                string[] palabras = nombreControl.Split('_');
                string claseEntorno = palabras[0];
                string nro = palabras[1];

                int evaluadorId = Int32.Parse(form[nombreControl].CompareTo("") == 0 ? "2" : form[nombreControl]);

                evaluadores.evaluadoresID.Add(evaluadorId);


            }

            //string nombreControl = "Pares_12_Combo";
            //string[] palabras = nombreControl.Split('_');
            //string claseEntorno = palabras[0];
            //string nro = palabras[1];
            //string nombreDeUnPar = form["ComboPares0_input"];

            new DP2Context().TablaColaboradorXEvaluadores.AddElement(evaluadores);
            ColaboradorXEvaluadores enBaseDeDatos = new DP2Context().TablaColaboradorXEvaluadores.FindByID(evaluadores.ID);

            return View();
        }

        private Colaborador consigueAlEmpleado(int idEvaluado) {
        
            return new DP2Context().TablaColaboradores.FindByID(idEvaluado);
        
        }

        private Puesto consigueSuPerfil(int idEvaluado)
        {
            Colaborador elColaborador = new DP2Context().TablaColaboradores.FindByID(idEvaluado);
            //elColaborador.ColaboradoresPuesto.GetEnumerator;

            foreach (ColaboradorXPuesto cxp in elColaborador.ColaboradoresPuesto)
            {
                return cxp.Puesto;
            }

            return null;

            //return new DP2Context().TablaPuestos.FindByID(idEvaluado);
            //return new Perfil("un perfil");
        }

        private List<PuestoXEvaluadores> consigueSusEvaluadores(int idEvaluado)
        {
            Puesto perfil = consigueSuPerfil(idEvaluado);
            //List<PuestoXEvaluadores> puestoYSusEvaluadores();
            List<PuestoXEvaluadores> puestoYSusEvaluadores = new DP2Context().TablaPuestoXEvaluadores.Where(e => e.PuestoID == perfil.ID).ToList();
            return puestoYSusEvaluadores;
        }

        private Colaborador consigueSuJefe(int idEvaluado)
        {
            Colaborador colaborador = new DP2Context().TablaColaboradores.FindByID(idEvaluado);

            return colaborador;
        }

        private List<Colaborador> consigueSusPares(int idEvaluado)
        {
            Colaborador colaborador = new DP2Context().TablaColaboradores.FindByID(idEvaluado);
            List<Colaborador> colaboradores = new List<Colaborador>();
            colaboradores.Add(colaborador);
            colaboradores.Add(colaborador);
            colaboradores.Add(colaborador);
            return colaboradores;
        }
    }
}
