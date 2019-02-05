using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.DynamicLinq;

using DataLayer;
using System.Data.Entity;
using ViewModels;

namespace ServiceLayer
{
    
    public class PostService : IDisposable
    {


        private DataLayer.CMSDataEntities db = null;


        public PostService()
        {
            db = new DataLayer.CMSDataEntities();
        }


        //public IList<ViewModels.PostListViewModel> getFeaturedPosts()
        //{
        //    if (db == null)
        //    {
        //        db = new DataLayer.CMSDataEntities();
        //    }
        //    return db.Posts.Where(a => (bool)a.IsFeatured && (bool)a.IsPublished).OrderByDescending(a => a.ID).
        //        Select( a=> new ViewModels.PostListViewModel{ ID=a.ID,Pic=a.Pic,RegFaDate=a.RegFaDate,Title=a.Title,Url= a.Url }).ToList<ViewModels.PostListViewModel>();

        //}
        //public IList<ViewModels.PostListViewModel> getLatestPosts(int langID, int take=10, int skip=0,int type=1)
        //{
        //    if (db == null)
        //    {
        //        db = new DataLayer.CMSDataEntities();
        //    }
        //    return db.getLatestNewsByLang(langID,take,skip,type).
        //        Select(a => new ViewModels.PostListViewModel { ID = a.ID, LikeCount=a.LikeCount, Pic = a.Pic, PublishDate = a.PublishDate, Brief=a.Brief, Title = a.Title, Url = a.Url }).ToList<ViewModels.PostListViewModel>();

        //}




        public IList<ViewModels.PostListViewModel> getLatestPosts(int langID, int take = 10, int skip = 0, int type = 1)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }
            return db.getLatestNewsByLang(langID, take, skip, type).
                Select(a => new ViewModels.PostListViewModel { ID = a.ID, LikeCount = a.LikeCount, Pic = a.Pic, PublishDate = a.PublishDate.ToString(),
                    Brief = a.Brief, Title = a.Title, Url = a.Url }).ToList<ViewModels.PostListViewModel>();

        }




        public DataSourceResult getPostList(byte typeID,int take, int skip, IEnumerable<Sort> sort,
            Kendo.DynamicLinq.Filter filter )
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            return db.Posts.Where(a=>a.Type== typeID && !a.IsDeleted).Select(a => new
            {
                a.ID,
                a.Title,
                a.Pic, 
                a.IsPublished,
                a.ViewCount,
                a.Url,
                a.RegDate,
                LanguageID =a.LanguageID
            }
            ).OrderByDescending(a => a.ID).ToDataSourceResult(take, skip, sort, filter);

        }

        public PostGroupPageViewModel GetGroupName(string url)
        {
            return db.PostGroups.Where(a => a.Url == url).Select(a => new PostGroupPageViewModel {Title= a.Title,SeoTitle= a.SeoTitle,SeoDescription= a.SeoDescription,Text= a.Text }).FirstOrDefault();
        }

        public IList<ViewModels.PostListViewModel> getPostList(int pageNumber, int pageSize,int langID,
            out int totalCount)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }
            totalCount = db.Posts.Where(a => (bool)a.IsPublished && a.Type == (int)DomainClasses.PostType.Post && a.LanguageID==langID).Count();
            var resultsToSkip = pageNumber * pageSize;
            var _posts = db.Posts.Where(a => (bool)a.IsPublished && a.Type == (int)DomainClasses.PostType.Post && a.LanguageID == langID).OrderByDescending(a => a.PublishDate)
                .Skip(() => resultsToSkip)
                .Take(() => pageSize)
                .Select(a => new ViewModels.PostListViewModel { ID = a.ID, Pic = a.Pic, PublishDate = a.PublishDate, Brief = a.Brief, Title = a.Title, Url = a.Url }).ToList<ViewModels.PostListViewModel>();




            return _posts;

        }




        /// <summary>
        /// گرفتن سوالات متداول.
        /// </summary>
        public List<ViewModels.HomePagePostViewModel> GetHomeFaqs( int take)
        {
            var data = db.Posts.Where(a => a.IsPublished && a.Type==(int)DomainClasses.PostType.Faq && !a.IsDeleted).OrderByDescending(a => a.PublishDate)
                .Take(take).Select(a =>
            new ViewModels.HomePagePostViewModel
            {
                Title = a.Title,
                Brief = a.Text,
                ID = a.ID.ToString()

            }).ToList();

            return data;
        }
        public List<ViewModels.HomePagePostViewModel> GetHomePagePost(int type, int take)
        {
            var data = db.Posts.Where(a => a.IsPublished && a.Type ==type &&!a.IsDeleted).OrderByDescending(a => a.PublishDate)
                .Take(take).Select(a =>
            new ViewModels.HomePagePostViewModel
            {
                Title = a.Title,
                Url = a.Url,
                Pic = a.Pic,
                RegDate = a.RegDate,
                Brief = a.Brief,
                ID = a.ID.ToString()

            }).ToList();

            return data;
        }


        //public IList<ViewModels.PostListViewModel> getPostList(int pageNumber, int pageSize,
        //    out int totalCount)
        //{
        //    if (db == null)
        //    {
        //        db = new DataLayer.CMSDataEntities();
        //    }
        //    totalCount = db.Posts.Where(a => (bool)a.IsPublished && a.Type == (int)DomainClasses.PostType.Post).Count();
        //    var resultsToSkip = pageNumber * pageSize;
        //    var _posts= db.Posts.Where(a => (bool)a.IsPublished && a.Type==(int) DomainClasses.PostType.Post).OrderByDescending(a => a.PublishDate)
        //        .Skip(() => resultsToSkip)
        //        .Take(() => pageSize)
        //        .Select(a => new ViewModels.PostListViewModel { ID = a.ID, Pic = a.Pic,Brief=a.Brief, Title = a.Title, Url = a.Url }).ToList<ViewModels.PostListViewModel>();




        //    return _posts;

        //}

        public IList<ViewModels.PostListViewModel> getArticleList(int pageNumber, int pageSize,int langID,
            out int totalCount)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }
            totalCount = db.Posts.Where(a => (bool)a.IsPublished && !(bool)a.IsDeleted && a.Type == (short)DomainClasses.PostType.Article && a.LanguageID == langID).Count();
            var resultsToSkip = pageNumber * pageSize;
            var _posts = db.Posts.Where(a => (bool)a.IsPublished && !(bool)a.IsDeleted && a.Type == (int)DomainClasses.PostType.Article && a.LanguageID == langID).OrderByDescending(a => a.PublishDate)
                .Skip(() => resultsToSkip)
                .Take(() => pageSize)
                .Select(a => new ViewModels.PostListViewModel { ID = a.ID, Pic = a.Pic, Brief = a.Brief, Title = a.Title, Url = a.Url, LikeCount = a.LikeCount }).ToList<ViewModels.PostListViewModel>();




            return _posts;

        }
        public IList<ViewModels.PostListViewModel> getGroupArticleList(string groupUrl, int pageNumber, int pageSize, int langID,
          out int totalCount)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }
            totalCount = 0;
            var group = db.PostGroups.FirstOrDefault(a => a.Url == groupUrl)?.ID;
            if (group == null)
                return null;


            totalCount = db.Posts.Where(a => (bool)a.IsPublished && !(bool)a.IsDeleted && a.Type == (short)DomainClasses.PostType.Article && a.LanguageID == langID && a.PostGroupMappings.Where(c => c.PostGroupID == group).Count() > 0).Count();
            var resultsToSkip = pageNumber * pageSize;
            var _posts = db.Posts.Where(a => (bool)a.IsPublished && !(bool)a.IsDeleted && a.Type == (int)DomainClasses.PostType.Article && a.LanguageID == langID && a.PostGroupMappings.Where(c => c.PostGroupID == group).Count() > 0).OrderByDescending(a => a.PublishDate)
                .Skip(() => resultsToSkip)
                .Take(() => pageSize)
                .Select(a => new ViewModels.PostListViewModel { ID = a.ID, Pic = a.Pic, Brief = a.Brief, Title = a.Title, Url = a.Url, LikeCount = a.LikeCount }).ToList<ViewModels.PostListViewModel>();




            return _posts;

        }




        public IList<ViewModels.PostListViewModel> searchArticles(string text, int pageNumber, int pageSize, int langID,
          out int totalCount)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }
            var query = db.Posts.Where(a => (bool)a.IsPublished && !(bool)a.IsDeleted && a.Type == (short)DomainClasses.PostType.Article && a.LanguageID == langID && a.Title.Contains(text) || a.Text.Contains(text));


            totalCount = 0; 
            totalCount = query.Count();
            var resultsToSkip = pageNumber * pageSize;
            var _posts = query.OrderByDescending(a => a.PublishDate)
                .Skip(() => resultsToSkip)
                .Take(() => pageSize)
                .Select(a => new ViewModels.PostListViewModel { ID = a.ID, Pic = a.Pic, Brief = a.Brief, Title = a.Title, Url = a.Url, LikeCount = a.LikeCount }).ToList<ViewModels.PostListViewModel>();




            return _posts;

        }

        public Post Find(int? entityID)
        {
            Post Post = db.Posts.Find(entityID);
            try
            {
                Post.ViewCount++;
                db.SaveChanges();
            }
            catch { }
            return Post;
        }

        public int Add(DataLayer.Post entity )
        {
            try
            {
                db.Posts.Add(entity);
                 Convert.ToBoolean(db.SaveChanges());
                return entity.ID;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }
        public bool Edit(DataLayer.Post entity, bool autoSave = true)
        {
            try
            {
                db.Entry(entity).State = EntityState.Modified;



                if (autoSave)//برای زمانی که میخواهیم تعدادی رکورد ذخیره کنیم و نمیخوایم هربار اینسرت انجام بشه و همه با هم انجام بشن
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePost(int PostID)
        {

            Post post = db.Posts.Find(PostID);
            if (post == null) return false;

            post.IsDeleted = true;
            post.DeleteDesc = "حذف در " + DateTime.Now.ToString();
            //db.Posts.Remove(Post);

            //db.PostGroupMappings.RemoveRange(db.PostGroupMappings.Where(a => a.PostID == PostID));
            //db.PostComments.RemoveRange(db.PostComments.Where(a => a.PostID == PostID));
            try
            {
                if (db.SaveChanges() > 0)
                    return true;
                return false;
            }catch(Exception ex)
            {
                return false; }
        }


        public void AddViewCount(int entityID)
        {
            Post entity = db.Posts.Find(entityID);
            if (entity == null) return;

            entity.ViewCount = entity.ViewCount + 1;

            db.SaveChanges();
        }

        public bool AddLikeCount(int entityID, bool type)
        {
            Post entity = db.Posts.Find(entityID);
            if (entity == null) return false;
            if(type)
            entity.LikeCount = entity.LikeCount + 1;
            else
                entity.LikeCount = entity.LikeCount -1;


            return (db.SaveChanges() > 0);
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
        ~PostService()
        {
            Dispose(false);
        }

    }
}
