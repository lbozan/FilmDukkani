using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FilmDukkaniMvc
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
                name: "Users",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Users", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Film",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Film", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
              name: "Error",
              url: "Error/{action}/{id}",
              defaults: new { controller = "Error", action = "Index", id = UrlParameter.Optional }
              );
        }
    }
}
