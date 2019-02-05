using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Kendo.DynamicLinq;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult View(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    using (ServiceLayer.ContactPMService service = new ServiceLayer.ContactPMService())
        //    {

        //        ContactPM contactPM = service.Find(id);
        //        if (contactPM == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(contactPM);
        //    }
        //}

         [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ServiceLayer.ContactPMService service = new ServiceLayer.ContactPMService())
            {

                if (service.DeleteContactPM(id))
                    return Json("success", JsonRequestBehavior.AllowGet);

                return Json("error", JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public ActionResult MarkAsRead(int id)
        {
            using (ServiceLayer.ContactPMService service = new ServiceLayer.ContactPMService())
            {

                if (service.MarkAsRead(id))
                    return Json("success", JsonRequestBehavior.AllowGet);

                return Json("error", JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult ListContactPMs(int take, int skip, IEnumerable<Sort> sort, 
            Kendo.DynamicLinq.Filter filter)
        {
            using (ServiceLayer.ContactPMService service = new ServiceLayer.ContactPMService())
            {

                var fetched_data = service.getContactPMList(take, skip, sort, filter);
                return Json(fetched_data, JsonRequestBehavior.AllowGet);

            }

        }
         
    }
}
