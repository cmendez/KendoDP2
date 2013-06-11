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

<<<<<<< HEAD
        public static string ConnectionString = "Server=e4905d2e-b957-4e64-9890-a1da008d2557.sqlserver.sequelizer.com;Database=dbe4905d2eb9574e649890a1da008d2557;User ID=cvswckqhymbvebga;Password=c5BbWuK8zr7cZu66dqBkEhoBX3JVbUa3yxfwJXYSpTBWzLzKFfV3HdD3Qam4TX6M;";
=======
        public static string ConnectionString = "Server=3b9e79ed-fe00-4091-bc45-a1da00866293.sqlserver.sequelizer.com;Database=db3b9e79edfe004091bc45a1da00866293;User ID=dzzgiyezstppppjj;Password=mM8a3qZodQzfkEJmDwd7EoPRDeUijtX8VF6TfCyYePmLFeTkjyfoGxZ67pDWDQdR;";
>>>>>>> d86d7b43bb9341940518d0fe7d4fa502150dbfbe
        
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