using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Evaluacion360.Models;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class ReclutamientoMobileModel
    {
    }

    public class PostulanteConCompetenciasDTO
    {
        public int IdPostulante { get; set; }
        public string Nombre { get; set; }
        public ICollection<CompetenciaConPonderadoDTO> CompetenciasPostulante { get; set; }
        public int MatchLevel { get; set; }

        public PostulanteConCompetenciasDTO(ICollection<CompetenciaConPonderadoDTO> competenciasPuesto, Postulante postulante)
        {
            IdPostulante = postulante.Colaborador.ID;
            Nombre = postulante.Nombres + " " + postulante.ApellidoPaterno + " " + postulante.ApellidoMaterno;
            //Competencias
            var context = new DP2Context();
            Puesto puesto = context.TablaColaboradoresXPuestos.Where(a => a.ColaboradorID == postulante.Colaborador.ID)
                .Select(a => a.Puesto).First();
            var CompetenciasPostulanteAux = OfertaLaboralMobileJefeDTO.ListaCompetenciasConPonderadoToDTO(puesto.CompetenciasXPuesto);
            //Filtrar las competencias que me interesan matchear
            var CompetenciasPostulanteAuxFiltrado = new List<CompetenciaConPonderadoDTO>();
            foreach (CompetenciaConPonderadoDTO competenciaColaborador in CompetenciasPostulanteAux)
            {
                if (competenciasPuesto.Any(a => a.CompetenciaNombre.Equals(competenciaColaborador.CompetenciaNombre)))
                    CompetenciasPostulanteAuxFiltrado.Add(competenciaColaborador);
            }
            //Llenar las competencias vacías
            CompetenciasPostulante = new List<CompetenciaConPonderadoDTO>();
            int cont = 0;
            foreach (CompetenciaConPonderadoDTO competenciaPuesto in competenciasPuesto)
            {
                if (!CompetenciasPostulanteAuxFiltrado
                    .Any(a => a.CompetenciaNombre.Equals(competenciaPuesto.CompetenciaNombre)))
                {
                    //crear la comp
                    CompetenciaConPonderadoDTO competenciaVacia = new CompetenciaConPonderadoDTO();
                    competenciaVacia.CompetenciaID = -1;
                    competenciaVacia.CompetenciaNombre = competenciaPuesto.CompetenciaNombre;
                    competenciaVacia.Porcentaje = 0;
                    //agregarla
                    CompetenciasPostulante.Add(competenciaVacia);
                }
                else
                {
                    CompetenciasPostulante.Add(CompetenciasPostulanteAuxFiltrado.ElementAt(cont));
                    cont++;
                }
            }
            //MatchLevel
            double sumaCompetenciasPuesto = 0;
            foreach (CompetenciaConPonderadoDTO competencia in competenciasPuesto)
            {
                sumaCompetenciasPuesto += competencia.Porcentaje;
            }
            double sumaCompetenciasColaborador = 0;
            foreach (CompetenciaConPonderadoDTO competencia in CompetenciasPostulante)
            {
                sumaCompetenciasColaborador += competencia.Porcentaje;
            }
            if (sumaCompetenciasPuesto != 0)
                MatchLevel = (int)((sumaCompetenciasColaborador / sumaCompetenciasPuesto) * 100);
            else
                MatchLevel = 100;
        }

    }
}