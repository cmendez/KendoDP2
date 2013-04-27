using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Areas.Objetivos.Models
{
    public class TipoObjetivoBSC : DBObject
    {

        public string Nombre { get; set; }

        public ICollection<Objetivo> Objetivos { get; set; }

        public TipoObjetivoBSC() { }

        public TipoObjetivoBSC(string nombre)
        {
            Nombre = nombre;
        }

    }

    public class TipoObjetivoBSCConstants
    {
        public static string Financiero = "Financiero";
        public static string AprendizajeCrecimiento = "Aprendizaje y crecimiento";
        public static string Cliente = "Cliente";
        public static string ProcesosInternos = "Procesos internos";

        public static List<string> GetNombresDeTipos()
        {
            List<string> ret = new List<string>();
            ret.Add(Financiero);
            ret.Add(AprendizajeCrecimiento);
            ret.Add(Cliente);
            ret.Add(ProcesosInternos);
            return ret;
        }
    }
}