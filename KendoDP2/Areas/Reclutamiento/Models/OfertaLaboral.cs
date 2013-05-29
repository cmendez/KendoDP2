﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Areas.Organizacion.Models;
using KendoDP2.Models.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoDP2.Areas.Evaluacion360.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;
using System.Security.Principal;
using System.Web.Mvc;

namespace KendoDP2.Areas.Reclutamiento.Models
{
    public class OfertaLaboral : DBObject
    {
        public int PuestoID { get; set; }
        public virtual Puesto Puesto { get; set; }

        public int AreaID { get; set; }
        public virtual Area Area { get; set; }

        public int ResponsableID { get; set; }
        public virtual Colaborador Responsable { get; set; }

        public int EstadoSolicitudOfertaLaboralID { get; set; }
        public virtual EstadosSolicitudOfertaLaboral EstadoSolicitudOfertaLaboral { get; set; }

        public string FechaRequerimiento { get; set; }

        public string FechaFinVigenciaSolicitud { get; set; }
        
        public string Descripcion { get; set; }

        public int ModoSolicitudOfertaLaboralID { get; set; }
        public virtual ModoSolicitudOfertaLaboral ModoSolicitudOfertaLaboral { get; set; }

        public int SueldoTentativo { get; set; }

        public string Comentarios { get; set; }

        public int NumeroVacantes { get; set; }

        [InverseProperty("OfertaLaboral")]
        public virtual ICollection<OfertaLaboralXPostulante> Postulantes { get; set; }

        public string FechaPublicacion { get; set; }

        public OfertaLaboral(OfertaLaboralDTO o) : this()
        {
            LoadFromDTO(o);
        }

        //Constructor
        public OfertaLaboral()
        {
        }

        public OfertaLaboral LoadFromDTO(OfertaLaboralDTO o)
        {
            ID = o.ID;
            PuestoID = o.PuestoID;
            AreaID = o.AreaID;
            ResponsableID = o.ResponsableID;
            ModoSolicitudOfertaLaboralID = o.ModoSolicitudID;
            EstadoSolicitudOfertaLaboralID = o.EstadoSolicitudOfertaLaboralID;
            Descripcion = o.Descripcion;
            FechaFinVigenciaSolicitud = o.FechaFinRequerimiento;
            FechaRequerimiento = o.FechaRequerimiento;
            SueldoTentativo = o.SueldoTentativo;
            Comentarios = o.Comentarios;
            NumeroVacantes = o.NumeroVacantes;
            FechaPublicacion = o.FechaPublicacion;
            return this;
        }

        public OfertaLaboralDTO ToDTO()
        {
            return new OfertaLaboralDTO(this);
        }

        public OfertaLaboralMobilePostulanteDTO ToMobilePostulanteDTO(string userName)
        {
            return new OfertaLaboralMobilePostulanteDTO(this, userName);
        }
    }

    public class OfertaLaboralDTO
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
        [DisplayName("Puesto")]
        [UIHint("GridForeignKey")]
        public int PuestoID { get; set; }

        public string Puesto { get; set; }

        [DisplayName("Area")]
        [UIHint("GridForeignKey")]
        public int AreaID { get; set; }

        public string Area { get; set; }

        [DisplayName("Responsable")]
        public int ResponsableID { get; set; }
        
        [DisplayName("Tipo Convocatoria")]
        public int ModoSolicitudID { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [DisplayName("Fecha de Registro")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaRequerimiento { get; set; }

        [DisplayName("Fecha Límite de Solicitud")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaFinRequerimiento { get; set; }

        [DisplayName("Estado de Solicitud")]
        public int EstadoSolicitudOfertaLaboralID { get; set; }

        [DisplayName("Numero de Vacantes")]
        public int NumeroVacantes { get; set; }

        [DisplayName("Comentarios")]
        public string Comentarios { get; set; }

        [DisplayName("Sueldo Tentativo S/.")]
        public int SueldoTentativo { get; set; }

        [DisplayName("Código")]
        public string Codigo { get; set; }
        

        [DisplayName("Fecha Publicación")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaPublicacion { get; set; }
        
        public OfertaLaboralDTO() { }

        public OfertaLaboralDTO(OfertaLaboral o)
        {
            ID = o.ID;
            PuestoID = o.PuestoID;
            Puesto = o.Puesto.Descripcion;
            AreaID = o.AreaID;
            Area = o.Area.Descripcion;
            ResponsableID = o.ResponsableID;
            EstadoSolicitudOfertaLaboralID = o.EstadoSolicitudOfertaLaboralID;
            ModoSolicitudID = o.ModoSolicitudOfertaLaboralID;
            FechaRequerimiento = o.FechaRequerimiento;
            FechaFinRequerimiento = o.FechaFinVigenciaSolicitud;
            Descripcion = o.Descripcion;
            NumeroVacantes = o.NumeroVacantes;
            Comentarios = o.Comentarios;
            SueldoTentativo = o.SueldoTentativo;
            FechaPublicacion = o.FechaPublicacion;
        }

        public ICollection<FuncionDTO> ListaFuncionesToDTO(ICollection<Funcion> funciones)
        {
            ICollection<FuncionDTO> ListaDTO = null;
            FuncionDTO fun = new FuncionDTO();

            foreach (Funcion f in funciones)
            {
                fun = new FuncionDTO(f);
                ListaDTO.Add(fun);
            }

            return ListaDTO;
        }
    }

    public class OfertaLaboralXPostulanteWSDTO //Para el WS getOfertaLaboral
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
        public string Puesto { get; set; }
        public string Area { get; set; }
        public string Responsable { get; set; }
        //public int ModoPublicacionID { get; set; }
        //public string Descripcion { get; set; }
        public string FechaRequerimiento { get; set; }
        public int NumeroPostulantes { get; set; }
        //public string FechaFinRequerimiento { get; set; }
        //public int EstadoSolicitudOfertaLaboralID { get; set; }

        //public string FechaUltimaEntrevista { get; set; } //pedido por el profe segun Cesarin

        public List<PostulanteDTO> Postulantes { get; set; }

        public OfertaLaboralXPostulanteWSDTO() { }

        public OfertaLaboralXPostulanteWSDTO(OfertaLaboral oflab, List<Postulante> lstPostulantes)
        {
            ID = oflab.ID;
            Puesto = oflab.Puesto.Nombre;
            Area = oflab.Area.Nombre;
            Responsable = oflab.Responsable.Nombres + " " + oflab.Responsable.ApellidoPaterno + " " + oflab.Responsable.ApellidoMaterno;
            FechaRequerimiento = oflab.FechaRequerimiento;
            NumeroPostulantes = lstPostulantes.Count;
            Postulantes = lstPostulantes.Select(x => x.ToDTO()).ToList();
        }
    }


    //WS para android para que cualquier colaborador pueda ver a qué puestos puede postular
    public class OfertaLaboralMobilePostulanteDTO 
    {
        public int ID { get; set; }
        public string NombreAreaPuesto { get; set; }
        public string DescripcionOferta { get; set; }
        public int SueldoTentativo { get; set; } 
        public ICollection<FuncionDTO> Funciones { get; set; }
        public ICollection<CompetenciaConPonderadoDTO> CompetenciasPonderadasPuesto { get; set; }
        public ICollection<CompetenciaConPonderadoDTO> CompetenciasPonderadasColaborador { get; set; }
        public double MatchLevel { get; set; }

        public OfertaLaboralMobilePostulanteDTO(OfertaLaboral oferta, string userName)
        {
            ID = oferta.ID;
            NombreAreaPuesto = oferta.Area.Nombre + "-" +oferta.Puesto.Nombre;
            DescripcionOferta = oferta.Descripcion;
            SueldoTentativo = oferta.SueldoTentativo;
            Funciones = ListaFuncionesToDTO(oferta.Puesto.Funciones);
            CompetenciasPonderadasPuesto = ListaCompetenciasConPonderadoToDTO(oferta.Puesto.CompetenciasXPuesto);
            //Las competencias del puesto del colaborador:
            var context = new DP2Context();
            Colaborador colaboradorActual = context.TablaColaboradores.Where(a => a.Username.Equals(userName)).First();
            Puesto puesto = context.TablaColaboradoresXPuestos.Where(a=>a.ColaboradorID == colaboradorActual.ID).Select(a=>a.Puesto).First();
            CompetenciasPonderadasColaborador = ListaCompetenciasConPonderadoToDTO(puesto.CompetenciasXPuesto);
            //MatchLevel:
            double sumaCompetenciasPuesto = 1;
            foreach (CompetenciaConPonderadoDTO competencia in CompetenciasPonderadasPuesto)
            {
                sumaCompetenciasPuesto += competencia.Ponderado;
            }
            double sumaCompetenciasColaborador = 0;
            foreach (CompetenciaConPonderadoDTO competencia in CompetenciasPonderadasColaborador)
            {
                sumaCompetenciasColaborador += competencia.Ponderado;
            }
            MatchLevel = sumaCompetenciasColaborador / sumaCompetenciasPuesto;
        }

        //Funciones Auxiliares
        public ICollection<FuncionDTO> ListaFuncionesToDTO(ICollection<Funcion> funciones)
        {
            List<FuncionDTO> ListaDTO = new List<FuncionDTO>();
            FuncionDTO fun = new FuncionDTO();

            foreach (Funcion f in funciones)
            {
                fun = new FuncionDTO(f);
                ListaDTO.Add(fun);
            }
            return ListaDTO;
        }

        public ICollection<CompetenciaConPonderadoDTO> ListaCompetenciasConPonderadoToDTO(ICollection<CompetenciaXPuesto> competencias)
        {
            List<CompetenciaConPonderadoDTO> ListaDTO = new List<CompetenciaConPonderadoDTO>();
            CompetenciaConPonderadoDTO comp;

            foreach (CompetenciaXPuesto c in competencias)
            {
                comp = new CompetenciaConPonderadoDTO(c);
                ListaDTO.Add(comp);
            }
            return ListaDTO;
        }
    }
}