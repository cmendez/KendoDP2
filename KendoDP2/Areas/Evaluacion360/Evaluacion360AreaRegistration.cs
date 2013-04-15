using System.Web.Mvc;

namespace KendoDP2.Areas.Evaluacion360
{
    public class Evaluacion360AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Evaluacion360";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Evaluacion360_default",
                "Evaluacion360/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
