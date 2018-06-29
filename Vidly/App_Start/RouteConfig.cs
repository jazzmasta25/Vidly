using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes(); //enabled attribute routes


            /* Olden Days way
            routes.MapRoute(
                "MoviesByReleaseDate", //unique name
                "movies/released/{year}/{month}", // url structure
                new { controller = "Movies", action = "ByReleaseDate" }, //set the controller and action that will handle
                new { year = @"2015|2016", month = @"\d{2}"} // set constraints on variables optionally
                );
            */



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
