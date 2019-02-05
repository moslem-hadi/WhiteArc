using CMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using DomainClasses;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Owin.Security;
using System.ComponentModel.DataAnnotations;
using Utilities;
using Westwind.Globalization;

namespace CMS.Controllers
{
    public class IndexController :Controller
    {
    
        // GET: Index
        public ActionResult Index()
        {
            var model = SetHomePage();
            using (ServiceLayer.ProductService srv = new ServiceLayer.ProductService())
            {
                int langCode = Utilities.Functions.GetLanguageIdByCode(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);

                ViewBag.Products = srv.getLatestProductsByLangID(langCode,6,0);

                    }
            return View(model);
        }
        protected ViewModels.HomePageViewModel SetHomePage()
        {
            return new HomePageViewModel()
            {
                SeoTitle = Utilities.Functions.GetSettingVal("seotitle",Utilities.Functions.GetLangCodeName()),
                SeoDescription = Utilities.Functions.GetSettingVal("seodescription", Utilities.Functions.GetLangCodeName()),
                SiteName= Utilities.Functions.GetSettingVal("sitename", Utilities.Functions.GetLangCodeName())
            };

        }


        public ActionResult ChangeLanguage(string langName, string returnurl)
        {
            if (!string.IsNullOrEmpty(langName))
            {
                Westwind.Utilities.WebUtils.SetUserLocale(langName, langName);

                HttpCookie cookie = new HttpCookie("lang");
                cookie.Value = langName;
                Response.Cookies.Add(cookie);

            }
            return Redirect(returnurl);
            //return RedirectToAction("index");
        }
        



        [Route("ContactUs")]
        public ActionResult ContactUs()
        {
            return View();
        }

        [Route("ContactUs")]
        [HttpPost]
        public ActionResult ContactUs(ViewModels.ContactUsForm obj)
        {
            dynamic showMessageString = string.Empty;
            try
            {

                using (ServiceLayer.ContactPMService srv = new ServiceLayer.ContactPMService())
                {
                    DataLayer.ContactPM model = new DataLayer.ContactPM();
                    model.Email = obj.Email;
                    model.FullName = obj.Name;
                    model.Tell = obj.Phone;
                    model.IP = Utilities.Functions.GetUser_IP();
                    model.IsRead = false;
                    model.RegDateFa = model.ReplyDateFa= Utilities.Functions.PersianDateTime();
                    model.Tell = obj.Phone;
                    model.Text = obj.Message;
                    model.Title = "پیام دریافتی از فرم تماس با ما";
                    srv.Add(model);

                } 
                return PartialView("partial/_contactDone");

            }
            catch  
            {

                showMessageString = new
                {
                    stat = 200,
                    msg = "You have enter correct date !!!"
                };
                return Json(showMessageString, JsonRequestBehavior.AllowGet);
            }
        }





        [HttpPost]
        public JsonResult ContactSubmit(ContactFormViewModel model)
        {
            using (ServiceLayer.ContactPMService srv = new ServiceLayer.ContactPMService())
            {


                var entity = model.ToEntity();
                entity.Text = entity.Text.Replace("\r\n", "<br>").Replace("'", "\"");
                entity.Title = "پیام دریافتی از فرم تماس با ما";
                entity.RegDateFa = Utilities.Functions.PersianDateTime();
                entity.IP = Utilities.Functions.GetUser_IP();
                entity.TraceCode = Utilities.Functions.RandomNumber().ToString();//آیدی هم در تریگر به اولش اضافه میشه.
                if (srv.Add(entity))
                {
                    return Json(new AjaxStat { Stat = true, Msg = (entity.ID + entity.TraceCode).SplitString(2) });
                }
                else
                {
                    return Json(new AjaxStat { Stat = false, Msg = DbRes.T("Home.ContactFailedMessage", "txt") });

                }
            }
        }


        [HttpPost]
        public void Subscribe(string mail)
        {
            dynamic showMessageString = string.Empty;
            try
            {
                if (!Utilities.Functions.IsEmail(mail))
                    return;
                using (ServiceLayer.ContactPMService srv = new ServiceLayer.ContactPMService())
                {
                    DataLayer.NewsLetterEmail model = new DataLayer.NewsLetterEmail();
                    model.Email = mail;
                    model.RegDate = Utilities.Functions.PersianDateTime();
                    srv.SubscribeEmail(model);

                }

            }
            catch
            { }
        }


        [ChildActionOnly]
        [CustomOutputCacheAttribute(VaryByParam = "none")]
        public PartialViewResult HomeArticles()
        {
            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {
                var data = service.GetHomePagePost((int)DomainClasses.PostType.Article, 6);
                if (data.Any())
                    return PartialView("Partial/_HomeArticles", data);
                else
                    return null;
            }

        }


        [ChildActionOnly]
        [CustomOutputCacheAttribute(VaryByParam = "none")]
        public PartialViewResult HomeNews()
        {
            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {
                var data = service.GetHomePagePost((int)DomainClasses.PostType.Post, 3);
                if (data.Any())
                    return PartialView("Partial/_HomeNews", data);
                else
                    return null;

            }

        }


        [ChildActionOnly]
        [CustomOutputCacheAttribute(VaryByParam = "none")]
        public PartialViewResult HomeTutorials()
        {
            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {
                var data = service.GetHomePagePost((int)DomainClasses.PostType.Learn, 8);
                if (data.Any())
                    return PartialView("Partial/_HomeTutorials", data);
                else
                    return null;
            }

        }


        [ChildActionOnly]
        [CustomOutputCacheAttribute(VaryByParam = "none")]
        public PartialViewResult HomeFaqs()
        {
            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {
                var data = service.GetHomeFaqs(6);
                if (data.Any())
                    return PartialView("Partial/_HomeFaqs", data);
                else
                    return null;
            }

        }


        #region متغیرها
        public IndexController() { }

        public IndexController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion



        #region لوگین



        [Route("login")]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }



        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        //
        // POST: /Account/Login
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = Request.QueryString["ReturnUrl"] ?? "/";

            // This doen't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password,
                true, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lock");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }



        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "index");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        [Route("logoff")]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Index");
        }

        #endregion











    }
}