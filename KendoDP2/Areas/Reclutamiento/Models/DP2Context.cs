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
        public DbSet<SolicitudPromocion> InternalSolicitudPromociones { get; set; }

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
        public DBGenericRequester<SolicitudPromocion> TablaSolicitudPromociones { get; set; }

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
            TablaSolicitudPromociones = new DBGenericRequester<SolicitudPromocion>(this, InternalSolicitudPromociones);

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
            TablaEstadosSolicitudes.AddElement(new EstadosSolicitudOfertaLaboral { Descripcion = "Cerrado" });
        }

        private void SeedOfertaLaboral()
        {
        }

        private void SeedPostulante()
        {
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
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Registrado" }); // Aprobado para Fase 1 == Aprobado Fase 1
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Aprobado Externo" }); // Aprobado para Fase 2 == Aprobado Fase 2
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Aprobado RRHH" }); // Aprobado para Fase 3 == Aprobado Fase 3
            TablaFasePostulacion.AddElement(new FasePostulacion { Descripcion = "Aprobado Jefe" });
        }

        private void SeedOfertaLaboralXPostulante()
        {
            //Colaborador como postulante (Mono):  CORRIGE DESPUES DEL CAMBIO QUE VOY A HACER
            //Oferta 5
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = TablaOfertaLaborales.One(x => x.Descripcion.Equals("Trabajo en operaciones")).ID,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Juan") && x.ApellidoPaterno.Equals("Cahuin") && x.Username == null).ID,
                EstadoPostulantePorOfertaID = 1,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = TablaOfertaLaborales.One(x => x.Descripcion.Equals("Trabajo en operaciones")).ID,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Andre") && x.ApellidoPaterno.Equals("Montoya") && x.Username == null).ID,
                EstadoPostulantePorOfertaID = 1,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = TablaOfertaLaborales.One(x => x.Descripcion.Equals("Trabajo en operaciones")).ID,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Hans") && x.ApellidoPaterno.Equals("Espinoza") && x.Username == null).ID,
                EstadoPostulantePorOfertaID = 1,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });
            //Oferta 4
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = TablaOfertaLaborales.One(x => x.Descripcion.Equals("Trabajo importante en el directorio")).ID,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Juan") && x.ApellidoPaterno.Equals("Cahuin") && x.Username == null).ID,
                EstadoPostulantePorOfertaID = 1,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = TablaOfertaLaborales.One(x => x.Descripcion.Equals("Trabajo importante en el directorio")).ID,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Andre") && x.ApellidoPaterno.Equals("Montoya") && x.Username == null).ID,
                EstadoPostulantePorOfertaID = 1,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = TablaOfertaLaborales.One(x => x.Descripcion.Equals("Trabajo importante en el directorio")).ID,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Hans") && x.ApellidoPaterno.Equals("Espinoza") && x.Username == null).ID,
                EstadoPostulantePorOfertaID = 1,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });
            //Oferta 3
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = TablaOfertaLaborales.One(x => x.Descripcion.Equals("Trabajo en ventas")).ID,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Juan") && x.ApellidoPaterno.Equals("Cahuin") && x.Username == null).ID,
                EstadoPostulantePorOfertaID = 1,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = TablaOfertaLaborales.One(x => x.Descripcion.Equals("Trabajo en ventas")).ID,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Andre") && x.ApellidoPaterno.Equals("Montoya") && x.Username == null).ID,
                EstadoPostulantePorOfertaID = 1,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });
            TablaOfertaLaboralXPostulante.AddElement(new OfertaLaboralXPostulante
            {
                OfertaLaboralID = TablaOfertaLaborales.One(x => x.Descripcion.Equals("Trabajo en ventas")).ID,
                PostulanteID = TablaPostulante.One(x => x.Nombres.Equals("Hans") && x.ApellidoPaterno.Equals("Espinoza") && x.Username == null).ID,
                EstadoPostulantePorOfertaID = 1,
                FlagAprobado = false,
                PuntajeTotal = 0,
                MotivoRechazo = String.Empty,
                Comentarios = String.Empty,
                Observaciones = String.Empty
            });
        }

        private void SeedFasePostulacionXOfertaLaboralXPostulante()
        {
            
        }
    }
}