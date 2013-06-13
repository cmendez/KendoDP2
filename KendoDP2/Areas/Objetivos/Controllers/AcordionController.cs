using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KendoDP2.Areas.Objetivos.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;

namespace KendoDP2.Areas.Objetivos.Controllers
{
    public class AcordionController : Controller
    {
        //
        // GET: /Evaluacion360/Acordion/

        public ActionResult Index()
        {
            using (DP2Context contexto = new DP2Context())
            {
                int elUsuarioQueInicioSesion = DP2MembershipProvider.GetPersonaID(this);

                List<Colaborador> subordinadosBaseDeDatos = GestorServiciosPrivados.consigueSusSubordinados(elUsuarioQueInicioSesion, contexto);

                //Unos subordinados de demostracion que presentan avances en sus objetivos.
                subordinadosBaseDeDatos.Add(contexto.TablaColaboradores.FindByID(21));
                subordinadosBaseDeDatos.Add(contexto.TablaColaboradores.FindByID(22));
                subordinadosBaseDeDatos.Add(contexto.TablaColaboradores.FindByID(23));

                foreach (Colaborador subordinado in subordinadosBaseDeDatos)
                {
                    contexto.Entry(subordinado).Collection(s => s.Objetivos).Load();
                    contexto.Entry(subordinado).Collection(s => s.ColaboradoresPuesto).Load();
                    contexto.Entry(subordinado).Collection(s => s.ColaboradorXProcesoEvaluaciones).Load();
                    contexto.Entry(subordinado).Reference(s => s.EstadoColaborador).Load();
                    contexto.Entry(subordinado).Reference(s => s.Pais).Load();
                    contexto.Entry(subordinado).Collection(s => s.EsContactoDe).Load();
                    contexto.Entry(subordinado).Collection(s => s.Contactos).Load();

                }

                List<ColaboradorDTO> subordinadosCliente = subordinadosBaseDeDatos.Select(s => s.ToDTO()).ToList();

                ViewBag.Colaboradores = subordinadosCliente;

                //Depuracion
                //Las siguientes cuatro lineas imprimen, en formato XML, el objeto brindado a la vista
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(subordinadosCliente.GetType());
                StringWriter escritor = new StringWriter();
                x.Serialize(escritor, subordinadosCliente);
                System.Diagnostics.Debug.WriteLine(escritor.ToString());

                ViewBag.Area = "";
                return View();
            }
        }

        public JsonResult capturarValidacionDelJefe(int progresoID, int valorConsideradoPorElJefe)
        {

            //Depuracion
            System.Diagnostics.Debug.WriteLine("progresoID = " + progresoID);
            System.Diagnostics.Debug.WriteLine("valorConsideradoPorElJefe = " + valorConsideradoPorElJefe);

            using (DP2Context contexto = new DP2Context())
            {
                AvanceObjetivo adelanto = contexto.TablaAvanceObjetivo.FindByID(progresoID);

                //Depuracion
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(adelanto.enFormatoDTO().GetType());
                StringWriter escritor = new StringWriter();
                x.Serialize(escritor, adelanto.enFormatoDTO());
                System.Diagnostics.Debug.WriteLine(escritor.ToString());


                adelanto.FueRevisado = true;
                adelanto.ValorDelJefe = valorConsideradoPorElJefe;

                contexto.TablaAvanceObjetivo.ModifyElement(adelanto);

                //Depuracion
                x = new System.Xml.Serialization.XmlSerializer(adelanto.enFormatoDTO().GetType());
                escritor = new StringWriter();
                x.Serialize(escritor, adelanto.enFormatoDTO());
                System.Diagnostics.Debug.WriteLine(escritor.ToString());

            }

            return null;
        }

    }
}







        //public ActionResult Index()
        //{
        //    using (DP2Context contexto = new DP2Context())
        //    {
        //        List<Colaborador> subordinadosBaseDeDatos = contexto.TablaColaboradores.All();

        //        foreach (Colaborador subordinado in subordinadosBaseDeDatos)
        //        {
        //            contexto.Entry(subordinado).Collection(s => s.Objetivos).Load();
        //            contexto.Entry(subordinado).Collection(s => s.ColaboradoresPuesto).Load();
        //            contexto.Entry(subordinado).Collection(s => s.ColaboradorXProcesoEvaluaciones).Load();
        //            contexto.Entry(subordinado).Reference(s => s.EstadoColaborador).Load();
        //            contexto.Entry(subordinado).Reference(s => s.Pais).Load();
        //            contexto.Entry(subordinado).Collection(s => s.EsContactoDe).Load();
        //            contexto.Entry(subordinado).Collection(s => s.Contactos).Load();

        //        }

        //        List<ColaboradorDTO> subordinadosCliente = subordinadosBaseDeDatos.Where(s => s.ID == 21 || s.ID == 22 || s.ID == 23).Select(s => s.ToDTO()).ToList();

        //        ViewBag.Colaboradores = subordinadosCliente;

        //        //Depuracion
        //        //Las siguientes cuatro lineas imprimen, en formato XML, el objeto brindado a la vista
        //        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(subordinadosCliente.GetType());
        //        StringWriter escritor = new StringWriter();
        //        x.Serialize(escritor, subordinadosCliente);
        //        System.Diagnostics.Debug.WriteLine(escritor.ToString());

        //        ViewBag.Area = "";
        //        return View();
        //    }
        //}






        //public ActionResult Index()
        //{
        //    using (DP2Context contexto = new DP2Context())
        //    {
        //        //int colaboradorID = DP2MembershipProvider.GetPersonaID(this);
        //        int elUsuarioQueInicioSesion = DP2MembershipProvider.GetPersonaID(this);

        //        //List<Colaborador> subordinadosBaseDeDatos = contexto.TablaColaboradores.All();
        //        List<Colaborador> subordinadosBaseDeDatos = GestorServiciosPrivados.consigueSusSubordinados(elUsuarioQueInicioSesion, contexto);

        //        //Unos subordinados ficticios que presentan avances en sus objetivos.
        //        //Unos subordinados de demostracion que presentan avances en sus objetivos.
        //        subordinadosBaseDeDatos.Add(contexto.TablaColaboradores.FindByID(21));
        //        subordinadosBaseDeDatos.Add(contexto.TablaColaboradores.FindByID(22));
        //        subordinadosBaseDeDatos.Add(contexto.TablaColaboradores.FindByID(23));

        //        foreach (Colaborador subordinado in subordinadosBaseDeDatos)
        //        {
        //            contexto.Entry(subordinado).Collection(s => s.Objetivos).Load();
        //            contexto.Entry(subordinado).Collection(s => s.ColaboradoresPuesto).Load();
        //            contexto.Entry(subordinado).Collection(s => s.ColaboradorXProcesoEvaluaciones).Load();
        //            contexto.Entry(subordinado).Reference(s => s.EstadoColaborador).Load();
        //            contexto.Entry(subordinado).Reference(s => s.Pais).Load();
        //            contexto.Entry(subordinado).Collection(s => s.EsContactoDe).Load();
        //            contexto.Entry(subordinado).Collection(s => s.Contactos).Load();

        //        }

        //        //List<ColaboradorDTO> subordinadosCliente = subordinadosBaseDeDatos.Where(s => s.ID == 21 || s.ID == 22 || s.ID == 23).Select(s => s.ToDTO()).ToList();
        //        List<ColaboradorDTO> subordinadosCliente = subordinadosBaseDeDatos.Select(s => s.ToDTO()).ToList();

        //        ViewBag.Colaboradores = subordinadosCliente;

        //        //Depuracion
        //        //Las siguientes cuatro lineas imprimen, en formato XML, el objeto brindado a la vista
        //        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(subordinadosCliente.GetType());
        //        StringWriter escritor = new StringWriter();
        //        x.Serialize(escritor, subordinadosCliente);
        //        System.Diagnostics.Debug.WriteLine(escritor.ToString());

        //        ViewBag.Area = "";
        //        return View();
        //    }
        //}