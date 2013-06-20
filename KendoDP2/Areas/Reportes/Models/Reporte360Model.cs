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
        public const string COMPANHERO = "Compañeros pares";
        public const string SUBORDINADO = "Subordinados";
        public const string MISMO = "Él mismo";
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
            //en la lista evaluadores, se tienen todas las evaluaciones del colaborador en un solo proceso
            List<NotaXTipoEvaluadorDTO> ListaNotasDTO = new List<NotaXTipoEvaluadorDTO>();
            NotaXTipoEvaluadorDTO notaParcial = null;

            //Jefes:
            DP2Context context = new DP2Context();
            foreach (Evaluador evaluador in evaluadores)
            {
                var jefeAux = GestorServiciosPrivados.consigueElJefe(evaluador.ElEvaluado, context);
                if (jefeAux != null)
                {
                    if (jefeAux.ID == evaluador.ElIDDelEvaluador)
                    {
                        notaParcial = new NotaXTipoEvaluadorDTO();
                        notaParcial.notaParcial = context.TablaExamenes.One(a => a.EvaluadorID == evaluador.ID).NotaExamen;
                        notaParcial.tipoEvaluador = Reporte360Model.JEFE;
                        ListaNotasDTO.Add(notaParcial);
                        break;
                    }
                }
            }


            //Compañeros Pares:
            int notaSuma = 0;
            int count = 0;
            int notaPromedio = 0;
            foreach (Evaluador evaluador in evaluadores)
            {
                var companheros = GestorServiciosPrivados.consigueSusCompañerosPares(evaluador.ElEvaluado, context);
                if (companheros != null)
                {
                    foreach (Colaborador companhero in companheros)
                    {
                        if (companhero.ID == evaluador.ElIDDelEvaluador && companhero.ID != evaluador.ElEvaluado)
                        {
                            count++;
                            notaSuma += context.TablaExamenes.One(a => a.EvaluadorID == evaluador.ID).NotaExamen; 
                            break;
                        }
                    }
                }
            }
            if (count != 0)
            {
                notaPromedio = notaSuma / count;
                notaParcial = new NotaXTipoEvaluadorDTO();
                notaParcial.notaParcial = notaPromedio;
                notaParcial.tipoEvaluador = Reporte360Model.COMPANHERO;
                ListaNotasDTO.Add(notaParcial);
            }


            //Subordinados:
            notaSuma = 0;
            count = 0;
            notaPromedio = 0;
            foreach (Evaluador evaluador in evaluadores)
            {
                var subordinados = GestorServiciosPrivados.consigueSusSubordinados(evaluador.ElEvaluado, context);
                if (subordinados != null)
                {
                    foreach (Colaborador subordinado in subordinados)
                    {
                        if (subordinado.ID == evaluador.ElIDDelEvaluador)
                        {
                            count++;
                            notaSuma += context.TablaExamenes.One(a => a.EvaluadorID == evaluador.ID).NotaExamen;
                            break;
                        }
                    }
                }
            }
            if (count != 0)
            {
                notaPromedio = notaSuma / count;
                notaParcial = new NotaXTipoEvaluadorDTO();
                notaParcial.notaParcial = notaPromedio;
                notaParcial.tipoEvaluador = Reporte360Model.SUBORDINADO;
                ListaNotasDTO.Add(notaParcial);
            }

            //El mismo:
            foreach (Evaluador evaluador in evaluadores)
            {
                if (evaluador.ElEvaluado == evaluador.ElIDDelEvaluador)
                {
                    notaParcial = new NotaXTipoEvaluadorDTO();
                    notaParcial.notaParcial = context.TablaExamenes.One(a => a.EvaluadorID == evaluador.ID).NotaExamen;
                    notaParcial.tipoEvaluador = Reporte360Model.MISMO;
                    ListaNotasDTO.Add(notaParcial);
                    break;
                }
            }

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