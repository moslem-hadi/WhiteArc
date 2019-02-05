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
    public class ProductGroupService : IDisposable
    {


        private DataLayer.CMSDataEntities db = null;


        public ProductGroupService()
        {
            db = new DataLayer.CMSDataEntities();
        }



        public DataSourceResult getProductGroupList(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            return db.ProductGroups.OrderByDescending(a => a.ID).ToDataSourceResult(take, skip, sort, filter);


        }

        public ProductGroup FindByUrl(string url)
        {
            return db.ProductGroups.FirstOrDefault(a => a.Url == url);
        }

        public Dictionary<string, string> GetCategories(int langCode)
        {
            return db.GetGroupsByLang(langCode).ToDictionary(x=> x.Title,x=>x.Url); ;
        }

        /// <summary>
        /// گرفتن یک گروه با آدرس و بر اساس زبان
        /// </summary>
        /// <returns></returns>
        public ProductGroupDetailUiViewModel FindByUrl(string url,int langID)
        {
            getGroupDetailsByUrl_Result tmp = (getGroupDetailsByUrl_Result)db.getGroupDetailsByUrl(url, langID).FirstOrDefault();

            return tmp.ToUiModel();

        }

        public ProductGroup Find(int? entityID)
        {
            ProductGroup entity = db.ProductGroups.Find(entityID);

            return entity;
        }

        public int Add(DataLayer.ProductGroup entity)
        {
            try
            {
                db.ProductGroups.Add(entity);

                if (Convert.ToBoolean(db.SaveChanges()))
                    return entity.ID;
                else
                    return -1;

            }
            catch(Exception ex)
            {
                return -1;
            }
        }
        public bool Edit(DataLayer.ProductGroup entity, bool autoSave = true)
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
            ProductGroup entity = db.ProductGroups.Find(entityID);
            if (entity == null) return; 
            entity.Deleted = true;
            entity.DeleteDate = DateTime.Now;

            foreach (var item in db.ProductGroups.Where(a => a.ParentID == entityID))
            {
                DeleteChild(item.ID);
            }
        }

        public  bool DeleteProductGroup(int entityID)
        {

            ProductGroup entity = db.ProductGroups.Find(entityID);
            if (entity == null) return false; 
            entity.Deleted = true;
            entity.DeleteDate = DateTime.Now;
            DeleteChild(entityID);

            return (db.SaveChanges() > 0);
        }


        public IEnumerable<CategoryNode> getTreeGroups()
        {

            var groups = db.ProductGroups.Where(a=>a.Deleted==false).OrderBy(a=>a.Priority).Select(a => new Category { Id = a.ID, ParentCategoryId = a.ParentID, SortOrder = a.Priority, Text = a.Title });

            var categories = new List<Category>(groups);

            var categoryTree = CategoryTree.Create(categories, o => o.ParentCategoryId == null);

            return categoryTree.ToList<CategoryNode>();

        }


        /// <summary>
        /// گرفتن لیست گروه ها به صورت درختی برای دراپ داون
        /// </summary>
        public IEnumerable<DropDownViewModel> getTreeGroups(int? parentID, string level = "", int excludedID=0)
        {

            IList<DropDownViewModel> list = new List<DropDownViewModel>();

            foreach (var item in db.ProductGroups.Where(a => a.ParentID == parentID && a.ID!= excludedID && !(bool)a.Deleted))
            {
                list.Add(new DropDownViewModel { ID = item.ID, Text = level + item.Title });
                foreach (var child in getTreeGroups(item.ID,  level, excludedID))
                {
                    list.Add(new DropDownViewModel { ID = child.ID, Text = level + child.Text });

                }
            }
            return list;
        }





        #region کاربر UI برای

        public IEnumerable<ViewModels.ProductGroupListUiViewModel> getProductGroupListByLangID(int langID)
        {
            List<ViewModels.ProductGroupListUiViewModel> list = new List<ProductGroupListUiViewModel>();
            foreach (var item in db.getMainProductGroupsList(langID))
            {
                list.Add(new ProductGroupListUiViewModel(item.ID, item.Title, item.Url, item.Pic));
            }
            return list;
        }
        public IEnumerable<ViewModels.ProductGroupListUiViewModel> getSubGroupsList(int langID, int parentID)
        {
            List<ViewModels.ProductGroupListUiViewModel> list = new List<ProductGroupListUiViewModel>();
            foreach (var item in db.getSubGroupsList(langID, parentID))
            {
                list.Add(new ProductGroupListUiViewModel(item.ID, item.Title, item.Url, item.Pic));
            }
            return list;
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
        ~ProductGroupService()
        {
            Dispose(false);
        }


        public bool CheckIfUrlExists(int entityID, string url)
        {
            return db.ProductGroups.Any(a => a.Url == url && a.ID != entityID && !a.Deleted);
        }

    }
}
