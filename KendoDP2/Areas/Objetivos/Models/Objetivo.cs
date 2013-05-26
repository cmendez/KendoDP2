using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Objetivos.Models
{
    public class Objetivo : DBObject
    {
        public string Nombre { get; set; }
        public int Peso { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int AvanceFinal { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public bool IsAsignadoAPersona { get; set; } // O a BSC

        public int? CreadorID { get; set; }
        public Colaborador Creador { get; set; }

        public int? TipoObjetivoBSCID { get; set; }
        public virtual TipoObjetivoBSC TipoObjetivoBSC { get; set; }

        public int? ObjetivoPadreID { get; set; }
        public Objetivo ObjetivoPadre { get; set; }
        public virtual ICollection<Objetivo> ObjetivosHijos { get; set; }
        
        public int? BSCID { get; set; }
        public virtual BSC BSC { get; set; }


        public Objetivo() {
            FechaCreacion = DateTime.Now;
        }

        public Objetivo(string nombre,int BSDCid,int peso,int idpadre)  
        {
            Nombre = nombre;
            BSCID = 1;
            Peso = peso;
            if (idpadre != 100)
            {
                ObjetivoPadreID = idpadre;
            }
            FechaCreacion = DateTime.Now;
            CreadorID = 1;
            TipoObjetivoBSCID = BSDCid;
        }

        public Objetivo(ObjetivoDTO o) : this()
        {
            LoadFromDTO(o);
        }

        public Objetivo LoadFromDTO(ObjetivoDTO o)
        {

            ID = o.ID;
            Peso = o.Peso;
            Nombre = o.Nombre;
            AvanceFinal = o.AvanceFinal;
            IsAsignadoAPersona = o.IsAsignadoAPersona;
            CreadorID = o.CreadorID;
            TipoObjetivoBSCID = o.TipoObjetivoBSCID;
            ObjetivoPadreID = o.ObjetivoPadreID;
            BSCID = o.BSCID;

            //PeriodoID = o.PeriodoID;

            return this;

        }

        public ObjetivoDTO ToDTO()
        {
            return new ObjetivoDTO(this);
        }

        public ObjetivoRDTO ToRDTO()
        {
            return new ObjetivoRDTO(this);
        }
    }

    public class ObjetivoRDTO
    {
        public int idObjetivo { get; set; }
		public string descripcion { get; set; }
		public int numPersonas { get; set; }
		public int avance { get; set; }

        public ObjetivoRDTO(Objetivo o){

            idObjetivo = o.ID;
            descripcion = o.Nombre;
            numPersonas = 5;
            avance = 50;

        }

        public ObjetivoRDTO()
        {
            numPersonas = 10;
            avance = 50;
        }
    }

    public class ObjetivoDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public string Nombre { get; set; }
        public int Peso { get; set; }
        public int AvanceFinal { get; set; }
        public bool IsAsignadoAPersona { get; set; }
        [DisplayName("Creador")]
        public int CreadorID { get; set; }
        public int TipoObjetivoBSCID { get; set; }
        public int ObjetivoPadreID { get; set; }
        public int BSCID { get; set; }
        
        public ObjetivoDTO() { }
        


        public ObjetivoDTO(Objetivo o)
        {
            ID = o.ID;
            Nombre = o.Nombre;
            Peso = o.Peso;
            AvanceFinal = o.AvanceFinal;
            IsAsignadoAPersona = o.IsAsignadoAPersona;
            CreadorID = o.CreadorID.GetValueOrDefault();
            TipoObjetivoBSCID = o.TipoObjetivoBSCID.GetValueOrDefault();
            ObjetivoPadreID = o.ObjetivoPadreID.GetValueOrDefault();
            BSCID = o.BSCID.GetValueOrDefault();

            //PeriodoID = o.PeriodoID;

        }

    }
}