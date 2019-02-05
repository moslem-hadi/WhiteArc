using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.DynamicLinq;

using DataLayer;
using System.Data.Entity;



namespace ServiceLayer
{
    /// <summary>
    /// SettingValue & CodeValue
    /// </summary>
    public class SettingValueService : IDisposable
    {

        private DataLayer.CMSDataEntities db = null;


        public SettingValueService()
        {
            db = new DataLayer.CMSDataEntities();
        }


        public IList<DataLayer.SettingValue> getVisibleSettingList()
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            return db.SettingValues.OrderByDescending(a => a.ID).ToList<DataLayer.SettingValue>();


        }

        public string GetValue(string name)
        {
            SettingValue tmp = db.SettingValues.FirstOrDefault(a => a.Name == name);
            if (tmp != null)
                return tmp.Value;
            return "";
        }



       

        public SettingValue Find(int? entityID)
        {
            SettingValue SettingValue = db.SettingValues.Find(entityID);

            return SettingValue;
        }

        public int Add(DataLayer.SettingValue entity)
        {
            try
            {
                db.SettingValues.Add(entity);

                if (Convert.ToBoolean(db.SaveChanges()))
                    return entity.ID;
                else
                    return -1;
            }
            catch
            {
                return -1;
            }
        }
        public bool Edit(DataLayer.SettingValue entity, bool autoSave = true)
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

        //public bool Delete(int SettingValueID)
        //{

        //    SettingValue entity = db.SettingValues.Find(SettingValueID);
        //    if (entity == null) return false;
        //    // db.SettingValues.Remove(SettingValue);
        //    entity.IsDeleted = true;
        //    entity.DeleteDate = DateTime.Now;

        //    if (db.SaveChanges() > 0)
        //        return true;
        //    return false;
        //}








        #region CODEVALUE
        public List<DataLayer.CodeValue> GetCodesByGroup(string groupName)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            return db.CodeValues.Where(a => a.GroupName == groupName.ToLower()).OrderByDescending(a => a.ID).ToList();


        }
        public string GetCodeValue(string name)
        {
            return db.CodeValues.FirstOrDefault(a => a.Name == name).Value;

        }
        #endregion





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
        ~SettingValueService()
        {
            Dispose(false);
        }
    }
}
