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
using DomainClasses;
using System.ComponentModel.DataAnnotations;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class PostGroupController : Controller
    {
        // GET: Admin/PostGroup

        public ActionResult Index(int? langID)
        {
            string controllerName = Request.RawUrl.ToLower().Split('/')[2].Replace("group","").Split('?')[0];
            var typeVar = EnumExtensions.GetEnumValue<PostType>(controllerName);
            string typeName = typeVar.GetAttribute<DisplayAttribute>().Name;

            ViewBag.Type = controllerName.ToLower();
            ViewBag.TypeName = typeName;
            ViewBag.TypeCode = ((byte)typeVar).ToString();

            using (ServiceLayer.PostGroupService service = new ServiceLayer.PostGroupService())
            using (ServiceLayer.LanguageService langserv = new ServiceLayer.LanguageService())

            {

                var LanguageList = langserv.getLanguageListForDrop();
                if (langID == null)
                {
                    int topLangID = langserv.GrtFisrtLanguageID();
                    var lst = service.getTreeGroups((byte)typeVar,topLangID);
                    ViewBag.LanguageList = new SelectList(LanguageList, "ID", "Text");
                    return View(lst);
                }
                else
                {
                    var lst = service.getTreeGroups((byte)typeVar,(int)langID);
                    ViewBag.LanguageList = new SelectList(LanguageList, "ID", "Text", langID);
                    return View(lst);

                }
            }
          
        }
     


         
        public ActionResult GetGroupListByLangAndType(byte type,int lang)
        {

            using (ServiceLayer.PostGroupService service = new ServiceLayer.PostGroupService())
            {

                var tree = service.getTreeGroups(null, lang, type); 

                return new JsonResult
                {
                    Data = tree,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }



        }




        // ADD And Edit
        public ActionResult Add(int? id)
        {

            using (ServiceLayer.LanguageService langserv = new ServiceLayer.LanguageService())
            using (ServiceLayer.PostGroupService service = new ServiceLayer.PostGroupService()) 
            {
                string controllerName = Request.RawUrl.ToLower().Split('/')[2].Replace("group","");
                var typeVar = EnumExtensions.GetEnumValue<PostType>(controllerName);
                string typeName = typeVar.GetAttribute<DisplayAttribute>().Name;

                ViewBag.Type = controllerName.ToLower();
                ViewBag.TypeName = typeName;
                ViewBag.TypeCode = ((byte)typeVar).ToString();


                ViewModels.PostGroupViewModel model = new ViewModels.PostGroupViewModel();

                if (id == null)
                {

                    int topLangID = langserv.GrtFisrtLanguageID();
                    model.LanguageID = topLangID;

                }
                else {
                    model = service.Find(id).ToModel();
                    if (model == null)
                        return HttpNotFound();
                    if (model.IsDeleted)
                        return HttpNotFound();
                }


                var lanquages = langserv.getLanguageList();



                foreach (var item in lanquages)
                {
                    model.AvailableLanguage.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ID.ToString()
                    });
                }

                var groups = service.getTreeGroups(null, (int)model.LanguageID, (byte)typeVar, "", model.ID);
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
        public ActionResult Add(ViewModels.PostGroupViewModel model,string postType, string controllerName)
        {
            //string controllerName = Request.Url.OriginalString.ToLower().Split('/')[2].Replace("group", "");
             
                
                bool canSave = true;
            using (ServiceLayer.PostGroupService service = new ServiceLayer.PostGroupService())
            {
                int entityID = 0;
                if (postType == "delete")
                {
                    //حذف شود.
                    service.DeletePostGroup(model.ID);
                    TempData["Notify"] = "'success', 'top left', '', 'با موفقیت حذف شد'";
                    return RedirectToAction("index", controllerName+"group");
                }
                else {
                    if (ModelState.IsValid)
                    {
                        if (model.ID == 0)
                        {
                            if (string.IsNullOrEmpty(model.Url))
                                model.Url = Utilities.Functions.GenerateSlug(model.Title);
                            if (string.IsNullOrEmpty(model.SeoTitle))
                                model.SeoTitle = model.Title;
                            if (service.CheckIfUrlExists(model.ID,model.Type, model.Url))
                            {
                                //آدرس صفحه تکراری استی
                                ModelState.AddModelError("", "آدرس URL تکراری است.");
                                // return View(model);
                                canSave = false;
                            }
                            else
                            {
                                PostGroup entity = model.ToEntity();
                                entityID = service.Add(entity);
                                if(entityID==-1)
                                    canSave = false;
                            }
                        }
                        else
                        {
                            if (model.ParentID == model.ID)
                            {
                                ModelState.AddModelError("", "گروه والد معتبر انتخاب کنید.");
                                canSave = false;
                            }
                            else {
                                if (service.CheckIfUrlExists(model.ID, model.Type, model.Url))
                                {
                                    //آدرس صفحه تکراری استی
                                    ModelState.AddModelError("", "آدرس URL تکراری است.");
                                    canSave = false;
                                }
                                else {
                                    entityID = model.ID;
                                    PostGroup entity = model.ToEntity();
                                   if(!service.Edit(entity))
                                        canSave = false;
                                }
                            }

                        }
                        if (canSave)
                        { 
                            TempData["Notify"] = "'success', 'top left', '', 'با موفقیت ذخیره شد'";
                            //بسته به نوع دکمه کلیک شده، ریدایرکت میکنه
                            if (postType == "save")
                                return RedirectToAction("index", controllerName+"group" );
                            else if (postType == "save-continue")
                                return RedirectToAction("edit", controllerName+"group", new { @id = entityID });
                            else
                                return RedirectToAction("index", controllerName + "group");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "خطا در اطلاعات وارد شده");
                    }



                     
                    var groups = service.getTreeGroups(null, (int)model.LanguageID, model.Type,   "— ", model.ID);
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
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ServiceLayer.PostGroupService service = new ServiceLayer.PostGroupService())
            {
                if (service.DeletePostGroup(id))
                    return Json("success", JsonRequestBehavior.AllowGet);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }








        [HttpPost]
        public ActionResult DoJsTreeOperation(JsTreeOperationData data)
        {
            switch (data.Operation)
            {
                case JsTreeOperation.CopyNode:
                case JsTreeOperation.CreateNode:
                    //todo: save data
                    var rnd = new Random(); // آي دي ركورد پس از ثبت در بانك اطلاعاتي دريافت و بازگشت داده شود
                    return Json(new { id = rnd.Next() }, JsonRequestBehavior.AllowGet);

                case JsTreeOperation.DeleteNode:
                    //todo: save data
                    return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);

                case JsTreeOperation.MoveNode:
                    //todo: save data
                    return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);

                case JsTreeOperation.RenameNode:
                    //todo: save data
                    return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);

                default:
                    throw new InvalidOperationException(string.Format("{0} is not supported.", data.Operation));
            }
        }




        //[HttpPost]
        public ActionResult GetTreeJson()
        {
            var dir = Server.MapPath("~/fonts/");

            var nodesList = new List<JsTreeNode>();

            var rootNode = new JsTreeNode
            {
                id = dir,
                text = "Root 1",
                icon = Url.Content("~/Content/images/tree_icon.png"),
                // a_attr = { href = "http://www.bing.com" }
            };
            PopulateTree(dir, rootNode);
            nodesList.Add(rootNode);




            nodesList.Add(new JsTreeNode
            {
                id = "test1",
                text = "Root 2",
                icon = Url.Content("~/Content/images/tree_icon.png"),
                //a_attr = { href = "http://www.bing.com" }
            });

            return Json(nodesList, JsonRequestBehavior.AllowGet);
        }

        public void PopulateTree(string dir, JsTreeNode node)
        {
            var directory = new DirectoryInfo(dir);
            foreach (var directoryInfo in directory.GetDirectories())
            {
                var treeNode = new JsTreeNode
                {
                    id = directoryInfo.FullName,
                    text = directoryInfo.Name,
                    icon = Url.Content("~/Content/images/nuclear.png"),
                    // a_attr = { href = "http://www.bing.com" }
                };
                PopulateTree(directoryInfo.FullName, treeNode);
                node.children.Add(treeNode);
            }

            foreach (var fileInfo in directory.GetFiles("*.*"))
            {
                node.children.Add(new JsTreeNode
                {
                    id = fileInfo.FullName,
                    text = fileInfo.Name,
                    icon = Url.Content("~/Content/images/bookmark_book_open.png"),
                    // a_attr = { href = "http://www.bing.com" }
                });
            }
        }

        //[HttpPost]
        public ActionResult GetTreeGroupJson(byte type)
        { 
            var nodesList = new List<JsTreeNode>();


            DataLayer.CMSDataEntities db = new CMSDataEntities();
            foreach (var item in db.PostGroups.Where(a => a.Type== type && a.ParentID == null))
            {
                var treeNode = new JsTreeNode
                {
                    id = item.ID.ToString(),
                    text = item.Title,
                    icon = Url.Content("~/areas/admin/Content/images/bullet_black.png"),
                };
                GetTree(treeNode , type);
                nodesList.Add(treeNode);
            }


            return Json(nodesList, JsonRequestBehavior.AllowGet);
        }
        public void GetTree(JsTreeNode node, byte type)
        {
            DataLayer.CMSDataEntities db = new CMSDataEntities();

            foreach (var item in db.PostGroups.Where(a => a.Type == type && a.ParentID.ToString() == node.id))
            {
                var treeNode = new JsTreeNode
                {
                    id = item.ID.ToString(),
                    text = item.Title,
                    icon = Url.Content("~/areas/admin/Content/images/bullet_white.png"),
                };
                GetTree(treeNode, type);
                node.children.Add(treeNode);
            }

        }







    }
}