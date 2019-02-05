using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace CMS.Controllers
{
    public class PostController : Controller
    {

        /// <summary>
        /// گرفتن لیست اخبار برای آرشیو
        /// دو زبانه نیست
        /// </summary>
        /// <returns></returns> 
        [System.Web.Mvc.Route("news/{page:int?}")]
        public ActionResult News(int? page)
        {
            var pageIndex = (page ?? 1) - 1;
            const int pageSize = 10;

            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {

                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);

                int totalPostCount;
                var model = service.getPostList(pageIndex, pageSize, langCode,out totalPostCount); 
                var result = new StaticPagedList<ViewModels.PostListViewModel>(model, pageIndex + 1, pageSize, totalPostCount);
                ViewBag.posts = result; 
                return View(model);
            }

        }


        [System.Web.Mvc.Route("blog/{page:int?}")]
        public ActionResult articlelist(int? page)
        {
            var pageIndex = (page ?? 1) - 1;
            const int pageSize = 10;

            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);


                int totalPostCount;
                var model = service.getArticleList(pageIndex, pageSize,langCode, out totalPostCount); 
                var result = new StaticPagedList<ViewModels.PostListViewModel>(model, pageIndex + 1, pageSize, totalPostCount);
                ViewBag.posts = result; 
                return View(model);
            }

        }


        [System.Web.Mvc.Route("blog/cat/{url}/{page:int?}")]
        public ActionResult articleGroup(string url,int? page)
        {
            var pageIndex = (page ?? 1) - 1;
            const int pageSize = 10;

            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
                var group = service.GetGroupName(url);
                ViewBag.Text =group.Text;
                ViewBag.Title = group.SeoTitle;
                ViewBag.GroupTitle = group.Title;
                ViewBag.SeoDescription = group.SeoDescription;

                int totalPostCount;
                var model = service.getGroupArticleList(url,pageIndex, pageSize, langCode, out totalPostCount);
                var result = new StaticPagedList<ViewModels.PostListViewModel>(model, pageIndex + 1, pageSize, totalPostCount);
                ViewBag.posts = result;
                return View("articlelist",model);
            }

        }



        [System.Web.Mvc.Route("news/{id:int?}/{slgurl}")] 
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {
                DataLayer.Post post = service.Find(id);
                ViewModels.PostUiViewModel model = post.ToUiModel();
                if (post == null)
                {
                    return HttpNotFound();
                }
                return View(model);

            }
        }


        [System.Web.Mvc.Route("blog/{id:int?}/{slgurl}")]
        public ActionResult article(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {
                DataLayer.Post post = service.Find(id);
                ViewModels.PostUiViewModel model = post.ToUiModel();
                if (post == null)
                {
                    return HttpNotFound();
                }
                return View(model);

            }
        }


        [System.Web.Mvc.Route("blog/search/")]
        public ActionResult SearchPostsKey(string key)
        {
            return Redirect("/blog/search/" + key);
        }


        [System.Web.Mvc.Route("blog/search/{text}/{page:int?}")]
        public ActionResult SearchPosts(string text, int? page)
        {
            var pageIndex = (page ?? 1) - 1;
            const int pageSize = 10;

            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);


                int totalPostCount;
                var model = service.searchArticles(text, pageIndex, pageSize, langCode, out totalPostCount);
                var result = new StaticPagedList<ViewModels.PostListViewModel>(model, pageIndex + 1, pageSize, totalPostCount);
                ViewBag.posts = result;
                return View("articlelist", model);
            }

        }

         
        [System.Web.Mvc.Route("blog/SearchPosts")]
        public ActionResult SearchPosts( )
        {

            return Redirect("/blog/search/"+ Request.QueryString["text"] );

        }






        [DomainClasses.AjaxOnly]
        public void AddViewCount(int id)
        {
            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {
                service.AddViewCount(id);
            }
        }




        [DomainClasses.AjaxOnly]
        public ActionResult LikePost(int id , bool type)
        {
            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {
                if (service.AddLikeCount(id, type))
                    return Json("success", JsonRequestBehavior.AllowGet);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }


   
        [ChildActionOnly]
        public ActionResult SideBarArticlePartial()
        {
            int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
            Dictionary<string, string> model = new Dictionary<string, string>();

            using (ServiceLayer.PostGroupService srv = new ServiceLayer.PostGroupService())
            {
                 model = srv.getMainGeoups((byte)DomainClasses.PostType.Article, langCode);
            }

            return PartialView("_ArticleSidebar",model);
        }






    }
}