﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoDP2.Areas.Evaluacion360.Models;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class OfertaLaboral : DBObject
    {

        public int PuestoID { get; set; }
        public virtual Puesto Puesto { get; set; }

        public int AreaID { get; set; }
        public virtual Area Area { get; set; }

        public int ResponsableID { get; set; }
        public virtual Colaborador Responsable { get; set; }

        public int EstadoSolicitudOfertaLaboralID { get; set; }
        public virtual EstadosSolicitudOfertaLaboral EstadoSolicitudOfertaLaboral { get; set; }

        public string FechaRequerimiento { get; set; }

        public string FechaFinVigenciaSolicitud { get; set; }
        
        public string Descripcion { get; set; }

        public int ModoPublicacionOfertaLaboralID { get; set; }
        public virtual ModoSolicitudOfertaLaboral ModoSolicitudOfertaLaboral { get; set; }

        //public virtual ICollection<Capacidad> ListaCapacidades { get; set; }
        
        public virtual ICollection<Postulante> Postulantes { get; set; }


        public OfertaLaboral(OfertaLaboralDTO o) : this()
        {
            LoadFromDTO(o);
        }

        //Constructor
        public OfertaLaboral()
        {
        }

        public OfertaLaboral LoadFromDTO(OfertaLaboralDTO o)
        {
            ID = o.ID;
            PuestoID = o.PuestoID;
            AreaID = o.AreaID;
            ResponsableID = o.ResponsableID;
            ModoPublicacionOfertaLaboralID = o.ModoPublicacionID;
            EstadoSolicitudOfertaLaboralID = o.EstadoSolicitudOfertaLaboralID;
            Descripcion = o.Descripcion;
            FechaFinVigenciaSolicitud = o.FechaFinRequerimiento;
            FechaRequerimiento = o.FechaRequerimiento;

            return this;
        }

        public OfertaLaboralDTO ToDTO()
        {
            return new OfertaLaboralDTO(this);
        }
    }

    public class OfertaLaboralDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
        [DisplayName("Puesto")]
        public int PuestoID { get; set; }

        [DisplayName("Area")]
        public int AreaID { get; set; }


        [DisplayName("Responsable")]
        public int ResponsableID { get; set; }

        
        [DisplayName("Tipo Convocatoria")]
        public int ModoPublicacionID { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [DisplayName("Fecha de Registro")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaRequerimiento { get; set; }

        [DisplayName("Fecha Límite de Solicitud")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaFinRequerimiento { get; set; }

        [DisplayName("Estado de Solicitud")]
        public int EstadoSolicitudOfertaLaboralID { get; set; }

        
        public OfertaLaboralDTO() { }

        public OfertaLaboralDTO(OfertaLaboral o)
        {
            ID = o.ID;
            PuestoID = o.PuestoID;
            AreaID = o.AreaID;
            ResponsableID = o.ResponsableID;
            EstadoSolicitudOfertaLaboralID = o.EstadoSolicitudOfertaLaboralID;
            ModoPublicacionID = o.ModoPublicacionOfertaLaboralID;
            FechaRequerimiento = o.FechaRequerimiento;
            FechaFinRequerimiento = o.FechaFinVigenciaSolicitud;
            Descripcion = o.Descripcion;
            
            
        }
    }
}