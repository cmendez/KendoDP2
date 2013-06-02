using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    //public class Evaluador : Colaborador
    //{
    //    public int ProcesoEnElQueParticipaID { get; set; }

    //    [ForeignKey("ProcesoEnElQueParticipaID")]
    //    public ProcesoEvaluacion ElProceso { get; set; }

    //    public String FaseDeLaEvaluacion { get; set; }

    //    List<Colaborador> personasALasQueEvalua { get; set; }

    //    public Evaluador()
    //    {

    //    }

    //    public Evaluador(Colaborador participante, int unCicloDeEvaluacion)
    //        : base(participante)
    //    {
    //        ProcesoEnElQueParticipaID = unCicloDeEvaluacion;
    //        FaseDeLaEvaluacion = "Pendiente";
    //    }

    //    //public EvaluadorDTO aFormatoDelCliente(Evaluador empleado)
    //    //{
    //    //    return new EvaluadorDTO(empleado);
    //    //}

    //    public EvaluadorDTO aFormatoDelCliente()
    //    {
    //        return new EvaluadorDTO(this);
    //    }

    //    public static Evaluador enrolarlo(Colaborador esteEmpleado, int aEsteProceso)
    //    {
    //        return new Evaluador(esteEmpleado, aEsteProceso);
    //    }
    //}

    public class Evaluador : DBObject
    {

        public int ElIDDelEvaluador { get; set; }

        public int ElEvaluado { get; set; }

        public int ProcesoEnElQueParticipanID { get; set; }

        [ForeignKey("ProcesoEnElQueParticipanID")]
        public ProcesoEvaluacion ElProceso { get; set; }

        public String FaseDeLaEvaluacion { get; set; }

        //public virtual List<Colaborador> personasALasQueEvalua { get; set; }

        public Evaluador()
        {

        }

        public Evaluador(int elEmpleadoEvaluado, Colaborador participante, int unCicloDeEvaluacion)
        {
            ElEvaluado = elEmpleadoEvaluado;
            ElIDDelEvaluador = participante.ID;
            ProcesoEnElQueParticipanID = unCicloDeEvaluacion;
            FaseDeLaEvaluacion = "Pendiente";
        }

        public Evaluador(Colaborador participante, int unCicloDeEvaluacion)
        {
            ElIDDelEvaluador = participante.ID;
            ProcesoEnElQueParticipanID = unCicloDeEvaluacion;
            FaseDeLaEvaluacion = "Pendiente";
        }

        //public EvaluadorDTO aFormatoDelCliente(Evaluador empleado)
        //{
        //    return new EvaluadorDTO(empleado);
        //}

        public EvaluadorDTO aFormatoDelCliente()
        {
            return new EvaluadorDTO(this);
        }

        public static Evaluador enrolarlo(Colaborador esteEmpleado, int aEsteProceso)
        {
            return new Evaluador(esteEmpleado, aEsteProceso);
        }

        public EvaluadorDTO paraElCliente()
        {
            return aFormatoDelCliente();
        }
    }

    public class EvaluadorDTO : ColaboradorDTO
    {
        public String LaEtapa { get; set; }

        public EvaluadorDTO(Evaluador participante)
            : base(new DP2Context().TablaColaboradores.One(e => e.ID == participante.ElIDDelEvaluador))
        {
            LaEtapa = participante.FaseDeLaEvaluacion;
        }


    }
}