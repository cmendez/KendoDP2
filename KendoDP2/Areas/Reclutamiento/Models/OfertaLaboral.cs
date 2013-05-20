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
        public Puesto Puesto { get; set; }

        public int AreaID { get; set; }
        public Area Area { get; set; }

        public int ResponsableID { get; set; }
        public Colaborador Responsable { get; set; }

        public int EstadoSolicitudOfertaLaboralID { get; set; }
        public EstadosSolicitudOfertaLaboral EstadoSolicitudOfertaLaboral { get; set; }

        public DateTime FechaRequerimiento { get; set; }

        public DateTime FechaFinVigenciaSolicitud { get; set; }
        
        public string Descripcion { get; set; }

        public int ModoPublicacionOfertaLaboralID { get; set; }
        public ModoSolicitudOfertaLaboral ModoSolicitudOfertaLaboral { get; set; }

        //public virtual ICollection<Capacidad> ListaCapacidades { get; set; }

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

        public int AreaID { get; set; }

        public int ResponsableID { get; set; }


        public int ModoPublicacionID { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaRequerimiento { get; set; }

        public DateTime FechaFinVigenciaSolicitud { get; set; }

        public int EstadoSolicitudOfertaLaboralID { get; set; }

        public OfertaLaboralDTO() { }

        public OfertaLaboralDTO(OfertaLaboral o)
        {
            ID = o.ID;
            PuestoID = o.PuestoID;
        }
    }
}