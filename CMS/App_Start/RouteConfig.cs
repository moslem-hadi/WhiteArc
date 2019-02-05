using CMS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

          //  routes.MapRoute(
          //    "projects",
          //    "project",
          //    new { controller = "project", action = "all", url = UrlParameter.Optional },
          //     new[] { "CMS.Controllers" }
          //);

            routes.MapRoute(
              "project",
              "project/{*url}",
              new { controller = "project", action = "index", url = UrlParameter.Optional },
               new[] { "CMS.Controllers" }
          );
            routes.MapRoute(
              "pageAddViewCount",
              "page/AddViewCount/{id}",
              new { controller = "page", action = "AddViewCount", url = UrlParameter.Optional },
               new[] { "CMS.Controllers" }
          );


            routes.MapRoute(
              "page",
              "page/{*url}",
              new { controller = "page", action = "index", url = UrlParameter.Optional },
               new[] { "CMS.Controllers" }
          );
            routes.MapRoute(
              "view",
              "view/{*url}",
              new { controller = "project", action = "view", url = UrlParameter.Optional },
               new[] { "CMS.Controllers" }
          );



            routes.MapRoute(
               "Default",
               "{controller}/{action}/{id}",
               new { controller = "Index", action = "Index", id = UrlParameter.Optional },
               new[] { "CMS.Controllers" }//برای اینکه با ادمین قاطی نشه
           );

        }
    }
}
