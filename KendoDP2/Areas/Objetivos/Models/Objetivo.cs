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

        public int? PuestoAsignadoID { get; set; }
        public Puesto PuestoAsignado { get; set; }

        public int? TipoObjetivoBSCID { get; set; }
        public virtual TipoObjetivoBSC TipoObjetivoBSC { get; set; }

        public int? ObjetivoPadreID { get; set; }
        public Objetivo ObjetivoPadre { get; set; }
        public virtual ICollection<Objetivo> ObjetivosHijos { get; set; }
        
        public int? BSCID { get; set; }
        public virtual BSC BSC { get; set; }

        public bool IsObjetivoIntermedio { get; set; }


        public Objetivo() {
            FechaCreacion = DateTime.Now;
        }

        // Funciona para cualquier objetivo
        public int GetBSCIDPadre()
        {
            Objetivo o = this;
            while (o.ObjetivoPadreID.GetValueOrDefault() > 0)
            {
                o = o.ObjetivoPadre;
            }
            return o.BSCID.GetValueOrDefault();
        }

        // Para objetivo de BSC
        public Objetivo(string nombre, int BSCID, int TipoBSCID, int puestoID, int peso)
        {
            Nombre = nombre;
            this.BSCID = BSCID;
            FechaCreacion = DateTime.Now;
            TipoObjetivoBSCID = TipoBSCID;
            PuestoAsignadoID = puestoID;
            Peso = peso;
        }
        // Para objetivo que no es de ningun BSC
        public Objetivo(string nombre,int objetivoPadreID, int peso)  
        {
            Nombre = nombre;
            Peso = peso;
            ObjetivoPadreID = objetivoPadreID;
            FechaCreacion = DateTime.Now;
        }

        public Objetivo(ObjetivoDTO o, DP2Context context) : this()
        {
            LoadFromDTO(o, context);
        }

        public Objetivo LoadFromDTO(ObjetivoDTO o, DP2Context context)
        {

            ID = o.ID;
            Peso = o.Peso;
            Nombre = o.Nombre;
            AvanceFinal = o.AvanceFinal;
            if (o.TipoObjetivoBSCID > 0)
                TipoObjetivoBSC = context.TablaTipoObjetivoBSC.FindByID(o.TipoObjetivoBSCID);
            if (o.ObjetivoPadreID > 0)
                ObjetivoPadre = context.TablaObjetivos.FindByID(o.ObjetivoPadreID);
            if (o.BSCID > 0)
                BSC = context.TablaBSC.FindByID(o.BSCID);

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
            TipoObjetivoBSCID = o.TipoObjetivoBSCID.GetValueOrDefault();
            ObjetivoPadreID = o.ObjetivoPadreID.GetValueOrDefault();
            BSCID = o.GetBSCIDPadre();
        }

    }
}