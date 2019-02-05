using System.Globalization;
using CMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using System.Collections.Generic;
using Kendo.DynamicLinq;
using System.Net;
using ViewModels;
using MvcNotification.Infrastructure.Notification;

namespace CMS.Areas.Admin.Controllers
{

    [Authorize]
    public class UserController : Controller
    {
        // GET: Admin/User

        #region Constructors

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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
        public async Task<ActionResult> Index(string id)
        {
            return View();
        }
        public ActionResult getList(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            using (ServiceLayer.UserService srv = new ServiceLayer.UserService())
            {

                var fetched_data = srv.getUserList(take, skip, sort, filter);
                return Json(fetched_data, JsonRequestBehavior.AllowGet);

            }

        }

        public async Task<ActionResult> Add(string id)
        {

            
                ViewModels.AddUserViewModel model = new ViewModels.AddUserViewModel();
            IList<string> userRoles = new List<string>();


            if (string.IsNullOrEmpty(id)) //اگه در مود اینسرت باشد
            {
            }
            else
            {//اگر در مود آپدیت باشد
                using (ServiceLayer.UserService srv = new ServiceLayer.UserService())
                {
                    var user=srv.Find(id);
                    if(user==null)
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    model = user.ToModel();

                    userRoles  = await UserManager.GetRolesAsync(user.Id);
                }
            }



                ViewBag.RoleId =  await RoleManager.Roles.Where(a=>a.Name!="dev")
                    .Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                    }).ToListAsync() ;
             
                return View(model);
            

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Add(ViewModels.AddUserViewModel model, string postType, params string[] selectedRoles)
        {
            using (ServiceLayer.UserService srv = new ServiceLayer.UserService())
            {
                if (ModelState.IsValid)
                {

                    bool canSave = true;
                    string entityID = "";
                    if (postType == "delete")
                    {
                        //حذف شود.
                        DeleteConfirmed(model.ID);
                        TempData["Notify"] = "'success', 'top left', '', 'با موفقیت حذف شد'";
                        return RedirectToAction("index");
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(model.ID))
                        {
                            var user = new ApplicationUser { UserName = model.UserName, FullName = model.FullName, RegDateEn = DateTime.Now };
                            var adminresult = await UserManager.CreateAsync(user, model.Password);

                            //Add User to the selected Roles 
                            if (adminresult.Succeeded)
                            {
                                canSave = true;
                                entityID = user.Id;
                                //if (selectedRoles != null)
                                //{
                                    var result = await UserManager.AddToRolesAsync(user.Id, "Admin");
                                    if (!result.Succeeded)
                                    {
                                        AddErrors(adminresult);

                                        //canSave = false;
                                    }
                                //}
                            }
                            else
                            {
                                canSave = false;
                                AddErrors(adminresult);

                            }
                        }
                        else//ویرایش
                        {

                            var entity = srv.Find(model.ID);

                            entity.FullName = model.FullName;
                            entity.UserName = model.UserName;
                            

                           // var entity = model.ToEntity();
                            entityID = entity.Id;

                            string err = "";
                            if (!srv.Edit(entity,out err))
                            {
                                ModelState.AddModelError("", "خطا در ذخیره اطلاعات.<br>"+err);
                                canSave = false;
                            }

                            if (canSave)
                            {
                                if (!string.IsNullOrEmpty(model.Password))
                                {
                                    if (!await changePassword(model))
                                    {
                                        this.ShowMessage(MessageType.Info, $"پسورد تغییر نکرد.", false);
                                    }
                                }



                                var userRoles = await UserManager.GetRolesAsync(entity.Id);

                                selectedRoles = selectedRoles ?? new string[] { };

                                var result = await UserManager.AddToRolesAsync(entity.Id, selectedRoles.Except(userRoles).ToArray<string>());

                                if (!result.Succeeded)
                                {
                                    ModelState.AddModelError("", result.Errors.First());

                                    this.ShowMessage(MessageType.Info, result.Errors.First(), false);
                                }
                                result = await UserManager.RemoveFromRolesAsync(entity.Id, userRoles.Except(selectedRoles).ToArray<string>());

                                if (!result.Succeeded)
                                {
                                    ModelState.AddModelError("", result.Errors.First());

                                    this.ShowMessage(MessageType.Info, result.Errors.First(), false);
                                }
                            }

                        }



                    }
                    if (canSave)
                    {
                        //بسته به نوع دکمه کلیک شده، ریدایرکت میکنه
                        TempData["Notify"] = "'success', 'top left', '', 'با موفقیت ذخیره شد'";
                        if (postType == "save")
                            return RedirectToAction("index");
                        else if (postType == "save-continue")
                            return RedirectToAction("edit", "user", new { @id = entityID });
                    }
                }

                 
                ViewBag.RoleId = new SelectList(RoleManager.Roles.Where(a => a.Name != "dev"), "Name", "Name");
                return View(model);

 
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                if (error.Contains("is already taken"))
                    ModelState.AddModelError("", "این نام کاربری قبلا ثبت شده است.");
                else
                    ModelState.AddModelError("", error);
            }
        }

        public async Task<bool> changePassword(AddUserViewModel usermodel)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(usermodel.ID);
            if (user == null)
            {
                return false;
            }
            user.PasswordHash = UserManager.PasswordHasher.HashPassword(usermodel.Password);
            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }


        [HttpPost]
        public async Task<bool> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == null)
                    {
                        return false;
                    }

                    var user = await UserManager.FindByIdAsync(id);
                    var logins = user.Logins;
                    var rolesForUser = await UserManager.GetRolesAsync(id);
                    DataLayer.CMSDataEntities db = new DataLayer.CMSDataEntities();
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        foreach (var login in logins.ToList())
                        {
                            await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                        }

                        if (rolesForUser.Count() > 0)
                        {
                            foreach (var item in rolesForUser.ToList())
                            {
                                // item should be the name of the role
                                var result = await _userManager.RemoveFromRoleAsync(user.Id, item);
                            }
                        }

                        await UserManager.DeleteAsync(user);
                        transaction.Commit();
                    }

                    return true;
                }
                catch(Exception ex) {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //public ActionResult ChangePassword()
        //{
        //    return View(model:"");
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ChangePassword(string oldpass, string newpass , string newpassrepeat)
        //{
        //    if (newpass != newpassrepeat)
        //    {
        //        return View(model: "<div class='alert m-b-10 alert-warning'>تکرار پسورد جدید مطابقت ندارد</div>");
        //    }
        //    using (ServiceLayer.UserService service = new ServiceLayer.UserService())
        //    {
        //        string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(newpass, "md5");
        //        int userID = ((UserClass)(Session["User"])).KishUserID;

        //        if (service.CheckPass(userID, FormsAuthentication.HashPasswordForStoringInConfigFile(oldpass, "md5")))
        //        {
        //           if( service.ChangePass(userID, pass))
        //                return View(model: "<div class='alert m-b-10 alert-success'>پسورد تغییر انجام شد</div>");
        //                else
        //            return View(model: "<div class='alert m-b-10 alert-warning'>پسورد تغییر انجام نشد</div>");


        //        }
        //        else
        //            return View(model: "<div class='alert m-b-10 alert-warning'>پسورد قدیم اشتباه است</div>");


        //    }
        //    return View();
        //}
    }
}