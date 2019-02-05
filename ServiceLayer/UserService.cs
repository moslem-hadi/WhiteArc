using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataLayer;
using Kendo.DynamicLinq;

namespace ServiceLayer
{
   public  class UserService:IDisposable
    {

        private DataLayer.CMSDataEntities db = null;
        public UserService()
        {
            db = new DataLayer.CMSDataEntities();
        }



        public UsersData Find(string entityID)
        {
            UsersData entity = db.UsersDatas.Find(entityID);

            return entity;
        }
        public DataSourceResult getUserList(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            return db.UsersDatas.OrderByDescending(a => a.Id).Select(a=> new
            {
                a.Id,a.FullName,a.UserName,a.LastLoginDate,a.IsBanned
            }).ToDataSourceResult(take, skip, sort, filter);


        }

         
        public int GetUserMoney(string userID)
        {
            var user = db.UsersDatas.FirstOrDefault(a => a.Id == userID);
            if (user == null)
                throw new Exception("USER NOT FOUND!!!");

            return user.Money;

        }
        

        public DomainClasses.AccountType GetUserAccountType(string UserID)
        {
            var user = db.UsersDatas.Single(a => a.Id == UserID);

            return (DomainClasses.AccountType)(DateTime.Compare(user.AccountExpireDateEn ?? DateTime.Now.AddDays(-1), DateTime.Now) >= 0 ? user.AccountType : (byte)DomainClasses.AccountType.Free);
        }

        public ViewModels.IdentityStoreViewModel  GetIdentityInfo(string UserID)
        {
            var user = db.UsersDatas.Single(a => a.Id == UserID);
            return new ViewModels.IdentityStoreViewModel { AccountType = user.AccountType, FullName = user.FullName };

        }



        public bool DeleteUser(string userID)
        {

            UsersData item = db.UsersDatas.Find(userID);
            if (item == null) return false;

            db.UsersDatas.Remove(item);

            if (db.SaveChanges() > 0)
                return true;
            return false;
        }

        public bool Edit(DataLayer.UsersData entity,out string err)
        {
            err = "";
            try
            {
                db.Entry(entity).State = EntityState.Modified;

                
                return Convert.ToBoolean(db.SaveChanges());

            }
            catch (Exception ex)
            {
                err = ex.Message;
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
        ~UserService()
        {
            Dispose(false);
        }
    }
}
