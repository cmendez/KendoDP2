using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class GestorServiciosPrivados
    {

        public static Colaborador consigueAlEmpleado(int utilizandoEsteIdentificador)
        {

            return new DP2Context().TablaColaboradores.FindByID(utilizandoEsteIdentificador);

        }

        public static Puesto consigueElPerfil(int DeEsteColaborador)
        {
            Colaborador elColaborador = new DP2Context().TablaColaboradores.FindByID(DeEsteColaborador);
            //elColaborador.ColaboradoresPuesto.GetEnumerator;

            foreach (ColaboradorXPuesto cxp in elColaborador.ColaboradoresPuesto)
            {
                return cxp.Puesto;
            }

            return null;

            //return new DP2Context().TablaPuestos.FindByID(idEvaluado);
            //return new Perfil("un perfil");
        }

        public static Colaborador consigueElJefe(int deEsteSubordinado)
        {
            Colaborador colaborador = new DP2Context().TablaColaboradores.FindByID(22);

            return colaborador;
        }

        public static List<Colaborador> consigueSusCompañerosPares(int deEsteEmpleado)
        {
            Colaborador colaborador = new DP2Context().TablaColaboradores.FindByID(deEsteEmpleado);
            List<Colaborador> colaboradores = new List<Colaborador>();
            colaboradores.Add(colaborador);
            colaboradores.Add(colaborador);
            colaboradores.Add(colaborador);
            return colaboradores;
        }

        public static List<Colaborador> consigueSusSubordinados(int deEsteColaborador)
        {
            Colaborador colaborador = new DP2Context().TablaColaboradores.FindByID(deEsteColaborador);
            List<Colaborador> colaboradores = new List<Colaborador>();
            colaboradores.Add(colaborador);
            colaboradores.Add(colaborador);
            colaboradores.Add(colaborador);
            return colaboradores;

        }


        public static List<Colaborador> consigueSusSubordinadosFicticios(int deEsteColaborador)
        {
            //Colaborador colaborador = new DP2Context().TablaColaboradores.FindByID(deEsteColaborador);
            List<Colaborador> colaboradores = new List<Colaborador>();
            colaboradores.Add(new DP2Context().TablaColaboradores.FindByID(2));
            colaboradores.Add(new DP2Context().TablaColaboradores.FindByID(22));
            colaboradores.Add(new DP2Context().TablaColaboradores.FindByID(15));
            return colaboradores;

        }


        public static List<ProcesoEvaluacionDTO> listaTodosLosProcesos()
        {

            using (DP2Context laBD = new DP2Context())
            {
                //String unaFrase;
                //unaFrase.First();
                List<ProcesoEvaluacionDTO> lasEvaluaciones = laBD.TablaProcesoEvaluaciones.All().Select(p => p.ToDTO()).ToList();
                return lasEvaluaciones;
            }
        }


        //public static ProcesoXEvaluado devuelveDatosDeParticipacion(int dentroDeEsteProceso, int porEsteColaborador)
        //{
        //    using (DP2Context losDatos = new DP2Context())
        //    {
        //        ProcesoXEvaluado laRelacion = losDatos.TablaProcesoXEvaluado.One(p => p.procesoID == dentroDeEsteProceso && p.evaluadoID == porEsteColaborador);
        //        //Por hacer
        //        return laRelacion;
        //    }
        //}

        public static List<Evaluador> devuelveDatosDeParticipacion(int dentroDeEsteProceso, int porEsteColaborador)
        {
            using (DP2Context losDatos = new DP2Context())
            {
                List<Evaluador> laRelacion = losDatos.TablaEvaluadores.Where(p => p.ProcesoEnElQueParticipanID == dentroDeEsteProceso && p.ElEvaluado == porEsteColaborador).ToList();
                return laRelacion;
            }
        }

        public static Puesto devolverPuestoVigente(Colaborador deEsteEmpleado)
        {
            return new DP2Context().TablaPuestos.One(p => p.ID == 2);
        }
    }
}