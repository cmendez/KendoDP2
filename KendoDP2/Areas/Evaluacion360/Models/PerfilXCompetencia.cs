using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class PerfilXCompetencia : DBObject
    {
        public Perfil Perfil { get; set; }
        public Competencia Competencia { get; set; }
        public int Nivel { get; set; }
        public int Peso { get; set; }

        public PerfilXCompetencia() { 
        }

        public PerfilXCompetencia(Perfil perfil, Competencia competencia, int nivel, int peso)
        {
            Perfil = perfil;
            Competencia = competencia;
            Nivel = nivel;
            Peso = peso;
        }

        public PerfilXCompetencia CargarDesdeDTO(int perfilID , CompetenciaConNivelDTO competenciaConNivel) 
        { 
            //Buscar Perfil
            Perfil = null;
            //Buscar competencia
            Competencia = null;
            Nivel = competenciaConNivel.Nivel;
            Peso = competenciaConNivel.Peso;
            return this;
        }

        public CompetenciaConNivelDTO aDTO()
        {
            return new CompetenciaConNivelDTO(this);
        }
    }
}