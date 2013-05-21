using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoDP2.Areas.Evaluacion360.Models;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int SueldoTentativo { get; set; }

        public string Comentarios { get; set; }

        public int NumeroVacantes { get; set; }

        public virtual ICollection<Funcion> ListaFuncionesPuesto { get; set; }

        [InverseProperty("OfertaLaboral")]
        public virtual ICollection<OfertaLaboralXPostulante> Postulantes { get; set; }


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
            SueldoTentativo = o.SueldoTentativo;
            Comentarios = o.Comentarios;
            NumeroVacantes = o.NumeroVacantes;
           // ListaFuncionesPuesto = o.funciones;
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
        [UIHint("GridForeignKey")]
        public int PuestoID { get; set; }

        [DisplayName("Area")]
        [UIHint("GridForeignKey")]
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

        [DisplayName("Numero de Vacantes")]
        public int NumeroVacantes { get; set; }

        [DisplayName("Comentarios")]
        public string Comentarios { get; set; }

        [DisplayName("Sueldo Tentativo S/.")]
        public int SueldoTentativo { get; set; }

        [DisplayName("Código")]
        public string Codigo { get; set; }


        //no se si este bien

        public ICollection<FuncionDTO> funciones;
        
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
            NumeroVacantes = o.NumeroVacantes;
            Comentarios = o.Comentarios;
            SueldoTentativo = o.SueldoTentativo;
            //un cambio
            //funciones = ListaFuncionesToDTO(o.ListaFuncionesPuesto);
            
            
            
        }

        public ICollection<FuncionDTO> ListaFuncionesToDTO(ICollection<Funcion> funciones)
        {
            ICollection<FuncionDTO> ListaDTO = null;
            FuncionDTO fun = new FuncionDTO();

            foreach (Funcion f in funciones)
            {
                fun = new FuncionDTO(f);
                ListaDTO.Add(fun);
            }

            return ListaDTO;
        }
    }

    public class OfertaLaboralXPostulanteWSDTO //Para el WS getOfertaLaboral
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
        public string Puesto { get; set; }
        public string Area { get; set; }
        public string Responsable { get; set; }
        //public int ModoPublicacionID { get; set; }
        //public string Descripcion { get; set; }
        public string FechaRequerimiento { get; set; }
        public int NumeroPostulantes { get; set; }
        //public string FechaFinRequerimiento { get; set; }
        //public int EstadoSolicitudOfertaLaboralID { get; set; }

        //public string FechaUltimaEntrevista { get; set; } //pedido por el profe segun Cesarin

        public List<PostulanteDTO> Postulantes { get; set; }

        public OfertaLaboralXPostulanteWSDTO() { }

        public OfertaLaboralXPostulanteWSDTO(OfertaLaboral oflab, List<Postulante> lstPostulantes)
        {
            ID = oflab.ID;
            Puesto = oflab.Puesto.Nombre;
            Area = oflab.Area.Nombre;
            Responsable = oflab.Responsable.Nombres + " " + oflab.Responsable.ApellidoPaterno + " " + oflab.Responsable.ApellidoMaterno;
            FechaRequerimiento = oflab.FechaRequerimiento;
            NumeroPostulantes = lstPostulantes.Count;
            Postulantes = lstPostulantes.Select(x => x.ToDTO()).ToList();
        }
    }
}