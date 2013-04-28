using System.Web.Mvc;

namespace KendoDP2.Areas.Organizacion
{
    public class OrganizacionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Organizacion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Organizacion_default",
                "Organizacion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
