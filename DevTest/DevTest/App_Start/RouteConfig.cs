using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevTest
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "XMLDownload",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "ExpotarXML", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "JsonDownload",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "ExportarJson", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "csvDownload",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Home", action = "ExportarCSV", id = UrlParameter.Optional }
         );

            routes.MapRoute(
              name: "Provincia",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Provincia", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "PXMLDownload",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Provincia", action = "ExpotarXML", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "PJsonDownload",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Provincia", action = "ExportarJson", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "PcsvDownload",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Provincia", action = "ExportarCSV", id = UrlParameter.Optional }
          ); 
        }
    }
}
