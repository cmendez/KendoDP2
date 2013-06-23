using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class NodoOrganigramaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string ImagenURL { get; set; }
        public int PuestoId { get; set; }
        public string Puesto { get; set; }
        public string Area { get; set; }
        public string Color { get; set; }
        public bool IsAudit { get; set; }
        public bool IsAuditKid { get; set; }
        public bool HasChildren { get; set; }

    //  OJO : ¡¡¡¡¡ USAR SOLO DENTRO DE UN DP2Context !!!!!
        public NodoOrganigramaDTO(Puesto puesto)
        {
             //  Información del puesto:
                this.PuestoId = puesto.ID;
                this.Puesto = puesto.Nombre;

            //  Información del área
                this.Area = puesto.Area.Nombre;

                this.IsAudit = puesto.Area.IsAudit;
                this.IsAuditKid = IsAudit && puesto.PuestoSuperior.Area.IsAudit;
                this.Color = puesto.Area.AColor.Text;
                
            //  Información de los puestos inferiores:
                this.HasChildren = puesto.Puestos.Any(p => !p.IsEliminado);

            //  Si existe colaborador...
                if (puesto.ColaboradorPuestos.Any(c => c.FechaIngresoPuesto <= DateTime.Now && (c.FechaSalidaPuesto == null || c.FechaSalidaPuesto > DateTime.Today)))
                {
                    //  Información del colaborador:
                    Colaborador colaborador = puesto.ColaboradorPuestos.Last(c => c.FechaIngresoPuesto <= DateTime.Now && (c.FechaSalidaPuesto == null || c.FechaSalidaPuesto > DateTime.Today)).Colaborador;
                    this.Id = colaborador.ID;
                    this.Nombre = colaborador.ApellidoPaterno + " " + colaborador.ApellidoMaterno + ", " + colaborador.Nombres;
                    this.Correo = colaborador.CorreoElectronico != null? colaborador.CorreoElectronico : "";
                    this.Telefono = colaborador.Telefono != null? colaborador.Telefono : "";
                    this.ImagenURL = colaborador.ImagenColaboradorID > 0 ? "/Misc/GetImagen?archivoID=" + colaborador.ImagenColaboradorID : "../Images/unknown-person.jpg";
                }
            //  Si no existe colaborador...
                else
                {
                    this.Id = 0;
                    this.Nombre = "Vacante";
                    this.Correo = "";
                    this.Telefono = "";
                    this.ImagenURL = "../Images/job-vacancy.jpg";
                }

            
        }
    }
}