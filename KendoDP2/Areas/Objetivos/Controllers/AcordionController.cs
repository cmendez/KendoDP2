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
        public AcordionController()
        {
            ViewBag.Area = "Objetivos";
        }

        public ActionResult Index()
        {
            using (DP2Context contexto = new DP2Context())
            {
                int elUsuarioQueInicioSesion = DP2MembershipProvider.GetPersonaID(this);

                List<Colaborador> subordinadosBaseDeDatos = GestorServiciosPrivados.consigueSusSubordinados(elUsuarioQueInicioSesion, contexto);

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

                return View();
            }
        }

        [HttpPost]
        public JsonResult capturarValidacionDelJefe(int progresoID, int valorConsideradoPorElJefe)
        {

            using (DP2Context contexto = new DP2Context())
            {
                AvanceObjetivo adelanto = contexto.TablaAvanceObjetivo.FindByID(progresoID);
                AvanceObjetivo revision = new AvanceObjetivo { Comentario = "(Revisado por jefe)", Valor = valorConsideradoPorElJefe, Objetivo = adelanto.Objetivo, FechaCreacion = DateTime.Now.ToString("dd/MM/yyy") };
                adelanto.FueRevisado = true;
                contexto.TablaAvanceObjetivo.ModifyElement(adelanto);
                contexto.TablaAvanceObjetivo.AddElement(revision);
            }

            return null;
        }

    }
}

