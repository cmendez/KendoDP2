using KendoDP2.Areas.Personal.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
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
        public Boolean IsAsignadoAPersona { get; set; } // O a BSC

        public int CreadorID { get; set; }
        public Colaborador Creador { get; set; }

        public int TipoObjetivoBSCID { get; set; }
        public TipoObjetivoBSC TipoObjetivoBSC { get; set; }

        public int ObjetivoPadreID { get; set; }

        public int BSCID { get; set; }
        public BSC BSC { get; set; }
    }
}