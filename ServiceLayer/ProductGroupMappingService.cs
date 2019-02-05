using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;


namespace ServiceLayer
{
    public class ProductGroupMappingService : IDisposable
    {


        private DataLayer.CMSDataEntities db = null;


        public ProductGroupMappingService()
        {
            db = new DataLayer.CMSDataEntities();
        }


        public IEnumerable<int> GetProductSelectedGroupIds(int entityID)
        {

            return db.Product_Group_Mapping.Where(a => a.ProductID == entityID).Select(a => a.ProductGroupID).ToList<int>();
        }

        public void InsertGroupMapping(int entityID, IEnumerable<int> newlist)
        {
            IEnumerable<int> oldlist = GetProductSelectedGroupIds(entityID);

            //به ازای جدیدهایی که توی قدیمی نیستند اینست کنه
            foreach (var item in newlist.Where(a => !oldlist.Contains(a)))
            {

                Product_Group_Mapping prop = new Product_Group_Mapping
                {
                    ProductID = entityID,
                    ProductGroupID = item
                };
                Add(prop);
            }

            // وبه ازای قدیمی هایی که توی جدیده نیستند، حذف کنه
            foreach (var item in oldlist.Where(a => !newlist.Contains(a)))
            {
                Delete(item, entityID);
            }

        }
         
        public Product_Group_Mapping Exist(int entityID, int groupID)
        {
            var query = from lp in db.Product_Group_Mapping
                        where
                            lp.ProductID == entityID &&
                            lp.ProductGroupID == groupID
                        select lp;

            return query.FirstOrDefault();
        }






        public bool Edit(DataLayer.Product_Group_Mapping entity)
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

            DataLayer.Product_Group_Mapping entity = db.Product_Group_Mapping.Find(entityID);
            if (entity == null) return false;
            db.Product_Group_Mapping.Remove(entity);
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }
        public bool Delete(int groupID, int entityID)
        {

            DataLayer.Product_Group_Mapping entity = db.Product_Group_Mapping.FirstOrDefault(a => a.ProductGroupID == groupID && a.ProductID == entityID);
            if (entity == null) return false;
            db.Product_Group_Mapping.Remove(entity);
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }




        public DataLayer.Product_Group_Mapping Find(int? entityID)
        {
            DataLayer.Product_Group_Mapping Product_Group_Mapping = db.Product_Group_Mapping.Find(entityID);

            return Product_Group_Mapping;
        }

        public bool Add(DataLayer.Product_Group_Mapping entity)
        {
            try
            {
                db.Product_Group_Mapping.Add(entity);
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
        ~ProductGroupMappingService()
        {
            Dispose(false);
        }
    }
}
