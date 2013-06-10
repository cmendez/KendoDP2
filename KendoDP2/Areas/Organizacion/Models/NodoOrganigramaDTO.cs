using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Organizacion.Models
{
    public class NodoOrganigramaDTO
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string ImagenURL { get; set; }
        public int PuestoId { get; set; }
        public string Puesto { get; set; }
        public string Area { get; set; }
        public bool HasChildren { get; set; }

        public NodoOrganigramaDTO(DP2Context context, Puesto puesto)
        {
            using (context)
            {

             //  Información del puesto:
                this.PuestoId = puesto.ID;
                this.Puesto = puesto.Nombre;

            //  Información del área
                this.Area = puesto.Area.Nombre;

            //  Información de los puestos inferiores:
                this.HasChildren = puesto.Puestos.Any(p => !p.IsEliminado);

            //  Si existe puesto...
                if (puesto.ColaboradorPuestos.Last(c => c.FechaSalidaPuesto == null) != null)
                {
                    //  Información del colaborador:
                    Colaborador colaborador = puesto.ColaboradorPuestos.Last(c => c.FechaSalidaPuesto == null).Colaborador;
                    this.Nombre = colaborador.ApellidoPaterno + " " + colaborador.ApellidoMaterno + ", " + colaborador.Nombres;
                    this.Correo = colaborador.CorreoElectronico;
                    this.Telefono = colaborador.Telefono;
                    this.ImagenURL = colaborador.ImagenColaboradorID > 0 ? "/Misc/GetImagen?archivoID=" + colaborador.ImagenColaboradorID : "../Images/unknown-person.jpg";
                }
            //  Si no existe el puesto...
                else
                {
                    this.Nombre = "Vacante";
                    this.Correo = "";
                    this.Telefono = "";
                    this.ImagenURL = "../Images/job-vacancy.jpg";
                }

            }
        }
    }
}