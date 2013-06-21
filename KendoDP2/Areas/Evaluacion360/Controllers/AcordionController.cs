using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using KendoDP2.Models.Seguridad;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class AcordionController : Controller
    {

        public ActionResult Index()
        {
            using (DP2Context context = new DP2Context()) 
            {
                int ColaboradorID = DP2MembershipProvider.GetPersonaID(this);
                Colaborador yo = context.TablaColaboradores.FindByID(ColaboradorID);
                ColaboradorXPuesto cruce = yo.ColaboradoresPuesto.SingleOrDefault(x => x.FechaSalidaPuesto == null || x.FechaSalidaPuesto >= DateTime.Today);
                List<ColaboradorDTO> subordinadosCliente = new List<ColaboradorDTO>();

                if (cruce != null)
                {
                    Puesto puesto = cruce.Puesto;
                    foreach (var puestoHijo in puesto.Puestos)
                    {
                        ColaboradorXPuesto subordinado = puestoHijo.ColaboradorPuestos.SingleOrDefault(x => x.FechaSalidaPuesto == null || x.FechaSalidaPuesto >= DateTime.Today);
                        if (subordinado != null)
                            subordinadosCliente.Add(subordinado.Colaborador.ToDTO());
                    }
                }

                ViewBag.Colaboradores = subordinadosCliente;
                ViewBag.Area = "Objetivos";
                return View();
            }
        }

    }
}
