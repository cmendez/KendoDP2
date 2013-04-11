using System.Web.Mvc;

namespace KendoDP2.Areas.Reclutamiento
{
    public class ReclutamientoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Reclutamiento";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Reclutamiento_default",
                "Reclutamiento/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
