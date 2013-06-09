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
        public DbSet<Pregunta> InternalPreguntas { get; set; }
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
        public DBGenericRequester<Pregunta> TablaPreguntas { get; set; }


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
            TablaPreguntas = new DBGenericRequester<Pregunta>(this, InternalPreguntas);
		}

        // Area Evaluacion360

        private void SeedCompetencias()
        {
            TablaCompetencias.AddElement(new Competencia("CONFIA EN SI MISMO Y SE MOTIVA POR EL RETO"));
            TablaCompetencias.AddElement(new Competencia("COMPRENDE LA ORGANIZACIÓN Y SU ENTORNO.  SABE CÓMO HACER QUE LAS COSAS SUCEDAN"));
            TablaCompetencias.AddElement(new Competencia("LOGRA IMPACTO E INFLUENCIA EN LA COMUNICACIÓN Y RELACIONES"));
            TablaCompetencias.AddElement(new Competencia("DEMUESTRA CURIOSIDAD, IMAGINACIÓN Y PENSAMIENTO CONCEPTUAL.  (LO IMAGINAMOS, LO HACEMOS)"));
            TablaCompetencias.AddElement(new Competencia("TRABAJA EN EQUIPO Y BRINDA COLABORACIÓN"));
            TablaCompetencias.AddElement(new Competencia("SE ORIENTA AL LOGRO"));
            TablaCompetencias.AddElement(new Competencia("Matemáticas"));
            TablaCompetencias.AddElement(new Competencia("Comunicación linguística"));
            TablaCompetencias.AddElement(new Competencia("Tratamiento de información digital"));
            TablaCompetencias.AddElement(new Competencia("Sociabilidad y ciudadanía"));
            TablaCompetencias.AddElement(new Competencia("Aprendizaje"));
            TablaCompetencias.AddElement(new Competencia("Iniciativa personal"));
            TablaCompetencias.AddElement(new Competencia("Manejo de situaciones"));
        }

        private void SeedCapacidad()
        {
      
            // COMPETENCIA 1
                // nivel 1
                TablaCapacidades.AddElement(new Capacidad("Trabaja sin necesidad de supervisión directa. ", 1, 1, 30));
                TablaCapacidades.AddElement(new Capacidad("Muestra seguridad y una actitud positiva ante los problemas u obstáculos.", 1, 1, 30));
                TablaCapacidades.AddElement(new Capacidad("Tiene claridad de cuáles son sus fortalezas y oportunidades de mejora a nivel de habilidades", 1, 1, 40));
                // nivel 2
                TablaCapacidades.AddElement(new Capacidad("Toma la iniciativa para proponer nuevas ideas en el desarrollo de su trabajo ", 2, 1, 20));
                TablaCapacidades.AddElement(new Capacidad("Muestra una actitud positiva hacia el cambio y se reta constantemente.", 2, 1, 40));
                TablaCapacidades.AddElement(new Capacidad("Actúa con rapidez y decisión ante una crisis. Para encontrar una solución puede probar nuevas formas de hacer las cosas ", 2, 1, 40));
                // nivel 3
                TablaCapacidades.AddElement(new Capacidad("Demuestra compromiso con su propio desarrollo, actualiza sus conocimientos para profundizar y ampliar su ámbito de conocimiento", 3, 1, 40));
                TablaCapacidades.AddElement(new Capacidad("Genera planes de acción para evitar problemas en el corto plazo ", 3, 1, 30));
                TablaCapacidades.AddElement(new Capacidad("Interactúa con otros de forma segura, tranquila y firme, comunicando sus opiniones con convicción, incluso en situaciones de crisis o conflicto.", 3, 1, 30));
            // COMPETENCIA 2
                // nivel 1
                TablaCapacidades.AddElement(new Capacidad("Entiende el funcionamiento de la organización ( estructura , lìneas de reporte ) y sabe trabajar adecuadamente en ella. ", 1, 2, 30));
                TablaCapacidades.AddElement(new Capacidad("Utiliza la estructura formal de la organización para conseguir los resultados y en determinadas ocasiones busca soporte en la estructura informal de la organización", 1, 2, 30));
                TablaCapacidades.AddElement(new Capacidad("Conoce quiénes son las personas clave en la toma de decisiones y las involucra en el momento adecuado. ", 1, 2, 40));
                // nivel 2
                TablaCapacidades.AddElement(new Capacidad("Identifica qué conductas, reacciones se pueden o no tener en ciertos momentos o roles. Sabe relacionarse con diferentes interlocutores.", 2, 2, 30));
                TablaCapacidades.AddElement(new Capacidad("Demuestra los valores BELCORP en su gestión diaria (Pasión, Orgullo, Compromiso, Liderazgo)", 2, 2, 30));
                TablaCapacidades.AddElement(new Capacidad("Conoce la estructura informal (las relaciones entre áreas, unidades y personas) y sabe cómo aprovecharla para obtener la mejor respuesta posible.", 2, 2, 40));
                // nivel 3
                TablaCapacidades.AddElement(new Capacidad("Conoce y usa las fuentes de poder formales e informales para lograr que las cosas sucedan.", 3, 2, 30));
                TablaCapacidades.AddElement(new Capacidad("Monitorea a la organización (resultados de negocio y cambios de estructura). Sabe y entiende qué está pasando con las personas, con la estructura y con el negocio.", 3, 2, 30));
                TablaCapacidades.AddElement(new Capacidad("Sabe cuándo y cómo solicitar el apoyo de diferentes áreas o personas en la organización para lograr mejores resultados.", 3, 2, 40));
            
            // COMPETENCIA 3
                // nivel 1
                TablaCapacidades.AddElement(new Capacidad("Utiliza la persuasión directa en una presentación o discusión", 1, 3, 30));
                TablaCapacidades.AddElement(new Capacidad("Se apoya de ejemplos concretos, ayudas visuales, demostraciones, de acuerdo a la situaciòn", 1, 3, 30));
                TablaCapacidades.AddElement(new Capacidad("Para lograr sus objetivos apela a la razón, a hechos y datos", 1, 3, 40));
                // nivel 2
                TablaCapacidades.AddElement(new Capacidad("Da dos o más pasos (acciones o estrategias) para persuadir sin intentar adaptarse específicamente a los intereses de los interlocutores. ", 2, 3, 30));
                TablaCapacidades.AddElement(new Capacidad("Elabora dos o más argumentos o puntos de vista diferentes en una presentación o discusión. Prepara cuidadosamente los datos y sustentos para una presentación. ", 2, 3, 30));
                TablaCapacidades.AddElement(new Capacidad("Anticipa las reacciones de los demás y se preparara para responder de manera adecuada a dichas reacciones.", 2, 3, 40));
                // nivel 3
                TablaCapacidades.AddElement(new Capacidad("Busca información especializada o de expertos para apoyar decisiones, influenciar o negociar. ", 3, 3, 30));
                TablaCapacidades.AddElement(new Capacidad("Lleva a cabo acciones inusuales o  particulares especialmente pensadas para producir un impacto determinado.", 3, 3, 30));
                TablaCapacidades.AddElement(new Capacidad("Piensa en el efecto que una acción o cualquier otro detalle producirá en la imagen que los demás tienen de él.", 1, 3, 40));
            // COMPETENCIA 4
                // nivel 1
                TablaCapacidades.AddElement(new Capacidad("Busca tener un mayor entendimiento realizando preguntas que lo orienten (por ejemplo, busca a los directamente involucrados para conocer más a fondo la situación y el problema).", 1, 4, 30));
                TablaCapacidades.AddElement(new Capacidad("Utiliza la información disponible para solucionar un problema y en caso no cuente con la necesaria, la investiga ", 1, 4, 30));
                TablaCapacidades.AddElement(new Capacidad("Utiliza reglas sencillas, el sentido común y la experiencia previa para identificar problemas y soluciones.", 1, 4, 40));
                // nivel 2
                TablaCapacidades.AddElement(new Capacidad("Es intelectualmente curioso(a). Muestra interés frente a los problemas u oportunidades, busca nuevos enfoques para enfrentarlos. Hace preguntas para profundizar.", 2, 4, 30));
                TablaCapacidades.AddElement(new Capacidad("Utiliza diversas fuentes de información que le permitan ampliar su ámbito de conocimiento. ", 2, 4, 30));
                TablaCapacidades.AddElement(new Capacidad("Identifica pautas, tendencias o vacíos en la información que maneja.", 2, 4, 40));
                // nivel 3
                TablaCapacidades.AddElement(new Capacidad("Reúne información y la analiza. Es capaz de generar hipótesis, investigar y sacar conclusiones.", 3, 4, 30));
                TablaCapacidades.AddElement(new Capacidad("Piensa de manera creativa para generar soluciones a problemas", 3, 4, 30));
                TablaCapacidades.AddElement(new Capacidad("1. Presenta propuestas para la soluciòn de problemas sustentadas en análisis e investigación", 1, 4, 40));
            // COMPETENCIA 5
                // nivel 1
                TablaCapacidades.AddElement(new Capacidad("Participa activamente en la formación y consolidación del equipo, muestra un espíritu constructivo y actitud positiva. ", 1, 5, 30));
                TablaCapacidades.AddElement(new Capacidad("Cumple con las responsabilidades asignadas por el equipo", 1, 5, 30));
                TablaCapacidades.AddElement(new Capacidad("Demuestra compromiso con el equipo contribuyendo más allá de su estricta responsabilidad, para alcanzar los objetivos establecidos por el equipo (da una milla extra cuando la situación lo requiere).", 1, 5, 40));
                // nivel 2
                TablaCapacidades.AddElement(new Capacidad("Comparte sus conocimientos, expertise, así como la información relevante y útil con todos los miembros del equipo.", 2, 5, 30));
                TablaCapacidades.AddElement(new Capacidad("Respeta las decisiones del equipo y  promueve en todos los miembros el cumplimiento de los compromisos individuales.", 2, 5, 30));
                TablaCapacidades.AddElement(new Capacidad("Mantiene al equipo informado de cambios en objetivos comunes y de situaciones que puedan afectar el logro de los objetivos.", 2, 5, 40));
                // nivel 3
                TablaCapacidades.AddElement(new Capacidad("1. Toma decisiones como miembro del equipo, considerando los factores del entorno    ", 3, 5, 30));
                TablaCapacidades.AddElement(new Capacidad("Expresa sus propios puntos de vista, y escucha a los demás, está abierto a nuevas ideas. ", 3, 5, 30));
                TablaCapacidades.AddElement(new Capacidad("Acompaña y respalda públicamente las decisiones y acciones del equipo", 1, 5, 40));
            // COMPETENCIA 6
                // nivel 1
                TablaCapacidades.AddElement(new Capacidad("Està en contacto con el cliente interno y externo y tiene alta disposiciòn a atender sus necesidades ", 1, 6, 30));
                TablaCapacidades.AddElement(new Capacidad("Mantiene una comunicación clara con el cliente interno y externo, entiende su requerimiento y concreta compromisos claros ", 1, 6, 30));
                TablaCapacidades.AddElement(new Capacidad("Es proactivo evalua diferentes opciones para responder a su cliente interno o externo", 1, 6, 40));
                // nivel 2
                TablaCapacidades.AddElement(new Capacidad("Comparte sus conocimientos, expertise, así como la información relevante y útil con todos los miembros del equipo.", 2, 6, 30));
                TablaCapacidades.AddElement(new Capacidad("Respeta las decisiones del equipo y  promueve en todos los miembros el cumplimiento de los compromisos individuales.", 2, 6, 30));
                TablaCapacidades.AddElement(new Capacidad("Mantiene al equipo informado de cambios en objetivos comunes y de situaciones que puedan afectar el logro de los objetivos.", 2, 6, 40));
                // nivel 3
                TablaCapacidades.AddElement(new Capacidad("1. Toma decisiones como miembro del equipo, considerando los factores del entorno.", 3, 6, 30));
                TablaCapacidades.AddElement(new Capacidad("Expresa sus propios puntos de vista, y escucha a los demás, está abierto a nuevas ideas. ", 3, 6, 30));
                TablaCapacidades.AddElement(new Capacidad("Acompaña y respalda públicamente las decisiones y acciones del equipo", 1, 6, 40));

                //Capacidades (Mono):
                TablaCapacidades.AddElement(new Capacidad("Razonamiento mátematico", 5, TablaCompetencias.Where(a => a.Nombre.Equals("Matemáticas")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Lógica matemática", 4, TablaCompetencias.Where(a => a.Nombre.Equals("Matemáticas")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Cálculo", 6, TablaCompetencias.Where(a => a.Nombre.Equals("Matemáticas")).First().ID, 40));
                TablaCapacidades.AddElement(new Capacidad("Relaciones interpersonales", 3, TablaCompetencias.Where(a => a.Nombre.Equals("Comunicación linguística")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Comunicación de ideas", 4, TablaCompetencias.Where(a => a.Nombre.Equals("Comunicación linguística")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Claridez en el habla", 5, TablaCompetencias.Where(a => a.Nombre.Equals("Comunicación linguística")).First().ID, 40));
                TablaCapacidades.AddElement(new Capacidad("Ofimática", 3, TablaCompetencias.Where(a => a.Nombre.Equals("Tratamiento de información digital")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Lógica digital", 7, TablaCompetencias.Where(a => a.Nombre.Equals("Tratamiento de información digital")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Manejo de archivos", 2, TablaCompetencias.Where(a => a.Nombre.Equals("Tratamiento de información digital")).First().ID, 40));
                TablaCapacidades.AddElement(new Capacidad("Empatía", 5, TablaCompetencias.Where(a => a.Nombre.Equals("Sociabilidad y ciudadanía")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Compañerismo", 6, TablaCompetencias.Where(a => a.Nombre.Equals("Sociabilidad y ciudadanía")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Preocupación grupal", 8, TablaCompetencias.Where(a => a.Nombre.Equals("Aprendizaje")).First().ID, 40));
                TablaCapacidades.AddElement(new Capacidad("Investigador", 4, TablaCompetencias.Where(a => a.Nombre.Equals("Aprendizaje")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Certero", 5, TablaCompetencias.Where(a => a.Nombre.Equals("Aprendizaje")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Trabajador ", 1, TablaCompetencias.Where(a => a.Nombre.Equals("Iniciativa personal")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Responsable", 1, TablaCompetencias.Where(a => a.Nombre.Equals("Iniciativa personal")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Manejo de presión", 3, TablaCompetencias.Where(a => a.Nombre.Equals("Iniciativa personal")).First().ID, 40));
                TablaCapacidades.AddElement(new Capacidad("Manejo de grupos", 7, TablaCompetencias.Where(a => a.Nombre.Equals("Manejo de situaciones")).First().ID, 30));
                TablaCapacidades.AddElement(new Capacidad("Responsabilidad", 5, TablaCompetencias.Where(a => a.Nombre.Equals("Manejo de situaciones")).First().ID, 30));
        }

        private void SeedCompetenciasXPuesto()
        {
            //(int competenciaId, int puestoId, int nivelId, peso)
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(1,1,3, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(2, 1, 3, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(3, 1, 3, 40));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(4, 2, 3,30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(5, 2, 3, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(6, 2, 3, 40));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(1,3,1, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(2,3,2, 70));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(3,4,3, 50));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(4,4,1,50));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(5,5,2, 50));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(6,5,3, 50));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(1,6,1,60));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(2,6,2, 40));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(3,7,3, 80));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(4,7,1, 20));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(5,8,2, 75));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(6,8,3, 25));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(1,9,1, 40));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(2,9,2, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(3,9,3, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(4,10,1, 20));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(5,10,2, 40));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(6,10,3, 40));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(1,11,1, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(2,11,2, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(3,11,3, 40));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(4,12,1, 100));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(5,13,2, 80));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(6,13,3, 20));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(1,14,1, 100));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(2,15,2, 50));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(3,15,3, 50));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(4,16,1, 40));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(5,16,2, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(6,16,3, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(1,17,1, 100));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(3,18,2, 35));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(2,18,2, 65 ));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(3,19,3, 55));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(4,19,1, 45));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(5,20,2, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(6,20,3, 70));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(1,21,1, 60));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(2,21,2, 20));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(3,21,3, 20));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(4,22,1, 20));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(5,22,2, 40));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(6,22,3, 40));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(1,23,1, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(2,23,2, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(3,23,3, 40));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(4,24,1, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(5,24,2, 30));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(6,24,3, 40));

            //CompetenciaXPuesto (Mono):
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Matemáticas")).ID,
                TablaPuestos.One(a => a.Nombre.Equals("Presidente")).ID, 1, 10));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Comunicación linguística")).ID, 
                TablaPuestos.One(a => a.Nombre.Equals("Presidente")).ID, 1, 10));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Tratamiento de información digital")).ID,
                TablaPuestos.One(a => a.Nombre.Equals("Presidente")).ID, 1, 10));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Manejo de situaciones")).ID, 
                TablaPuestos.One(a => a.Nombre.Equals("Presidente")).ID, 1, 10));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Sociabilidad y ciudadanía")).ID,
                TablaPuestos.One(a => a.Nombre.Equals("Presidente")).ID, 1, 10));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Matemáticas")).ID,
                TablaPuestos.One(a => a.Nombre.Equals("Gerente de ventas")).ID, 1, 10));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Comunicación linguística")).ID, 
                TablaPuestos.One(a => a.Nombre.Equals("Gerente de ventas")).ID, 1, 10));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Iniciativa personal")).ID, 
                TablaPuestos.One(a => a.Nombre.Equals("Gerente de ventas")).ID, 1, 10));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Manejo de situaciones")).ID, 
                TablaPuestos.One(a => a.Nombre.Equals("Gerente de ventas")).ID, 1, 10));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Comunicación linguística")).ID,
                TablaPuestos.One(a => a.Nombre.Equals("Gerente de operaciones")).ID, 1, 10));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Iniciativa personal")).ID, 
                TablaPuestos.One(a => a.Nombre.Equals("Gerente de operaciones")).ID, 1, 10));
            TablaCompetenciaXPuesto.AddElement(new CompetenciaXPuesto(TablaCompetencias.One(a => a.Nombre.Equals("Aprendizaje")).ID, 
                TablaPuestos.One(a => a.Nombre.Equals("Gerente de operaciones")).ID, 1, 10));
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
            for (int i = 1; i <= 8; i++)
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

        /*private void seedColaboradorXProcesoEvaluacion() 
        {
            TablaColaboradorXProcesoEvaluaciones.AddElement(new ColaboradorXProcesoEvaluacion { ColaboradorID=1, ProcesoEvaluacionID=1, EstadoColaboradorXProcesoEvaluacionID = TablaEstadoColaboradorXProcesoEvaluaciones.One(e=>e.Nombre==ConstantsEstadoColaboradorXProcesoEvaluacion.Pendiente).ID, ReferenciasPorAreas=0, ReferenciaDirecta =true});
            TablaColaboradorXProcesoEvaluaciones.AddElement(new ColaboradorXProcesoEvaluacion { ColaboradorID =3, ProcesoEvaluacionID = 1, EstadoColaboradorXProcesoEvaluacionID = TablaEstadoColaboradorXProcesoEvaluaciones.One(e => e.Nombre == ConstantsEstadoColaboradorXProcesoEvaluacion.Pendiente).ID, ReferenciasPorAreas = 0, ReferenciaDirecta = true });
        }*/
        /*
        private void SeedEvaluacion() 
        {
            TablaEvaluaciones.AddElement(new Evaluacion { Nombre = "Evaluacion 1", FechaCierre = new DateTime(2013, 12, 15), Puntuacion = 0, EvaluadoID=3,EvaluadorID=5});
        }*/
    }
}