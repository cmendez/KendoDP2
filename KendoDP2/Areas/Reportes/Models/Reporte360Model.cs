using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;


namespace KendoDP2.Areas.Reportes.Models
{

    public class Reporte360Model
    {
        public const string JEFE = "Jefe";
    }

    public class ProcesoReportadoDTO
    {
        public string procesoNombre { get; set; }
        public ICollection<NotaXTipoEvaluadorDTO> notasParciales { get; set; }
        public int notaFinal { get; set; }

        public ProcesoReportadoDTO(ColaboradorXProcesoEvaluacion proceso)
        {
            var context = new DP2Context();
            procesoNombre = context.TablaProcesoEvaluaciones.One(a=>a.ID == proceso.ProcesoEvaluacionID).Nombre;
            //Lista de notas parciales
            var evaluadores = context.TablaEvaluadores.Where(a=>a.ElEvaluado == proceso.ColaboradorID
                && a.ProcesoEnElQueParticipanID == proceso.ProcesoEvaluacionID);
            notasParciales = ListaNotasParcialesToDTO(evaluadores).ToList();
            notaFinal = (int)proceso.Puntuacion;
        }

        public static ICollection<NotaXTipoEvaluadorDTO> ListaNotasParcialesToDTO(ICollection<Evaluador> evaluadores)
        {
            List<NotaXTipoEvaluadorDTO> ListaNotasDTO = new List<NotaXTipoEvaluadorDTO>();
            NotaXTipoEvaluadorDTO notaParcial;

            //Jefes:
            DP2Context context = new DP2Context();
            notaParcial = new NotaXTipoEvaluadorDTO();
            foreach (Evaluador evaluador in evaluadores)
            {
                var jefeAux = GestorServiciosPrivados.consigueElJefe(evaluador.ElEvaluado, context);
                if (jefeAux != null)
                    if (jefeAux.ID == evaluador.ElIDDelEvaluador)
                    {
                        notaParcial.notaParcial = context.TablaExamenes.One(a=>a.EvaluadorID == evaluador.ID).NotaExamen;
                        notaParcial.tipoEvaluador = Reporte360Model.JEFE;
                        break;
                    }
            }
            ListaNotasDTO.Add(notaParcial);

            //Compañeros Pares:


            //Subordinados:

            return ListaNotasDTO;
        }
    }

    public class NotaXTipoEvaluadorDTO
    {
        public string tipoEvaluador{get;set;}
        public int notaParcial {get;set;}

        public NotaXTipoEvaluadorDTO()
        {

        }
    }


}