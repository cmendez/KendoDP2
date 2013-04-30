using System.Web.Mvc;

namespace KendoDP2.Areas.Objetivos
{
    public class ObjetivosAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Objetivos";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Objetivos_default",
                "Objetivos/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
