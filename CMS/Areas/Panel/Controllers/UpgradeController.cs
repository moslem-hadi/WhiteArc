using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Areas.Panel.Controllers
{
    public class UpgradeController : Controller
    {
        // GET: Panel/Upgrade
        public ActionResult Index()
        {
            using (ServiceLayer.UpgradeInfoeservice srv= new ServiceLayer.UpgradeInfoeservice())
            {
                var model = srv.GetAll();
                return View(model);
            }
        }
    }
}