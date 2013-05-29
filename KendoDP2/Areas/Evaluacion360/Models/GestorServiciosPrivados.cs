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

    }
}