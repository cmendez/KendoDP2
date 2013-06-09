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
        public string Puesto { get; set; }
        public string Area { get; set; }
        public bool HasChildren { get; set; }

        public NodoOrganigramaDTO(DP2Context context, Colaborador colaborador)
        {
            using (context)
            {
            //  Información del colaborador:
                this.Nombre = colaborador.ApellidoPaterno + " " + colaborador.ApellidoMaterno + ", " + colaborador.Nombres;
                this.Correo = colaborador.CorreoElectronico;
                this.Telefono = colaborador.Telefono;
                this.ImagenURL = colaborador.ImagenColaboradorID > 0 ? "/Misc/GetImagen?archivoID=" + colaborador.ImagenColaboradorID : "../Images/unknown-person.jpg";
                
            //  Información del puesto:
                this.Puesto = colaborador.ColaboradoresPuesto.Last().Puesto.Nombre;

            //  Información del área
                this.Area = colaborador.ColaboradoresPuesto.Last().Puesto.Area.Nombre;

            //  Información de los puestos inferiores:
                this.HasChildren = colaborador.ColaboradoresPuesto.Last().Puesto.Puestos.Any(p => !p.IsEliminado);
            }
        }
    }
}