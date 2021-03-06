﻿using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Reportes.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int ObjetivoPadreID { get; set; }
        public List<Objetivo> ObjetivosHijos(DP2Context context)
        {
            return context.TablaObjetivos.Where(x => x.ObjetivoPadreID == this.ID).ToList();
        }
        
        public int? BSCID { get; set; }
        public virtual BSC BSC { get; set; }

        public bool IsObjetivoIntermedio { get; set; }

        public int PesoMiObjetivo { get; set; }

        public virtual ICollection<AvanceObjetivo> LosProgresos { get; set; }

        public Objetivo() {
            FechaCreacion = DateTime.Now;
        }

        // Funciona para cualquier objetivo
        public int GetBSCIDRaiz(DP2Context context)
        {
            Objetivo o = this;
            while (o.ObjetivoPadreID > 0)
            {
                o = context.TablaObjetivos.FindByID(o.ObjetivoPadreID);
            }
            return o.BSCID.GetValueOrDefault();
        }

        // Para objetivo de BSC
        public Objetivo(string nombre, int BSCID, int TipoBSCID, int puestoID, int peso, DP2Context context)
        {
            Nombre = nombre;
            BSC = context.TablaBSC.FindByID(BSCID);
            FechaCreacion = DateTime.Now;
            TipoObjetivoBSC = context.TablaTipoObjetivoBSC.FindByID(TipoBSCID);
            PuestoAsignado = context.TablaPuestos.FindByID(puestoID);
            Peso = peso;
        }
        // Para objetivo que no es de ningun BSC
        public Objetivo(string nombre,int objetivoPadreID, int peso, DP2Context context)  
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
            if (o.TipoObjetivoBSCID > 0)
                TipoObjetivoBSC = context.TablaTipoObjetivoBSC.FindByID(o.TipoObjetivoBSCID);
            ObjetivoPadreID = o.ObjetivoPadreID;
            if (o.BSCID > 0)
                BSC = context.TablaBSC.FindByID(o.BSCID);
            PesoMiObjetivo = o.PesoMiObjetivo;
            return this;
        }

        public ObjetivoDTO ToDTO(DP2Context context)
        {
            return new ObjetivoDTO(this, context);
        }

        public ObjetivoRDTO ToRDTO(DP2Context context)
        {
            return new ObjetivoRDTO(this);
        }

        public ObjetivoConPadreDTO ObjetivoConPadreDTO( DP2Context context){
            return new ObjetivoConPadreDTO(this, context);
        }


        internal void RegistrarAvancex(DP2Context context, int valor, string comentario)
        {
            //this.LosProgresos.Select(a => a.FueRevisado = true);
            //context.TablaObjetivos.ModifyElement(this);
            AvanceObjetivo avance = new AvanceObjetivo { Objetivo = this, Valor = valor, FechaCreacion = DateTime.Now.ToString("dd/MM/yyyy"), Comentario = comentario, EsRevision = false };
            context.TablaAvanceObjetivo.AddElement(avance);
        }

        internal void ActualizarPesos(DP2Context context)
        {
            List<Objetivo> Hijos = this.ObjetivosHijos(context);
            int total = Hijos.Count();
            int sumaPesos = 0;
            Hijos.ForEach(x => sumaPesos += x.Peso);
            if (total == 0)
            {
                AvanceFinal = 0;
            }
            else if (sumaPesos == 0)
            {
                double peso = 1.0 / total;
                double res = 0;
                Hijos.ForEach(x => res += peso * x.AvanceFinal);
                AvanceFinal = (int)Math.Floor(res);
            }
            else
            {
                double res = 0;
                Hijos.ForEach(x => res += x.Peso * x.AvanceFinal);
                res /= sumaPesos;
                AvanceFinal = (int)Math.Floor(res);
            }
            if (PesoMiObjetivo > 0)
            {
                int valor = LosProgresos == null || LosProgresos.Count > 0 ? LosProgresos.Last().Valor : 0;
                double peso1 = PesoMiObjetivo / 100.0;
                double peso2 = 1 - peso1;
                AvanceFinal = (int)Math.Floor(AvanceFinal * peso2 + valor * peso1);
            }
            context.TablaObjetivos.ModifyElement(this);
            Objetivo padre = context.TablaObjetivos.FindByID(ObjetivoPadreID);
            if(padre != null)
                padre.ActualizarPesos(context);

        }
    }

    //public class ObjetivoRDTO
    //{
    //    public int idObjetivo { get; set; }
    //    public string descripcion { get; set; }
    //    public int numPersonas { get; set; }
    //    public int avance { get; set; }

    //    public ObjetivoRDTO(Objetivo o)
    //    {

    //        idObjetivo = o.ID;
    //        descripcion = o.Nombre;
    //        numPersonas = 5;
    //        avance = 50;

    //    }

    //    public ObjetivoRDTO()
    //    {
    //        numPersonas = 10;
    //        avance = 50;
    //    }
    //}

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

        public string FechaCreacion { get; set; }
        public string FechaFinalizacion { get; set; }

        public List<AvanceObjetivoDTO> LosProgresos { get; set; }

        public string ComentarioUltimoAvance { get; set; }

        public int PesoMiObjetivo { get; set; }

        public ObjetivoDTO() { }

        public int AvanceFinalDeAlgunProgeso { get; set; }

        public ObjetivoDTO(Objetivo o, DP2Context context)
        {
            ID = o.ID;
            Nombre = o.Nombre;
            Peso = o.Peso;
            AvanceFinal = o.AvanceFinal;
            TipoObjetivoBSCID = o.TipoObjetivoBSCID.GetValueOrDefault();

            ObjetivoPadreID = o.ObjetivoPadreID;
            BSCID = o.GetBSCIDRaiz(context);

            FechaCreacion = o.FechaCreacion.HasValue ? o.FechaCreacion.GetValueOrDefault().ToString("D", new CultureInfo("es-ES")) : String.Empty;
            FechaFinalizacion = o.FechaFinalizacion.HasValue ? o.FechaFinalizacion.GetValueOrDefault().ToString("D", new CultureInfo("es-ES")) : String.Empty;
            PesoMiObjetivo = o.PesoMiObjetivo;
            //PeriodoID = o.PeriodoID;

            LosProgresos = o.LosProgresos == null ? new List<AvanceObjetivoDTO>() : o.LosProgresos.Select(a => a.enFormatoDTO()).ToList();

            if (LosProgresos.Count > 0)
                this.ComentarioUltimoAvance = LosProgresos.Last().Comentario;
            else
                this.ComentarioUltimoAvance = "";
            AvanceFinalDeAlgunProgeso = LosProgresos.Count > 0 ? LosProgresos.Last().Valor : 0;
        }
        
    }
}












            //LosProgresos = o.Avances == null ? new List<AvanceObjetivoDTO>() : o.Avances.Select(a => a.enFormatoDTO()).ToList();
