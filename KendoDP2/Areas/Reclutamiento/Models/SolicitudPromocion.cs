using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class SolicitudPromocion: DBObject
    {
        public int PuestoID { get; set; }
        public virtual Puesto Puesto { get; set; }

        public int AreaID { get; set; }
        public virtual Area Area { get; set; }

        public int ResponsableID { get; set; }
        public virtual Colaborador Responsable { get; set; }

        public int AscendidoID { get; set; }
        public virtual Colaborador Ascendido { get; set; }

        public int EstadoSolicitudOfertaLaboralID { get; set; }
        public virtual EstadosSolicitudOfertaLaboral EstadoSolicitudOfertaLaboral { get; set; }

        public string FechaRequerimiento { get; set; }
        public string FechaFinVigenciaSolicitud { get; set; }
        public string FechaInicioNuevoPuesto {get; set;}

        public string Descripcion { get; set; }

        public int SueldoTentativo { get; set; }

        public string Comentarios { get; set; }

     //   [InverseProperty("OfertaLaboral")]
      //  public virtual ICollection<OfertaLaboralXPostulante> Postulantes { get; set; }

        public string FechaAprobacion { get; set; }

        public SolicitudPromocion(SolicitudPromocionDTO p) : this()
        {
            LoadFromDTO(p);
        }

        //Constructor
        public SolicitudPromocion()
        {
        }

        public SolicitudPromocion LoadFromDTO(SolicitudPromocionDTO o)
        {
            ID = o.ID;
            PuestoID = o.PuestoID;
            AreaID = o.AreaID;
            ResponsableID = o.ResponsableID;
            EstadoSolicitudOfertaLaboralID = o.EstadoSolicitudOfertaLaboralID;
            Descripcion = o.Descripcion;
            FechaFinVigenciaSolicitud = o.FechaFinRequerimiento;
            FechaRequerimiento = o.FechaRequerimiento;
            SueldoTentativo = o.SueldoTentativo;
            Comentarios = o.Comentarios;
            FechaAprobacion = o.FechaAprobacion;
            FechaInicioNuevoPuesto = o.FechaInicioNuevoPuesto;
            AscendidoID = o.AscendidoID;
            return this;
        }

        public SolicitudPromocionDTO ToDTO()
        {
            return new SolicitudPromocionDTO(this);
        }

        
        /*
        public OfertaLaboralMobileJefeDTO ToMobileJefeDTO(string userName)
        {
            return new OfertaLaboralMobileJefeDTO (this, userName);
        }
    */
    
    }


    public class SolicitudPromocionDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
        [DisplayName("Puesto")]
        [UIHint("GridForeignKey")]
        public int PuestoID { get; set; }

        public string Puesto { get; set; }

        [DisplayName("Area")]
        [UIHint("GridForeignKey")]
        public int AreaID { get; set; }

        public string Area { get; set; }

        [DisplayName("Solicitante")]
        public int ResponsableID { get; set; }

        public string Responsable { get; set; }


        [DisplayName("Colaborador a Promover")]
        public int AscendidoID { get; set; }

        public string Ascendido { get; set; }
        
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

        [DisplayName("Comentarios")]
        public string Comentarios { get; set; }

        [DisplayName("Sueldo Tentativo S/.")]
        public int SueldoTentativo { get; set; }

        [DisplayName("Código")]
        public string Codigo { get; set; }
        

        [DisplayName("Fecha Publicación")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaAprobacion { get; set; }

        [DisplayName("Fecha Inicio en Nuevo Puesto")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaInicioNuevoPuesto { get; set; }


        public SolicitudPromocionDTO() { }

        public SolicitudPromocionDTO(SolicitudPromocion o)
        {
            ID = o.ID;
            PuestoID = o.PuestoID;
            Puesto = o.Puesto.Nombre;
            AreaID = o.AreaID;
            Area = o.Area.Nombre;
            ResponsableID = o.ResponsableID;
            Responsable = o.Responsable.ToDTO().NombreCompleto;
            EstadoSolicitudOfertaLaboralID = o.EstadoSolicitudOfertaLaboralID;
            FechaRequerimiento = o.FechaRequerimiento;
            FechaFinRequerimiento = o.FechaFinVigenciaSolicitud;
            Descripcion = o.Descripcion;
            Comentarios = o.Comentarios;
            SueldoTentativo = o.SueldoTentativo;
            AscendidoID = o.AscendidoID;
            Ascendido = o.Ascendido.ToDTO().NombreCompleto;
            FechaInicioNuevoPuesto = o.FechaInicioNuevoPuesto;
        }

    }
}