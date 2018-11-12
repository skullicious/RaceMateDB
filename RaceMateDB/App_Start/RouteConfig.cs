using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RaceMateDB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            // /Course/Hillingdon

           //  routes.MapRoute(
           //        name: "CourseSearch",
           //         url: "course/{name}",
           //        defaults: new { controller = "Course", action = "Search", name = UrlParameter.Optional }
           //     );



            // /Course/

            routes.MapRoute(
               name: "CourseIndex",
               url: "course/{action}/{id}",
               defaults: new { controller = "Course", action = "Index", name = UrlParameter.Optional }

               );

                        




            // /
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
