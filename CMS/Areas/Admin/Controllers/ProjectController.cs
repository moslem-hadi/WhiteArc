using DataLayer;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilities;
using ViewModels;

namespace CMS.Areas.Admin.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Admin/Project 
        public ActionResult Index()
        {
            return View();
        }
    



        public ActionResult Add(int? id)
        {

            using (ServiceLayer.ProjectService service = new ServiceLayer.ProjectService())
            {
                 
                if (id == null) //اگه در مود اینسرت باشد
                {
                    var entityModel = new ViewModels.ProjectViewModel();// یک مدل خالی به ویو می دهیم
                     

                    var groups = service.getTreeGroups(null, "— ");
                    ViewBag.Categories = new MultiSelectList(groups, "ID", "Text");
                    return View(entityModel);
                }
                else {//اگر در مود آپدیت باشد

                    using (ServiceLayer.LocalizedPropertyService localservice = new ServiceLayer.LocalizedPropertyService())
                    using (ServiceLayer.ProjectGroupMappingService postGroupMappServ = new ServiceLayer.ProjectGroupMappingService())
                    {

                        Project Project = service.Find(id);
                        if (Project == null)
                        {
                            return HttpNotFound();
                        }


                        ViewModels.ProjectViewModel model = Project.ToModel();//مپ کردن مدل به ویومدل

                        List<DataLayer.LocalizedProperty> localizedproperty = localservice.getEntityProperties("Project", (int)id);

                        List<ViewModels.ProjectLocalizedModel> langs = new List<ViewModels.ProjectLocalizedModel>();
                        LocalizedProperty l;

                        //به تعداد زبان ها تکرار میشه و هر فیلد را بر اساس زبان و فیلد پر میکنه
                        foreach (var item in lanquages)

                        {
                            langs.Add(new ViewModels.ProjectLocalizedModel()
                            {
                                LanguageId = item.ID,
                                SeoDescription = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "SeoDescription")) == null ? null : l.LocaleValue,
                                KeyWords = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "KeyWords")) == null ? null : l.LocaleValue,
                                SeoTitle = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "SeoTitle")) == null ? null : l.LocaleValue,
                                FullDescription = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "FullDescription")) == null ? null : l.LocaleValue,
                                Title = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "Title")) == null ? null : l.LocaleValue
                            });
                        }

                        model.Locales = langs;//ست کردن لوکیل ها



                        //گرفتن کد گروه های مپ شده و اختصاص به مدل
                        IEnumerable<int> selectedGroupIDs = postGroupMappServ.GetProjectSelectedGroupIds(model.ID);
                        model.SelectedGroupIDs = selectedGroupIDs;
                        var groups = service.getTreeGroups(null,   "— ");
                        ViewBag.Categories = new MultiSelectList(groups, "ID", "Text", selectedGroupIDs);
                        return View(model);
                    }
                }
            }

        }
         




        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(
        ViewModels.ProjectViewModel entity,string postType)
        {
            bool canSave = true;
            using (ServiceLayer.ProjectService service = new ServiceLayer.ProjectService())
            {
                int entityID = 0;
                if (postType == "delete")
                {
                    //حذف شود.
                    service.DeleteProject(entity.ID);
                    TempData["Notify"] = "'success', 'top left', '', 'با موفقیت حذف شد'";
                    return RedirectToAction("index");
                }
                else {
                    if (ModelState.IsValid)
                    {

                        if (entity.ID == 0)
                        {
                            entity.ViewCount = 0;
                            entity.IsActive = true;
                            entity.RegDate = entity.LastRenewal = entity.LastEdit =Utilities.Functions.String2date(DateTime.Now,2,"D,H");
                            if (entity.Url == null)
                                entity.Url = Functions.ReplaceSpace(entity.Title);
                            entity.IsActive = true;
                            entity.IsNew = true;
                            entity.IsHot = true;
                            entity.OldPrice = 0;
                            entity.Priority = 0;
                            entity.Emtiaz = 0;
                            entity.OffPercent = 0;
                            entity.Avalatility = (short)DomainClasses.AvailabilityType.Available;
                            entity.IsDeleted = false;
                            entity.BrandID = 0;
                            entity.BonCount = 0;
                            entity.Weight = 0;
                            entity.Length = 0;
                            entity.OrderMinimumQuantity = 0;
                            entity.OrderMaximumQuantity = 0;
                            entity.StockQuantity = 0;
                            entity.MaxNumberOfDownloads = 0;
                            entity.DisableBuy = false;
                            entity.DisplayStockQuantity = false;
                            entity.IsFreeShipping = false;
                            entity.IsDownload = false;

                            if (string.IsNullOrEmpty(entity.Url))
                                entity.Url = Utilities.Functions.ReplaceSpace(entity.Title);

                            var item = entity.ToEntity();

                            entityID = service.Add(item);
                            if (entityID != -1)
                            {
                                //ذخیره مقادیر زبان ها
                                SaveLocals(entityID, entity.Locales);
                                SaveGroupMapping(entityID, entity.SelectedGroupIDs);
                            }
                            else {
                                ModelState.AddModelError("", "خطا در ذخیره اطلاعات.");
                                canSave = false;
                            }

                        }
                        else
                        {


                            if (string.IsNullOrEmpty(entity.Url))
                                entity.Url = Utilities.Functions.ReplaceSpace(entity.Title);


                            var item = entity.ToEntity();
                            entityID = item.ID;
                            item.LastEdit = DateTime.Now.ToString();
                            if (service.Edit(item))
                            {
                                //ذخیره مقادیر زبان ها
                                SaveLocals(entityID, entity.Locales);
                                SaveGroupMapping(entityID, entity.SelectedGroupIDs);
                            }
                            else {
                                ModelState.AddModelError("", "خطا در ذخیره اطلاعات.");
                                canSave = false;
                            }

                        }

                        if (canSave)
                        {
                            //بسته به نوع دکمه کلیک شده، ریدایرکت میکنه
                            TempData["Notify"] = "'success', 'top left', '', 'با موفقیت دخیره شد'";
                            if (postType == "save")
                                return RedirectToAction("index");
                            else if (postType == "save-continiue")
                                return RedirectToAction("edit", "Project", new { @id = entityID });

                        }
                    }
                    else
                    {

                        ModelState.AddModelError("", "خطا در اطلاعات وارد شده.");
                    }




                    using (ServiceLayer.LanguageService langserv = new ServiceLayer.LanguageService())
                    using (ServiceLayer.ProjectGroupMappingService postGroupMappServ = new ServiceLayer.ProjectGroupMappingService())
                    {

                        var lanquages = langserv.getLanguageListNoDefault();
                        ViewBag.LanguageList = lanquages;
                        //گرفتن کد گروه های مپ شده و اختصاص به مدل
                        IEnumerable<int> selectedGroupIDs = postGroupMappServ.GetProjectSelectedGroupIds(entity.ID);
                        
                        var groups = service.getTreeGroups(null, "— ");
                        ViewBag.Categories = new MultiSelectList(groups, "ID", "Text", selectedGroupIDs);
                        return View(entity);

                    }
                }
            }
        }

        /// <summary>
        /// ذخیره فیلد های هر زبان
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="list"></param>
        public void SaveLocals(int entityID, List<ViewModels.ProjectLocalizedModel> list)
        {
            //به ازای هر فیلد مربوط به زبان، باید اینسرت یا آپدیت شود
            using (ServiceLayer.LocalizedPropertyService service = new ServiceLayer.LocalizedPropertyService())
            {
                string LocalKeyGroup = "Project";
                foreach (ViewModels.ProjectLocalizedModel item in list)
                {
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "SeoDescription", item.SeoDescription);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "KeyWords", item.KeyWords);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "SeoTitle", item.SeoTitle);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "FullDescription", item.FullDescription);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "Title", item.Title);
                }
            }
        }


        public void SaveGroupMapping(int entityID, IEnumerable<int> list)
        {
            //به ازای هر فیلد مربوط به زبان، باید اینسرت یا آپدیت شود
            using (ServiceLayer.ProjectGroupMappingService service = new ServiceLayer.ProjectGroupMappingService())
            {
                service.InsertGroupMapping(entityID, list);

            }
        }










        public ActionResult ListProjects(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            using (ServiceLayer.ProjectService service = new ServiceLayer.ProjectService())
            {

                var result_kala = service.getProjectList(take, skip, sort, filter);
                return Json(result_kala, JsonRequestBehavior.AllowGet);

            }

        }


         


        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ServiceLayer.ProjectService service = new ServiceLayer.ProjectService())
            {

                if (service.DeleteProject(id))
                    return Json("success", JsonRequestBehavior.AllowGet);

                return Json("error", JsonRequestBehavior.AllowGet);

            }



        }
    }
}