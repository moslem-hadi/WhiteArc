using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
namespace ServiceLayer
{
    public  class PostGroupMappingService : IDisposable
    {


        private DataLayer.CMSDataEntities db = null;


        public PostGroupMappingService()
        {
            db = new DataLayer.CMSDataEntities();
        }


        public IEnumerable<int> GetArticleSelectedGroupIds(int articleID)
        {
            return db.PostGroupMappings.Where(a => a.PostID == articleID).Select(a => a.PostGroupID).ToList<int>();
        }

        public void InsertGroupMapping(int entityID, IEnumerable<int> newlist)
        {
            IEnumerable<int> oldlist = GetArticleSelectedGroupIds(entityID);

            //به ازای جدیدهایی که توی قدیمی نیستند اینست کنه
            foreach (var item in newlist.Where(a=> !oldlist.Contains(a)))
            {

                PostGroupMapping prop = new PostGroupMapping
                {
                    PostID = entityID,
                    PostGroupID = item
                };
                Add(prop);
            }

            // وبه ازای قدیمی هایی که توی جدیده نیستند، حذف کنه
            foreach (var item in oldlist.Where(a => !newlist.Contains(a)))
            { 
                Delete(item, entityID);
            }

        } 

         
        public PostGroupMapping Exist( int entityID, int groupID)
        {
            var query = from lp in db.PostGroupMappings
                        where
                            lp.PostID == entityID &&
                            lp.PostGroupID == groupID
                        select lp;

            return query.FirstOrDefault();
        }






        public bool Edit(DataLayer.PostGroupMapping entity)
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

        public bool Delete(int entityID)
        {

            DataLayer.PostGroupMapping entity = db.PostGroupMappings.Find(entityID);
            if (entity == null) return false;
            db.PostGroupMappings.Remove(entity);
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }
        public bool Delete(int groupID, int postID)
        {

            DataLayer.PostGroupMapping entity = db.PostGroupMappings.FirstOrDefault(a=>a.PostGroupID==groupID && a.PostID==postID);
            if (entity == null) return false;
            db.PostGroupMappings.Remove(entity);
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }




        public DataLayer.PostGroupMapping Find(int? entityID)
        {
            DataLayer.PostGroupMapping PostGroupMapping = db.PostGroupMappings.Find(entityID);

            return PostGroupMapping;
        }

        public bool Add(DataLayer.PostGroupMapping entity )
        {
            try
            {
                db.PostGroupMappings.Add(entity);
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
        ~PostGroupMappingService()
        {
            Dispose(false);
        }
    }
}
