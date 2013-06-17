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

        public static Colaborador consigueElJefe(int deEsteSubordinado, DP2Context contexto)
        {
            ColaboradorXPuesto cxp = contexto.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == deEsteSubordinado && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto) && !x.IsEliminado);
            Puesto puestoSuperior = contexto.TablaPuestos.FindByID(cxp.PuestoID).PuestoSuperior;
            ColaboradorXPuesto jefePuesto = contexto.TablaColaboradoresXPuestos.One(x => x.PuestoID == puestoSuperior.ID && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto) && !x.IsEliminado);
            Colaborador elJefe = contexto.TablaColaboradores.FindByID(jefePuesto.ColaboradorID);
            return elJefe;
        }

        public static List<Colaborador> consigueSusCompañerosPares(int deEsteEmpleado, DP2Context contexto)
        {
            Colaborador suJefe = consigueElJefe(deEsteEmpleado, contexto);
            //List<Colaborador> losSubordinadosDeEseJefe = consigueSusSubordinados(deEsteEmpleado, contexto);
            List<Colaborador> losSubordinadosDeEseJefe = consigueSusSubordinados(suJefe.ID, contexto);
            return losSubordinadosDeEseJefe;
        }

        public static List<Colaborador> consigueSusSubordinados(int deEsteColaborador, DP2Context contexto)
        {
            ColaboradorXPuesto cxp = contexto.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == deEsteColaborador && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto) && !x.IsEliminado);
            List<Puesto> puestosSubordinados = contexto.TablaPuestos.Where(c => c.PuestoSuperiorID == cxp.PuestoID);

            List<Colaborador> empleadosASuCargo = new List<Colaborador>();

            foreach (Puesto cargo in puestosSubordinados)
            {
                ColaboradorXPuesto subordinadoXPuesto = contexto.TablaColaboradoresXPuestos.One(c => c.PuestoID == cargo.ID && (c.FechaSalidaPuesto == null || DateTime.Today <= c.FechaSalidaPuesto));

                if (subordinadoXPuesto != null)
                {
                    Colaborador subordinado = contexto.TablaColaboradores.FindByID(subordinadoXPuesto.ColaboradorID);
                    empleadosASuCargo.Add(subordinado);
                }
            }

            return empleadosASuCargo;

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

        public static int GetPesoPorEvaluador(int evaluadoID, int evaluadorID, int puestoEvaluadorID, DP2Context context)
        {
            List<Colaborador> subordinados = GestorServiciosPrivados.consigueSusSubordinados(evaluadoID, context);
            List<Colaborador> pares = GestorServiciosPrivados.consigueSusCompañerosPares(evaluadoID, context);
            Colaborador jefe = GestorServiciosPrivados.consigueElJefe(evaluadoID, context);
            int peso = 0;

            // Verificar si es el mismo
            if (evaluadorID == evaluadoID)
            {
                PuestoXEvaluadores p = context.TablaPuestoXEvaluadores.One(x => x.PuestoID == puestoEvaluadorID && x.ClaseEntorno == ConstantesClaseEntornoPuestoEvaluadores.El_mismo);
                peso = p.Peso;
                return peso;
            }
            // Verificar si es subordinado
            var subordinado = subordinados.Where(x => x.ID == evaluadorID);
            if (subordinado != null && subordinado.Count() > 0)
            {
                PuestoXEvaluadores p = context.TablaPuestoXEvaluadores.One(x => x.PuestoID == puestoEvaluadorID && x.ClaseEntorno == ConstantesClaseEntornoPuestoEvaluadores.Subordinados);
                if (p.Cantidad > 0)
                {
                    peso = p.Peso / p.Cantidad;
                }
                else
                    peso = p.Peso;
                return peso;
            }

            // verificar si es un par
            var par = pares.Where(x => x.ID == evaluadorID);
            if (par != null && pares.Count() > 0)
            {
                PuestoXEvaluadores p = context.TablaPuestoXEvaluadores.One(x => x.PuestoID == puestoEvaluadorID && x.ClaseEntorno == ConstantesClaseEntornoPuestoEvaluadores.Pares);
                if (p.Cantidad > 0)
                {
                    peso = p.Peso / p.Cantidad;
                }
                else
                    peso = p.Peso;
                return peso;
            }
            if (jefe != null)
            {
                PuestoXEvaluadores p = context.TablaPuestoXEvaluadores.One(x => x.PuestoID == puestoEvaluadorID && x.ClaseEntorno == ConstantesClaseEntornoPuestoEvaluadores.Jefe);
                if (p.Cantidad > 0)
                {
                    peso = p.Peso / p.Cantidad;
                }
                else
                    peso = p.Peso;
                return peso;
            }
            return peso;
        }
    }
}



























        ////public static Colaborador consigueElJefe(int deEsteSubordinado)
        //public static Colaborador consigueElJefe(int deEsteSubordinado, DP2Context contexto)
        //{
        //    //Colaborador colaborador = new DP2Context().TablaColaboradores.FindByID(22);

        //    //using (DP2Context contexto )

        //    //Colaborador elSuperior = contexto.TablaColaboradores.FindByID(deEsteSubordinado).ToDTO();
        //    //ColaboradorDTO elSuperior = contexto.TablaColaboradores.FindByID(deEsteSubordinado).ToDTO();
        //    ColaboradorDTO elEmpleado = contexto.TablaColaboradores.FindByID(deEsteSubordinado).ToDTO();

        //    //Puesto suCargo = contexto.TablaPuestos.FindByID(elSuperior.PuestoID);
        //    Puesto suCargo = contexto.TablaPuestos.FindByID(elEmpleado.PuestoID);

        //    //suCargo.PuestoSuperior;

        //    //Colaborador elJefe
        //    Colaborador elSuperior = contexto.TablaColaboradores.One(c => c.ToDTO().PuestoID == suCargo.PuestoSuperiorID);


        //    //return colaborador;
        //    return elSuperior;
        //}






        ////public static Colaborador consigueElJefe(int deEsteSubordinado)
        //public static Colaborador consigueElJefe(int deEsteSubordinado, DP2Context contexto)
        //{
        //    ColaboradorXPuesto cxp = contexto.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == deEsteSubordinado && !x.IsEliminado);
        //    //ColaboradorXPuesto cxp = contexto.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == deEsteSubordinado && x.FechaIngresoPuesto <= DateTime.Today && DateTime.Today <= x.FechaSalidaPuesto && !x.IsEliminado);
        //    //ColaboradorXPuesto cxp = contexto.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == deEsteSubordinado && x.FechaIngresoPuesto <= DateTime.Today && DateTime.Today <= x.FechaSalidaPuesto && !x.IsEliminado);
        //    Puesto puestoSuperior = contexto.TablaPuestos.FindByID(cxp.PuestoID).PuestoSuperior;
        //    ColaboradorXPuesto jefePuesto = contexto.TablaColaboradoresXPuestos.One(x => x.PuestoID == puestoSuperior.ID && !x.IsEliminado);
        //    //ColaboradorXPuesto jefePuesto = contexto.TablaColaboradoresXPuestos.One(x => x.PuestoID == puestoSuperior.ID && x.FechaIngresoPuesto <= DateTime.Today && DateTime.Today <= x.FechaSalidaPuesto && !x.IsEliminado);
        //    Colaborador elJefe = contexto.TablaColaboradores.FindByID(jefePuesto.ColaboradorID);
        //    return elJefe;
        //}






        ////public static List<Colaborador> consigueSusSubordinados(int deEsteColaborador)
        //public static List<Colaborador> consigueSusSubordinados(int deEsteColaborador, DP2Context contexto)
        //{
        //    //Colaborador colaborador = new DP2Context().TablaColaboradores.FindByID(deEsteColaborador);
        //    //List<Colaborador> colaboradores = new List<Colaborador>();
        //    //colaboradores.Add(colaborador);
        //    //colaboradores.Add(colaborador);
        //    //colaboradores.Add(colaborador);
        //    //return colaboradores;

        //    //ColaboradorXPuesto cxp = contexto.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == deEsteSubordinado && !x.IsEliminado);
        //    ColaboradorXPuesto cxp = contexto.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == deEsteColaborador && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto) && !x.IsEliminado);
        //    //Puesto puestoSuperior = contexto.TablaPuestos.FindByID(cxp.PuestoID).PuestoSuperior;
        //    ////ColaboradorXPuesto jefePuesto = contexto.TablaColaboradoresXPuestos.One(x => x.PuestoID == puestoSuperior.ID && !x.IsEliminado);
        //    //ColaboradorXPuesto jefePuesto = contexto.TablaColaboradoresXPuestos.One(x => x.PuestoID == puestoSuperior.ID && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto) && !x.IsEliminado);
        //    //Colaborador elJefe = contexto.TablaColaboradores.FindByID(jefePuesto.ColaboradorID);
        //    //return elJefe;

        //    //Puesto suPuesto = contexto.TablaPuestos.One(cxp.PuestoID)
        //    //Puesto suPuesto = contexto.TablaPuestos.FindByID(cxp.PuestoID);
        //    List<Puesto> puestosSubordinados = contexto.TablaPuestos.Where(c => c.PuestoSuperiorID == cxp.PuestoID);

        //    //List<Colaborador> empleadosSubord
        //    List<Colaborador> empleadosASuCargo = new List<Colaborador>();
        //    //foreach(Pu)

        //    foreach (Puesto cargo in puestosSubordinados)
        //    {
        //        //Colaborador subordinado = contexto.TablaColaboradores.FindByID();
        //        ColaboradorXPuesto subordinadoXPuesto = contexto.TablaColaboradoresXPuestos.One(c => c.ID == cargo.ID && (c.FechaSalidaPuesto == null || DateTime.Today <= c.FechaSalidaPuesto));
        //        Colaborador subordinado = contexto.TablaColaboradores.FindByID(subordinadoXPuesto.ColaboradorID);
        //        empleadosASuCargo.Add(subordinado);
        //    }

        //    return empleadosASuCargo;

        //}





        //public static Colaborador consigueElJefe(int deEsteSubordinado, DP2Context contexto)
        //{
        //    //ColaboradorXPuesto cxp = contexto.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == deEsteSubordinado && !x.IsEliminado);
        //    ColaboradorXPuesto cxp = contexto.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == deEsteSubordinado && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto) && !x.IsEliminado);
        //    Puesto puestoSuperior = contexto.TablaPuestos.FindByID(cxp.PuestoID).PuestoSuperior;
        //    //ColaboradorXPuesto jefePuesto = contexto.TablaColaboradoresXPuestos.One(x => x.PuestoID == puestoSuperior.ID && !x.IsEliminado);
        //    ColaboradorXPuesto jefePuesto = contexto.TablaColaboradoresXPuestos.One(x => x.PuestoID == puestoSuperior.ID && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto) && !x.IsEliminado);
        //    Colaborador elJefe = contexto.TablaColaboradores.FindByID(jefePuesto.ColaboradorID);
        //    return elJefe;
        //}




//public static List<Colaborador> consigueSusCompañerosPares(int deEsteEmpleado)
//{
//    Colaborador colaborador = new DP2Context().TablaColaboradores.FindByID(deEsteEmpleado);
//    List<Colaborador> colaboradores = new List<Colaborador>();
//    colaboradores.Add(colaborador);
//    colaboradores.Add(colaborador);
//    colaboradores.Add(colaborador);
//    return colaboradores;
//}






        ////public static List<Colaborador> consigueSusCompañerosPares(int deEsteEmpleado)
        //public static List<Colaborador> consigueSusCompañerosPares(int deEsteEmpleado, DP2Context contexto)
        //{
        //    //ColaboradorXPuesto cxp = contexto.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == deEsteEmpleado && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto) && !x.IsEliminado);
        //    //Puesto puestoSuperior = contexto.TablaPuestos.FindByID(cxp.PuestoID).PuestoSuperior;
        //    Colaborador suJefe = consigueElJefe(deEsteEmpleado, contexto);
        //    List<Colaborador> losSubordinadosDeEseJefe = consigueSusSubordinados(deEsteEmpleado, contexto);

        //    return losSubordinadosDeEseJefe;
        //}



        //public static List<Colaborador> consigueSusSubordinados(int deEsteColaborador, DP2Context contexto)
        //{
        //    ColaboradorXPuesto cxp = contexto.TablaColaboradoresXPuestos.One(x => x.ColaboradorID == deEsteColaborador && (x.FechaSalidaPuesto == null || DateTime.Today <= x.FechaSalidaPuesto) && !x.IsEliminado);
        //    List<Puesto> puestosSubordinados = contexto.TablaPuestos.Where(c => c.PuestoSuperiorID == cxp.PuestoID);

        //    List<Colaborador> empleadosASuCargo = new List<Colaborador>();

        //    foreach (Puesto cargo in puestosSubordinados)
        //    {
        //        //ColaboradorXPuesto subordinadoXPuesto = contexto.TablaColaboradoresXPuestos.One(c => c.ID == cargo.ID && (c.FechaSalidaPuesto == null || DateTime.Today <= c.FechaSalidaPuesto));
        //        ColaboradorXPuesto subordinadoXPuesto = contexto.TablaColaboradoresXPuestos.One(c => c.PuestoID == cargo.ID && (c.FechaSalidaPuesto == null || DateTime.Today <= c.FechaSalidaPuesto));

        //        //if (subordinadoXPuesto == null)
        //        ////Si subordinadoXPuesto es igual a null
        //        ////Si ese puesto
        //        //Si nadie ha ocupado ese puesto, entonces subordinadoXPuesto es igual a nulo.
        //        if (subordinadoXPuesto != null)
        //        {
        //            Colaborador subordinado = contexto.TablaColaboradores.FindByID(subordinadoXPuesto.ColaboradorID);
        //            empleadosASuCargo.Add(subordinado);
        //        }
        //    }

        //    return empleadosASuCargo;

        //}