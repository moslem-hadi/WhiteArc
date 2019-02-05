using MvcNotification.Infrastructure.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Areas.Panel.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Panel/Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string message)
        {
            this.ShowMessage(MessageType.Success, message);

            this.ShowMessage(MessageType.Success, "درخواست شما با موفقیت ارسال شد.");
            this.ShowMessage(MessageType.Info, $"مبلغ  تومان از حساب شما کسر گردید.", false);
            return View();
        }
    }
}