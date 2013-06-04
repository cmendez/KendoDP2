using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Evaluacion360.Models;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class ReclutamientoMobile
    {
    }

    public class PostulanteConCompetenciasDTO
    {
        public int? IdPostulante { get; set; }
        public string Nombre { get; set; }
        public ICollection<CompetenciaConPonderadoDTO> CompetenciasPostulante { get; set; }
        public int MatchLevel { get; set; }

        public PostulanteConCompetenciasDTO(ICollection<CompetenciaConPonderadoDTO> competenciasPuesto, Postulante postulante)
        {
            //puede ser null?
            IdPostulante = postulante.ColaboradorID;
            Nombre = postulante.Nombres + " " + postulante.ApellidoPaterno + " " + postulante.ApellidoMaterno;
            //Competencias
            var context = new DP2Context();
            //si postulante.ColaboradorID es null, se caerá
            Colaborador colaborador = context.TablaColaboradores.Where(a => a.ID == IdPostulante).First();
            Puesto puesto = context.TablaColaboradoresXPuestos.Where(a => a.ColaboradorID == colaborador.ID).Select(a => a.Puesto).First();
            CompetenciasPostulante = OfertaLaboralMobileJefeDTO.ListaCompetenciasConPonderadoToDTO(puesto.CompetenciasXPuesto);
        }

    }
}