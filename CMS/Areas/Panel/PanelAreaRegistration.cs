using System.Web.Mvc;

namespace CMS.Areas.Panel
{
    public class PanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Panel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {


            //context.MapRoute("LatestOffers", "panel/chat/LatestOffers", new { Areas = "panel", controller = "chat", action = "LatestOffers" });
            //context.MapRoute("LatestChats", "panel/chat/LatestChats", new { Areas = "panel", controller = "chat", action = "LatestChats" });

            //context.MapRoute(
            //    name: "chatView",
            //    url: "Panel/chat/{id}",
            //    defaults: new { Areas = "Panel", controller = "chat", action = "view", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                "Panel_default",
                "Panel/{controller}/{action}/{id}",
                new { controller="index", action = "Index", id = UrlParameter.Optional },
                 new[] { "CMS.Areas.Panel.Controllers" }
            );
        }
    }
}