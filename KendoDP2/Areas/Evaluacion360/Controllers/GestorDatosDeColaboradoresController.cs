using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExtensionMethods;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;

namespace KendoDP2.Areas.Evaluacion360.Controllers
{
    public class GestorDatosDeColaboradoresController : WSController
    {
        //
        // GET: /Evaluacion360/GestorDatosDeColaboradores/

        public JsonResult consultarDatosDelEmpleado(string conEsteIdentificador)
        {
            using (DP2Context context = new DP2Context())
            {
                try
                {
                    ColaboradorDTO colaborador = context.TablaColaboradores.FindByID(Convert.ToInt32(conEsteIdentificador)).ToDTO();
                    PuestoDTO puesto = colaborador.PuestoID == 0 ? new PuestoDTO() : context.TablaPuestos.FindByID(colaborador.PuestoID).ToDTO();
                    AreaDTO area = colaborador.AreaID == 0 ? new AreaDTO() : context.TablaAreas.FindByID(colaborador.AreaID).ToDTO();
                    return JsonSuccessGet(new
                    {
                        colaborador = colaborador,
                        puesto = puesto,
                        area = area
                    });
                }
                catch (Exception ex)
                {
                    return JsonErrorGet("Error en la BD: " + ex.Message);
                }
            }
        }

        public JsonResult consultarSusCompanerosPares(string deEsteColaborador)
        {

            try
            {
                int identificadorEnFormatoNumerico = Convert.ToInt32(deEsteColaborador);

                List<Colaborador> losPares = GestorServiciosPrivados.consigueSusCompañerosPares(identificadorEnFormatoNumerico);

                List<ColaboradorDTO> enFormatoParaElCliente = losPares.Select(p => p.ToDTO()).ToList();

                return JsonSuccessGet(new
                    {
                        losColaboradoresEnElMismoRango = enFormatoParaElCliente
                    });
            }
            catch (Exception ocurrioUnProblema)
            {
                return JsonErrorGet("Error en la BD: " + ocurrioUnProblema.Message);
            }
        }

        public JsonResult consultarElJefe(string deEsteColaborador)
        {

            try
            {
                int identificadorEnFormatoNumerico = Convert.ToInt32(deEsteColaborador);

                Colaborador losDatosDeSuJefe = GestorServiciosPrivados.consigueElJefe(identificadorEnFormatoNumerico);

                List<Colaborador> suJefeComoGrupoDeUno = new List<Colaborador>{ losDatosDeSuJefe };

                List<ColaboradorDTO> enFormatoParaElCliente = suJefeComoGrupoDeUno.Select(p => p.ToDTO()).ToList();

                return JsonSuccessGet(new
                {
                    suSuperior = enFormatoParaElCliente
                });
            }
            catch (Exception ocurrioUnProblema)
            {
                return JsonErrorGet("Error en la BD: " + ocurrioUnProblema.Message);
            }
        }

        public JsonResult conocerEquipoDeTrabajo(string deEsteColaborador)
        {

            try
            {
                int identificadorEnFormatoNumerico = Convert.ToInt32(deEsteColaborador);

                List<Colaborador> colegas = GestorServiciosPrivados.consigueSusCompañerosPares(identificadorEnFormatoNumerico);

                //List<Colaborador> suJefeComoGrupoDeUno = new List<Colaborador> { losDatosDeSuJefe };

                List<ColaboradorDTO> enFormatoParaElCliente = colegas.Select(p => p.ToDTO()).ToList();

                return JsonSuccessGet(new
                {
                    losEmpleadosQueLeReportan = enFormatoParaElCliente
                });
            }
            catch (Exception ocurrioUnProblema)
            {
                return JsonErrorGet("Error en la BD: " + ocurrioUnProblema.Message);
            }
        }

        public JsonResult consultarColaboradores(string asociadosA, string esteEmpleado)
        {

            try
            {
                int identificadorEnFormatoNumerico = Convert.ToInt32(esteEmpleado);

                switch (asociadosA)
                {
                    case "Sus_pares":
                        return consultarSusCompanerosPares(esteEmpleado);
                    case "Su_jefe":
                        return consultarElJefe(esteEmpleado);
                    case "Su_equipo_de_trabajo":
                        return conocerEquipoDeTrabajo(esteEmpleado);
                    default:
                        return JsonSuccessGet(new
                        {
                            interrupcion = "No se entendio lo que desea"
                        });      
                }
            }
            catch (Exception ocurrioUnProblema)
            {
                return JsonErrorGet("Error en la BD: " + ocurrioUnProblema.Message);
            }
        }

        //public JsonResult consultarDatos(string asociadosA, string esteEmpleado)
        //public JsonResult consultarResultadosEvaluaciones(int colaboradorID)
        //{

        //    return null;


        //}

    }
}
