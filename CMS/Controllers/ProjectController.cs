using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Westwind.Globalization;

namespace CMS.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Product
        public ActionResult Index(string url)
        {
            using (ServiceLayer.ProductGroupService service = new ServiceLayer.ProductGroupService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
                ProductGroupDetailUiViewModel tmp=new ProductGroupDetailUiViewModel();
                if (string.IsNullOrEmpty(url))
                {
                    tmp.SeoTitle = tmp.Title=tmp.FullTitle = DbRes.T("Projects.Title","txt");
                    tmp.ID = 0;

                    Dictionary<string, string> cats = service.GetCategories(langCode);
                    ViewBag.Cats = cats;


                }
                else {
                    tmp = service.FindByUrl(url, langCode);

                if (tmp == null)
                    return HttpNotFound();

            }
                return View(tmp);
            }
        }

        public ActionResult All() {

            return View();

        }
        public ActionResult view(string url)
        {
            using (ServiceLayer.ProductService srv = new ServiceLayer.ProductService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);

                var pr = srv.getProductByLang(url,langCode);
                if (pr == null )
                    return HttpNotFound();

                return View(pr);

            }

        }


        /// <summary>
        /// گرفتن لیست محصولات یک گروه بر اساس زبان
        /// </summary> 
        public ActionResult GroupProducts(int groupID, int? page)
        {
            var pageIndex = (page ?? 1) - 1;
            const int pageSize = 12;
            int totalPostCount;

            using (ServiceLayer.ProductService service = new ServiceLayer.ProductService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
                var list = service.getGroupProducts(langCode, groupID, pageIndex, pageSize, out totalPostCount);

                var result = new StaticPagedList<ViewModels.ProductListUiViewModel>(list, pageIndex + 1, pageSize, totalPostCount);
                ViewBag.Products = result;

                //پارشال ویوی این قسمت نوشتهشود.
                return PartialView("_GroupProducts", list);
            }

        }







        /// <summary>
        /// گرفتن لیست محصولات برای صفحه اول
        /// </summary>
        /// <returns></returns>
        public ActionResult HomepageProducts()
        {
            using (ServiceLayer.ProductService service = new ServiceLayer.ProductService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
                var list = service.getLatestProductsByLangID(langCode,6,0);

                return PartialView("_HomepageProducts", list);
            }

        }



    }
}