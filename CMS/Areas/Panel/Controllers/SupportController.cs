using DomainClasses;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using CMS.Models;
using System.Threading.Tasks;

namespace CMS.Areas.Panel.Controllers
{
    [Authorize]
    [RouteArea(AreaPrefix ="Panel")]
    public class SupportController : Controller
    {
        
        public ActionResult Index(int? page)
        {
            var pageIndex = (page ?? 1) - 1;
            const int pageSize = 10;

            using (ServiceLayer.CtrtService srv = new ServiceLayer.CtrtService())
            {

                int customerID = int.Parse(User.Identity.GetUserName());
                int totalPostCount;
                var model = srv.GetTicketList(customerID, pageIndex, pageSize, out totalPostCount);
                var result = new StaticPagedList<ViewModels.SupportListViewModel>(model, pageIndex + 1, pageSize, totalPostCount);
                ViewBag.Pager = result;
                return View(model);
            }

        }


        [HttpGet] 
        public ActionResult Add()
        {
            FillViewBag();

            return View();
        }

        private void FillViewBag()
        {
            using (ServiceLayer.CtrtService srv = new ServiceLayer.CtrtService())
            {
                int customerID = int.Parse(User.Identity.GetUserName());

                var productNames = srv.GetProductNames(customerID);
                ViewBag.Systems = new SelectList(productNames, "ID" , "Text");

                ViewBag.HasContract = srv.GetContractStatus(customerID);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ViewModels.SendTicketViewModel model)
        {
            try
            {
                using (ServiceLayer.CtrtService srv = new ServiceLayer.CtrtService())
                {
                    int customerID = int.Parse(User.Identity.GetUserName());
                    var entity = new DataLayer.CtrtSuportH();
                    entity.ADate = Utilities.Functions.PersianDate();
                    entity.CTime = DateTime.Now.ToString("HH:MM");
                    entity.MainID = customerID;
                    entity.CCustName = model.Name;
                    entity.IOpCode = int.Parse(Utilities.Functions.GetSettingVal("iopcode", false, true));
                    entity.CDesc = model.Text;
                    entity.CStatus = "01";//جاری
                    entity.CPrimeRegisterer = Utilities.Functions.GetSettingVal("cprimeregisterer", false, true);//خودم فعلا
                    entity.CReasonCall = "00";
                    entity.BPollStatus = true;//برای اینکه نمایش داده شود.
                    entity.CReasonCall = "0";
                    var savedID = srv.AddSupportHead(entity,model.Tell,model.System);

                    if(savedID==-1)
                    {

                        TempData["Notify"] = "'error', 'top left', '', 'در حال حاضر امکان ارتباط با سرور نیست. بعدا تلاش کنید'";
                        FillViewBag();
                        return View();


                    }
                    else
                    {
                        TempData["Notify"] = $"'success', 'top left', '', 'تیکت شما ارسال شد. کد رهگیری: {savedID}'";
                        return RedirectToAction("index", "support");

                    }

                }


                
            }
            catch
            {

                TempData["Notify"] = "'error', 'top left', '', 'در حال حاضر امکان ارتباط با سرور وبتینا میسر نمی باشد. لطفا بعدا امتحان کنید'";
                FillViewBag();
                return View();
            }
        }



        public ActionResult View(int id)
        { 
            using (ServiceLayer.CtrtService srv = new ServiceLayer.CtrtService())
            {
                int customerID = int.Parse(User.Identity.GetUserName());
                var model = srv.getTicketInfos(id);
                if(model==null || model.Details.MainID!=customerID)
                    return HttpNotFound();
                model.Details.Pic = User.Identity.GetAvatar();
               // ViewBag.UserID = User.Identity.GetUserName();
                srv.ReadTicketByUser(id);
                return View(model);

            }



        }

        [AjaxOnly]
        public JsonResult  SendReply(string text,int isid)
        {
            int replyID = -1;

            using (ServiceLayer.CtrtService srv = new ServiceLayer.CtrtService())
            {
                var customerID = User.Identity.GetUserName();
                var date = Utilities.Functions.PersianDateTime();
                text = Utilities.Functions.GetPlainTextFromHtml(text).Trim(Environment.NewLine.ToCharArray()).Replace("\n","<br>");
                replyID = srv.AddTicketReply(isid, text, customerID, date);
            }

            if (replyID > 0)
                return Json(new { stat = true, msg = replyID.ToString() });
            else
                return Json(new { stat = false, msg = "خطایی رخ داد." });
        }

        [AjaxOnly]
        public JsonResult SendRating(string text,string rate, int isid)
        {
            bool done = false;

            using (ServiceLayer.CtrtService srv = new ServiceLayer.CtrtService())
            {
                var customerID =  User.Identity.GetUserName() ;
                var date = Utilities.Functions.PersianDate();
                done=srv.RateTicket(isid, text, date,rate);

            }

            if (done)
                return Json(new { stat = true, msg = "با موفقیت انجام شد." });
            else
                return Json(new { stat = false, msg = "خطایی رخ داد." });
        }



        

        public JsonResult GetUnreadTickets()
        {
 
            int ticketCount=0;
            return Json(new { Success = true, Result = ticketCount.ToString() }, JsonRequestBehavior.AllowGet);


        }


    }
}