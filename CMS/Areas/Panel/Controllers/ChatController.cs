using DomainClasses;
using Microsoft.AspNet.Identity;
using MvcNotification.Infrastructure.Notification;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace CMS.Areas.Panel.Controllers
{
    [Authorize]
    [RouteArea(areaName:"Panel")]
    public class ChatController : Controller
    {
        // GET: Panel/Chat

        public ActionResult Index(int? page)
        {
            var pageIndex = (page ?? 1) - 1;
            const int pageSize = 10;

            using (ServiceLayer.OfferService service = new ServiceLayer.OfferService())
            {

                int totalPostCount;
                var model = service.getUserChats(User.Identity.GetUserId(), pageIndex, pageSize, out totalPostCount);
                var result = new StaticPagedList<ViewModels.ChatListViewModel>(model, pageIndex + 1, pageSize, totalPostCount);
                ViewBag.Projects = result;
                model.ForEach(a =>
                {
                    a.LastPostDateEn = Utilities.Functions.String2date(a.LastPostDateEn, 1, "p");
                });
                return View(model);
            }

        }

 

        public ActionResult View(int id)
        {
            return View();
            //try
            //{
                Webtina.Support ws = new Webtina.Support();
                int userID = 0;

                SystemConfig sys = SystemConfig.Instance;
                string secKey = Utilities.Functions.GetSettingVal("seckey", true, true);
            
                int.TryParse(Utilities.Functions.GetSettingVal("userid", true, true), out userID);


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
            //}
            //catch(Exception ex)
            //{

                
            //    this.ShowMessage(MessageType.Error,ex.Message, true);

            //    //return RedirectToAction("index", "index");

            //}

        }


        //در صفحه اول پروفایل
        [AjaxOnly]
        public PartialViewResult LatestOffers()
        {
            using (ServiceLayer.OfferService service = new ServiceLayer.OfferService())
            { var lst = service.getOffers(User.Identity.GetUserId());
                lst.ForEach(a =>
                {
                    a.RegDateEn = Utilities.Functions.PersianDateTime(a.RegDateEn);
            });
                return PartialView("Partial/_LastFreelancerOffers", lst);
            }
        }
        //در صفحه اول پروفایل
        [AjaxOnly]
        public PartialViewResult LatestChats()
        {
            using (ServiceLayer.OfferService service = new ServiceLayer.OfferService())
            {
                var lst = service.getLastChats(User.Identity.GetUserId());
                lst.ForEach(a =>
                {
                    a.LastPostDateEn = Utilities.Functions.String2date(a.LastPostDateEn, 1, "p");
                });
                return PartialView("Partial/_LastChats", lst);
            }
        }
    }
}