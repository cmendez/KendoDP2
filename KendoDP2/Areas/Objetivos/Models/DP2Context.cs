using KendoDP2.Areas.Configuracion.Models;
using KendoDP2.Areas.Evaluacion360.Models;
using KendoDP2.Areas.Objetivos.Models;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Areas.Reclutamiento.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using KendoDP2.Models.Helpers;
using KendoDP2.Models.Seguridad;

namespace KendoDP2.Models.Generic
{
    public partial class DP2Context : DbContext
    {
        public DbSet<Objetivo> InternalObjetivos { get; set; }
        public DbSet<TipoObjetivoBSC> InternalTipoObjetivoBSC { get; set; }
        public DbSet<BSC> InternalBSC { get; set; }
        public DbSet<AvanceObjetivo> InternalAvanceObjetivo { get; set; }

        public DBGenericRequester<Objetivo> TablaObjetivos { get; set; }
        public DBGenericRequester<TipoObjetivoBSC> TablaTipoObjetivoBSC { get; set; }
        public DBGenericRequester<BSC> TablaBSC { get; set; }
        public DBGenericRequester<AvanceObjetivo> TablaAvanceObjetivo { get; set; }

        private void RegistrarTablasObjetivos()
        {
            TablaBSC = new DBGenericRequester<BSC>(this, InternalBSC);
            TablaObjetivos = new DBGenericRequester<Objetivo>(this, InternalObjetivos);
            TablaTipoObjetivoBSC = new DBGenericRequester<TipoObjetivoBSC>(this, InternalTipoObjetivoBSC);

            TablaAvanceObjetivo = new DBGenericRequester<AvanceObjetivo>(this, InternalAvanceObjetivo);
        }

        private void SeedTipoObjetivoBSC()
        {
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.Financiero));
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.AprendizajeCrecimiento));
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.Cliente));
            TablaTipoObjetivoBSC.AddElement(new TipoObjetivoBSC(TipoObjetivoBSCConstants.ProcesosInternos));

        }

        private void SeedBSC()
        {
            TablaBSC.AddElement(new BSC(2));
        }

        private void SeedObjetivos()
        {
            Puesto presidente = TablaPuestos.One(p => p.Nombre.Equals("Presidente"));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 1", 1, 1, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 2", 1, 1, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 1.1", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Financiero 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Financiero 1.2", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Financiero 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 1", 1, 3, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 2", 1, 3, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 1.1", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Cliente 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Cliente 1.2", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Cliente 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 1", 1, 4, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 2", 1, 4, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 1.1", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Interno 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Interno 1.2", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Interno 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 1", 1, 2, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 2", 1, 2, presidente.ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 1.1", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Formación 1")).ID, 50, this));
            TablaObjetivos.AddElement(new Objetivo("Objetivo Formación 1.2", TablaObjetivos.One(d => d.Nombre.Equals("Objetivo Formación 1")).ID, 50, this));
           
       
        }

        private void SeedObjetivosPersonales()
        {

            //Colaborador elB = TablaColaboradores.One(e => e.Nombres.Equals("colaborador modulo tres b"));
            Colaborador elB = TablaColaboradores.FindByID(2);

            elB.Objetivos = new List<Objetivo>
            {
                new Objetivo { Nombre = "Involucrarme en las campañas de marketing de este periodo", Peso = 50, IsEliminado = false,
                                LosProgresos = new List<AvanceObjetivo> {
                                    new AvanceObjetivo { Valor = 10, FechaCreacion = "10/10/2012", Creador = elB, FueRevisado = false, ValorDelJefe = 0 },
                                    new AvanceObjetivo { Valor = 20, FechaCreacion = "20/10/2012", Creador = elB, FueRevisado = false, ValorDelJefe = 0 },
                                    new AvanceObjetivo { Valor = 30, FechaCreacion = "30/10/2012", Creador = elB, FueRevisado = false, ValorDelJefe = 0 },
                                }
                
                },
                new Objetivo { Nombre = "Inscribirme en lecturas de gestión del personal", Peso = 50, IsEliminado = false, 
                                LosProgresos = new List<AvanceObjetivo> {
                                    new AvanceObjetivo { Valor = 15, FechaCreacion = "15/10/2012", Creador = elB, FueRevisado = false, ValorDelJefe = 0 },
                                    new AvanceObjetivo { Valor = 25, FechaCreacion = "25/10/2012", Creador = elB, FueRevisado = false, ValorDelJefe = 0 },
                                    new AvanceObjetivo { Valor = 35, FechaCreacion = "35/10/2012", Creador = elB, FueRevisado = false, ValorDelJefe = 0 },
                                }
                },

            };

            //Colaborador elEmpleadoC = TablaColaboradores.One(e => e.Nombres.Equals("colaborador modulo tres c"));
            Colaborador elEmpleadoC = TablaColaboradores.FindByID(3);

            elEmpleadoC.Objetivos = new List<Objetivo>
            {
                new Objetivo { Nombre = "Involucrarme en las campañas de marketing de este periodo", Peso = 50, IsEliminado = false,
                                LosProgresos = new List<AvanceObjetivo> {
                                    new AvanceObjetivo { Valor = 15, FechaCreacion = "10/10/2012", Creador = elEmpleadoC, FueRevisado = false, ValorDelJefe = 0 },
                                    new AvanceObjetivo { Valor = 25, FechaCreacion = "20/10/2012", Creador = elEmpleadoC, FueRevisado = false, ValorDelJefe = 0 },
                                    new AvanceObjetivo { Valor = 100, FechaCreacion = "21/10/2012", Creador = elEmpleadoC, FueRevisado = false, ValorDelJefe = 0 },
                                }
                },
                new Objetivo { Nombre = "Inscribirme en lecturas de gestión del personal", Peso = 50, IsEliminado = false,
                                LosProgresos = new List<AvanceObjetivo> {
                                    new AvanceObjetivo { Valor = 05, FechaCreacion = "10/10/2012", Creador = elEmpleadoC, FueRevisado = true, ValorDelJefe = 04 },
                                    new AvanceObjetivo { Valor = 10, FechaCreacion = "20/10/2012", Creador = elEmpleadoC, FueRevisado = true, ValorDelJefe = 09 },
                                    new AvanceObjetivo { Valor = 20, FechaCreacion = "21/10/2012", Creador = elEmpleadoC, FueRevisado = true, ValorDelJefe = 19 },
                                }                
                
                },

            };

            //Colaborador elColaboradorD = TablaColaboradores.One(e => e.Nombres.Equals("colaborador modulo tres d"));
            Colaborador elColaboradorD = TablaColaboradores.FindByID(4);
            ////Estos presentan un avance del
            //Estos objetivos están sin iniciar
            elColaboradorD.Objetivos = new List<Objetivo>
            {
                new Objetivo { Nombre = "Participar en talleres de gestión de la innovación", Peso = 80, IsEliminado = false },
                new Objetivo { Nombre = "Participar en la elaboración del presupuesto para ventas", Peso = 20, IsEliminado = false },

            };

            TablaColaboradores.ModifyElement(elB);
            TablaColaboradores.ModifyElement(elEmpleadoC);
            TablaColaboradores.ModifyElement(elColaboradorD);

        }
    }
}