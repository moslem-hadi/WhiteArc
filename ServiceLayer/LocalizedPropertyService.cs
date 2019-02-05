using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
  public  class LocalizedPropertyService : IDisposable
    {


        private DataLayer.CMSDataEntities db = null;


        public LocalizedPropertyService()
        {
            db = new DataLayer.CMSDataEntities();
        }


        /// <summary>
        /// گرفتن لیست پراپرتی های زبان یک انتیتی از جدول
        /// </summary>
        /// <param name="localeKeyGroup">اسم جدول</param>
        /// <param name="entityId">کد انتیتی</param>
        /// <returns>لیست از LocalizedProperty</returns>
        public List<LocalizedProperty> getEntityProperties(string localeKeyGroup, int entityId)
        {
            return db.LocalizedProperties.Where(a => a.LocaleKeyGroup == localeKeyGroup && a.EntityID == entityId).ToList();
        }





        public void InsertLocalizedData(int entityId, int languageId, string localeKeyGroup, string localeKey, string localeValue)
        {

            var prop = Exist(languageId, entityId, localeKeyGroup, localeKey);
             

            if (prop != null)
            {
                if (string.IsNullOrEmpty( localeValue ))
                {
                    // delete
                    Delete(prop.ID);
                }
                else
                {
                    // update
                    if (prop.LocaleValue != localeValue)
                    {
                        prop.LocaleValue = localeValue;
                        Edit(prop);
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(localeValue))
                {
                    // insert
                    prop = new LocalizedProperty
                    {
                        EntityID = entityId,
                        LanguageID = languageId,
                        LocaleKey = localeKey,
                        LocaleKeyGroup = localeKeyGroup,
                        LocaleValue = localeValue
                    };
                    Add(prop);
                }
            }

        }

        /// <summary>
        /// چک میکنه یه لوکیل در دیتابیس هست یا نه
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="entityID"></param>
        /// <param name="localeKeyGroup"></param>
        /// <param name="localeKey"></param>
        /// <returns></returns>
        public LocalizedProperty Exist(int languageId, int entityID, string localeKeyGroup, string localeKey)
        {
            var query = from lp in db.LocalizedProperties 
                        where
                            lp.EntityID == entityID &&
                            lp.LocaleKey == localeKey &&
                            lp.LocaleKeyGroup == localeKeyGroup &&
                            lp.LanguageID == languageId
                        select lp;

            return query.FirstOrDefault();
        }
         



        public bool Edit(DataLayer.LocalizedProperty entity )
        {
            try
            {
                db.Entry(entity).State = EntityState.Modified;



                    return Convert.ToBoolean(db.SaveChanges());
            }
            catch
            {
                return false;
            }
        }

        public bool Delete (int entityID)
        {

            LocalizedProperty entity = db.LocalizedProperties.Find(entityID);
            if (entity == null) return false;
            db.LocalizedProperties.Remove(entity);
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }



        public LocalizedProperty Find(int? entityID)
        {
            LocalizedProperty LocalizedProperty = db.LocalizedProperties.Find(entityID);

            return LocalizedProperty;
        }

        public bool Add(DataLayer.LocalizedProperty entity, bool autoSave = true)
        {
            try
            {
                db.LocalizedProperties.Add(entity);
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
        ~LocalizedPropertyService()
        {
            Dispose(false);
        }
    }
}
