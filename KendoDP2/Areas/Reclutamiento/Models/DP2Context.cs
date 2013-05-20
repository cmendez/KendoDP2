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
        public DbSet<EstadosOfertaLaboralXPostulante> InternalEstadosOfertaLaboralXPostulante { get; set; }

        public DBGenericRequester<OfertaLaboral> TablaOfertaLaborales { get; set; }
        public DBGenericRequester<EstadosSolicitudOfertaLaboral> TablaEstadosSolicitudes { get; set; }
        public DBGenericRequester<ModoSolicitudOfertaLaboral> TablaModosSolicitudes { get; set; }
        public DBGenericRequester<Postulante> TablaPostulante { get; set; }
        public DBGenericRequester<EstadosOfertaLaboralXPostulante> TablaEstadosOfertaLaboralXPostulante { get; set; }

        private void RegistrarTablasReclutamiento()
        {
            TablaOfertaLaborales = new DBGenericRequester<OfertaLaboral>(this, InternalOfertaLaborales);
            TablaEstadosSolicitudes = new DBGenericRequester<EstadosSolicitudOfertaLaboral>(this, InternalEstadosSolicitudes);
            TablaModosSolicitudes = new DBGenericRequester<ModoSolicitudOfertaLaboral>(this, InternalModosSolicitudes);
            TablaPostulante = new DBGenericRequester<Postulante>(this, InternalPostulante);
            TablaEstadosOfertaLaboralXPostulante = new DBGenericRequester<EstadosOfertaLaboralXPostulante>(this, InternalEstadosOfertaLaboralXPostulante);

        }

        private void SeedEstadosOfertaLaboralXPostulante()
        {
            TablaEstadosOfertaLaboralXPostulante.AddElement(new EstadosOfertaLaboralXPostulante { Descripcion = "Registrado" });
            TablaEstadosOfertaLaboralXPostulante.AddElement(new EstadosOfertaLaboralXPostulante { Descripcion = "Aprobado Externo" });
            TablaEstadosOfertaLaboralXPostulante.AddElement(new EstadosOfertaLaboralXPostulante { Descripcion = "Aprobado RRHH" });
            TablaEstadosOfertaLaboralXPostulante.AddElement(new EstadosOfertaLaboralXPostulante { Descripcion = "Aprobado Jefe" });
            TablaEstadosOfertaLaboralXPostulante.AddElement(new EstadosOfertaLaboralXPostulante { Descripcion = "Cerrado" });
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

            //TablaOfertaLaboral.AddElement(new OfertaLaboral { EstadoSolicitudOfertaLaboralID = 1, PuestoID = TablaPuestos.One(a => a.Nombre.Equals("Presidente")).ID });
        }

        private void SeedPostulante()
        {
            //TablaPostulante.AddElement(new Postulante );

        }
    }
}