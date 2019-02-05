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
using Westwind.Globalization;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class LangController : Controller
    {
        public LangController()
        {
        }
 

        public ActionResult index()
        {
            DataLayer.CMSDataEntities db = new DataLayer.CMSDataEntities();
            var list = db.Localizations.OrderBy(a => a.ResourceId).ThenBy(a => a.LocaleId).ToList();
            return View(list);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult index(string id,string value)
        {
            DataLayer.CMSDataEntities db = new DataLayer.CMSDataEntities();

            int idval = 0;
            int.TryParse(id, out idval);
            string msg = ""; 
            value = value.Replace("\r\n", "");

           
            {

                if (string.IsNullOrEmpty(value))
                {

                    msg = "مقدار اجباری است";

                }
                else
                {
                    try
                    {
                        
                        {
                            var tmp = db.Localizations.FirstOrDefault(a => a.pk == idval);
                            if (tmp != null)
                            { 
                                tmp.Value = value;
                                db.SaveChanges();
                                msg = "انجام شد. پس از اتمام تغییرات، دکمه رفرش کردن کش را بزنید.";

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

           // DbResourceConfiguration.ClearResourceCache();
            //DbRes.ClearResources();
            var list = db.Localizations.OrderBy(a => a.ResourceId).ThenBy(a=>a.LocaleId).ToList();
            return View(list);
        }

        [HttpPost]
        public void Referesh()
        {
            DbResourceConfiguration.ClearResourceCache();
            DbRes.ClearResources();
            Response.Redirect("/admin/lang");

        }
        public JsonResult getLocalVal(int id)
        {

            DataLayer.CMSDataEntities db = new DataLayer.CMSDataEntities();
          var tmp = db.Localizations.FirstOrDefault(a => a.pk == id);
            if (tmp != null)
                return Json(tmp.Value, JsonRequestBehavior.AllowGet);
            else
                return Json("NOT FOUND !!!!!!", JsonRequestBehavior.AllowGet);

        }



    }
}