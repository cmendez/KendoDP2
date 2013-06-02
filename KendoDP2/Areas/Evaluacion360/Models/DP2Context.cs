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
        public DbSet<Competencia> InternalCompetencias { get; set; }
        public DbSet<Capacidad> InternalCapacidades { get; set; }
        public DbSet<NivelCapacidad> InternalNivelCapacidades { get; set; }
        public DbSet<Perfil> InternalPerfiles { get; set; }
        public DbSet<Examen> InternalExamenes { get; set; }
        public DbSet<Evaluador> InternalEvaluadores { get; set; }
        public DbSet<TipoEvaluador> InternalTipoEvaluadores { get; set; }
        public DbSet<ProcesoEvaluacion> InternalProcesoEvaluaciones { get; set; }
        public DbSet<PerfilXCompetencia> InternalPerfilXCompetencia { get; set; }
        public DbSet<ColaboradorXProcesoEvaluacion> InternalColaboradorXProcesoEvaluaciones { get; set; }
        public DbSet<EstadoColaboradorXProcesoEvaluacion> InternalEstadoColaboradorXProcesoEvaluaciones { get; set; }
        public DbSet<EstadoProcesoEvaluacion> InternalEstadoProcesoEvaluacion { get; set; }
        public DbSet<PuestoXEvaluadores> InternalPuestoXEvaluadores { get; set; }
        public DbSet<CompetenciaXPuesto> InternalCompetenciaXPuesto { get; set; }
        public DbSet<AreaXProcesoEvaluacion> InternalAreaXProcesoEvaluaciones { get; set; }
        public DbSet<ProcesoXEvaluado> InternalProcesoXEvaluado { get; set; }

        public DBGenericRequester<Competencia> TablaCompetencias { get; set; }
        public DBGenericRequester<Capacidad> TablaCapacidades { get; set; }
        public DBGenericRequester<NivelCapacidad> TablaNivelCapacidades { get; set; }
        public DBGenericRequester<Perfil> TablaPerfiles { get; set; }
        public DBGenericRequester<Examen> TablaExamenes { get; set; }
        public DBGenericRequester<Evaluador> TablaEvaluadores { get; set; }
        public DBGenericRequester<TipoEvaluador> TablaTipoEvaluador { get; set; }
        public DBGenericRequester<ProcesoEvaluacion> TablaProcesoEvaluaciones { get; set; }
        public DBGenericRequester<PerfilXCompetencia> TablaPerfilXCompetencia { get; set; }
        public DBGenericRequester<ColaboradorXProcesoEvaluacion> TablaColaboradorXProcesoEvaluaciones { get; set; }
        public DBGenericRequester<EstadoColaboradorXProcesoEvaluacion> TablaEstadoColaboradorXProcesoEvaluaciones { get; set; }
        public DBGenericRequester<PuestoXEvaluadores> TablaPuestoXEvaluadores { get; set; }
        public DBGenericRequester<CompetenciaXPuesto> TablaCompetenciaXPuesto { get; set; }
        public DBGenericRequester<AreaXProcesoEvaluacion> TablaAreaXProcesoEvaluaciones { get; set; }
        public DBGenericRequester<ProcesoXEvaluado> TablaProcesoXEvaluado { get; set; }
        public DBGenericRequester<EstadoProcesoEvaluacion> TablaEstadoProcesoEvaluacion { get; set; }


        private void RegistrarTablasEvaluacion360()
        {
            TablaCompetencias = new DBGenericRequester<Competencia>(this, InternalCompetencias);
            TablaCapacidades = new DBGenericRequester<Capacidad>(this, InternalCapacidades);
            TablaNivelCapacidades = new DBGenericRequester<NivelCapacidad>(this, InternalNivelCapacidades);
            TablaPerfiles = new DBGenericRequester<Perfil>(this, InternalPerfiles);
            TablaExamenes = new DBGenericRequester<Examen>(this, InternalExamenes);
            TablaEvaluadores = new DBGenericRequester<Evaluador>(this, InternalEvaluadores);
            TablaTipoEvaluador = new DBGenericRequester<TipoEvaluador>(this, InternalTipoEvaluadores);
            TablaProcesoEvaluaciones = new DBGenericRequester<ProcesoEvaluacion>(this, InternalProcesoEvaluaciones);
            TablaPerfilXCompetencia = new DBGenericRequester<PerfilXCompetencia>(this, InternalPerfilXCompetencia);
            TablaEstadoColaboradorXProcesoEvaluaciones = new DBGenericRequester<EstadoColaboradorXProcesoEvaluacion>(this, InternalEstadoColaboradorXProcesoEvaluaciones);
            TablaColaboradorXProcesoEvaluaciones = new DBGenericRequester<ColaboradorXProcesoEvaluacion>(this, InternalColaboradorXProcesoEvaluaciones);
            TablaPuestoXEvaluadores = new DBGenericRequester<PuestoXEvaluadores>(this, InternalPuestoXEvaluadores);
            TablaCompetenciaXPuesto = new DBGenericRequester<CompetenciaXPuesto>(this, InternalCompetenciaXPuesto);
            TablaAreaXProcesoEvaluaciones = new DBGenericRequester<AreaXProcesoEvaluacion>(this, InternalAreaXProcesoEvaluaciones);
            TablaProcesoXEvaluado = new DBGenericRequester<ProcesoXEvaluado>(this, InternalProcesoXEvaluado);
            TablaEstadoProcesoEvaluacion = new DBGenericRequester<EstadoProcesoEvaluacion>(this, InternalEstadoProcesoEvaluacion);
		}

        // Area Evaluacion360

        private void SeedCompetencias()
        {
            TablaCompetencias.AddElement(new Competencia("Ser chiquito"));
            TablaCompetencias.AddElement(new Competencia("Ser grande"));
            TablaCompetencias.AddElement(new Competencia("Ser kiwi"));
        }

        private void SeedCapacidad()
        {
            TablaCapacidades.AddElement(new Capacidad("Trabajador",1,1));
        }

        private void SeedCompetenciasXPuesto()
        {
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(1,1,1));
        }

        private void SeedPuestoXEvaluadores()
        {
            //TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(TablaPuestos.One(p => p.Nombre.Eq a.Nombre.Equals("La gran Área")).ID));

            List<Puesto> losPuestos = TablaPuestos.All();

            foreach (Puesto puesto in losPuestos)
            {
                int suID = puesto.ID;


                TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(suID, true, "El mismo", 1, 50));
                TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(suID, true, "Jefe", 1, 25));
                TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(suID, false, "Pares", 0, 0));
                TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(suID, false, "Subordinados", 2, 25));
                TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(suID, false, "Clientes", 0, 0));
                TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(suID, false, "Otros", 0, 0));


            }

            //int puestoPresidenteID = TablaPuestos.One(p => p.Nombre.Equals("Presidente")).ID;


            //TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoPresidenteID, true, "El mismo", 1, 50));
            //TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoPresidenteID, true, "Jefe", 1, 25));
            //TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoPresidenteID, false, "Pares", 0, 0));
            //TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoPresidenteID, false, "Subordinados", 2, 25));
            //TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoPresidenteID, false, "Clientes", 0, 0));
            //TablaPuestoXEvaluadores.AddElement(new PuestoXEvaluadores(puestoPresidenteID, false, "Otros", 0, 0));


        }

        
        private void SeedPerfiles()
        {
            TablaPerfiles.AddElement(new Perfil("Gerente general"));
            TablaPerfiles.AddElement(new Perfil("Vendedor"));
            TablaPerfiles.AddElement(new Perfil("Programador Junior"));
            TablaPerfiles.AddElement(new Perfil("Jefe de RR.HH."));
            TablaPerfiles.AddElement(new Perfil("Colaborador de marketing"));
            TablaPerfiles.AddElement(new Perfil("Asistente de contabilidad"));
            TablaPerfiles.AddElement(new Perfil("Subgerente"));
            TablaPerfiles.AddElement(new Perfil("Analista de procesos"));
            TablaPerfiles.AddElement(new Perfil("Analista de riesgos"));
            TablaPerfiles.AddElement(new Perfil("Jefe Gestión de proyectos"));
            TablaPerfiles.AddElement(new Perfil("Programador Senior"));

        }

        private void SeedNivelCapacidades()
        {
            for (int i = 1; i <= 3; i++)
                TablaNivelCapacidades.AddElement(new NivelCapacidad(i));
        }

        private void SeedEstadoProcesoEvaluacion() { 
           TablaEstadoProcesoEvaluacion.AddElement(new EstadoProcesoEvaluacion{Descripcion = ConstantsEstadoProcesoEvaluacion.Creado});
           //TablaEstadoProcesoEvaluacion.AddElement(new EstadoProcesoEvaluacion{Descripcion = ConstantsEstadoProcesoEvaluacion.Iniciado});
           TablaEstadoProcesoEvaluacion.AddElement(new EstadoProcesoEvaluacion{Descripcion = ConstantsEstadoProcesoEvaluacion.EnProceso});
           TablaEstadoProcesoEvaluacion.AddElement(new EstadoProcesoEvaluacion{Descripcion = ConstantsEstadoProcesoEvaluacion.Terminado});
        }

        private void SeedEstadoPersonaXProcesoEvaluaciones()
        {
            TablaEstadoColaboradorXProcesoEvaluaciones.AddElement(new EstadoColaboradorXProcesoEvaluacion { Nombre = ConstantsEstadoColaboradorXProcesoEvaluacion.Pendiente });
            TablaEstadoColaboradorXProcesoEvaluaciones.AddElement(new EstadoColaboradorXProcesoEvaluacion { Nombre = ConstantsEstadoColaboradorXProcesoEvaluacion.Iniciado });
            TablaEstadoColaboradorXProcesoEvaluaciones.AddElement(new EstadoColaboradorXProcesoEvaluacion { Nombre = ConstantsEstadoColaboradorXProcesoEvaluacion.Terminado });
        }

        private void seedProcesosDeEvaluacion()
        {
            TablaProcesoEvaluaciones.AddElement(new ProcesoEvaluacion { AutorizadorID = 2, FechaCierre = new DateTime(2013, 12, 1), Nombre = "Proceso por defecto", EstadoProcesoEvaluacionID = TablaEstadoProcesoEvaluacion.One(e => e.Descripcion == ConstantsEstadoProcesoEvaluacion.Creado).ID });
        }

       /*private void seedEvaluacion()
       {
           TablaEvaluaciones.AddElement(new Evaluacion { Nombre= "evaluacion1",EvaluadoID = 2, EvaluadorID = 1, Puntuacion=100});
           TablaEvaluaciones.AddElement(new Evaluacion { Nombre = "evaluacion2", EvaluadoID = 3, EvaluadorID = 1, Puntuacion = 50 });
           TablaEvaluaciones.AddElement(new Evaluacion { Nombre = "evaluacion3", EvaluadoID = 4, EvaluadorID = 1, Puntuacion = 70 });
           TablaEvaluaciones.AddElement(new Evaluacion { Nombre = "evaluacion4", EvaluadoID = 5, EvaluadorID = 1, Puntuacion = 80 });           
           
           
       }*/


    }
}