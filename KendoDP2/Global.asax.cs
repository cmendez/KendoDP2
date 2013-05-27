using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KendoDP2
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    

    public class MvcApplication : System.Web.HttpApplication
    {
        public static bool IsDebug = System.Configuration.ConfigurationManager.AppSettings["Environment"].ToString().Equals("Debug");

        public static string ConnectionString = "Server=b1dae61f-f8eb-44a4-933b-a1ca0185e017.sqlserver.sequelizer.com;Database=dbb1dae61ff8eb44a4933ba1ca0185e017;User ID=ajiyyrlhmmewznrd;Password=t8h5V2ZuLHhgTyj7CD3LJ5eKF6bMRD6AUyAsfXsiD8vyGuFeumxAMdEw3DxXZmKh;";
        
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-MX");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
            AreaRegistration.RegisterAllAreas();
            // Se registra el inicializador de base de datos.
            if (IsDebug) {
                Database.SetInitializer<DP2Context>(new DP2ContextInitializerDEBUG());
            } else {
                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DP2Context, Configuration>());
                Database.SetInitializer<DP2Context>(new DP2ContextInitializerRELEASE());
            }
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}