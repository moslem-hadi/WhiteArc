using System.Web.Mvc;

namespace CMS.Areas.Admin
{
    [Authorize]
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute("editpage","admin/page/edit/{id}",new { Areas = "Admin", controller = "Page", action = "add" });
            context.MapRoute("editpost","admin/post/edit/{id}",new { Areas = "Admin", controller = "post", action = "add" });
            context.MapRoute("login", "admin/login", new { Areas = "Admin", controller = "index", action = "login" });
            context.MapRoute("editpostgroup", "admin/postgroup/edit/{id}", new { Areas = "Admin", controller = "postgroup", action = "add" });
            context.MapRoute("addarticle", "admin/article/add", new { Areas = "Admin", controller = "post", action = "add" });
            context.MapRoute("editarticle", "admin/article/edit/{id}", new { Areas = "Admin", controller = "post", action = "add" });
            context.MapRoute("listarticle", "admin/article", new { Areas = "Admin", controller = "post", action = "index" });
            context.MapRoute("editcategory", "admin/category/edit/{id}", new { Areas = "Admin", controller = "category", action = "add" });
            context.MapRoute("editproduct", "admin/product/edit/{id}", new { Areas = "Admin", controller = "product", action = "add" });
            context.MapRoute("editDownload", "admin/download/edit/{id}", new { Areas = "Admin", controller = "download", action = "add" });
            context.MapRoute("editUser", "admin/User/edit/{id}", new { Areas = "Admin", controller = "User", action = "add" });
             
            context.MapRoute("addfaq", "admin/faq/add", new { Areas = "Admin", controller = "post", action = "add" });
            context.MapRoute("editfaq", "admin/faq/edit/{id}", new { Areas = "Admin", controller = "post", action = "add" });
            context.MapRoute("listfaq", "admin/faq", new { Areas = "Admin", controller = "post", action = "index" });

            context.MapRoute("addlearn", "admin/learn/add", new { Areas = "Admin", controller = "post", action = "add" });
            context.MapRoute("editlearn", "admin/learn/edit/{id}", new { Areas = "Admin", controller = "post", action = "add" });
            context.MapRoute("listlearn", "admin/learn", new { Areas = "Admin", controller = "post", action = "index" });

            context.MapRoute("addarticlegroup", "admin/articlegroup/add", new { Areas = "Admin", controller = "postgroup", action = "add" });
            context.MapRoute("editarticlegroup", "admin/articlegroup/edit/{id}", new { Areas = "Admin", controller = "postgroup", action = "add" });
            context.MapRoute("listarticlegroup", "admin/articlegroup", new { Areas = "Admin", controller = "postgroup", action = "index" });

            
            context.MapRoute("addlearngroup", "admin/learngroup/add", new { Areas = "Admin", controller = "postgroup", action = "add" });
            context.MapRoute("editlearngroup", "admin/learngroup/edit/{id}", new { Areas = "Admin", controller = "postgroup", action = "add" });
            context.MapRoute("listlearngroup", "admin/learngroup", new { Areas = "Admin", controller = "postgroup", action = "index" });


            context.MapRoute("addfaqgroup", "admin/faqgroup/add", new { Areas = "Admin", controller = "postgroup", action = "add" });
            context.MapRoute("editfaqgroup", "admin/faqgroup/edit/{id}", new { Areas = "Admin", controller = "postgroup", action = "add" });
            context.MapRoute("listfaqgroup", "admin/faqgroup", new { Areas = "Admin", controller = "postgroup", action = "index" });



            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller="index", action = "Index", id = UrlParameter.Optional },
                new[] { "CMS.Areas.Admin.Controllers" }
            );
        }
    }
}