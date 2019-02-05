using Kendo.DynamicLinq;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class StatController : Controller
    {
        // GET: Admin/Stat
        public ActionResult Index(int? page)
        {

            var pageIndex = (page ?? 1) - 1;
            const int pageSize = 30;
            int skip = pageSize * pageIndex;
            using (ServiceLayer.SiteStatService service = new ServiceLayer.SiteStatService())
            {
                int totalcount = 0;
                var model = service.getSiteStatList(pageSize, skip,out totalcount);

                var paging = new StaticPagedList<ViewModels.StatsViewModel>(model, pageIndex + 1, pageSize, totalcount);
                ViewBag.Pager = paging;
                return View(model);

            }
        }

        
    }
}