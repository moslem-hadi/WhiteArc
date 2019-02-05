using DomainClasses;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace CMS.Areas.Admin.Controllers
{
   [Authorize]
    public class SupportController : Controller
    {
        // GET: Admin/Support
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet] 
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ViewModels.WebtinaSupportViewModel model)
        {
            try
            {
                Webtina.Support ws = new Webtina.Support();
                int userID = 0;

                SystemConfig sys = SystemConfig.Instance;
                string secKey = Utilities.Functions.GetSettingVal("seckey","", true);

                int.TryParse(Utilities.Functions.GetSettingVal("userid", "", true), out userID);

                if (userID == 0 || string.IsNullOrEmpty(secKey))
                    ModelState.AddModelError("", "خطا در ارتباط با سرور پشتیبانی. بعدا تلاش کنید.");

                bool stat = ws.AddTicket(userID, model.Priority, model.Part, model.Text, model.Title, secKey);

                if (!stat)
                {
                    ModelState.AddModelError("", "خطا در ارتباط با سرور پشتیبانی. بعدا تلاش کنید.");
                }
                else
                    return RedirectToAction("index");

                return View();
            }
            catch
            {

                TempData["Notify"] = "'error', 'top left', '', 'در حال حاضر امکان ارتباط با سرور وبتینا میسر نمی باشد. لطفا بعدا امتحان کنید'";
                return RedirectToAction("index", "index");
            }
        }

        public ActionResult ListTickets(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            try
            {
                Webtina.Support ws = new Webtina.Support();
                int userID = 0;

                SystemConfig sys = SystemConfig.Instance;
                string secKey = Utilities.Functions.GetSettingVal("seckey", "", true);

                int.TryParse(Utilities.Functions.GetSettingVal("userid", "", true), out userID);


                var result_kala = ws.GetTicketList(userID, take, skip, secKey).AsQueryable().ToDataSourceResult(take, skip, sort, filter);
                return Json(result_kala, JsonRequestBehavior.AllowGet);
            }
            catch
            {

                TempData["Notify"] = "'error', 'top left', '', 'در حال حاضر امکان ارتباط با سرور وبتینا میسر نمی باشد. لطفا بعدا امتحان کنید'";
                return RedirectToAction("index", "index");
            }

        }

        public ActionResult ticket(int id)
        {
            try
            {
                Webtina.Support ws = new Webtina.Support();
                int userID = 0;

                SystemConfig sys = SystemConfig.Instance;
                string secKey = Utilities.Functions.GetSettingVal("seckey", "", true);

                int.TryParse(Utilities.Functions.GetSettingVal("userid", "", true), out userID);



                var getitem = ws.GetTicketReplies(userID, id, secKey);

                TicketReplyViewModel result = new TicketReplyViewModel();
                result.ReplyList = getitem.ReplyList.Select(a => new Replies
                {
                    FileName = a.FileName,
                    ID = a.ID,
                    IsManageReply = a.IsManageReply,
                    SendDate = a.SendDate,
                    Text = a.Text
                }).ToList();
                result.TicketLastUpdate = getitem.TicketLastUpdate;
                result.TicketStatus = getitem.TicketStatus;
                result.TicketTitle = getitem.TicketTitle;
                result.TicketPart = getitem.TicketPart;

                ViewBag.UserPic = "/areas/admin/content/images/user.png";
                return View(result);
            }
            catch
            {

                TempData["Notify"] = "'error', 'top left', '', 'در حال حاضر امکان ارتباط با سرور وبتینا میسر نمی باشد. لطفا بعدا امتحان کنید'";
                return RedirectToAction("index","index");
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ticket(TicketReplyViewModel model, string responseText, int ticketID)
        {

            Webtina.Support ws = new Webtina.Support();
            int userID = 0;

            SystemConfig sys = SystemConfig.Instance;
            string secKey = Utilities.Functions.GetSettingVal("seckey","", true);

            int.TryParse(Utilities.Functions.GetSettingVal("userid", "", true), out userID);


            if (userID == 0 || string.IsNullOrEmpty(secKey))
                ModelState.AddModelError("", "خطا در ارتباط با سرور پشتیبانی. بعدا تلاش کنید.");
            try
            {
                bool stat = ws.RespondeTicket(userID, ticketID, responseText, secKey);

                if (!stat)
                {
                    ModelState.AddModelError("", "خطا در ارتباط با سرور پشتیبانی. بعدا تلاش کنید.");
                }
                else
                    return RedirectToAction("ticket", "support", new { id = ticketID });
            }
            catch
            {
                ModelState.AddModelError("", "خطا در ارتباط با سرور پشتیبانی. بعدا تلاش کنید.");

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                Webtina.Support ws = new Webtina.Support();
                int userID = 0;

                SystemConfig sys = SystemConfig.Instance;
                string secKey = Utilities.Functions.GetSettingVal("seckey", "", true);

                int.TryParse(Utilities.Functions.GetSettingVal("userid", "", true), out userID);



                if (ws.DeleteTicket(userID, id, secKey))
                    return Json("success", JsonRequestBehavior.AllowGet);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            catch
            {

                TempData["Notify"] = "'error', 'top left', '', 'در حال حاضر امکان ارتباط با سرور وبتینا میسر نمی باشد. لطفا بعدا امتحان کنید'";
                return RedirectToAction("index", "index");
            }
        }

         
        public ActionResult MarkAsRead(int id)
        {

            Webtina.Support ws = new Webtina.Support();
            int userID = 0;

            SystemConfig sys = SystemConfig.Instance;
            string secKey = Utilities.Functions.GetSettingVal("seckey", "", true);

            int.TryParse(Utilities.Functions.GetSettingVal("userid", "", true), out userID);


            ws.SetTicketAsRead(userID, id, secKey);
            return Json("success", JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetUnreadTickets()
        {

            Webtina.Support ws = new Webtina.Support();
            int userID = 0;

            SystemConfig sys = SystemConfig.Instance;
            string secKey = Utilities.Functions.GetSettingVal("seckey", "", true);

            int.TryParse(Utilities.Functions.GetSettingVal("userid", "", true), out userID);


            int ticketCount =ws.UnreadTicketsCount(userID, secKey);
            return Json(new { Success = true, Result = ticketCount.ToString() }, JsonRequestBehavior.AllowGet);


        }


    }
}