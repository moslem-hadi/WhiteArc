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
    //[Authorize(Roles = "Admin")]
    public class DownloadController : Controller
    {

        public ActionResult Index()
        {
            //return View(db.DownloadFiles.ToList());

            return View();
        }
        public ActionResult getList(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            using (ServiceLayer.DownloadFileService srv = new ServiceLayer.DownloadFileService())
            {

                var fetched_data = srv.getDownloadList(take, skip, sort, filter);
                return Json(fetched_data, JsonRequestBehavior.AllowGet);

            }

        }



        // ADD And Edit
        public ActionResult Add(int? id)
        {

            using (ServiceLayer.DownloadFileService srv = new ServiceLayer.DownloadFileService())
            {
                IEnumerable<string> groupnames = srv.getMainCategories(); 
                ViewBag.Groups = new SelectList(groupnames);
                 

                if (id == null) //اگه در مود اینسرت باشد
                {
                    var entityModel = new ViewModels.DownloadFileListViewModel();// یک مدل خالی به ویو می دهیم

                    return View(entityModel);
                }
                else
                {//اگر در مود آپدیت باشد


                    DownloadFile entity = srv.Find(id);
                    if (entity == null)
                    {
                        return HttpNotFound();
                    }


                    ViewModels.DownloadFileListViewModel model = entity.ToModel();//مپ کردن مدل به ویومدل


                    return View(model);
                }
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(ViewModels.DownloadFileListViewModel entity, string postType)
        {
            using (ServiceLayer.DownloadFileService srv = new ServiceLayer.DownloadFileService())
            {
                bool canSave = true;
                int entityID = 0;
                if (postType == "delete")
                {
                    //حذف شود.
                    srv.DeleteDownloadFile(entity.ID);
                    TempData["Notify"] = "'success', 'top left', '', 'با موفقیت حذف شد'";
                    return RedirectToAction("index");
                }
                else
                {
                    if (ModelState.IsValid)
                    {

                        if (entity.ID == 0)
                        {
                            entity.DownloadCount = 0;
                                var item = entity.ToEntity();

                                entityID = srv.Add(item);
                                if (entityID == -1)
                                {
                                    ModelState.AddModelError("", "خطا در ذخیره اطلاعات.");
                                    canSave = false;
                                }
                        }
                        else
                        {
                             
                             

                                var item = entity.ToEntity();
                                entityID = item.ID; 
                                if (!srv.Edit(item))
                                {
                                    ModelState.AddModelError("", "خطا در ذخیره اطلاعات.");
                                    canSave = false;
                                } 
                        }

                        if (canSave)
                        {
                            //بسته به نوع دکمه کلیک شده، ریدایرکت میکنه
                            TempData["Notify"] = "'success', 'top left', '', 'با موفقیت ذخیره شد'";
                            if (postType == "save")
                                return RedirectToAction("index");
                            else if (postType == "save-continue")
                                return RedirectToAction("edit", "download", new { @id = entityID });
                        }
                    }

                    else
                    {

                        ModelState.AddModelError("", "خطا در اطلاعات وارد شده.");
                    }

                    IEnumerable<string> groupnames = srv.getMainCategories();
                    ViewBag.Groups = new SelectList(groupnames);
                    return View(entity);
                }
            }
        }



        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            using (ServiceLayer.DownloadFileService service = new ServiceLayer.DownloadFileService())
            {
                if (service.DeleteDownloadFile(id))
                    return Json("success", JsonRequestBehavior.AllowGet);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }





    }
}
 