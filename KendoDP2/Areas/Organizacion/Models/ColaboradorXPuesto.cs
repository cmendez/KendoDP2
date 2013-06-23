using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class ColaboradorXPuesto: DBObject
    {
        public int PuestoID { get; set; }
        public virtual Puesto Puesto { get; set; }
        
        public int ColaboradorID { get; set; }
        public virtual Colaborador Colaborador { get; set; }

        public int Sueldo { get; set; }
        public DateTime? FechaIngresoPuesto { get; set; }
        public DateTime? FechaSalidaPuesto { get; set; }
        public string Comentarios { get; set; }

        public ColaboradorXPuesto(Puesto puesto, Colaborador colaborador, DateTime FechaIngreso,DateTime FechaSalida,int sueldo, string comentario)
        {
            this.PuestoID=puesto.ID;
            this.Puesto = puesto;
            this.Colaborador=colaborador;
            this.ColaboradorID= colaborador.ID;
            this.Sueldo=sueldo;
            this.FechaIngresoPuesto=FechaIngreso;
            this.FechaSalidaPuesto=FechaSalida;
        }


        public ColaboradorXPuestoDTO ToDTO()
        {
            return new ColaboradorXPuestoDTO(this);
        }
        public ColaboradorXPuesto LoadFromDTO(ColaboradorXPuestoDTO dto)
        {
            this.ID = dto.ID;
            this.IsEliminado = dto.IsEliminado;
            this.PuestoID = dto.PuestoID;
            this.ColaboradorID =dto.PuestoID;
            this.Sueldo=dto.Sueldo;
            this.Comentarios = dto.Comentarios;
            return this;    
        }

        public ColaboradorXPuesto() { }
    }

    public class ColaboradorXPuestoDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        public bool IsEliminado { get; set; }

        [Display(Name = "Modificar")]
        public bool ModificarPuesto { get; set; }

        [Display(Name = "Nuevo")]
        public bool AgregarPuesto { get; set; }

        [Display(Name="Posee contrato indefinido")]
        public bool ContratoIndefinido { get; set; }

        public ColaboradorDTO Colaborador { get; set; }
        public int PuestoID { get; set; }
        public int AreaID { get; set; }
        public PuestoDTO Puesto { get; set; }
        public int Sueldo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaIngresoPuesto { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaSalidaPuesto { get; set; }

        public string Comentarios { get; set; }
        
        public ColaboradorXPuestoDTO(ColaboradorXPuesto cxp)
        {
            ID = cxp.ID;
            Colaborador = cxp.Colaborador.ToDTO();
            Puesto = cxp.Puesto.ToDTO();
            PuestoID = Puesto.ID;
            AreaID = Puesto.AreaID;
            IsEliminado = cxp.IsEliminado;
            Sueldo = cxp.Sueldo;
            FechaIngresoPuesto = cxp.FechaIngresoPuesto.ToString();
            FechaSalidaPuesto = cxp.FechaSalidaPuesto.ToString();
            Comentarios = cxp.Comentarios;
        }

        public ColaboradorXPuestoDTO() { }
    }
}