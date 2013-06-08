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

        public static string ConnectionString = "Server=4ad4b1ec-2113-4f88-8978-a1d701475c8a.sqlserver.sequelizer.com;Database=db4ad4b1ec21134f888978a1d701475c8a;User ID=mnbvrnmkzpinygnv;Password=5Gmre4V4jVZVdDmByE62vtkL2XfjgTDrtkKEMYMGThQ2NQuyyZhAtMDJvGypXLWx;";
        
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