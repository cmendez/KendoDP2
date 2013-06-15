using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoDP2.Areas.Evaluacion360.Models
{
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

        public EvaluadorDTO ToDTO()
        {
            return new EvaluadorDTO(this);
        }

        public Evaluador2DTO ToDTOEvaluacion()
        {
            return new Evaluador2DTO(this);
        }

        //public EvaluadorSubordinadosDTO enFormatoParaElCliente()
        public ProcesoXEvaluadoXEvaluadorDTO enFormatoParaElClienteVistaSubordinados()
        {
            return new ProcesoXEvaluadoXEvaluadorDTO(this);
        }

    }

    public class EvaluadorDTO : ColaboradorDTO
    {
        public virtual ColaboradorXProcesoEvaluacion Evaluado { get; set; }
        public int EvaluadoID { get; set; }

        public virtual Colaborador EvaluadorX { get; set; }
        public int EvaluadorID { get; set; }

        public virtual Examen Evaluacion { get; set; }

        public int EvaluacionID { get; set; }

        public String LaEtapa { get; set; }

        public EvaluadorDTO(Evaluador participante)
            : base(new DP2Context().TablaColaboradores.One(e => e.ID == participante.ElIDDelEvaluador))
        {
            LaEtapa = participante.FaseDeLaEvaluacion;


        }

    }

    //Esta clase representa a un examen. Puede ser llamada tambien ProcesoXEvaluadoXEvaluadorDTO.
    public class ProcesoXEvaluadoXEvaluadorDTO
    {
        public ColaboradorDTO Evaluador { get; set; }
        public ColaboradorDTO Evaluado { get; set; }
        public ProcesoEvaluacionDTO Proceso { get; set; }
        public int ProcesoID { get; set; }
        public int ID { get; set; }
        public string Estado { get; set; }

        public int Nota { get; set; }


        public ProcesoXEvaluadoXEvaluadorDTO()
        {


        }

        public ProcesoXEvaluadoXEvaluadorDTO(Evaluador procesoXEvaluadoXEvaluador)
        {
            using (DP2Context laBD = new DP2Context())
            {
                ID = procesoXEvaluadoXEvaluador.ID;
                Evaluador = laBD.TablaColaboradores.FindByID(procesoXEvaluadoXEvaluador.ElIDDelEvaluador).ToDTO();
                Evaluado = laBD.TablaColaboradores.FindByID(procesoXEvaluadoXEvaluador.ElEvaluado).ToDTO();
                Proceso = laBD.TablaProcesoEvaluaciones.FindByID(procesoXEvaluadoXEvaluador.ProcesoEnElQueParticipanID).ToDTO();
                Estado = procesoXEvaluadoXEvaluador.FaseDeLaEvaluacion;
                ProcesoID = Proceso.ID;
                //Nota = laBD.TablaExamenes.Where(p => p.EvaluadorID == procesoXEvaluadoXEvaluador.ID)
                //Examen laPrueba = laBD.TablaExamenes.Where(p => p.EvaluadorID == procesoXEvaluadoXEvaluador.ID)
                Examen laPrueba = laBD.TablaExamenes.One(p => p.EvaluadorID == procesoXEvaluadoXEvaluador.ID);

                Estado = laBD.TablaEstadoColaboradorXProcesoEvaluaciones.FindByID(laPrueba.EstadoExamenID).Nombre;
                Nota = laPrueba.NotaExamen;


            }


        }
    }



    public class Evaluador2DTO
    {
        // evaluadoID
        // evaluadorID != colaboradorID
        public int ID { get; set; }
        public ColaboradorDTO evaluado { get; set; }
        public EstadoProcesoEvaluacionDTO estadoevaluacion { get; set; }

        public ProcesoEvaluacionDTO procesoevaluacion { get; set; }

        public int ElIDDelEvaluador { get; set; }

        public int ElEvaluado { get; set; }

        public int ProcesoEnElQueParticipanID { get; set; }
        public ColaboradorDTO colaborador { get; set; }

        public Evaluador2DTO()
        {

        }

        public Evaluador2DTO(Evaluador evaluador)
        {
            // =evaluador
            evaluado = (new DP2Context()).TablaColaboradores.FindByID(evaluador.ElEvaluado).ToDTO();
            estadoevaluacion = (new DP2Context()).TablaEstadoProcesoEvaluacion.FindByID(evaluador.ProcesoEnElQueParticipanID).ToDTO();
            ID = evaluador.ID;
            ElIDDelEvaluador = evaluador.ElIDDelEvaluador;
            ElEvaluado = evaluador.ElEvaluado;
            ProcesoEnElQueParticipanID = evaluador.ProcesoEnElQueParticipanID;

            procesoevaluacion = (new DP2Context()).TablaProcesoEvaluaciones.FindByID(evaluador.ProcesoEnElQueParticipanID).ToDTO();
        }
    }


}
















        //    public ProcesoXEvaluadoXEvaluadorDTO(Evaluador procesoXEvaluadoXEvaluador)
        //{
        //    //Evaluador = procesoXEvaluadoXEvaluador.
        //    //using ()
        //    using (DP2Context laBD = new DP2Context())
        //    {
        //        Evaluador = laBD.TablaColaboradores.FindByID(procesoXEvaluadoXEvaluador.ElIDDelEvaluador).ToDTO();
        //        Evaluado = laBD.TablaColaboradores.FindByID(procesoXEvaluadoXEvaluador.ElEvaluado).ToDTO();
        //        //Proceso = laBD.TablaColaboradores
        //        //Proceso = laBD.TablaProcesoEvaluaciones.FindByID(procesoXEvaluadoXEvaluador.ElProceso).ToDTO();
        //        Proceso = laBD.TablaProcesoEvaluaciones.FindByID(procesoXEvaluadoXEvaluador.ProcesoEnElQueParticipanID).ToDTO();
        //        //FaseDeLaEvaluacion = laBD.
        //        Estado = procesoXEvaluadoXEvaluador.FaseDeLaEvaluacion;

        //    }


        //}