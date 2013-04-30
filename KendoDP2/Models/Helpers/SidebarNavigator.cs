﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

            // Evaluacion 360
            Opciones.Add(new SidebarOption("Evaluacion360", "Evaluación 360°", "icon-pencil", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption("Por perfil", "Configuracion360", "Index", "icon-group"),
                new SidebarSuboption("Competencias", "Competencias", "Index", "icon-plus-sign"),
                new SidebarSuboption("Capacidades", "Capacidades", "Index", "icon-check"),
                new SidebarSuboption("Procesos de evaluación", "ProcesoEvaluacion", "Index", "icon-road")
            })));
            // Configuracion
            Opciones.Add(new SidebarOption("Configuracion", "Configuración", "icon-wrench", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption("Períodos", "Periodos", "Index", "icon-time")
            })));
        }
    }

    public class SidebarOption
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public List<SidebarSuboption> Suboptions { get; set; }

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

    public class SidebarSuboption
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