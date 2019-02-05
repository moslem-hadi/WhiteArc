using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace CMS.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            //باید لیست کل گروه ها رو بیاره.
            return RedirectToAction("index","index");
        }

        /// <summary>
        /// لیست محصولات و زیرگروه های یک گروه
        /// </summary>
        /// <returns></returns>
        public ActionResult list(string url)
        {
            using (ServiceLayer.ProductGroupService service = new ServiceLayer.ProductGroupService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);

                ProductGroupDetailUiViewModel tmp = service.FindByUrl(url, langCode);
                 
                if (tmp == null)
                    return HttpNotFound();



                return View(tmp);
            }
        }



        /// <summary>
        /// گرفتن لیست گروه ها برای صفحه اول
        /// </summary>
        /// <returns></returns>
        public ActionResult HomepageProductGroups()
        {
            using (ServiceLayer.ProductGroupService service = new ServiceLayer.ProductGroupService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
                var list = service.getProductGroupListByLangID(langCode);

                return PartialView("_HomepageProductGroups", list);
            }
           
        }


        /// <summary>
        /// زیرگروه های یک گروه
        /// </summary>
        /// <returns></returns>
        public ActionResult SubGroups(int groupID)
        {
            using (ServiceLayer.ProductGroupService service = new ServiceLayer.ProductGroupService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
                var list = service.getSubGroupsList(langCode, groupID);

                return PartialView("_HomepageProductGroups", list);
            }
           
        }


        /// <summary>
        /// گرفتن لیست گروه ها برای منو
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuProductGroups()
        {
            using (ServiceLayer.ProductGroupService service = new ServiceLayer.ProductGroupService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
                var list = service.getProductGroupListByLangID(langCode);

                return PartialView("_MenuProductGroups", list);
            }
           
        }
    }
}