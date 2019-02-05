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
   [Authorize]
    public class ProductController : Controller
    {
        // GET: Admin/Product 
        public ActionResult Index()
        {
            return View();
        }
    



        public ActionResult Add(int? id)
        {

            using (ServiceLayer.ProductService service = new ServiceLayer.ProductService())
            using (ServiceLayer.LanguageService langserv = new ServiceLayer.LanguageService())
            {

                var lanquages = langserv.getLanguageListNoDefault();
                ViewBag.LanguageList = lanquages; //برای نمایش لیست زبان ها در تب اطلاعات
                 
                if (id == null) //اگه در مود اینسرت باشد
                {
                    var entityModel = new ViewModels.ProductViewModel();// یک مدل خالی به ویو می دهیم

                    #region اضافه کردن زبان ها به مدل
                    List<ViewModels.ProductLocalizedModel> pagelans = new List<ViewModels.ProductLocalizedModel>();
                    foreach (var item in lanquages)
                    {
                        pagelans.Add(new ViewModels.ProductLocalizedModel(item.ID));
                    }
                    entityModel.Locales = pagelans;
                    #endregion


                    var groups = service.getTreeGroups(null, "— ");
                    ViewBag.Categories = new SelectList(groups, "ID", "Text");
                    return View(entityModel);
                }
                else {//اگر در مود آپدیت باشد

                    using (ServiceLayer.LocalizedPropertyService localservice = new ServiceLayer.LocalizedPropertyService())
                    using (ServiceLayer.ProductGroupMappingService postGroupMappServ = new ServiceLayer.ProductGroupMappingService())
                    {

                        Product Product = service.Find(id);
                        if (Product == null)
                        {
                            return HttpNotFound();
                        }


                        ViewModels.ProductViewModel model = Product.ToModel();//مپ کردن مدل به ویومدل

                        List<DataLayer.LocalizedProperty> localizedproperty = localservice.getEntityProperties("Product", (int)id);

                        List<ViewModels.ProductLocalizedModel> langs = new List<ViewModels.ProductLocalizedModel>();
                        LocalizedProperty l;

                        //به تعداد زبان ها تکرار میشه و هر فیلد را بر اساس زبان و فیلد پر میکنه
                        foreach (var item in lanquages)

                        {
                            langs.Add(new ViewModels.ProductLocalizedModel()
                            {
                                LanguageId = item.ID,
                                SeoDescription = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "SeoDescription")) == null ? null : l.LocaleValue,
                                SeoTitle = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "SeoTitle")) == null ? null : l.LocaleValue,
                                FullDescription = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "FullDescription")) == null ? null : l.LocaleValue,
                                Title = (l = localizedproperty.FirstOrDefault(a => a.LanguageID == item.ID && a.LocaleKey == "Title")) == null ? null : l.LocaleValue
                            });
                        }

                        model.Locales = langs;//ست کردن لوکیل ها



                        //گرفتن کد گروه های مپ شده و اختصاص به مدل
                        //IEnumerable<int> selectedGroupIDs = postGroupMappServ.GetProductSelectedGroupIds(model.ID);
                       // model.SelectedGroupIDs = selectedGroupIDs;
                        var groups = service.getTreeGroups(null,   "— ");
                        ViewBag.Categories = new SelectList(groups, "ID", "Text", model.CategoryID);
                        return View(model);
                    }
                }
            }

        }
         




        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(
        ViewModels.ProductViewModel entity,string postType)
        {
            bool canSave = true;
            using (ServiceLayer.ProductService service = new ServiceLayer.ProductService())
            {
                int entityID = 0;
                if (postType == "delete")
                {
                    //حذف شود.
                    service.DeleteProduct(entity.ID);
                    TempData["Notify"] = "'success', 'top left', '', 'با موفقیت حذف شد'";
                    return RedirectToAction("index");
                }
                else {
                    if (ModelState.IsValid)
                    {

                        if (entity.ID == 0)
                        {
                            entity.ViewCount = 0; 
                            entity.Priority = 0;
                            entity.IsDeleted = false;
                            entity.IsActive = true;
                            if (string.IsNullOrEmpty(entity.Url))
                                entity.Url = Utilities.Functions.GenerateSlug(entity.Title);

                            var item = entity.ToEntity();

                            entityID = service.Add(item);
                            if (entityID != -1)
                            {
                                //ذخیره مقادیر زبان ها
                                SaveLocals(entityID, entity.Locales);
                              //  SaveGroupMapping(entityID, entity.SelectedGroupIDs);
                            }
                            else {
                                ModelState.AddModelError("", "خطا در ذخیره اطلاعات.");
                                canSave = false;
                            }

                        }
                        else
                        {


                            if (string.IsNullOrEmpty(entity.Url))
                                entity.Url = Utilities.Functions.GenerateSlug(entity.Title);


                            var item = entity.ToEntity();
                            entityID = item.ID;
                           
                            if (service.Edit(item))
                            {
                                //ذخیره مقادیر زبان ها
                                SaveLocals(entityID, entity.Locales);
                               // SaveGroupMapping(entityID, entity.SelectedGroupIDs);
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
                            else if (postType == "save-continue")
                                return RedirectToAction("edit", "product", new { @id = entityID });

                        }
                    }
                    else
                    {

                        ModelState.AddModelError("", "خطا در اطلاعات وارد شده.");
                    }




                    using (ServiceLayer.LanguageService langserv = new ServiceLayer.LanguageService())
                    using (ServiceLayer.ProductGroupMappingService postGroupMappServ = new ServiceLayer.ProductGroupMappingService())
                    {

                        var lanquages = langserv.getLanguageListNoDefault();
                        ViewBag.LanguageList = lanquages;
                        //گرفتن کد گروه های مپ شده و اختصاص به مدل
                        IEnumerable<int> selectedGroupIDs = postGroupMappServ.GetProductSelectedGroupIds(entity.ID);
                        
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
        public void SaveLocals(int entityID, List<ViewModels.ProductLocalizedModel> list)
        {
            //به ازای هر فیلد مربوط به زبان، باید اینسرت یا آپدیت شود
            using (ServiceLayer.LocalizedPropertyService service = new ServiceLayer.LocalizedPropertyService())
            {
                string LocalKeyGroup = "Product";
                foreach (ViewModels.ProductLocalizedModel item in list)
                {
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "SeoDescription", item.SeoDescription);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "SeoTitle", item.SeoTitle);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "FullDescription", item.FullDescription);
                    service.InsertLocalizedData(entityID, item.LanguageId, LocalKeyGroup, "Title", item.Title);
                }
            }
        }


        public void SaveGroupMapping(int entityID, IEnumerable<int> list)
        {
            //به ازای هر فیلد مربوط به زبان، باید اینسرت یا آپدیت شود
            using (ServiceLayer.ProductGroupMappingService service = new ServiceLayer.ProductGroupMappingService())
            {
                service.InsertGroupMapping(entityID, list);

            }
        }










        public ActionResult ListProducts(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            using (ServiceLayer.ProductService service = new ServiceLayer.ProductService())
            {

                var result_kala = service.getProductList(take, skip, sort, filter);
                return Json(result_kala, JsonRequestBehavior.AllowGet);

            }

        }


         


        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ServiceLayer.ProductService service = new ServiceLayer.ProductService())
            {

                if (service.DeleteProduct(id))
                    return Json("success", JsonRequestBehavior.AllowGet);

                return Json("error", JsonRequestBehavior.AllowGet);

            }



        }
    }
}