using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
  public  class LanguageService : IDisposable
    {


        private DataLayer.CMSDataEntities db = null;


        public LanguageService()
        {
            db = new DataLayer.CMSDataEntities();
        }

        /// <summary>
        /// گرفتن کد زبان پیشفرض - اولین زبان با اولیت
        /// </summary>
        /// <returns>کد زبان</returns>
        public int GrtFisrtLanguageID()
        {
            return db.Languages.FirstOrDefault().ID;
        }

        /// <summary>
        /// لیست زبان ها به ترتیب اولویت برای دراپ داون
        /// </summary>
        public IList<ViewModels.DropDownViewModel> getLanguageListForDrop()
        {
            return db.Languages.OrderBy(a => a.DisplayOrder).Select(a => new ViewModels.DropDownViewModel { ID = a.ID, Text = a.Name }).ToList<ViewModels.DropDownViewModel>();
        }

        /// <summary>
        /// دریافت لیست زبان ها بصورت کامل
        /// </summary>
        public IList<DataLayer.Language> getLanguageList()
        {
            return db.Languages.OrderBy(a => a.DisplayOrder).ToList<DataLayer.Language>();
        }




        /// <summary>
        /// (دریافت لیست زبان ها بصورت کامل بجز زبان پیشفرض (اولین اولویت
        /// </summary>
        public IList<DataLayer.Language> getLanguageListNoDefault()
        {
            return db.Languages.OrderBy(a => a.DisplayOrder).Skip(1).ToList<DataLayer.Language>();
        }




        /// <summary>
        /// (دریافت لیست زبان ها برای مدل بصورت کامل بجز زبان پیشفرض (اولین اولویت
        /// </summary>
        public List<ViewModels.LanquageViewModel> getLanguageListModelNoDefault()
        {
            return db.Languages.OrderBy(a => a.DisplayOrder).Skip(1).Select( a=> new ViewModels.LanquageViewModel {FlagImageFileName=a.FlagImageFileName,ID=a.ID ,LanguageCulture=a.LanguageCulture,Name=a.Name,Rtl=a.Rtl,UniqueSeoCode=a.UniqueSeoCode}).ToList<ViewModels.LanquageViewModel>();
        }
        public Language Find(int? entityID)
        {
            Language Language = db.Languages.Find(entityID);

            return Language;
        }

        public bool Add(DataLayer.Language entity, bool autoSave = true)
        {
            try
            {
                db.Languages.Add(entity);
                    return Convert.ToBoolean(db.SaveChanges());
             
            }
            catch
            {
                return false;
            }
        }   

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
        ~LanguageService()
        {
            Dispose(false);
        }
    }
}
