using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KendoDP2.Areas.Objetivos.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

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
                List<Colaborador> subordinadosBaseDeDatos = contexto.TablaColaboradores.All();

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

                //List<ColaboradorDTO> subordinadosCliente = subordinadosBaseDeDatos.Where(s => s.ID == 21 || s.ID == 22 || s.ID == 23 || s.ID == 3 || s.ID == 4).Select(s => s.ToDTO()).ToList();
                List<ColaboradorDTO> subordinadosCliente = subordinadosBaseDeDatos.Where(s => s.ID == 21 || s.ID == 22 || s.ID == 23).Select(s => s.ToDTO()).ToList();

                ViewBag.Colaboradores = subordinadosCliente;

                //Depuracion
                //subordinadosCliente.Select(e => System)
                //subordinadosCliente.Select(e => Console.Out.WriteLine(e.Objetivos.A))
                //subordinadosCliente.Select(e => e.Objetivos.Select(m => m.LosProgresos))
                //subordinadosCliente.Select(e => Console.WriteLine(e), e.Objetivos.Select(m => m.LosProgresos))
                //(List<ColaboradorDTO>)subordinadosCliente.Select(e => Console.WriteLine(e));
                //(List<ColaboradorDTO>)subordinadosCliente.Select(e => Console.WriteLine(e));

                //Console.WriteLine(subordinadosCliente);
                //System.Diagnostics.Debug.WriteLine(subordinadosCliente);
                //System.Diagnostics.Debug.WriteLine(new Json { empleados = subordinadosCliente });
                //System.Diagnostics.Debug.WriteLine(new JsonResult { empleados = subordinadosCliente });
                //System.Diagnostics.Debug.WriteLine(new Json(new { empleados = subordinadosCliente }));
                //System.Diagnostics.Debug.WriteLine(Json(new { empleados = subordinadosCliente }));

                //JsonResult unObjeto = new Js

                //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(p.GetType());
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(subordinadosCliente.GetType());
                //XMLWriter escritor = new XMLWriter(); 
                //System.Xml.Serialization.XMLWriter escritor = new XMLWriter(); 
                //System.Xml.XMLWriter escritor = new XMLWriter(); 
                //System.Xml.XmlWriter escritor = new System.Xml.XmlWriter(); 
                //System.Xml.XmlTextWriter escritor = new System.Xml.XmlTextWriter();
                StringWriter escritor = new StringWriter();


                x.Serialize(escritor, subordinadosCliente);

                //System.Diagnostics.Debug.WriteLine(escritor.Write);
                System.Diagnostics.Debug.WriteLine(escritor.ToString());




                //StringWriter textWriter = new StringWriter();

                //xmlSerializer.Serialize(textWriter, toSerialize);
                //return textWriter.ToString();

                ViewBag.Area = "";
                return View();
            }
        }

        //public JsonResult validarA
        //public JsonResult validacionDel
        //public JsonResult capturarValidacionDelJefe(int progresoID, int valorDelJefe)
        public JsonResult capturarValidacionDelJefe(int progresoID, int valorConsideradoPorElJefe)
        {

            //return new JsonResult

            //return null;
            //return JsonSuccessGet(new
            //{
            //    colaborador = colaborador,
            //    puesto = puesto,
            //    area = area
            //});
            //System.Diagnostics.Debug.WriteLine(progresoID);
            //System.Diagnostics.Debug.WriteLine(valorConsideradoPorElJefe);


            System.Diagnostics.Debug.WriteLine("progresoID = " + progresoID);
            System.Diagnostics.Debug.WriteLine("valorConsideradoPorElJefe = " + valorConsideradoPorElJefe);

            //using()
            using (DP2Context contexto = new DP2Context())
            {
                //contexto.TablaAvance
                //contexto.TablaColaboradores.Where(e => e.Objetivos)
                AvanceObjetivo adelanto = contexto.TablaAvanceObjetivo.FindByID(progresoID);

                //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(adelanto.GetType());
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(adelanto.enFormatoDTO().GetType());
                StringWriter escritor = new StringWriter();
                //x.Serialize(escritor, adelanto);
                x.Serialize(escritor, adelanto.enFormatoDTO());
                System.Diagnostics.Debug.WriteLine(escritor.ToString());


                adelanto.FueRevisado = true;
                adelanto.ValorDelJefe = valorConsideradoPorElJefe;

                //contexto.Mod
                contexto.TablaAvanceObjetivo.ModifyElement(adelanto);

                //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(adelanto.GetType());
                //StringWriter escritor = new StringWriter();
                //x.Serialize(escritor, adelanto);
                //System.Diagnostics.Debug.WriteLine(escritor.ToString());

                //x = new System.Xml.Serialization.XmlSerializer(adelanto.GetType());
                //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(adelanto.enFormatoDTO().GetType());
                x = new System.Xml.Serialization.XmlSerializer(adelanto.enFormatoDTO().GetType());
                escritor = new StringWriter();
                //x.Serialize(escritor, adelanto);
                x.Serialize(escritor, adelanto.enFormatoDTO());
                System.Diagnostics.Debug.WriteLine(escritor.ToString());

            }

            return null;
        }

    }
}
