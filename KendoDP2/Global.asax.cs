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

        public static string ConnectionString = "Server=7049bbbf-4c18-486b-9a2b-a1cd0123ec0c.sqlserver.sequelizer.com;Database=db7049bbbf4c18486b9a2ba1cd0123ec0c;User ID=olhumijobjagjrmo;Password=UrDEedbYMQim7SavcsMoCd2ak6GSUavKrducKfCEAzkVjiHtuNmedqeQyCuhDreM;";
        
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