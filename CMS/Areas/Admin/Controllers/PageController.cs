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
using ViewModels;
using Utilities;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class PageController : Controller
    {
        
        public ActionResult Index()
        {
           //return View(db.PageContents.ToList());

            return View();
        }
        public ActionResult ListPages(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        { 
            using (ServiceLayer.PageContentService pageService = new ServiceLayer.PageContentService())
            {

                var fetched_data = pageService.getPageList(take, skip, sort, filter);
                return Json(fetched_data, JsonRequestBehavior.AllowGet);

            }
             
        }




        // ADD And Edit
        public ActionResult Add(int? id)
        {

            using (ServiceLayer.PageContentService pageService = new ServiceLayer.PageContentService())
            using (ServiceLayer.LanguageService langserv = new ServiceLayer.LanguageService())
            {

                var lanquages = langserv.getLanguageListNoDefault();
                ViewBag.LanguageList = lanquages; //برای نمایش لیست زبان ها در تب اطلاعات

                if (id == null) //اگه در مود اینسرت باشد
                {
                    var entityModel = new ViewModels.PageViewModel();// یک مدل خالی به ویو می دهیم

                    #region اضافه کردن زبان ها به مدل
                    List<ViewModels.PageLocalizedModel> pagelans = new List<ViewModels.PageLocalizedModel>();
                    foreach (var item in lanquages)
                    {
                        pagelans.Add(new ViewModels.PageLocalizedModel(item.ID));
                    }
                    entityModel.Locales = pagelans;
                    #endregion


                    return View(entityModel);
                }
                else
                {//اگر در مود آپدیت باشد

                    using (ServiceLayer.LocalizedPropertyService localservice = new ServiceLayer.LocalizedPropertyService())
                    {

                        PageContent pageContent = pageService.Find(id);
                        if (pageContent == null)
                        {
                            return HttpNotFound();
                        }


                        ViewModels.PageViewModel model = pageContent.ToModel();//مپ کردن مدل به ویومدل

                        List<DataLayer.LocalizedProperty> localizedproperty = localservice.getEntityProperties("PageContent", (int)id);

                        List<ViewModels.PageLocalizedModel> pagelans = new List<ViewModels.PageLocalizedModel>();
                        LocalizedProperty l;

                        //به تعداد زبان ها تکرار میشه و هر فیلد را بر اساس زبان و فیلد پر میکنه
                        foreach (var item in lanquages)

                        {
                            pagelans.Add(new ViewModels.PageLocalizedModel()
                            {
                                LanguageId = item.ID,
                                Description = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "Description")) == null ? null : l.LocaleValue,
                                KeyWords = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "KeyWords")) == null ? null : l.LocaleValue,
                                SeoTitle = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "SeoTitle")) == null ? null : l.LocaleValue,
                                Text = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "Text")) == null ? null : l.LocaleValue,
                                Title = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "Title")) == null ? null : l.LocaleValue
                            });
                        }

                        model.Locales = pagelans;//ست کردن لوکیل ها

                        return View(model);
                    }
                }
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(
        ViewModels.PageViewModel entity,
        string postType
            )
        {
            using (ServiceLayer.LanguageService langserv = new ServiceLayer.LanguageService())
            using (ServiceLayer.PageContentService pageService = new ServiceLayer.PageContentService())
            {

                var lanquages = langserv.getLanguageListNoDefault();
                ViewBag.LanguageList = lanquages; //برای نمایش لیست زبان ها در تب اطلاعات

                int entityID = 0;
                if (postType == "delete")
                {
                    //حذف شود.
                    pageService.DeletePage(entity.ID);
                    return RedirectToAction("index");
                }
                else
                {
                    if (ModelState.IsValid)
                    {

                        if (entity.ID == 0)
                        {
                            entity.ViewCount = 0;
                            entity.Editable = true;
                            if (string.IsNullOrEmpty(entity.Url))
                                entity.Url = Utilities.Functions.GenerateSlug(entity.Title);

                            if (pageService.CheckIfUrlExists(entity.ID, entity.Url))
                            {
                                //آدرس صفحه تکراری استی

                                ModelState.AddModelError("", "آدرس صفحه تکراری است.");
                                return View(entity);
                            }

                            var item = entity.ToEntity();

                            entityID = pageService.Add(item);
                            if (entityID != -1)
                            {
                                //ذخیره مقادیر زبان ها
                                SaveLocals(entityID, entity.Locales);
                            }
                            else
                            {
                                ModelState.AddModelError("", "خطا در ذخیره اطلاعات.");
                                return View(entity);
                            }

                        }
                        else
                        {


                            if (string.IsNullOrEmpty(entity.Url))
                                entity.Url = Utilities.Functions.GenerateSlug(entity.Title);

                            if (pageService.CheckIfUrlExists(entity.ID, entity.Url))
                            {
                                //آدرس صفحه تکراری استی

                                ModelState.AddModelError("", "آدرس صفحه تکراری است.");
                                return View(entity);
                            }



                            var item = entity.ToEntity();
                            entityID = item.ID;
                            if (pageService.Edit(item))
                            {
                                //ذخیره مقادیر زبان ها
                                SaveLocals(entityID, entity.Locales);
                            }
                            else
                            {
                                ModelState.AddModelError("", "خطا در ذخیره اطلاعات.");
                                return View(entity);
                            }

                        }

                        //بسته به نوع دکمه کلیک شده، ریدایرکت میکنه
                        if (postType == "save")
                            return RedirectToAction("index");
                        else if (postType == "save-continiue")
                            return RedirectToAction("edit", "page", new { @id = entityID });
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
        public void SaveLocals(int entityID, List<ViewModels.PageLocalizedModel> list)
        {
            //به ازای هر فیلد مربوط به زبان، باید اینسرت یا آپدیت شود
            using (ServiceLayer.LocalizedPropertyService service = new ServiceLayer.LocalizedPropertyService())
            {
                string LocalKeyGroup = "PageContent";
                foreach (ViewModels.PageLocalizedModel item in list)
                {
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "Description", item.Description);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "KeyWords", item.KeyWords);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "SeoTitle", item.SeoTitle);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "Text", item.Text);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "Title", item.Title);
                }
            }
        }




        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ServiceLayer.PageContentService service = new ServiceLayer.PageContentService())
            {
                if (service.DeletePage(id))
                    return Json("success", JsonRequestBehavior.AllowGet);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }





    }
}
