using System.Web.Mvc;

namespace KendoDP2.Areas.Carrera
{
    public class CarreraAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Carrera";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Carrera_default",
                "Carrera/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
