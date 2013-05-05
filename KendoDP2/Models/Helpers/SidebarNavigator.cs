﻿using System;
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
            Opciones.Add(new SidebarOption(1,"", "Home", "Index", "Inicio", "icon-home"));
            
            //Seguridad
            Opciones.Add(new SidebarOption(2,"Seguridad", "Seguridad", "icon-user-mid", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption(21,"Usuarios", "Usuarios", "Index", "icon-user-md"),
                new SidebarSuboption(22,"Roles", "Roles", "Index", "icon-user-mid")
            })));

            // Evaluacion 360
            Opciones.Add(new SidebarOption(3,"Evaluacion360", "Evaluación 360°", "icon-pencil", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption(31,"Competencias", "Competencias", "Index", "icon-plus-sign"),
                new SidebarSuboption(32,"Capacidades", "Capacidades", "Index", "icon-check"),
                new SidebarSuboption(33,"Procesos de evaluación", "ProcesoEvaluacion", "Index", "icon-road"),
                new SidebarSuboption(34,"Evaluación de puestos de trabajo", "PuestosEvaluacion", "Index", "icon-ok-sign")
            })));

            // Objetivos
            Opciones.Add(new SidebarOption(4,"Objetivos", "Objetivos", "icon-bookmark", new List<SidebarSuboption>(new SidebarSuboption[]{
               new SidebarSuboption(41,"Objetivos de la empresa", "Objetivosempresa", "Index", "icon-ok")

            })));

            // Configuracion
            Opciones.Add(new SidebarOption(5,"Configuracion", "Configuración", "icon-wrench", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption(51,"Períodos", "Periodos", "Index", "icon-time")
            })));

            // Organizacion
            Opciones.Add(new SidebarOption(6,"Personal", "Personal", "icon-group", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption(61,"Colaboradores", "Colaboradores", "Index", "icon-time"),
                new SidebarSuboption(62,"Puestos", "Puestos", "Index", "icon-tag"),
                new SidebarSuboption(63,"Áreas", "Areas", "Index", "icon-sitemap")
            })));

            // Pako Puesto
            Opciones.Add(new SidebarOption(7,"Organizacion", "Organización", "icon-group  ", new List<SidebarSuboption>(new SidebarSuboption[]{
                new SidebarSuboption(71,"Puestos", "Puestos", "Index", "icon-time"),
                new SidebarSuboption(72,"Áreas", "Areas", "Index", "icon-time")
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
        public SidebarOption(int id,string area, string text, string icon, List<SidebarSuboption> suboptions) : this(id,area, null, null, text, icon, suboptions) { }
        public SidebarOption(int id,string area, string controller, string method, string text, string icon) : this(id,area, controller, method, text, icon, new List<SidebarSuboption>()) { }
        private SidebarOption(int id,string area, string controller, string method, string text, string icon, List<SidebarSuboption> suboptions)
        {
            ID = id;
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
        public SidebarSuboption(int id,string title, string controller, string method, string icon)
        {
            ID = id;
            Title = title;
            Icon = icon;
            Controller = controller;
            Method = method;
        }
    }

    
}