using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.DynamicLinq;

using DataLayer;
using System.Data.Entity; 
using ViewModels;

namespace ServiceLayer
{
    public class PageContentService : IDisposable
    {


        private DataLayer.CMSDataEntities db = null;


        public PageContentService()
        {
            db = new DataLayer.CMSDataEntities();
        }



        public DataSourceResult getPageList(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            return db.PageContents.OrderByDescending(a => a.ID).ToDataSourceResult(take, skip, sort, filter);


        }

        public PageContent Find(int? entityID)
        {
            PageContent pageContent = db.PageContents.Find(entityID);

            return pageContent;
        }


        /// <summary>
        /// گرفتن یک گروه با آدرس و بر اساس زبان
        /// </summary>
        /// <returns></returns>
        public PageUiDetails FindByUrl(string url, int langID)
        {
            getPageByUrl_Result tmp = (getPageByUrl_Result)db.getPageByUrl(url, langID).FirstOrDefault();

            return tmp.ToUiModel();

        }

        public int Add(DataLayer.PageContent entity, bool autoSave = true)
        {
            try
            {
                db.PageContents.Add(entity);
                     Convert.ToBoolean(db.SaveChanges());
                return entity.ID;
            }
            catch
            {
                return -1;
            }
        }
        public bool Edit(DataLayer.PageContent entity, bool autoSave = true)
        {
            try
            {
                  db.Entry(entity).State = EntityState.Modified;
                 

                
                if (autoSave)//برای زمانی که میخواهیم تعدادی رکورد ذخیره کنیم و نمیخوایم هربار اینسرت انجام بشه و همه با هم انجام بشن
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePage(int pageID)
        {

            PageContent pageContent = db.PageContents.Find(pageID);
            if (pageContent == null) return false;
            if (!(bool)pageContent.Editable) return false;
            db.PageContents.Remove(pageContent);
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }

        public bool CheckIfUrlExists(int pageID, string url)
        {
            return db.PageContents.Any(a => a.Url == url && a.ID != pageID);
        }

        public void AddViewCount(int entityID)
        {
            PageContent entity= db.PageContents.Find(entityID);
            if (entity == null) return;
            entity.ViewCount = entity.ViewCount + 1;

            db.SaveChanges();
        }



        public void  Dispose()
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
        ~PageContentService()
        {
            Dispose(false);
        }
    }
}
