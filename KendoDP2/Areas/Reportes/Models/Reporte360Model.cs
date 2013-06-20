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

    public class ListaAux
    {
        public int competenciaId { get; set; }
        public string tipoEvaluador { get; set; }
        public int sumaNota { get; set; }
        public int count { get; set; }

        public ListaAux(int competenciaId, string tipoEvaluador, int sumaNota, int count)
        {
            this.competenciaId = competenciaId;
            this.tipoEvaluador = tipoEvaluador;
            this.sumaNota = sumaNota;
            this.count = count;
        }

        public ListaAux()
        {
        }
    }

    public class ProcesoReportadoDTO
    {
        public int idProceso { get; set; }
        public string procesoNombre { get; set; }
        public ICollection<NotaXTipoEvaluadorDTO> notasParciales { get; set; }
        public int notaFinal { get; set; }
        public ICollection<CompetenciasEvualuadasDTO> competenciasEvaluadas { get; set; }

        public ProcesoReportadoDTO(ColaboradorXProcesoEvaluacion proceso)
        {
            var context = new DP2Context();
            idProceso = context.TablaProcesoEvaluaciones.One(a => a.ID == proceso.ProcesoEvaluacionID).ID;
            procesoNombre = context.TablaProcesoEvaluaciones.One(a=>a.ID == proceso.ProcesoEvaluacionID).Nombre;
            //Lista de notas parciales
            var evaluadores = context.TablaEvaluadores.Where(a=>a.ElEvaluado == proceso.ColaboradorID
                && a.ProcesoEnElQueParticipanID == proceso.ProcesoEvaluacionID);
            notasParciales = ListaNotasParcialesProcesoToDTO(evaluadores).ToList();
            notaFinal = (int)proceso.Puntuacion;
            competenciasEvaluadas = ListaNotasParcialesCompetenciasToDTO(evaluadores).ToList();
        }

        public static List<CompetenciaXExamen> obtenerCompetenciasXExamen(int evaluacionId)
        {
            DP2Context context = new DP2Context();

            var examen = context.TablaExamenes.One(a=>a.EvaluadorID == evaluacionId);

            var competencias = context.TablaCompetenciaXExamen.Where(a=>a.ExamenID == examen.ID).ToList();

            return competencias;
        }

        public static string obtenerTipoEvaluador(Evaluador evaluacion)
        {
            //Jefe:
            DP2Context context = new DP2Context();
            var jefe = GestorServiciosPrivados.consigueElJefe(evaluacion.ElEvaluado,context);
            if (jefe != null)
            {
                if (jefe.ID == evaluacion.ElIDDelEvaluador)
                    return Reporte360Model.JEFE;
            }

            //Compañeros pares:
            var companheros = GestorServiciosPrivados.consigueSusCompañerosPares(evaluacion.ElEvaluado,context);
            foreach (Colaborador companhero in companheros)
            {
                if (companhero.ID == evaluacion.ElIDDelEvaluador && companhero.ID != evaluacion.ElEvaluado)
                {
                    return Reporte360Model.COMPANHERO;
                }
            }

            //Subordinados:
            var subordinados = GestorServiciosPrivados.consigueSusSubordinados(evaluacion.ElEvaluado,context);
            foreach (Colaborador subordinado in subordinados)
            {
                if (subordinado.ID == evaluacion.ElIDDelEvaluador)
                {
                    return Reporte360Model.SUBORDINADO;
                }
            }

            //El mismo
            if(evaluacion.ElEvaluado == evaluacion.ElIDDelEvaluador)
            {
                return Reporte360Model.MISMO;
            }

            return null;
        }

        public static ICollection<CompetenciasEvualuadasDTO> ListaNotasParcialesCompetenciasToDTO(ICollection<Evaluador> evaluaciones)
        {
            List<ListaAux> listaAuxiliar = new List<ListaAux>();
            List<CompetenciaXExamen> competencias;
            string tipoEvaluador;

            //Aquí se armará la estructura con la información de cada competencia por tipo de evaluador
            foreach (Evaluador evaluacion in evaluaciones)
            {
                //Obtener las competencias por examen
                competencias = obtenerCompetenciasXExamen(evaluacion.ID);
                //Obtener tipo de evaluador
                tipoEvaluador = obtenerTipoEvaluador(evaluacion);
                foreach (CompetenciaXExamen competencia in competencias)
                {
                    //En caso sea una nueva competencias
                    bool existeCompetencia = listaAuxiliar.Any(a=>a.competenciaId == competencia.CompetenciaID);
                    if (!existeCompetencia)
                    {
                        bool existeTipoEvaluador = listaAuxiliar.Any(a => a.competenciaId == competencia.CompetenciaID
                             && a.tipoEvaluador.Equals(tipoEvaluador));
                        if (!existeTipoEvaluador)
                        {
                            ListaAux listaElemento = new ListaAux(competencia.CompetenciaID, tipoEvaluador, competencia.Nota, 1);
                            listaAuxiliar.Add(listaElemento);
                        }
                    }
                    //En caso la competencia ya exista
                    else
                    {
                        bool existeTipoEvaluador = listaAuxiliar.Any(a => a.competenciaId == competencia.CompetenciaID
                             && a.tipoEvaluador.Equals(tipoEvaluador));
                        if (!existeTipoEvaluador)
                        {
                            ListaAux listaElemento = new ListaAux(competencia.CompetenciaID, tipoEvaluador, competencia.Nota, 1);
                            listaAuxiliar.Add(listaElemento);
                        }
                        else
                        {
                            ListaAux listaElemento = listaAuxiliar.Single(a=>a.tipoEvaluador.Equals(tipoEvaluador)
                                && a.competenciaId == competencia.CompetenciaID);
                            listaElemento.sumaNota += competencia.Nota;
                            listaElemento.count++;
                        }
                    }
                }
            }

            //Aquí se armará la estructura final
            List<CompetenciasEvualuadasDTO> listaCompetenciasEvaluadas = new List<CompetenciasEvualuadasDTO>();
            CompetenciasEvualuadasDTO competenciaEvaluada;
            //Obtener las competencias que son distintas en codigo
            foreach(ListaAux listaElemento in listaAuxiliar)
            {
                bool existeCompetencia = listaCompetenciasEvaluadas.Any(a=>a.competenciaId == listaElemento.competenciaId);
                if (!existeCompetencia)
                {
                    competenciaEvaluada = new CompetenciasEvualuadasDTO();
                    competenciaEvaluada.competenciaId = listaElemento.competenciaId;
                    competenciaEvaluada.competenciaNombre = new DP2Context().TablaCompetencias.One(a => a.ID == listaElemento.competenciaId).Nombre;
                    //Nota parcial
                    NotaXTipoEvaluadorDTO notaParcial;
                    notaParcial = new NotaXTipoEvaluadorDTO();
                    notaParcial.tipoEvaluador = listaElemento.tipoEvaluador;
                    notaParcial.notaParcial = listaElemento.sumaNota/listaElemento.count;
                    competenciaEvaluada.notasParciales = new List<NotaXTipoEvaluadorDTO>();
                    //Agregar la nota parcial de la competencia
                    competenciaEvaluada.notasParciales.Add(notaParcial);
                    //--
                    competenciaEvaluada.notaFinal = 0;
                    //Agregar la competenciaEvaluada
                    listaCompetenciasEvaluadas.Add(competenciaEvaluada);
                }
                else
                {
                    var competenciaEvaluadaAux = listaCompetenciasEvaluadas.Single(a=>a.competenciaId == listaElemento.competenciaId);
                    //Nota parcial
                    NotaXTipoEvaluadorDTO notaParcial;
                    notaParcial = new NotaXTipoEvaluadorDTO();
                    notaParcial.tipoEvaluador = listaElemento.tipoEvaluador;
                    notaParcial.notaParcial = listaElemento.sumaNota/listaElemento.count;
                    //Agregar la nota parcial de la competencia
                    competenciaEvaluadaAux.notasParciales.Add(notaParcial);
                }
            }

            return listaCompetenciasEvaluadas;
        }

        public static ICollection<NotaXTipoEvaluadorDTO> ListaNotasParcialesProcesoToDTO(ICollection<Evaluador> evaluadores)
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

    public class CompetenciasEvualuadasDTO
    {
        public int competenciaId { get; set; }
        public string competenciaNombre { get; set; }
        public ICollection<NotaXTipoEvaluadorDTO> notasParciales { get; set; }
        public int notaFinal { get; set; }

        public CompetenciasEvualuadasDTO() { }
    }
}