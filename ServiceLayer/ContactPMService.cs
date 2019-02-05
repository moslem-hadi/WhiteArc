using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;

using DataLayer;
using System.Data.Entity;


namespace ServiceLayer
{
  public  class ContactPMService :IDisposable
    {

        private DataLayer.CMSDataEntities db = null;


        public ContactPMService()
        {
            db = new DataLayer.CMSDataEntities();
        }


        public bool Add(DataLayer.ContactPM entity)
        {
            try
            {
                db.ContactPMs.Add(entity);
                //برای زمانی که میخواهیم تعدادی رکورد ذخیره کنیم و نمیخوایم هربار اینسرت انجام بشه و همه با هم انجام بشن
                return Convert.ToBoolean(db.SaveChanges());
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public DataSourceResult getContactPMList(int take, int skip, IEnumerable<Sort> sort, 
            Kendo.DynamicLinq.Filter filter)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            return db.ContactPMs.Select(a => new { a.ID, a.FullName,a.RegDateFa, a.Title,a.IsRead,a.Text ,a.Email,a.Tell}).OrderByDescending(a => a.ID).ToDataSourceResult(take, skip, sort, filter);


        }

        public ContactPM Find(int? entityID)
        {
            ContactPM ContactPM = db.ContactPMs.Find(entityID);

            return ContactPM;
        }
          
        public bool MarkAsRead(int ContactPMID)
        {

            ContactPM entity = db.ContactPMs.Find(ContactPMID);
            if (entity == null) return false;
            entity.IsRead = true;

            if (db.SaveChanges() > 0)
                return true;
            return false;
        }

        public bool DeleteContactPM(int ContactPMID)
        {

            ContactPM ContactPM = db.ContactPMs.Find(ContactPMID);
            if (ContactPM == null) return false;
            db.ContactPMs.Remove(ContactPM);
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public string GetNewsLetterEmails()
        {
            return string.Join(",",db.NewsLetterEmails.Select(a => a.Email).ToArray());
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
        ~ContactPMService()
        {
            Dispose(false);
        }

        public void SubscribeEmail(NewsLetterEmail model)
        {
            try
            {
                db.NewsLetterEmails.Add(model);
                db.SaveChanges();
            }
            catch { }
        }
    }
}
