using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoDP2.Models.Helpers
{
    public class SidebarNavigator
    {
        public List<SidebarOption> Opciones { get; set; }

        public SidebarNavigator()
        {
            Opciones = new List<SidebarOption>();
            // Inicio
            Opciones.Add(new SidebarOption("", "Home", "Index", "Inicio", "icon-home", new List<SidebarSuboption>()));

            // Evaluacion 360
            Opciones.Add(new SidebarOption("Evaluacion360", null, null, "Evaluación 360°", "icon-pencil", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption("Competencias", "Competencias", "Index", "Competencias", "icon-plus-sign"),
                new SidebarSuboption("Capacidades", "Capacidades", "Index", "Capacidades", "icon-check")
            })));
        }
    }

    public class SidebarOption
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public List<SidebarSuboption> Suboptions { get; set; }

        public SidebarOption(string area, string text, string icon) : this(area, null, null, text, icon, new List<SidebarSuboption>()) { }

        public SidebarOption(string area, string controller, string method, string text, string icon, List<SidebarSuboption> suboptions)
        {
            Area = area;
            Text = text;
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
        public string Text { get; set; }
        public string Icon { get; set; }
        public SidebarSuboption(string title, string controller, string method, string text, string icon)
        {
            Title = title;
            Text = text;
            Icon = icon;
            Controller = controller;
            Method = method;
        }
    }

    
}