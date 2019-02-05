using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Localize
{
    public abstract class LocalizeBase : Controller
    {
        protected override void ExecuteCore()
        {

            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null && cookie.Value != null)
                Westwind.Utilities.WebUtils.SetUserLocale(cookie.Value, cookie.Value);

            else
                Westwind.Utilities.WebUtils.SetUserLocale("fa", "fa");


            base.ExecuteCore();
        }

    }
}