﻿using KendoDP2.Areas.Configuracion.Models;
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

        public DbSet<OfertaLaboral> InternalOfertaLaborales { get; set; }
        public DbSet<EstadosSolicitudOfertaLaboral> InternalEstadosSolicitudes { get; set; }
        public DbSet<ModoSolicitudOfertaLaboral> InternalModosSolicitudes { get; set; }
        public DbSet<Postulante> InternalPostulante { get; set; }
        public DbSet<FasePostulacion> InternalFasePostulacion { get; set; }
        public DbSet<OfertaLaboralXPostulante> InternalOfertaLaboralXPostulante { get; set; }

        public DBGenericRequester<OfertaLaboral> TablaOfertaLaborales { get; set; }
        public DBGenericRequester<EstadosSolicitudOfertaLaboral> TablaEstadosSolicitudes { get; set; }
        public DBGenericRequester<ModoSolicitudOfertaLaboral> TablaModosSolicitudes { get; set; }
        public DBGenericRequester<Postulante> TablaPostulante { get; set; }
        public DBGenericRequester<FasePostulacion> TablaFasePostulacion { get; set; }
        public DBGenericRequester<OfertaLaboralXPostulante> TablaOfertaLaboralXPostulante { get; set; }

        private void RegistrarTablasReclutamiento()
        {
            TablaOfertaLaborales = new DBGenericRequester<OfertaLaboral>(this, InternalOfertaLaborales);
            TablaEstadosSolicitudes = new DBGenericRequester<EstadosSolicitudOfertaLaboral>(this, InternalEstadosSolicitudes);
            TablaModosSolicitudes = new DBGenericRequester<ModoSolicitudOfertaLaboral>(this, InternalModosSolicitudes);
            TablaPostulante = new DBGenericRequester<Postulante>(this, InternalPostulante);
            TablaFasePostulacion = new DBGenericRequester<FasePostulacion>(this, InternalFasePostulacion);
            TablaOfertaLaboralXPostulante = new DBGenericRequester<OfertaLaboralXPostulante>(this, InternalOfertaLaboralXPostulante);
        }

        private void SeedFasePostulacion()
        {
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Registrado" });
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Aprobado Externo" });
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Aprobado RRHH" });
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Aprobado Jefe" });
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Cerrado" });
        }

        private void SeedModosSolicitudes()
        {
            TablaModosSolicitudes.AddElement(new ModoSolicitudOfertaLaboral { Descripcion = "Convocatoria Pública" });
            TablaModosSolicitudes.AddElement(new ModoSolicitudOfertaLaboral { Descripcion = "Convocatoria Interna" });

        }

        private void SeedEstadosSolicitudes()
        {
            TablaEstadosSolicitudes.AddElement(new EstadosSolicitudOfertaLaboral { Descripcion = "Pendiente" });
            TablaEstadosSolicitudes.AddElement(new EstadosSolicitudOfertaLaboral { Descripcion = "Aprobado" });
            TablaEstadosSolicitudes.AddElement(new EstadosSolicitudOfertaLaboral { Descripcion = "Rechazado" });
        }

        private void SeedOfertaLaboral()
        {
            TablaOfertaLaborales.AddElement(new OfertaLaboral
            {
                PuestoID = TablaPuestos.One(a => a.Nombre.Equals("Presidente")).ID,
                AreaID = TablaAreas.One(a => a.Nombre.Equals("Directorio")).ID,
                ResponsableID = TablaColaboradores.One(a => a.ApellidoPaterno.Equals("Solorzano")).ID,
                EstadoSolicitudOfertaLaboralID = 1,
                FechaRequerimiento = DateTime.Now.AddDays(-1).ToString("MM/dd/yy"),
                FechaFinVigenciaSolicitud = DateTime.Now.AddDays(10).ToString("MM/dd/yy"),
                Descripcion = "",
                ModoSolicitudOfertaLaboralID = TablaModosSolicitudes.One(a => a.Descripcion.Equals("Convocatoria Interna")).ID,
                SueldoTentativo = 15000,
                Comentarios = "",
                NumeroVacantes = 3
            });

            TablaOfertaLaborales.AddElement(new OfertaLaboral
            {
                PuestoID = TablaPuestos.One(a => a.Nombre.Equals("Gerente general")).ID,
                AreaID = TablaAreas.One(a => a.Nombre.Equals("Gerencia general")).ID,
                ResponsableID = TablaColaboradores.One(a => a.ApellidoPaterno.Equals("Solorzano")).ID,
                EstadoSolicitudOfertaLaboralID = 1,
                FechaRequerimiento = DateTime.Now.AddDays(-1).ToString("MM/dd/yy"),
                FechaFinVigenciaSolicitud = DateTime.Now.AddDays(10).ToString("MM/dd/yy"),
                Descripcion = "",
                ModoSolicitudOfertaLaboralID = TablaModosSolicitudes.One(a => a.Descripcion.Equals("Convocatoria Interna")).ID,
                SueldoTentativo = 15000,
                Comentarios = "",
                NumeroVacantes = 3
            });
        }

        private void SeedPostulante()
        {
            TablaPostulante.AddElement(new Postulante
            {
                Nombres = "Postulante 1",
                ApellidoPaterno = "XXX",
                ApellidoMaterno = "YYY",
                CentroEstudios = "PUCP",
                CorreoElectronico = "algo@rhplusplus.com",
                TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID,
                NumeroDocumento = "70705645",
                GradoAcademicoID = TablaGradosAcademicos.One(ga => ga.Descripcion.Equals("Licenciado")).ID,
            });

            TablaPostulante.AddElement(new Postulante
            {
                Nombres = "Postulante 2",
                ApellidoPaterno = "XXX",
                ApellidoMaterno = "YYY",
                CentroEstudios = "PUCP",
                CorreoElectronico = "algo@rhplusplus.com",
                TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID,
                NumeroDocumento = "70455645",
                GradoAcademicoID = TablaGradosAcademicos.One(ga => ga.Descripcion.Equals("Licenciado")).ID,
            });

            TablaPostulante.AddElement(new Postulante
            {
                Nombres = "Postulante 3",
                ApellidoPaterno = "XXX",
                ApellidoMaterno = "YYY",
                CentroEstudios = "PUCP",
                CorreoElectronico = "algo@rhplusplus.com",
                TipoDocumentoID = TablaTiposDocumentos.One(d => d.Descripcion.Equals("DNI")).ID,
                NumeroDocumento = "70708445",
                GradoAcademicoID = TablaGradosAcademicos.One(ga => ga.Descripcion.Equals("Licenciado")).ID,
            });

        }

        private void SeedOfertaLaboralXPostulante()
        {
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = 1,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Postulante 1")).ID,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });

            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = 2,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Postulante 2")).ID,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });
        }
    }
}