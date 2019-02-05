using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using ViewModels;
using CMS.Models;
using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CMS.Areas.Admin.Controllers
{
    //[Authorize(Roles = "dev")]
    [Authorize]
    public class sysAdminController : Controller
    {
        public sysAdminController()
        {
        }

        public sysAdminController(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
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
            set
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



        public ActionResult index()
        {
            return View();
        }


        public ActionResult RolesList()
        {
            return View(RoleManager.Roles);
        }

        //
        // GET: /Roles/Details/5
        public async Task<ActionResult> RoleDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        //
        // GET: /Roles/Create
        public ActionResult RoleCreate()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> RoleCreate(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(roleViewModel.Name);
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                return RedirectToAction("roleslist");
            }
            return View();
        }

        //
        // GET: /Roles/Edit/Admin
        public async Task<ActionResult> RoleEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };
            return View(roleModel);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RoleEdit([Bind(Include = "Name,Id")] RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("roleslist");
            }
            return View();
        }

        //
        // GET: /Roles/Delete/5
        public async Task<ActionResult> RoleDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost, ActionName("RoleDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                IdentityResult result;
                if (deleteUser != null)
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("roleslist");
            }
            return View();
        }

        public async Task<ActionResult> EditUserRoles(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                UserName=user.UserName,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserRoles([Bind(Include = "Id",Exclude = "UserName")] EditUserViewModel editUser, 
            params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                 
                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("roleslist");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }



        public ActionResult settingvalue()
        {
            DataLayer.CMSDataEntities db = new DataLayer.CMSDataEntities();
            var list = db.SettingValues.OrderByDescending(a=>a.ID).ToList();
            return View(list);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult settingValue(string id, string titlefa, string title, string value, string coded,string loadatfirst)
        {
            DataLayer.CMSDataEntities db = new DataLayer.CMSDataEntities();

            int idval = 0;
            int.TryParse(id, out idval);
            string msg = ""; 
            title = title.ToLower();
            value = value.Replace("\r\n", "");

            if (db.SettingValues.Any(a => a.Name == title && a.ID!=idval))
            {
                msg = "با این Name قبلا وجود داشته.";
            }
            else {

                if (string.IsNullOrEmpty(title))
                {

                    msg = "Name اجباری است";

                }
                else {
                    try
                    {
                        if (idval == 0)//insert
                        {

                            DataLayer.SettingValue tmp = new DataLayer.SettingValue();
                            
                            tmp.Name = (coded == "EnCode" ? Encrypt.EncryptString(title) : title);
                            tmp.NameFa = titlefa;
                            tmp.LoadAtFirst = (loadatfirst == "Yes" ? true : false);
                            tmp.Value = (coded == "EnCode" ? Encrypt.EncryptString(value) : value);
                            db.SettingValues.Add(tmp);
                            db.SaveChanges();
                            msg = "انجام شد.";

                        }
                        else //update
                        {
                            DataLayer.SettingValue tmp = db.SettingValues.FirstOrDefault(a => a.ID == idval);
                            if (tmp != null)
                            {
                                tmp.NameFa = titlefa;
                                tmp.Name = title;
                                tmp.Value = value;
                                tmp.LoadAtFirst = (loadatfirst == "Yes" ? true : false);
                                db.SaveChanges();
                                msg = "انجام شد.";

                            }
                            else
                            {
                                msg = "با این ID وجود ندارد";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = "انجام نشد:<br>" + ex.Message;
                    }
                }
            }
            ViewBag.Message = msg;
            
            SystemConfig.ClearInstance();

            var list = db.SettingValues.OrderByDescending(a => a.ID).ToList();
            return View(list);
        }


        public JsonResult getSettingValue(int id)
        {

            DataLayer.CMSDataEntities db = new DataLayer.CMSDataEntities();
            DataLayer.SettingValue tmp = db.SettingValues.FirstOrDefault(a => a.ID == id);
            if (tmp != null)
                return Json(tmp.Value, JsonRequestBehavior.AllowGet);
            else
                return Json("NOT FOUND !!!!!!", JsonRequestBehavior.AllowGet);
            
        }



        public async Task<ActionResult> Crypt()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crypt(string text,string pre,string type)
        {
            if (pre != "rezaya2938")
            {
                text = "code";
            }
            if(type=="code")
                ViewBag.Encrypted = Encrypt.EncryptString(text);
            else
                ViewBag.Encrypted = Encrypt.DecryptString(text);

            return View("Crypt");
        }

        [HttpPost]
        public async Task<ActionResult> getuserforedit(string UserName)
        {

            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("edituserroles", new { id = user.Id });

        }



        public ActionResult Data()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Data(string query, bool list)
        {
            DataTable tbl = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CMSConnectionString"].ConnectionString);
            conn.Open();
            if (!list)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query.Replace("\r\n", " "), conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    int cnt = cmd.ExecuteNonQuery();
                    if(cnt>0)
                    ViewBag.Msg = "<div class='alert alert-success'>انجام شد. " + cnt + " رکورد آپدیت شد.</div>";
                    else
                    ViewBag.Msg = "<div class='alert alert-warning'>هیچ رکوردی تغییر داده نشد.</div>";
                   
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    ViewBag.Msg = "<div class='alert alert-danger'>خطا .<br> " + ex.Message + "</div>";
                }
            }
            else
            {
                try
                {
                    SqlCommand com = new SqlCommand(query.Replace("\r\n", " "), conn);
                    System.Data.DataSet ds = new System.Data.DataSet();

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    da.Fill(ds);
                    int cnt = 0;

                    if (ds.Tables.Count > 0)
                    {
                        tbl = ds.Tables[0];
                        cnt = tbl.Rows.Count;
                        ViewBag.Msg = "<div class='alert alert-success'>انجام شد. " + cnt + " رکورد پیدا شد.</div>";

                    }else
                        ViewBag.Msg = "<div class='alert alert-alert'>هیچی پیدا نشد.</div>";


                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    ViewBag.Msg = "<div class='alert alert-danger'>خطا .<br> " + ex.Message + "</div>";
                }
            }

            conn.Close();
            ViewBag.Query = query;
            ViewBag.List = list;
            return View(tbl);
        }

    }
}