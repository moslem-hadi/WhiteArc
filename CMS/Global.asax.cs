using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Optimization;
using System.Web.Routing;
using DomainClasses;
using System.Threading;
using System.Globalization;

namespace CMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Thread.CurrentThread.CurrentCulture =
             CultureInfo.CreateSpecificCulture("fa-IR");
            Thread.CurrentThread.CurrentUICulture = new
                CultureInfo("fa-IR");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }



        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (context != null && context.Request.Cookies != null)
            {
                HttpCookie cookie = context.Request.Cookies["lang"];
                if (cookie != null && cookie.Value != null)
                    Westwind.Utilities.WebUtils.SetUserLocale(cookie.Value, cookie.Value);

                else
                    Westwind.Utilities.WebUtils.SetUserLocale("fa", "fa");

            }
            else
                Westwind.Utilities.WebUtils.SetUserLocale("fa", "fa");
        }


        void Session_Start(object sender, EventArgs e)
        {
            //آمار بازدید
            if (!Request.Browser.Crawler)
            {
                Utilities.Functions.AddViewCount();
            }
        }





    }
}
