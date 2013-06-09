using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class Competencia : DBObject
    {
        public string Nombre { get; set; }
        public int NroDeNiveles { get; set; }
        public virtual ICollection<Capacidad> Capacidades { get; set; }

        public Competencia() { }

        public Competencia(string nombre, int nroDeNiveles)
        {
            Nombre = nombre;
            NroDeNiveles = nroDeNiveles;
        }

        public Competencia(string nombre)
        {
            Nombre = nombre;
        }

        public Competencia(CompetenciaDTO dto)
        {
            LoadFromDTO(dto);
        }

        public Competencia LoadFromDTO(CompetenciaDTO dto)
        {
            ID = dto.ID;
            Nombre = dto.Nombre;
            NroDeNiveles = 3; //Por defecto siempre es 3
            return this;
        }

        public CompetenciaDTO ToDTO()
        {
            return new CompetenciaDTO(this);
        }

        public List<CompetenciaConNivelDTO> clasificarEnNiveles() 
        {
            int i = 0;

            List<CompetenciaConNivelDTO> competenciasClasificadas = new List<CompetenciaConNivelDTO>();

            for (i = 0; i < 3; i++)
            {
                competenciasClasificadas.Add(new CompetenciaConNivelDTO(this, 1, 0, false));
                competenciasClasificadas.Add(new CompetenciaConNivelDTO(this, 2, 0, false));
                competenciasClasificadas.Add(new CompetenciaConNivelDTO(this, 3, 0, false));
            }

            return competenciasClasificadas;
        }
    }

    public class CompetenciaDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.Text, ErrorMessage = "Solo se admite caracteres")]
        public string Nombre { get; set; }
        
        public CompetenciaDTO(Competencia c)
        {
            Nombre = c.Nombre;
            ID = c.ID;
        }
        public CompetenciaDTO() { }
    }

    public class CompetenciaConNivelDTO
    {

        public int ID { get; set; }
        public int CompetenciaID { get; set; }
        public string CompetenciaNombre { get; set; }
        public int Nivel { get; set; }
        public int Peso { get; set; }
        public bool Seleccionado { get; set; }


        public CompetenciaConNivelDTO(Competencia c, int nivel, int peso, bool seleccionado)
        {
            Nivel = nivel;
            CompetenciaNombre = c.Nombre;
            CompetenciaID = c.ID;
            ID = CompetenciaID * 10 + nivel;
            Peso = peso;
            Seleccionado = seleccionado;
        }
        public CompetenciaConNivelDTO(PerfilXCompetencia perfilXCompetencia)
        {
            Nivel = perfilXCompetencia.Nivel;
            CompetenciaNombre = perfilXCompetencia.Competencia.Nombre;
            ID = perfilXCompetencia.Competencia.ID * 10 + perfilXCompetencia.Nivel;
            CompetenciaID = perfilXCompetencia.Competencia.ID;
            Peso = perfilXCompetencia.Peso;
            Seleccionado = true;
        }
        public CompetenciaConNivelDTO() { }        
    }

    public class CompetenciaConPonderadoDTO
    {
        public int CompetenciaID { get; set; }
        public string CompetenciaNombre { get; set; }
        public int Porcentaje { get; set; }

        public CompetenciaConPonderadoDTO()
        {
        }

        public CompetenciaConPonderadoDTO(CompetenciaXPuesto competencia)
        {
            CompetenciaID = competencia.ID;
            CompetenciaNombre = competencia.Competencia.Nombre;

            var context = new DP2Context();
            //Nivel Máximo
            int maxLevel=0; 
            var niveles = context.TablaNivelCapacidades.All();
            if (niveles.Count > 0)
            {
                NivelCapacidad ultimo = niveles[niveles.Count - 1];
                maxLevel = ultimo.Nivel;
            }else
                //Esto es solo por si por error existe un nivel máximo 0
                maxLevel = 1;
            //Pormedio de los niveles de cada capacidad
            int sumaNivelAux = 0;
            int cont = 0;
            var capacidades = context.TablaCapacidades.Where(a => a.CompetenciaID == competencia.CompetenciaID).ToList();

            foreach(Capacidad capacidad in capacidades)
            {
                sumaNivelAux = sumaNivelAux + capacidad.NivelCapacidad.Nivel;
                cont++;
            }

            Porcentaje = (int)((sumaNivelAux / maxLevel) * 1.0 / cont * 100);
        }
    }

}