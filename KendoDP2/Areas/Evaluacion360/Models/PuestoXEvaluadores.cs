using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Evaluacion360.Models
{
    public class PuestoXEvaluadores : DBObject
    {
        public int PuestoID { get; set; }
        public string ClaseEntorno { get; set; } //Jefe, Pares, Subordinados...
        //El atributo Participan es obsoleto. Hacer caso omiso.
        public bool Participan { get; set; }
        public int Cantidad { get; set; }
        public int Peso { get; set; }

        public PuestoXEvaluadores()
        {
        }

        public PuestoXEvaluadores(int puestoID, bool participan, string claseEntorno, int cantidad, int peso)
        {
            PuestoID = puestoID;
            ClaseEntorno = claseEntorno;
            Participan = participan;
            Cantidad = cantidad;
            Peso = peso;
        }

        public PuestoXEvaluadoresDTO ToDTO()
        {
            return new PuestoXEvaluadoresDTO(ID, Participan, ClaseEntorno, Cantidad, Peso);
        }

        public PuestoXEvaluadores LoadFromDTO(int puestoID, PuestoXEvaluadoresDTO pxeDTO)
        {
            PuestoID = puestoID;
            ClaseEntorno = pxeDTO.ClaseEntorno;
            Participan = pxeDTO.Participan;
            Cantidad = pxeDTO.Cantidad;
            Peso = pxeDTO.Peso;

            return this;
        }
    }

    public class PuestoXEvaluadoresDTO {

        public int ID { get; set; }
        public bool Participan { get; set; }
        [Editable(false)]
        public string ClaseEntorno { get; set; } //Jefe, Pares, Subordinados...
        [Range(0, 10000000, ErrorMessage = "La cantidad de debe ser cero o positiva")]
        public int Cantidad { get; set; }
        [Range(0, 100, ErrorMessage = "El peso(%) debe ser un numero entero entre 0 y 100")]
        public int Peso { get; set; }

        public PuestoXEvaluadoresDTO() { 
        }

        public PuestoXEvaluadoresDTO(int id, bool participan, string claseEntorno, int cantidad, int peso) {
            ID = id;
            Participan = participan;
            ClaseEntorno = claseEntorno;
            Cantidad = cantidad;
            Peso = peso;        
        }
    }

    public class ConstantesClaseEntornoPuestoEvaluadores
    {
        public static string El_mismo = "El mismo";
        public static string Jefe = "Jefe";
        public static string Pares = "Pares";
        public static string Terminado = "Terminado";
        public static string Subordinados = "Subordinados";
        public static string Clientes = "Clientes";
        public static string Otros = "Otros";
    }
}