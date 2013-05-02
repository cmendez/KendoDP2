using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KendoDP2.Models.Generic;

namespace KendoDP2.Models.Helpers
{
    public class SidebarNavigator
    {
        public List<SidebarOption> Opciones { get; set; }

        // Puedes encontar iconos en http://fortawesome.github.io/Font-Awesome/
        public SidebarNavigator()
        {

            Opciones = new List<SidebarOption>();
            // Agregue aqui  las opciones y subopciones del navegador de la barra de menu

            // Inicio
            Opciones.Add(new SidebarOption("", "Home", "Index", "Inicio", "icon-home"));
            
            //Seguridad
            Opciones.Add(new SidebarOption("Seguridad", "Seguridad", "icon-user-mid", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption("Roles", "Roles", "Index", "icon-user-mid")
            })));

            // Evaluacion 360
            Opciones.Add(new SidebarOption("Evaluacion360", "Evaluación 360°", "icon-pencil", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption("Competencias", "Competencias", "Index", "icon-plus-sign"),
                new SidebarSuboption("Capacidades", "Capacidades", "Index", "icon-check"),
                new SidebarSuboption("Procesos de evaluación", "ProcesoEvaluacion", "Index", "icon-road")
            })));

            // Objetivos
            Opciones.Add(new SidebarOption("Objetivos", "Objetivos", "icon-bookmark", new List<SidebarSuboption>(new SidebarSuboption[]{
               new SidebarSuboption("Objetivos de la empresa", "Objetivosempresa", "Index", "icon-ok")
            })));

            // Configuracion
            Opciones.Add(new SidebarOption("Configuracion", "Configuración", "icon-wrench", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption("Períodos", "Periodos", "Index", "icon-time")
            })));

            // Personal
            Opciones.Add(new SidebarOption("Personal", "Personal", "icon-group", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption("Colaboradores", "Colaboradores", "Index", "icon-time")
            })));

            // Pako Puesto
            Opciones.Add(new SidebarOption("Organizacion", "Organización", "icon-group  ", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption("Puestos", "Puestos", "Index", "icon-time"),
                new SidebarSuboption("Áreas", "Areas", "Index", "icon-time")
            })));
        }
    }

    public class SidebarOption : DBObject
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public List<SidebarSuboption> Suboptions { get; set; }

        public SidebarOption() { }
        public SidebarOption(string area, string text, string icon, List<SidebarSuboption> suboptions) : this(area, null, null, text, icon, suboptions) { }
        public SidebarOption(string area, string controller, string method, string text, string icon) : this(area, controller, method, text, icon, new List<SidebarSuboption>()) { }
        private SidebarOption(string area, string controller, string method, string text, string icon, List<SidebarSuboption> suboptions)
        {
            Area = area;
            Title = text;
            Icon = icon;
            Suboptions = suboptions;
            Controller = controller;
            Method = method;
        }
    }

    public class SidebarSuboption : DBObject
    {
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public string Icon { get; set; }
        public SidebarSuboption(string title, string controller, string method, string icon)
        {
            Title = title;
            Icon = icon;
            Controller = controller;
            Method = method;
        }
    }

    
}