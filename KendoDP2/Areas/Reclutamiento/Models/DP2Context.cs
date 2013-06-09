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

        public DbSet<OfertaLaboral> InternalOfertaLaborales { get; set; }
        public DbSet<EstadosSolicitudOfertaLaboral> InternalEstadosSolicitudes { get; set; }
        public DbSet<ModoSolicitudOfertaLaboral> InternalModosSolicitudes { get; set; }
        public DbSet<Postulante> InternalPostulante { get; set; }
        public DbSet<FasePostulacion> InternalFasePostulacion { get; set; }
        public DbSet<OfertaLaboralXPostulante> InternalOfertaLaboralXPostulante { get; set; }
        public DbSet<FasePostulacionXOfertaLaboralXPostulante> InternalFasePostulacionXOfertaLaboralXPostulante { get; set; }
        public DbSet<EvaluacionXFaseXPostulacion> InternalEvaluacionXFaseXPostulacion { get; set; }
        public DbSet<Respuesta> InternalRespuesta { get; set; }
        public DbSet<EstadoPostulantePorOferta> InternalEstadoPostulantePorOferta { get; set; }

        public DBGenericRequester<OfertaLaboral> TablaOfertaLaborales { get; set; }
        public DBGenericRequester<EstadosSolicitudOfertaLaboral> TablaEstadosSolicitudes { get; set; }
        public DBGenericRequester<ModoSolicitudOfertaLaboral> TablaModosSolicitudes { get; set; }
        public DBGenericRequester<Postulante> TablaPostulante { get; set; }
        public DBGenericRequester<FasePostulacion> TablaFasePostulacion { get; set; }
        public DBGenericRequester<OfertaLaboralXPostulante> TablaOfertaLaboralXPostulante { get; set; }
        public DBGenericRequester<FasePostulacionXOfertaLaboralXPostulante> TablaFasePostulacionXOfertaLaboralXPostulante { get; set; }
        public DBGenericRequester<EvaluacionXFaseXPostulacion> TablaEvaluacionXFaseXPostulacion { get; set; }
        public DBGenericRequester<Respuesta> TablaRespuesta { get; set; }
        public DBGenericRequester<EstadoPostulantePorOferta> TablaEstadoPostulanteXOferta { get; set; }

        private void RegistrarTablasReclutamiento()
        {
            TablaOfertaLaborales = new DBGenericRequester<OfertaLaboral>(this, InternalOfertaLaborales);
            TablaEstadosSolicitudes = new DBGenericRequester<EstadosSolicitudOfertaLaboral>(this, InternalEstadosSolicitudes);
            TablaModosSolicitudes = new DBGenericRequester<ModoSolicitudOfertaLaboral>(this, InternalModosSolicitudes);
            TablaPostulante = new DBGenericRequester<Postulante>(this, InternalPostulante);
            TablaFasePostulacion = new DBGenericRequester<FasePostulacion>(this, InternalFasePostulacion);
            TablaOfertaLaboralXPostulante = new DBGenericRequester<OfertaLaboralXPostulante>(this, InternalOfertaLaboralXPostulante);
            TablaFasePostulacionXOfertaLaboralXPostulante = new DBGenericRequester<FasePostulacionXOfertaLaboralXPostulante>(this, InternalFasePostulacionXOfertaLaboralXPostulante);
            TablaEvaluacionXFaseXPostulacion = new DBGenericRequester<EvaluacionXFaseXPostulacion>(this, InternalEvaluacionXFaseXPostulacion);
            TablaRespuesta = new DBGenericRequester<Respuesta>(this, InternalRespuesta);
            TablaEstadoPostulanteXOferta = new DBGenericRequester<EstadoPostulantePorOferta>(this, InternalEstadoPostulantePorOferta);
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
                PuestoID = 1,
                AreaID = TablaAreas.One(a => a.Nombre.Equals("Directorio")).ID,
                ResponsableID = TablaColaboradores.One(a => a.ApellidoPaterno.Equals("Solorzano")).ID,
                EstadoSolicitudOfertaLaboralID = TablaEstadosSolicitudes.One(a => a.Descripcion.Equals("Aprobado")).ID,
                FechaRequerimiento = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"),
                FechaFinVigenciaSolicitud = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy"),
                Descripcion = "Trabajo en el directorio",
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
                FechaRequerimiento = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"),
                FechaFinVigenciaSolicitud = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy"),
                Descripcion = "Trabajo de gerencia",
                ModoSolicitudOfertaLaboralID = TablaModosSolicitudes.One(a => a.Descripcion.Equals("Convocatoria Interna")).ID,
                SueldoTentativo = 15000,
                Comentarios = "",
                NumeroVacantes = 3
            });

            TablaOfertaLaborales.AddElement(new OfertaLaboral
            {
                PuestoID = 3,
                AreaID = TablaPuestos.One(a => a.ID == 3).AreaID,
                ResponsableID = TablaColaboradores.One(a => a.ApellidoPaterno.Equals("Solorzano")).ID,
                EstadoSolicitudOfertaLaboralID = TablaEstadosSolicitudes.One(a => a.Descripcion.Equals("Aprobado")).ID,
                FechaRequerimiento = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"),
                FechaFinVigenciaSolicitud = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy"),
                Descripcion = "Trabajo en ventas",
                ModoSolicitudOfertaLaboralID = TablaModosSolicitudes.One(a => a.Descripcion.Equals("Convocatoria Interna")).ID,
                SueldoTentativo = 15000,
                Comentarios = "",
                NumeroVacantes = 3
            });

            TablaOfertaLaborales.AddElement(new OfertaLaboral
            {
                PuestoID = 1,
                AreaID = TablaPuestos.One(a=>a.ID == 1).AreaID,
                ResponsableID = TablaColaboradores.One(a => a.ApellidoPaterno.Equals("Solorzano")).ID,
                EstadoSolicitudOfertaLaboralID = TablaEstadosSolicitudes.One(a => a.Descripcion.Equals("Aprobado")).ID,
                FechaRequerimiento = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"),
                FechaFinVigenciaSolicitud = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy"),
                Descripcion = "Trabajo en el directorio",
                ModoSolicitudOfertaLaboralID = TablaModosSolicitudes.One(a => a.Descripcion.Equals("Convocatoria Interna")).ID,
                SueldoTentativo = 15000,
                Comentarios = "",
                NumeroVacantes = 3
            });

            TablaOfertaLaborales.AddElement(new OfertaLaboral
            {
                PuestoID = 6,
                AreaID = TablaPuestos.One(a => a.ID == 1).AreaID,
                ResponsableID = TablaColaboradores.One(a => a.ApellidoPaterno.Equals("Solorzano")).ID,
                EstadoSolicitudOfertaLaboralID = TablaEstadosSolicitudes.One(a => a.Descripcion.Equals("Aprobado")).ID,
                FechaRequerimiento = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"),
                FechaFinVigenciaSolicitud = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy"),
                Descripcion = "Trabajo en operaciones",
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

            //Colaborador que va a tener creado su entidad Postulante para postular a una OfertaLaboral
            TablaPostulante.AddElement(new Postulante(TablaColaboradores.One(x => x.Nombres.Equals("Colaborador Postulante"))));
        }

        private void SeedEstadoPostulantePorOferta()
        {
            TablaEstadoPostulanteXOferta.AddElement(new EstadoPostulantePorOferta { Descripcion = "Inscrito" });
            TablaEstadoPostulanteXOferta.AddElement(new EstadoPostulantePorOferta { Descripcion = "Aprobado Fase 1" });
            TablaEstadoPostulanteXOferta.AddElement(new EstadoPostulantePorOferta { Descripcion = "Aprobado Fase 2" });
            TablaEstadoPostulanteXOferta.AddElement(new EstadoPostulantePorOferta { Descripcion = "Aprobado Fase 3" });
            TablaEstadoPostulanteXOferta.AddElement(new EstadoPostulantePorOferta { Descripcion = "Rechazado" });
            TablaEstadoPostulanteXOferta.AddElement(new EstadoPostulantePorOferta { Descripcion = "Rechazado Fase 1" });
            TablaEstadoPostulanteXOferta.AddElement(new EstadoPostulantePorOferta { Descripcion = "Rechazado Fase 2" });
            TablaEstadoPostulanteXOferta.AddElement(new EstadoPostulantePorOferta { Descripcion = "Rechazado Fase 3" });
            TablaEstadoPostulanteXOferta.AddElement(new EstadoPostulantePorOferta { Descripcion = "Contratado" });

        }
        
        private void SeedFasePostulacion()
        {
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Registrado" });
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Aprobado Externo" });
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Aprobado RRHH" });
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Aprobado Jefe" });
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Cerrado" });
        }

        private void SeedOfertaLaboralXPostulante()
        {
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = 1,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Postulante 1")).ID,
                EstadoPostulantePorOfertaID = 1,
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
                EstadoPostulantePorOfertaID = 1,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });

            //Colaborador que va a tener creado su entidad Postulante para postular a una OfertaLaboral
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = 1,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Colaborador Postulante")).ID,
                EstadoPostulantePorOfertaID  = 1,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });
        }

        private void SeedFasePostulacionXOfertaLaboralXPostulante()
        {
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Registrado")).ID, OfertaLaboralXPostulanteID = 1 });
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Aprobado Externo")).ID, OfertaLaboralXPostulanteID = 1 });
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Aprobado RRHH")).ID, OfertaLaboralXPostulanteID = 1 });
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Aprobado Jefe")).ID, OfertaLaboralXPostulanteID = 1 });
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Registrado")).ID, OfertaLaboralXPostulanteID = 2 });
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Aprobado Externo")).ID, OfertaLaboralXPostulanteID = 2 });
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Aprobado RRHH")).ID, OfertaLaboralXPostulanteID = 2 });
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Aprobado Jefe")).ID, OfertaLaboralXPostulanteID = 2 });
            
            //Colaborador que va a tener creado su entidad Postulante para postular a una OfertaLaboral
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Registrado")).ID, OfertaLaboralXPostulanteID = 3 });
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Aprobado Externo")).ID, OfertaLaboralXPostulanteID = 3 });
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Aprobado RRHH")).ID, OfertaLaboralXPostulanteID = 3 });
            TablaFasePostulacionXOfertaLaboralXPostulante.AddElement(new FasePostulacionXOfertaLaboralXPostulante { FasePostulacionID = TablaFasePostulacion.One(x => x.Descripcion.Equals("Aprobado Jefe")).ID, OfertaLaboralXPostulanteID = 3 });
        }
    }
}