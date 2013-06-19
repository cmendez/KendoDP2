using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

using KendoDP2.Areas.Configuracion.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Eventos.Models;
using System.Reflection;
namespace KendoDP2.Areas.Organizacion.Models
{
    public class Contactos : DBObject
    {
        public int ContactoID { get; set; }
        public int ColaboradorID { get; set; }
        public string Relacion { get; set; }
        
      
        public virtual Colaborador Contacto { get; set; }
      
        public virtual Colaborador Colaborador { get; set; }


         public Contactos() { }

        public Contactos(ContactosDTO c)
        {
            LoadFromDTO(c);
        }

        public Contactos LoadFromDTO(ContactosDTO c)
        {
            ID = c.ID;
            ContactoID = c.ContactoID;
            ColaboradorID = c.ColaboradorID;
            Relacion = c.Relacion;
           
            return this;
        }


        public ContactosDTO ToDTO()
        {
            return new ContactosDTO(this);
        }
   }

        public class ContactosDTO
        {

            public string Relacion { get; set; }
            [DisplayName("Relaci[on")]
            public int ID { get; set; }

            [Required]
            [DisplayName("Contacto ID")]
            public int ContactoID { get; set; }

            [Required]
            [DisplayName("Colaborador ID")]
            public int ColaboradorID { get; set; }
            
            public ColaboradorDTO ColaboradorDTO { get; set; }
            public ColaboradorDTO ContactoDTO { get; set; }
            public string Nombre { get; set; }

            public ContactosDTO() { }

            public ContactosDTO(Contactos c)
            {
                ContactoID = c.ContactoID;
                ColaboradorID = c.ColaboradorID;
                ID = c.ID;
                Relacion = c.Relacion;
                Nombre = c.Contacto.ApellidoPaterno + " " + c.Contacto.ApellidoMaterno + ", " + c.Contacto.Nombres;
                try
                {
                   
                }
                catch (Exception)
                {
                   
         
                }




              

            }

        }

    
}