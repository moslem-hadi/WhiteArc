using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Utilities;
using Kendo.DynamicLinq;
using ViewModels;
using DomainClasses;
using System.ComponentModel.DataAnnotations;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        public ActionResult Index()
        {
            string controllerName = Request.RawUrl.ToLower().Split('/')[2];
            var typeVar = EnumExtensions.GetEnumValue<PostType>(controllerName);
            string typeName = typeVar.GetAttribute<DisplayAttribute>().Name;

            ViewBag.Type = controllerName.ToLower();
            ViewBag.TypeName = typeName;
            ViewBag.TypeCode = ((byte)typeVar).ToString();

            return View();
        }
        public ActionResult ArticleList()
        {
            return View();
        }

        public ActionResult Add(int? id)
        {
            using (ServiceLayer.LanguageService langserv = new ServiceLayer.LanguageService())
            using (ServiceLayer.PostGroupService service = new ServiceLayer.PostGroupService())
            {
                string controllerName = Request.RawUrl.ToLower().Split('/')[2]; 
                var typeVar = EnumExtensions.GetEnumValue<PostType>(controllerName);
                string typeName = typeVar.GetAttribute<DisplayAttribute>().Name;

                ViewBag.Type = controllerName.ToLower();
                ViewBag.TypeName = typeName;
                ViewBag.TypeCode =( (byte)typeVar).ToString();
                ViewBag.LanguageList = langserv.getLanguageListForDrop();
                int topLangID = langserv.GrtFisrtLanguageID();

                if (id == null)
                {
                    var entityModel = new ViewModels.PostViewModel();// یک مدل خالی به ویو می دهیم

                    var groups = service.getTreeGroups(null,topLangID, (byte)typeVar,"—");
                    ViewBag.Categories = new MultiSelectList(groups, "ID", "Text");


                    return View(entityModel);
                }
                else
                {
                    using (ServiceLayer.PostService postService = new ServiceLayer.PostService())
                    using (ServiceLayer.PostGroupMappingService postGroupMappServ = new ServiceLayer.PostGroupMappingService())
                    {

                        Post post = postService.Find(id);
                        if (post == null || post.IsDeleted)
                        {
                            return HttpNotFound();
                        }


                        ViewModels.PostViewModel model = post.ToModel();//مپ کردن مدل به ویومدل


                        //گرفتن کد گروه های مپ شده و اختصاص به مدل
                        IEnumerable<int> selectedGroupIDs = postGroupMappServ.GetArticleSelectedGroupIds(model.ID);
                        model.SelectedGroupIDs = selectedGroupIDs;
                        var groups = service.getTreeGroups(null, (int)model.LanguageID, (byte)typeVar, "— ");
                        ViewBag.Categories = new MultiSelectList(groups, "ID", "Text", selectedGroupIDs);

                        return View(model);
                    }
                }
            }
        }



        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ViewModels.PostViewModel entity,  string postType)
        {
            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {

                bool canSave = true;
                int entityID = 0;
                string enumtype = Enum.GetName(typeof(DomainClasses.PostType), entity.Type).ToLower();

                if (postType == "delete")
                {
                    //حذف شود.
                    service.DeletePost(entity.ID);
                    TempData["Notify"] = "'success', 'top left', '', 'با موفقیت حذف شد'";
                    return RedirectToAction("index", enumtype,null);
                }
                else {

                    if (ModelState.IsValid)
                    {

                        if (entity.ID == 0)
                        {
                            DateTime now = DateTime.Now;
                            entity.ViewCount = 0; 
                            entity.Cover = "";
                            entity.IsActive = true;
                            entity.IsCommentActive = true;
                            entity.OnlyUserCanComment = false;
                            entity.RegDate = Utilities.Functions.PersianDateTime(now);
                            entity.PublishDate =DateTime.Now.ToShortDateString();//برای اینکه نشون بده.
                            entity.Url = Functions.GenerateSlug(entity.Url.Trim());
                            if (string.IsNullOrEmpty(entity.Url))
                                entity.Url = Utilities.Functions.GenerateSlug(entity.Title);
                            entity.Url = Functions.SubStringText(entity.Url, 0, 70);

                          var item = entity.ToEntity();

                            entityID = service.Add(item);

                            if (entityID != -1)
                            {
                                //ذخیره مقادیر گروپ مپینگ
                                if (entity.SelectedGroupIDs != null && entity.SelectedGroupIDs.Count() > 0)
                                    SaveGroupMapping(entityID, entity.SelectedGroupIDs);
                            }
                            else {
                                ModelState.AddModelError("", "خطا در ذخیره محتوا.");
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
                                //ذخیره مقادیر گروپ مپینگ
                                SaveGroupMapping(entityID, entity.SelectedGroupIDs);
                            }
                            else {
                                ModelState.AddModelError("", "خطا در ذخیره اطلاعات.");
                                canSave = false;
                            }


                        }



                        if (canSave)
                        {
                            TempData["Notify"] = "'success', 'top left', '', 'با موفقیت ذخیره شد'";
                            //بسته به نوع دکمه کلیک شده، ریدایرکت میکنه
                            if (postType == "save")
                            {

                                return RedirectToAction("index", enumtype, null);
                            }
                            else if (postType == "save-continue")
                            {
                                return RedirectToAction("edit", enumtype, new { @id = entityID });
                            }

                        }

                    }

                    else
                    {

                        ModelState.AddModelError("", "خطا در اطلاعات وارد شده.");
                    }



                    using (ServiceLayer.PostGroupMappingService postGroupMappServ = new ServiceLayer.PostGroupMappingService())
                    using (ServiceLayer.PostGroupService groupserv = new ServiceLayer.PostGroupService())
                    {
                        //گرفتن کد گروه های مپ شده و اختصاص به مدل
                        IEnumerable<int> selectedGroupIDs = postGroupMappServ.GetArticleSelectedGroupIds(entity.ID);
                        var groups = groupserv.getTreeGroups(null, (int)entity.LanguageID, (byte)entity.Type,  "— ");
                        ViewBag.Categories = new MultiSelectList(groups, "ID", "Text", selectedGroupIDs);

                        string controllerName = Request.RawUrl.ToLower().Split('/')[2];
                        var typeVar = EnumExtensions.GetEnumValue<PostType>(controllerName);
                        string typeName = typeVar.GetAttribute<DisplayAttribute>().Name;

                        ViewBag.Type = controllerName.ToLower();
                        ViewBag.TypeName = typeName;
                        ViewBag.TypeCode = ((byte)typeVar).ToString();

                        return View(entity);
                    }
                }


            }
        }



        public void SaveGroupMapping(int entityID, IEnumerable<int> list)
        { 
            using (ServiceLayer.PostGroupMappingService service = new ServiceLayer.PostGroupMappingService())
            {  
                    service.InsertGroupMapping(entityID, list);
                
            }
        }


        [HttpPost]
        public ActionResult ListPosts(byte typeID, int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            using (ServiceLayer.PostService service = new ServiceLayer.PostService())
            {

                var fetched_data = service.getPostList(typeID,take, skip, sort, filter);
                return Json(fetched_data, JsonRequestBehavior.AllowGet);

            }

        }


    
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ServiceLayer.PostService postService = new ServiceLayer.PostService())
            {

                if (postService.DeletePost(id))
                    return Json("success", JsonRequestBehavior.AllowGet);

                return Json("error", JsonRequestBehavior.AllowGet);

            }



        }
    }
}
