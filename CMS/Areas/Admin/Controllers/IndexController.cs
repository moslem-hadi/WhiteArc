using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CMS.Models;
using ViewModels;
using DomainClasses;

namespace CMS.Areas.Admin.Controllers
{

   [Authorize]
    public class IndexController : Controller
    {
        // GET: Admin/Index



        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public IndexController()
        {
        }
        public IndexController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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



        public ActionResult Index()
        {
            /////////////
            /////////////////////////
            //////////////////////////
            ////مقدار دهی به مقادیر صفحه اول داشبورد



            using (ServiceLayer.AllService srv = new ServiceLayer.AllService())
            {

                var model = srv.GetDashboardInfo();
                return View(model);
            }
        }

        public FileContentResult newletter()
        {
            using (ServiceLayer.ContactPMService srv = new ServiceLayer.ContactPMService())
            {


                string csv = srv.GetNewsLetterEmails();
                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "emails"+Utilities.Functions.PersianDate().Replace("/","")+".csv");
            }
    }

        //[ChildActionOnly]
        //public PartialViewResult LatestContactPM()
        //{
        //    using (ServiceLayer.ContactPMService service = new ServiceLayer.ContactPMService())
        //    {
        //        var data = service.GetLatestContactPM(6);
        //        if (data.Any())
        //            return PartialView("Partial/_LatestTickets", data);
        //        else
        //            return null;
        //    }

        //}


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                //case SignInStatus.RequiresVerification:
                //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "اطلاعات وارد شده صحیح نیست.");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        


    }
}