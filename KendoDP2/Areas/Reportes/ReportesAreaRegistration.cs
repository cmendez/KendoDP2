﻿using System.Web.Mvc;

namespace KendoDP2.Areas.Reportes
{
    public class ReportesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Reportes";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Reportes_default",
                "Reportes/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
