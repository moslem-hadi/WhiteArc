using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.DynamicLinq;

using DataLayer;
using System.Data.Entity;



namespace ServiceLayer
{
    public class CodeValueService : IDisposable
    {

        private DataLayer.CMSDataEntities db = null;


        public CodeValueService()
        {
            db = new DataLayer.CMSDataEntities();
        }


        public List<DataLayer.CodeValue> GetCodesByGroup( string groupName)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            return db.CodeValues.Where(a=>a.GroupName== groupName.ToLower()).OrderByDescending(a => a.ID).ToList();


        }

        public string GetValue(string name)
        {
            CodeValue tmp = db.CodeValues.FirstOrDefault(a => a.Name == name);
            if (tmp != null)
                return tmp.Value;
            return "";
        }
     

        public CodeValue Find(int? entityID)
        {
            CodeValue CodeValue = db.CodeValues.Find(entityID);

            return CodeValue;
        }

        public int Add(DataLayer.CodeValue entity)
        {
            try
            {
                db.CodeValues.Add(entity);

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
        public bool Edit(DataLayer.CodeValue entity, bool autoSave = true)
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

        //public bool Delete(int CodeValueID)
        //{

        //    CodeValue entity = db.CodeValues.Find(CodeValueID);
        //    if (entity == null) return false;
        //    // db.CodeValues.Remove(CodeValue);
        //    entity.IsDeleted = true;
        //    entity.DeleteDate = DateTime.Now;

        //    if (db.SaveChanges() > 0)
        //        return true;
        //    return false;
        //}


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
        ~CodeValueService()
        {
            Dispose(false);
        }
    }
}
