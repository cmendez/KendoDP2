﻿using System;
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

        public ActionResult SeleccionarEvaluadores(int procesoEvaluacionID, int colaboradorID) {
                                                          
            using (DP2Context context = new DP2Context())
            {

                ViewBag.Area = ""; //Solo es temporal
                ViewBag.elProceso = procesoEvaluacionID;

                if (losEvaluadoresDeEsteColaboradorYaFueronElegidos(procesoEvaluacionID, colaboradorID))
                {
                    Colaborador elEmpleado = consigueAlEmpleado(colaboradorID);
                    ViewBag.elEvaluado = elEmpleado.ToDTO();
                    ViewBag.losEvaluadoresYaFueronSeleccionados = true;

                    return View();
                }

                ViewBag.losEvaluadoresYaFueronSeleccionados = false;
                ViewBag.idEvaluado = colaboradorID * 100;
                Colaborador colaborador = consigueAlEmpleado(colaboradorID);
                Puesto suPerfil = consigueSuPerfil(colaboradorID);
                List<PuestoXEvaluadores> puestoXEvaluadores = consigueSusEvaluadores(colaboradorID);

                //foreach (PuestoXEvaluadores puestoXEvaluador in puestoXEvaluadores) {
                //    puestoXEvaluador.Cantidad = 1;

                //}
                ViewBag.elEvaluado = colaborador.ToDTO();

                ViewBag.puestoXEvaluadores = puestoXEvaluadores.Select(p => p.ToDTO()).ToList();

                List<ColaboradorDTO> elJefe = new List<ColaboradorDTO>();
                elJefe.Add(consigueSuJefe(colaboradorID).ToDTO());
                ViewBag.suJefe = elJefe;
                List<ColaboradorDTO> elMismo = new List<ColaboradorDTO>();
                elMismo.Add(colaborador.ToDTO());
                ViewBag.elMismo = elMismo;
                ViewBag.susSubordinados = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.susPares = consigueSusPares(colaboradorID).Select(p => p.ToDTO()).ToList();
                ViewBag.otros = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();


                
                //ViewBag.susSubordinados = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                return View();
            }
        }

        public ActionResult enviar_evaluaciones(int idDelProceso, int evaluadoId, FormCollection form)
        {

            using (DP2Context context = new DP2Context())
            {
                System.Collections.Specialized.NameObjectCollectionBase.KeysCollection llaves = form.Keys;
                int controles = form.Keys.Count;
                //string nombreControl = "ComboPares12";
                //Regex patron = new Regex(@"Combo(<claseEntorno>)");
                //Match coincidencias = patron.Match(nombreControl);
                //string claseEntorno = coincidencias.Groups["claseEntorno"].Value;

                ProcesoXEvaluado evaluadores = new ProcesoXEvaluado();

                evaluadores.procesoID = idDelProceso;
                evaluadores.evaluadoID = evaluadoId;
                //evaluadores.evaluadores = new List<Colaborador>();
                evaluadores.evaluadores = new List<Evaluador>();

                for (int i = 0; i < llaves.Count / 2; i++)
                {
                    string nombreControl = llaves[i * 2 + 1];
                    string[] palabras = nombreControl.Split('_');
                    string claseEntorno = palabras[0];
                    string nro = palabras[1];

                    int evaluadorId = Int32.Parse(form[nombreControl].CompareTo("") == 0 ? "2" : form[nombreControl]);



                    //evaluadores.evaluadores.Add(new Colaborador { ID = evaluadorId });
                    //evaluadores.evaluadores.Add(context.TablaColaboradores.FindByID(evaluadorId));


                    //evaluadores.evaluadores.Add(context.TablaColaboradores.FindByID(evaluadorId));

                    Colaborador elParticipante = context.TablaColaboradores.FindByID(evaluadorId);
                    //Evaluador comoEvaluador = Evaluador.enrolarlo(elParticipante, idDelProceso);
                    Evaluador comoEvaluador = new Evaluador(evaluadoId, elParticipante, idDelProceso);



                    evaluadores.evaluadores.Add(context.TablaEvaluadores.FindByID(evaluadorId));

                    context.TablaEvaluadores.AddElement(comoEvaluador);
                    Examen examen = new Examen();
                    //examen.FechaCierre = new DateTime();
                    examen.EvaluadorID = comoEvaluador.ID;
                    examen.Estado = context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x => x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Pendiente));
                    context.TablaExamenes.AddElement(examen);
                }

                //string nombreControl = "Pares_12_Combo";
                //string[] palabras = nombreControl.Split('_');
                //string claseEntorno = palabras[0];
                //string nro = palabras[1];
                //string nombreDeUnPar = form["ComboPares0_input"];

                context.TablaProcesoXEvaluado.AddElement(evaluadores);
                ProcesoXEvaluado enBaseDeDatos = context.TablaProcesoXEvaluado.FindByID(evaluadores.ID);
                context.Entry(enBaseDeDatos).Collection(u => u.evaluadores).Load();
                //ColaboradorXEvaluadores enBaseDeDatos = new DP2Context().InternalColaboradorXProcesoEvaluaciones.Include().TablaColaboradorXEvaluadores.FindByID(evaluadores.ID);

                ViewBag.Area = "";
                return View();

            }
        }

        private bool losEvaluadoresDeEsteColaboradorYaFueronElegidos(int procesoEvaluacionID, int colaboradorID)
        {

            try
            {

                using (DP2Context laBaseDeDatos = new DP2Context())
                {
                    //if (laBaseDeDatos.TablaProcesoXEvaluado.One(pxe => pxe.evaluadoID == colaboradorID && pxe.procesoID == procesoEvaluacionID).evaluadores.Count == 0)
                    if (laBaseDeDatos.TablaEvaluadores.Where(e => e.ElEvaluado == colaboradorID && e.ProcesoEnElQueParticipanID == procesoEvaluacionID).Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }

            }
            catch (NullReferenceException aunSinEvaluadores)
            {
                return false;
            }

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
            Colaborador colaborador = new DP2Context().TablaColaboradores.FindByID(22);

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
