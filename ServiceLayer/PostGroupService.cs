using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.DynamicLinq;

using DataLayer;
using System.Data.Entity;
using DomainClasses;
using ViewModels;

namespace ServiceLayer
{
    public class PostGroupService : IDisposable
    {


        private DataLayer.CMSDataEntities db = null;


        public PostGroupService()
        {
            db = new DataLayer.CMSDataEntities();
        }
        
        public DataSourceResult getPostGroupList(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            return db.PostGroups.Where(a=> !a.IsDeleted).OrderByDescending(a => a.ID).ToDataSourceResult(take, skip, sort, filter);


        }

        public PostGroup Find(int? entityID)
        {
            PostGroup entity = db.PostGroups.Find(entityID);

            return entity;
        }

        public int Add(DataLayer.PostGroup entity )
        {
            try
            {
                db.PostGroups.Add(entity);

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
        public bool Edit(DataLayer.PostGroup entity, bool autoSave = true)
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

        public void DeleteChild(int entityID)
        {
            PostGroup entity = db.PostGroups.Find(entityID);
            if (entity == null) return;
            entity.IsDeleted = true;

            foreach (var item in db.ProductGroups.Where(a => a.ParentID == entityID))
            {
                DeleteChild(item.ID);
            }
        }

        public bool DeletePostGroup(int entityID)
        {

            PostGroup entity = db.PostGroups.Find(entityID);
            if (entity == null) return false;
            entity.IsDeleted = true;
            DeleteChild(entityID);

            return (db.SaveChanges() > 0);
        }


        public Dictionary<string ,  string> getMainGeoups(byte type, int langID)
        {

            var groups = db.PostGroups.Where(a => !a.IsDeleted && a.ParentID == null && a.Type == type && a.LanguageID == langID).OrderBy(a => a.Priority)
                .Select(a => new { a.Title, a.Url }).ToDictionary(a=> a.Title,a=>a.Url);
            return groups;

        }
        public IEnumerable<CategoryNode> getTreeGroups(byte type, int langID)
        {

            var groups = db.PostGroups.Where(a=> !a.IsDeleted && a.Type==type && a.LanguageID==langID)
                .Select(a => new Category { Id = a.ID, ParentCategoryId = a.ParentID, SortOrder = a.Priority, Text = a.Title });

            var categories = new List<Category>(groups);

            var categoryTree = CategoryTree.Create(categories, o => o.ParentCategoryId == null);

            return categoryTree.ToList<CategoryNode>();

        }

        /// <summary>
        /// گرفتن لیست گروه ها به صورت درختی برای دراپ داون
        /// </summary>
        public List<DropDownViewModel> getTreeGroups(int? parentID, int langID, int typeID, string level = "", int excludedID = 0)
        {

            List<DropDownViewModel> list = new List<DropDownViewModel>();

            foreach (var item in db.PostGroups.Where(a => a.ParentID == parentID && a.LanguageID == langID && a.Type==typeID
            && !a.IsDeleted && a.ID != excludedID))
            {
                list.Add(new DropDownViewModel { ID = item.ID, Text = level + item.Title });
                foreach (var child in getTreeGroups(item.ID, langID, typeID,level, excludedID))
                {
                    list.Add(new DropDownViewModel { ID = child.ID, Text = level + child.Text });

                }
            }
            return list;
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
        ~PostGroupService()
        {
            Dispose(false);
        }

        public bool CheckIfUrlExists(int entityID,byte type, string url)
        {
            return db.PostGroups.Any(a => a.Url == url && a.Type== type && a.ID != entityID && !a.IsDeleted);
        }
    }
}
