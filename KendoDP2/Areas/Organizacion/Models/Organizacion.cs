using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class Organizacion: DBObject
    {
        public string RazonSocial { get; set; }
        public string Ruc { get; set; }
        public string Rubro { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public int? PaisID { get; set; }
        public Pais Pais { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Mision { get; set; }
        public string Vision { get; set; }
        public int? RepresentanteLegalID { get; set; }
        public Colaborador RepresentanteLegal { get; set; }
        public string Documento { get; set; }
        

        public Organizacion() { }

        public Organizacion(OrganizacionDTO e)
        {
            LoadFromDTO(e);
        }

        public Organizacion LoadFromDTO(OrganizacionDTO e)
        {
            ID = e.ID;
            RazonSocial = e.RazonSocial;
            Rubro = e.Rubro;
            Ruc = e.Ruc;
            Direccion = e.Direccion;
            Telefono = e.Telefono;
            Ciudad = e.Ciudad;
            PaisID = e.PaisID;
            CorreoElectronico = e.CorreoElectronico;
            Mision = e.Mision;
            Vision = e.Vision;
            RepresentanteLegalID = e.ColaboradorID;
            Documento = e.Documento;

            return this;
        }

        public OrganizacionDTO ToDTO()
        {
            return new OrganizacionDTO(this);
        }
    }

    public class OrganizacionDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
      
        [DisplayName("Razon Social")]
        [Required]
        [StringLength(50)]
        public string RazonSocial { get; set; }

        
        [DisplayName("RUC")]
        [Required]
        [StringLength(11)]
        public string Ruc { get; set; }

        [StringLength(30)]
        [DisplayName("Rubro")]
        public string Rubro { get; set; }
        
        [StringLength(60)]
        [DisplayName("Direccion")]
        public string Direccion { get; set; }

        [StringLength(28)]
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }
        
        [StringLength(28)]
        [DisplayName("Ciudad")]
        public string Ciudad { get; set; }
        
        [DisplayName("País")]
        public int PaisID { get; set; }
        
        [StringLength(80)]
        [DisplayName("Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        
        [StringLength(300)]
        [DisplayName("Misión")]
        public string Mision { get; set; }
        
        [StringLength(300)]
        [DisplayName("Visión")]
        public string Vision { get; set; }
        
        [DisplayName("Representante Legal")]
        public int ColaboradorID { get; set; }

        [DisplayName("Tipo Documento")]
        public string TipoDocumento { get; set; }

        [DisplayName("Documento")]        
        public string Documento { get; set; }


        public OrganizacionDTO() { }

        public OrganizacionDTO(Organizacion e)
        {
            ID = e.ID;
            RazonSocial = e.RazonSocial;
            Ruc = e.Ruc;
            Rubro = e.Rubro;
            Direccion = e.Direccion;
            Telefono = e.Telefono;
            Ciudad = e.Ciudad;
            PaisID = e.PaisID.GetValueOrDefault();
            CorreoElectronico = e.CorreoElectronico;
            Mision = e.Mision;
            Vision = e.Vision;
            ColaboradorID = e.RepresentanteLegalID.GetValueOrDefault();
            Documento = e.Documento;
        }
    
    }


}