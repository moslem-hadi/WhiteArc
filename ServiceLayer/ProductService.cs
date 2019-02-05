using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.DynamicLinq;

using DataLayer;
using System.Data.Entity;
using ViewModels;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ProductService : IDisposable
    {


        private DataLayer.CMSDataEntities db = null;


        public ProductService()
        {
            db = new DataLayer.CMSDataEntities();
        }



        /// <summary>
        /// گرفتن لیست گروه ها به صورت درختی برای دراپ داون
        /// </summary>
        public IEnumerable<DropDownViewModel> getTreeGroups(int? parentID, string level = "")
        {

            IList<DropDownViewModel> list = new List<DropDownViewModel>();

            foreach (var item in db.ProductGroups.Where(a => a.ParentID == parentID))
            {
                list.Add(new DropDownViewModel { ID = item.ID, Text = level + item.Title });
                foreach (var child in getTreeGroups(item.ID, level))
                {
                    list.Add(new DropDownViewModel { ID = child.ID, Text = level + child.Text });

                }
            }
            return list;
        }

        public DataSourceResult getProductList(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            return db.Products.Where(a => !(bool)a.IsDeleted)
                .Select(c => new { c.ID, c.Title, c.Url, c.ViewCount ,c.Pic}).OrderByDescending(a => a.ID).ToDataSourceResult(take, skip, sort, filter);


        }

        public Product Find(int? entityID)
        {
            Product Product = db.Products.Find(entityID);

            return Product;
        }
        public async Task<Product> Find(string Url)
        {
            var tmp=await db.Products.FirstOrDefaultAsync(a=> a.Url==Url);

            return tmp;
        }
        public getProductByLang_Result getProductByLang(string url , int lang)
        {

            return db.getProductByLang(url, lang).FirstOrDefault();
        }

        public int Add(DataLayer.Product entity, bool autoSave = true)
        {
            try
            {
                db.Products.Add(entity);
                if (Convert.ToBoolean(db.SaveChanges()))
                    return entity.ID;
                else
                    return -1;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public bool Edit(DataLayer.Product entity, bool autoSave = true)
        {
            try
            {
                db.Entry(entity).State = EntityState.Modified;


                return Convert.ToBoolean(db.SaveChanges());

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteProduct(int pageID)
        {

            Product entity = db.Products.Find(pageID);
            if (entity == null) return false;
            entity.IsDeleted = true;
            db.Entry(entity).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }
         


        /// <summary>
        /// گرفتن لیست محصولات یک گروه بر اساس زبان
        /// </summary> 
        public IEnumerable<ViewModels.ProductListUiViewModel> getGroupProducts(int langID, int groupID, 
            int pageNumber, int pageSize, out int totalCount)
        {
            var resultsToSkip = pageNumber * pageSize;
            List<ViewModels.ProductListUiViewModel> list = new List<ProductListUiViewModel>();
            foreach (var item in db.getGroupProductList(langID, groupID, pageSize, resultsToSkip))
            {
                list.Add(new ProductListUiViewModel(item.ID, item.Title,  item.Url, item.Pic, item.GroupTitle,item.GroupUrl));
            }
            totalCount = db.Products.Include(a=>a.Product_Group_Mapping).Where(a=> !a.IsDeleted   && a.Product_Group_Mapping.Where(c=>c.ProductGroupID==groupID).Count()!=0).Count();
            return list;
        }
        public void AddViewCount(int entityID)
        {
            Product entity = db.Products.Find(entityID);
            if (entity == null) return;
            entity.ViewCount = entity.ViewCount + 1;

            db.SaveChanges();
        }





        #region UI

        public List<ViewModels.ProductListUiViewModel> getLatestProductsByLangID(int langID, int take , int skip)
        {
            List<ViewModels.ProductListUiViewModel> list = new List<ProductListUiViewModel>();
            foreach (var item in db.getLatestProductByLang(langID,take, skip))
            {
                list.Add(new ProductListUiViewModel(item.ID, item.Title, item.Url, item.Pic,item.GroupTitle,item.GroupUrl));
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
        ~ProductService()
        {
            Dispose(false);
        }
    }
}
