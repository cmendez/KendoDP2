using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class ProcesoXEvaluado : DBObject
    {
        public int procesoID { get; set; }
        [ForeignKey("procesoID")]
        public ProcesoEvaluacion proceso { get; set; }

        public int evaluadoID { get; set; }
        [ForeignKey("evaluadoID")]
        public Colaborador colaborador { get; set; }

        //public virtual List<int> evaluadoresID { get; set; }
        //public virtual List<Colaborador> evaluadores { get; set; }
        public virtual List<Evaluador> evaluadores { get; set; }



        public ProcesoXEvaluado()
        {
            //evaluadores = new List<Colaborador>();
            evaluadores = new List<Evaluador>();
        }

        //public ProcesoXEvaluado(int p_procesoID, int p_evaluadoID, List<Colaborador> p_evaluadores)
        public ProcesoXEvaluado(int p_procesoID, int p_evaluadoID, List<Evaluador> p_evaluadores)
        {
            procesoID = p_procesoID;
            evaluadoID = p_evaluadoID;
            evaluadores = p_evaluadores;
        }

        public SupervisionASubordinadoDTO paraElCliente()
        {
            //return new SupervisionSubordinadosDTO(this);
            return SupervisionASubordinadoDTO.serializarParaVigilanciaDeSubordinados(this);
        }

    }

    public class SupervisionASubordinadoDTO
    {
        public int PersonaID { get; set; }
        public String PersonaNombre { get; set; }
        public int CargoID { get; set; }
        public String CargoNombre { get; set; }
        //String estadoEvaluacion;
        //List<Integer> evaluadorID;
        //String nombreEvaluador;
        //List<ColaboradorDTO> evaluadores;
        public List<EvaluadorDTO> Evaluadores { get; set; }
        //String faseDeSuEvaluación;


        public SupervisionASubordinadoDTO()
        {

        }

        //public static List<SupervisionSubordinadosDTO> serializarParaVigilanciaDeSubordinados(ProcesoXEvaluado elColaboradorParticipante)
        public static SupervisionASubordinadoDTO serializarParaVigilanciaDeSubordinados(ProcesoXEvaluado elColaboradorParticipante)
        {
            SupervisionASubordinadoDTO losDatosDeLaPantalla = new SupervisionASubordinadoDTO();

            losDatosDeLaPantalla.PersonaID = elColaboradorParticipante.evaluadoID;
            losDatosDeLaPantalla.PersonaNombre = elColaboradorParticipante.colaborador.Nombres;
            //cargoID = elColaboradorParticipante.colaborador.ColaboradoresPuesto.First();
            losDatosDeLaPantalla.CargoID = GestorServiciosPrivados.devolverPuestoVigente(elColaboradorParticipante.colaborador).ID;

            losDatosDeLaPantalla.Evaluadores = new List<EvaluadorDTO>();

            foreach (Evaluador participanteQueEvalua in elColaboradorParticipante.evaluadores)
            {
                losDatosDeLaPantalla.Evaluadores.Add(participanteQueEvalua.aFormatoDelCliente());
            }

            return losDatosDeLaPantalla;

        }




    }
}