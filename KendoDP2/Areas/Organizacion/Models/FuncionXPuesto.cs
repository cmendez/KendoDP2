using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class FuncionXPuesto : DBObject
    {
        public int FuncionID { get; set; }
        public Funcion Funcion { get; set; }

        public int PuestoID { get; set; }
        public Puesto Puesto { get; set; }

       
        public int Peso { get; set; }

        public FuncionXPuesto cargaConDatosDelCliente(FuncionXPuestoDTO cxpDTO, int puestoID)
        {
            PuestoID = puestoID;
            FuncionID = cxpDTO.FuncionID;
            
            Peso = cxpDTO.Peso;

            return this;
        }

        public FuncionXPuestoDTO enFormatoParaElCliente()
        {
            return new FuncionXPuestoDTO(this);
        }
    }

    public class FuncionXPuestoDTO
    {
        public int ID { get; set; }

        [Required]
        [DisplayName("Funcion")]
        public int FuncionID { get; set; }
        
        public int PuestoID { get; set; }

        public int Peso { get; set; }

       

        public FuncionXPuestoDTO() { }

        public FuncionXPuestoDTO(FuncionXPuesto x)
        {
            ID = x.ID;
            FuncionID = x.FuncionID;
            PuestoID = x.PuestoID;
          
            Peso = x.Peso;
        }

    }
}