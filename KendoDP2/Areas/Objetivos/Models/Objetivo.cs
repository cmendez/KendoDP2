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

        public int CreadorID { get; set; }
        public Colaborador Creador { get; set; }

        public int TipoObjetivoBSCID { get; set; }
        public virtual TipoObjetivoBSC TipoObjetivoBSC { get; set; }

        public int ObjetivoPadreID { get; set; }

        public int BSCID { get; set; }
        public virtual BSC BSC { get; set; }

        public Objetivo() {
            FechaCreacion = DateTime.Now;
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
            return this;
        }

        public ObjetivoDTO ToDTO()
        {
            return new ObjetivoDTO(this);
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
            CreadorID = o.CreadorID;
            TipoObjetivoBSCID = o.TipoObjetivoBSCID;
            ObjetivoPadreID = o.ObjetivoPadreID;
            BSCID = o.BSCID;
        }
    }
}