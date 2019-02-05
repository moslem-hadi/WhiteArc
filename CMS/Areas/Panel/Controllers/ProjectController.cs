using DomainClasses;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MvcNotification.Infrastructure.Notification;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace CMS.Areas.Panel.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {


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

        public ProjectController() { }
        public ProjectController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
        }

        // GET: Panel/Project
        public ActionResult Index(int? page)
        {
            var pageIndex = (page ?? 1) - 1;
            const int pageSize = 10;

            using (ServiceLayer.ProjectService service = new ServiceLayer.ProjectService())
            {

                int totalPostCount;
                var model = service.getUserProjectList(User.Identity.GetUserId(), pageIndex, pageSize, out totalPostCount);
                var result = new StaticPagedList<ViewModels.ProjectList>(model, pageIndex + 1, pageSize, totalPostCount);
                ViewBag.Projects = result;
                return View(model);
            }

        }

        //لیست 7 پروژه آخر - مثلا برای صفحه اول پروفایل
        [AjaxOnly]
        public PartialViewResult BriefList()
        { 
            using (ServiceLayer.ProjectService service = new ServiceLayer.ProjectService())
            {
                int totalPostCount;
                var model = service.getUserProjectList(User.Identity.GetUserId(), 1,7, out totalPostCount);
                ViewBag.TotalCount = totalPostCount;
                return PartialView("partial/_BriefList",model);
            }

        }

        public void SetCategories()
        {
            using (ServiceLayer.ProjectService service = new ServiceLayer.ProjectService())
            {
                List<SelectListItem> cats = new List<SelectListItem>();//برای سلکت
                List<SelectListItem> skillList = new List<SelectListItem>();//برای سلکت
                List<CategorySkillsViewModel> catskills = new List<CategorySkillsViewModel>();
                SelectListItem defitem = new SelectListItem() { Value = "null", Text = "انتخاب کنید" };

                //Add select list item to list of selectlistitems
                cats.Add(defitem);
                skillList.Add(defitem);
                foreach (var item in service.getMainCategories())
                {
                    SelectListGroup optionGroup = new SelectListGroup() { Name = item.Text };
                    foreach (var sub in service.getSubCategories(item.ID))
                    {
                        cats.Add(new SelectListItem()
                        {
                            Value = sub.ID.ToString(),
                            Text = sub.Title,
                            Group = optionGroup
                        });
                        catskills.Add(sub);
                    }
                }
                ViewBag.Categories = new SelectList(cats, "Value", "Text", "Group.Name", (object)null);
                ViewBag.SkillData = Newtonsoft.Json.JsonConvert.SerializeObject(catskills);

                var skills = service.GetSkillList();
                foreach (var item in skills)
                {
                    SelectListGroup optionGroup = new SelectListGroup() { Name = item.Title };
                    foreach (var sub in item.Skills1)
                    {
                        skillList.Add(new SelectListItem()
                        {
                            Value = sub.ID.ToString(),
                            Text = sub.Title,
                            Group = optionGroup
                        });
                    }
                }



                ViewBag.Skills = new SelectList(skillList, "Value", "Text", "Group.Name", (object)null);
                ////////////ذخیره تخصص ها در ویوبگ.........
            }
        }

        public ActionResult Add()
        {
            SetCategories();





            ViewModels.ProjectAddViewModel model = new ViewModels.ProjectAddViewModel();

            using (ServiceLayer.SettingValueService service = new ServiceLayer.SettingValueService())
            {
                var prices = service.GetCodesByGroup("ProjectBadges").ToDictionary(a => a.Name, a => int.Parse(a.Value));
                model.Prices = prices;
                //foreach (var item in prices)
                //{
                //    model.Prices.Add(item.Name, int.Parse(item.Value));
                //}
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Add([Bind(Exclude = "Prices")] ViewModels.ProjectAddViewModel model)
        {
            using (ServiceLayer.SettingValueService serviceCodeValue = new ServiceLayer.SettingValueService())
            using (ServiceLayer.ProjectService service = new ServiceLayer.ProjectService())
            using (ServiceLayer.UserService userservice = new ServiceLayer.UserService())
            {
                var prices = serviceCodeValue.GetCodesByGroup("ProjectBadges").ToDictionary(a => a.Name, a => int.Parse(a.Value));
                model.Prices = prices;
                


                if (ModelState.IsValid)
                {
                    DataLayer.Project entity = model.ToEntity();
                    
                        string userId = User.Identity.GetUserId();

                        DateTime now = DateTime.Now;
                        entity.RegDate = Utilities.Functions.PersianDateTime(now);

                        entity.Url = Utilities.Functions.GenerateSlug(entity.Title);
                        //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        entity.UserID = userId;
                        entity.ExpireDateEn = now.AddDays(30);
                        entity.LastOfferDateEn = null;
                        entity.RenewDateEn = now;
                        entity.Description = entity.Description.Replace("\r\n", "<br>");
                   

                    #region محاسبه هزینه
                    int cost = prices["Submit"];
                        if (model.ContactAllowBadge)
                            cost += prices["ContactAllowBadge"];
                        if (model.HiddenOffersBadge)
                            cost += prices["HiddenOffersBadge"];
                        if (model.LadderBadge)
                            cost += prices["LadderBadge"];
                        if (model.PrivateBadge)
                            cost += prices["PrivateBadge"];
                        if (model.RecruiteBadge)
                            cost += prices["RecruiteBadge"];
                        if (model.SeHiddenBadge)
                            cost += prices["SeHiddenBadge"];
                        if (model.SpecialBadge)
                            cost += prices["SpecialBadge"];
                        if (model.UrgentBadge)
                            cost += prices["UrgentBadge"];

                        entity.SubmitCost = cost;




                        if (cost == 0) entity.ProjectStatus = (byte)DomainClasses.ProjectStatus.NewToAdmin;
                        else entity.ProjectStatus = (byte)DomainClasses.ProjectStatus.PayPending;
                        #endregion


                        int entityID = service.Add(entity);

                        if (entityID == -1)
                        {
                            ModelState.AddModelError("", "خطا در افزودن پروژه. لطفا بعدا مجدد تلاش کنید.");
                        }
                        else
                        {

                        SaveSkillMapping(entityID, model.SelectedSkills);
                        if (cost != 0)
                            {
                                bool canPayByUserMoney = Utilities.Actions.AddBill(userId, cost, $"کسر بابت ارسال پروژه کد {entity.ID}",
                                                                        false, null, DomainClasses.EntityTypes.Project, entity.ID);
                                if (canPayByUserMoney)
                                {
                                    entity.ProjectStatus = (byte)DomainClasses.ProjectStatus.NewToAdmin;

                                    service.Edit(entity);
                                    this.ShowMessage(MessageType.Success, "درخواست شما با موفقیت ارسال شد.", true);
                                    this.ShowMessage(MessageType.Info, $"مبلغ {Utilities.Functions.SetCama(cost)} تومان از حساب شما کسر گردید.", true);
                                    //TempData["IsNewSubmit"] = true;
                                    return RedirectToAction("detail", new { id = entity.ID });
                                }
                                else
                                {

                                }

                            }
                        }




                    }
                else
                {
                    string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    ModelState.AddModelError("", "خطا در اطلاعات وارد شده. "+ messages);
                    }
                } 
            SetCategories();
            return View(model);
        }



        public void SaveSkillMapping(int entityID, IEnumerable<int> list)
        {
            //به ازای هر فیلد مربوط به زبان، باید اینسرت یا آپدیت شود
            using (ServiceLayer.ProjectService service = new ServiceLayer.ProjectService())
            {
                service.InsertSkillMapping(entityID, list);

            }
        }

        public async Task<ActionResult> Detail(int id)
        {
            using (ServiceLayer.ProjectService service = new ServiceLayer.ProjectService())
            {

                DataLayer.Project entity =await service.FindForUser(id, User.Identity.GetUserId());
                if (entity == null)
                    return HttpNotFound();
                switch (entity.ProjectStatus)
                {
                    case (byte)DomainClasses.ProjectStatus.EditedNotApproved:
                    case (byte)DomainClasses.ProjectStatus.NewToAdmin:
                        {
                            ViewBag.ProjectStatus = "WaitingToApprove";
                            break;
                        }
                    case (byte)DomainClasses.ProjectStatus.NotApproved:
                        {
                            ViewBag.ProjectStatus = "NotApproved";
                            break;
                        }
                    case (byte)DomainClasses.ProjectStatus.PayPending:
                        {
                            ViewBag.ProjectStatus = "PayPending";
                            break;
                        }
                    case (byte)DomainClasses.ProjectStatus.ApprovedShowing:
                        {
                            ViewBag.ProjectStatus = "ApprovedShowing";
                            break;
                        }
                    default:
                        break;
                }

                return View(entity.ToModel());
            }
        }

    }
}