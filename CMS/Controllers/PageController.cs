using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace CMS.Controllers
{
    public class PageController : Controller
    {
        // GET: Page
         
        public ActionResult Index(string url)
        {
            if (url == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }


            using (ServiceLayer.PageContentService service = new ServiceLayer.PageContentService())
            {

                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);

                PageUiDetails tmp = service.FindByUrl(url, langCode);
                 
                if (tmp == null)
                {
                    return HttpNotFound();
                }
                return View(tmp);

            }

        }


        [DomainClasses.AjaxOnly]
        public void AddViewCount(int id)
        {
            using (ServiceLayer.PageContentService service = new ServiceLayer.PageContentService())
            {
                service.AddViewCount(id);
            }
        }





    }
}