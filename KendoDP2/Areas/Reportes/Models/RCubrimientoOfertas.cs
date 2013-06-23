using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;
using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Objetivos.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoDP2.Areas.Reportes.Models
{
    public class ROfertasLaborales
    {
        public string nombreProveniencia;
        public string gradoAcademico;
        public int cantPostulantes;
        public int cantElegidos;
    }
    public class RPostulante
    {
        public string Proveniencia;
        public string gradoAcademico;
        public string nombre;
    }
    public class RFase
    {
        public string nombreFase;
        public int numPostulantes;
        public List<RPostulante>  Postulantes;
    }
    public class ROferta
    {
        public string descripcion;
        public string fecha;
        public List<RFase> Fases;
    }


}