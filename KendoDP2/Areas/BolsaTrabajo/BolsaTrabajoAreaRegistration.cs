using System.Web.Mvc;

namespace KendoDP2.Areas.BolsaTrabajo
{
    public class BolsaTrabajoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BolsaTrabajo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BolsaTrabajo_default",
                "BolsaTrabajo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
