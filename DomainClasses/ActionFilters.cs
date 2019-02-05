using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;


namespace DomainClasses
{
    /// <summary>
    /// برای اکشن هایی که فقط با ایجکش فراخونی میشن.
    /// </summary>
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
                filterContext.HttpContext.Response.Redirect("~/404.html");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}
