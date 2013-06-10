using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class LogicaSubordinadosEnEvaluacion
    {

        //public static Exa
        //public static ColeccionDeExamenes
        public static ColeccionDeExamenesDeLosSubordinados consultarEstadoDeExamenes(int deLosSubordinadosDeEsteColaborador)
        {
            ColeccionDeExamenesDeLosSubordinados examenes = new ColeccionDeExamenesDeLosSubordinados();

            //using ()
            //using (DP2Context context = new DP2Context())
            using (DP2Context contexto = new DP2Context())
            {
                //ColeccionDeExamenesDeLosSubordinados examenes = new ColeccionDeExamenesDeLosSubordinados();

                //List<Colaborador> losEmpleadosASuCargo = contexto.TablaColaboradores.Where(e => e.Jefe);
                //List<Colaborador> losEmpleadosASuCargo = contexto.TablaColaboradores.Where(e => e.Jefe);
                List<Colaborador> losEmpleadosASuCargo = GestorServiciosPrivados.consigueSusSubordinados(deLosSubordinadosDeEsteColaborador);


                //contexto.TablaEvaluadores.

                //List<Evaluador> lasEvaluaciones = contexto.TablaEva
                //List<Evaluador> lasEvaluaciones = contexto.TablaEvaluadores.Where(p => losEmpleadosASuCargo.Contains(contexto.TablaColaboradores.FindByID()))
                //List<Evaluador> lasEvaluaciones = contexto.TablaEvaluadores.Where(p => losEmpleadosASuCargo.Contains(contexto.TablaColaboradores.Where(e =)))
                //List<Evaluador> lasEvaluaciones = contexto.TablaEvaluadores.Where
                //    (p => losEmpleadosASuCargo.Contains(contexto.TablaColaboradores.FindByID(p => p.)))
            }

            return examenes;


        }


        ////public static Exa
        ////public static ColeccionDeExamenes
        //public static ColeccionDeExamenesDeLosSubordinados consultarEstadoDeExamenes(int deLosSubordinadosDeEsteColaborador)
        //{
        //    ColeccionDeExamenesDeLosSubordinados examenes = new ColeccionDeExamenesDeLosSubordinados();

        //    //using ()
        //    //using (DP2Context context = new DP2Context())
        //    using (DP2Context contexto = new DP2Context())
        //    {
        //        //ColeccionDeExamenesDeLosSubordinados examenes = new ColeccionDeExamenesDeLosSubordinados();

        //        //List<Colaborador> losEmpleadosASuCargo = contexto.TablaColaboradores.Where(e => e.Jefe);
        //        //List<Colaborador> losEmpleadosASuCargo = contexto.TablaColaboradores.Where(e => e.Jefe);
        //        List<Colaborador> losEmpleadosASuCargo = GestorServiciosPrivados.consigueSusSubordinados(deLosSubordinadosDeEsteColaborador);


        //        //contexto.TablaEvaluadores.

        //        //List<Evaluador> lasEvaluaciones = contexto.TablaEva
        //        //List<Evaluador> lasEvaluaciones = contexto.TablaEvaluadores.Where(p => losEmpleadosASuCargo.Contains(contexto.TablaColaboradores.FindByID()))
        //        //List<Evaluador> lasEvaluaciones = contexto.TablaEvaluadores.Where(p => losEmpleadosASuCargo.Contains(contexto.TablaColaboradores.Where(e =)))
        //        List<Evaluador> lasEvaluaciones = contexto.TablaEvaluadores.Where(p => losEmpleadosASuCargo.Contains(contexto.TablaColaboradores.FindByID(p => p.)))
        //    }

        //    return examenes;


        //}

    }
}