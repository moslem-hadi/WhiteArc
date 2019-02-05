using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Kendo.DynamicLinq;
using CMS.Areas.Admin.Models;
using System.IO;
using ViewModels;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: Admin/Category

        public ActionResult Index()
        {
            using (ServiceLayer.ProductGroupService service = new ServiceLayer.ProductGroupService())
   {
                 
                var lst = service.getTreeGroups();
                return View(lst);
            }
        }




        public ActionResult Add(int? id)
        {

            using (ServiceLayer.ProductGroupService service = new ServiceLayer.ProductGroupService())
            using (ServiceLayer.LanguageService langserv = new ServiceLayer.LanguageService())
            {

                var lanquages = langserv.getLanguageListModelNoDefault();
                ViewModels.ProductGroupViewModel model= new ViewModels.ProductGroupViewModel();

             

                if (id == null) //اگه در مود اینسرت باشد
                {
                    //model= new ViewModels.ProductGroupViewModel();// یک مدل خالی به ویو می دهیم

                    #region اضافه کردن زبان ها به مدل
                    List<ViewModels.ProductGroupLocalizedModel> pagelans = new List<ViewModels.ProductGroupLocalizedModel>();
                    foreach (var item in lanquages)
                    {
                        pagelans.Add(new ViewModels.ProductGroupLocalizedModel(item.ID));
                    }
                    model.Locales = pagelans;


                    #endregion



                }
                else {//اگر در مود آپدیت باشد

                    using (ServiceLayer.LocalizedPropertyService localservice = new ServiceLayer.LocalizedPropertyService())
                    {

                        ProductGroup entity = service.Find(id);
                        if (entity == null)
                        {
                            return HttpNotFound();
                        }
                        if((bool)entity.Deleted)
                            return HttpNotFound();


                         model = entity.ToModel();//مپ کردن مدل به ویومدل

                        List<DataLayer.LocalizedProperty> localizedproperty = localservice.getEntityProperties("ProductGroup", (int)id);

                        List<ViewModels.ProductGroupLocalizedModel> pagelans = new List<ViewModels.ProductGroupLocalizedModel>();
                        LocalizedProperty l;

                        //به تعداد زبان ها تکرار میشه و هر فیلد را بر اساس زبان و فیلد پر میکنه
                        foreach (var item in lanquages)

                        {
                            pagelans.Add(new ViewModels.ProductGroupLocalizedModel()
                            {
                                LanguageId = item.ID, 
                                SeoDescription = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "SeoDescription")) == null ? null : l.LocaleValue,
                                SeoKeywords = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "SeoKeywords")) == null ? null : l.LocaleValue,
                                SeoTitle = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "SeoTitle")) == null ? null : l.LocaleValue,
                                Text = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "Text")) == null ? null : l.LocaleValue,
                                Title = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "Title")) == null ? null : l.LocaleValue,
                                FullTitle = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "FullTitle")) == null ? null : l.LocaleValue
                            });
                        }

                        model.Locales = pagelans;//ست کردن لوکیل ها
                         

                         
                    }
                }




                model.AvailableLanguage = lanquages; 

                var groups = service.getTreeGroups(null, "— ",model.ID);

                foreach (var item in groups)
                {
                    model.AvailableParentCategories.Add(new SelectListItem()
                    {
                        Text = item.Text,
                        Value = item.ID.ToString()
                    });
                }




                return View(model);
            }

        }



 

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(  ViewModels.ProductGroupViewModel entity,string postType)
        {
            bool canSave = true;
            using (ServiceLayer.ProductGroupService service = new ServiceLayer.ProductGroupService())
            using (ServiceLayer.LanguageService langserv = new ServiceLayer.LanguageService())
            {
                int entityID = 0;
                if (postType == "delete")
                {
                    //حذف شود.
                    service.DeleteProductGroup(entity.ID);
                    TempData["Notify"] = "'success', 'top left', '', 'با موفقیت حذف شد'";
                    return RedirectToAction("index");
                }
                else
                {
                    if (ModelState.IsValid)
                    {

                        if (entity.ID == 0)
                        {

                            if (string.IsNullOrEmpty(entity.Url))
                                entity.Url = Utilities.Functions.GenerateSlug(entity.Title);

                            if (service.CheckIfUrlExists(entity.ID, entity.Url))
                            {
                                //آدرس صفحه تکراری استی

                                ModelState.AddModelError("", "آدرس URL تکراری است.");
                                canSave = false;
                                //--------------------  return View(entity);
                            }
                            else {

                                var item = entity.ToEntity();
                                item.RegDate = DateTime.Now;
                                item.Show = true;
                                if (string.IsNullOrEmpty(item.FullTitle))
                                    item.FullTitle = item.Title;
                                if (string.IsNullOrEmpty(item.SeoTitle))
                                    item.SeoTitle = item.Title;
                                entityID = service.Add(item);
                                if (entityID != -1)
                                {
                                    //ذخیره مقادیر زبان ها
                                    SaveLocals(entityID, entity.Locales);
                                }
                                else {
                                    ModelState.AddModelError("", "خطا در ذخیره اطلاعات.");
                                    canSave = false;
                                    //-----------  return View(entity);
                                }
                            }

                        }
                        else
                        {


                            if (string.IsNullOrEmpty(entity.Url))
                                entity.Url = Utilities.Functions.GenerateSlug(entity.Title);

                            if (service.CheckIfUrlExists(entity.ID, entity.Url))
                            {
                                //آدرس صفحه تکراری استی

                                ModelState.AddModelError("", "آدرس صفحه تکراری است.");
                                canSave = false;
                                //------------------  return View(entity);
                            }
                            else {

                                var item = entity.ToEntity();
                                entityID = item.ID;
                                if (string.IsNullOrEmpty(item.FullTitle))
                                    item.FullTitle = item.Title;
                                if (string.IsNullOrEmpty(item.SeoTitle))
                                    item.SeoTitle = item.Title;
                                if (service.Edit(item))
                                {
                                    //ذخیره مقادیر زبان ها
                                    SaveLocals(entityID, entity.Locales);
                                }
                                else {
                                    ModelState.AddModelError("", "خطا در ذخیره اطلاعات.");
                                    canSave = false;
                                    //-----------------  return View(entity);
                                }
                            }

                        }

                        //بسته به نوع دکمه کلیک شده، ریدایرکت میکنه
                        if (canSave)
                        {
                            TempData["Notify"] = "'success', 'top left', '', 'با موفقیت دخیره شد'";
                            if (postType == "save")
                                return RedirectToAction("index");
                            else if (postType == "save-continiue")
                                return RedirectToAction("edit", "category", new { @id = entityID });
                            else
                                return RedirectToAction("index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "خطا در اطلاعات وارد شده");
                    }







                    //---------------------اگه فرم سیو نشده باشه، میاد اینجا، اطلاعات لازم پر میشه و دوباره ویو رو میاره
                    // این روش احتمالا اشتباه است.
                    var lanquages = langserv.getLanguageListModelNoDefault();
                    entity.AvailableLanguage = lanquages;
                    var groups = service.getTreeGroups(null, "— ", entity.ID);
                    foreach (var item in groups)
                    {
                        entity.AvailableParentCategories.Add(new SelectListItem()
                        {
                            Text = item.Text,
                            Value = item.ID.ToString()
                        });
                    }

                    return View(entity);


                }
            }
        }

        /// <summary>
        /// ذخیره فیلد های هر زبان
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="list"></param>
        public void SaveLocals(int entityID, List<ViewModels.ProductGroupLocalizedModel> list)
        {
            //به ازای هر فیلد مربوط به زبان، باید اینسرت یا آپدیت شود
            using (ServiceLayer.LocalizedPropertyService service = new ServiceLayer.LocalizedPropertyService())
            {
                string LocalKeyGroup = "ProductGroup";
                foreach (ViewModels.ProductGroupLocalizedModel item in list)
                {
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "SeoDescription", item.SeoDescription);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "SeoKeywords", item.SeoKeywords);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "SeoTitle", item.SeoTitle);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "Text", item.Text);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "Title", item.Title);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "FullTitle", item.FullTitle);
                }
            }
        }











         


        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ServiceLayer.ProductGroupService service = new ServiceLayer.ProductGroupService())
            {
                if(service.DeleteProductGroup(id))
                    return Json("success", JsonRequestBehavior.AllowGet);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }




    }
}