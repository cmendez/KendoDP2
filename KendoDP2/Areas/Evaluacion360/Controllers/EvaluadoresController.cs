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
        public ActionResult Index(int idEvaluado)
        {


            using (DP2Context context = new DP2Context())
            {

                ViewBag.idEvaluado = idEvaluado * 100;
                Colaborador colaborador = consigueAlEmpleado(idEvaluado, context);
                Puesto suPerfil = consigueSuPerfil(idEvaluado, context);
                List<PuestoXEvaluadores> puestoXEvaluadores = consigueSusEvaluadores(idEvaluado, context);

                //foreach (PuestoXEvaluadores puestoXEvaluador in puestoXEvaluadores) {
                //    puestoXEvaluador.Cantidad = 1;
                
                //}
                ViewBag.elEvaluado = colaborador.ToDTO();

                ViewBag.puestoXEvaluadores = puestoXEvaluadores.Select(p => p.ToDTO()).ToList();

                List<ColaboradorDTO> elJefe = new List<ColaboradorDTO>();
                elJefe.Add(consigueSuJefe(idEvaluado, context).ToDTO());
                ViewBag.suJefe = elJefe;
                List<ColaboradorDTO> elMismo = new List<ColaboradorDTO>();
                elMismo.Add(colaborador.ToDTO());
                ViewBag.elMismo = elMismo;
                ViewBag.susSubordinados = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.susPares = consigueSusPares(idEvaluado, context).Select(p => p.ToDTO()).ToList();
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

                if (losEvaluadoresDeEsteColaboradorYaFueronElegidos(procesoEvaluacionID, colaboradorID, context))
                {
                    Colaborador elEmpleado = consigueAlEmpleado(colaboradorID, context);
                    ViewBag.elEvaluado = elEmpleado.ToDTO();
                    ViewBag.losEvaluadoresYaFueronSeleccionados = true;

                    return View();
                }

                ViewBag.losEvaluadoresYaFueronSeleccionados = false;
                ViewBag.idEvaluado = colaboradorID * 100;
                Colaborador colaborador = consigueAlEmpleado(colaboradorID, context);
                Puesto suPerfil = consigueSuPerfil(colaboradorID, context);
                List<PuestoXEvaluadores> puestoXEvaluadores = consigueSusEvaluadores(colaboradorID, context);

                //foreach (PuestoXEvaluadores puestoXEvaluador in puestoXEvaluadores) {
                //    puestoXEvaluador.Cantidad = 1;

                //}
                ViewBag.elEvaluado = colaborador.ToDTO();

                ViewBag.puestoXEvaluadores = puestoXEvaluadores.Select(p => p.ToDTO()).ToList();

                List<ColaboradorDTO> elJefe = new List<ColaboradorDTO>();
                elJefe.Add(consigueSuJefe(colaboradorID, context).ToDTO());
                ViewBag.suJefe = elJefe;
                List<ColaboradorDTO> elMismo = new List<ColaboradorDTO>();
                elMismo.Add(colaborador.ToDTO());
                ViewBag.elMismo = elMismo;
                ViewBag.susSubordinados = context.TablaColaboradores.All().Select(c => c.ToDTO()).ToList();
                ViewBag.susPares = consigueSusPares(colaboradorID, context).Select(p => p.ToDTO()).ToList();
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
                ProcesoEvaluacion proceso = context.TablaProcesoEvaluaciones.FindByID(idDelProceso);

                evaluadores.procesoID = idDelProceso;
                evaluadores.evaluadoID = evaluadoId;
                //evaluadores.evaluadores = new List<Colaborador>();
                evaluadores.evaluadores = new List<Evaluador>();

                CorreoController correoController = new CorreoController();
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

                    //Enviar email: katy agregó esto
                    correoController.SendEmailRH("pruebas.rhpp+RHADMIN@gmail.com",
                                                elParticipante.CorreoElectronico,
                                                "Inicio Proceso evaluacion: " + proceso.Nombre.ToUpper(),
                                                correoController.getMensajeParaEvaluador(elParticipante.ToDTO().NombreCompleto));
                    //Fin: Enviar email

                    evaluadores.evaluadores.Add(context.TablaEvaluadores.FindByID(evaluadorId));

                    context.TablaEvaluadores.AddElement(comoEvaluador);
                    CrearEvaluaciones(comoEvaluador, context);
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

                //Actualizar proceso 
                EstadoProcesoEvaluacion enProceso  = context.TablaEstadoProcesoEvaluacion.One(x => x.Descripcion.Equals(ConstantsEstadoProcesoEvaluacion.EnProceso));
                proceso.EstadoProcesoEvaluacion = enProceso;
                context.TablaProcesoEvaluaciones.ModifyElement(proceso);

                ViewBag.Area = "";
                return View();

            }
        }

        public void CrearEvaluaciones(Evaluador evaluador, DP2Context context) {

            //Guardar la evaluación
            Examen examen = new Examen();
            examen.EvaluadorID = evaluador.ID;

            examen.EstadoExamenID = context.TablaEstadoColaboradorXProcesoEvaluaciones.One(x => x.Nombre.Equals(ConstantsEstadoColaboradorXProcesoEvaluacion.Pendiente)).ID;
            context.TablaExamenes.AddElement(examen);

            //Obtener capacidades y guardarlas para cada pregunta
            int idColaboradorEvaluado = evaluador.ElEvaluado;
            ColaboradorDTO evaluado = context.TablaColaboradores.FindByID(idColaboradorEvaluado).ToDTO();
               
            // Obtener capacidades  
            IList<CompetenciaXPuesto> cxp = context.TablaCompetenciaXPuesto.Where(x=> x.PuestoID == evaluado.PuestoID);
            IList<Capacidad> listaCapacidades = new List<Capacidad>();
            foreach (CompetenciaXPuesto c in cxp) {
                int nivelID = c.NivelID;
                //Guardar competencias por examen 
                CompetenciaXExamen cxe = new CompetenciaXExamen(c, examen);
                context.TablaCompetenciaXExamen.AddElement(cxe);
                
                // Listar capacidades y guardarlas
                listaCapacidades = context.TablaCapacidades.Where( x=> x.CompetenciaID == c.CompetenciaID && x.NivelCapacidadID == c.NivelID);
                foreach(Capacidad capacidad in listaCapacidades) {
                    Pregunta p = new Pregunta();
                    p.ExamenID = examen.ID;
                    p.CapacidadID = capacidad.ID;
                    p.TextoPregunta = capacidad.Nombre;
                    p.Puntuacion = 0;
                    p.Peso = capacidad.Peso;
                    p.competenciaID = cxe.CompetenciaID;
                    context.TablaPreguntas.AddElement(p);
                }
            }   
        }

        private bool losEvaluadoresDeEsteColaboradorYaFueronElegidos(int procesoEvaluacionID, int colaboradorID, DP2Context context)
        {
            try
            {
                    if (context.TablaEvaluadores.Where(e => e.ElEvaluado == colaboradorID && e.ProcesoEnElQueParticipanID == procesoEvaluacionID).Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
            }
            catch (NullReferenceException aunSinEvaluadores)
            {
                return false;
            }

        }

        private Colaborador consigueAlEmpleado(int idEvaluado, DP2Context context) {
            return context.TablaColaboradores.FindByID(idEvaluado);
        }

        private Puesto consigueSuPerfil(int idEvaluado, DP2Context context)
        {
            Colaborador elColaborador = context.TablaColaboradores.FindByID(idEvaluado);
            foreach (ColaboradorXPuesto cxp in elColaborador.ColaboradoresPuesto)
            {
                return cxp.Puesto;
            }
            return null;
        }

        private List<PuestoXEvaluadores> consigueSusEvaluadores(int idEvaluado, DP2Context context)
        {
            Puesto perfil = consigueSuPerfil(idEvaluado, context);
            //List<PuestoXEvaluadores> puestoYSusEvaluadores();
            List<PuestoXEvaluadores> puestoYSusEvaluadores = new DP2Context().TablaPuestoXEvaluadores.Where(e => e.PuestoID == perfil.ID).ToList();
            return puestoYSusEvaluadores;
        }

        private Colaborador consigueSuJefe(int idEvaluado, DP2Context context)
        {
            Colaborador colaborador = context.TablaColaboradores.FindByID(22);
            return colaborador;
        }

        private List<Colaborador> consigueSusPares(int idEvaluado, DP2Context context)
        {
            Colaborador colaborador = context.TablaColaboradores.FindByID(idEvaluado);
            List<Colaborador> colaboradores = new List<Colaborador>();
            colaboradores.Add(colaborador);
            colaboradores.Add(colaborador);
            colaboradores.Add(colaborador);
            return colaboradores;
        }
    }
}
