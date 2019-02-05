﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CMSDataEntities : DbContext
    {
        public CMSDataEntities()
            : base("name=CMSDataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ContactPM> ContactPMs { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Localization> Localizations { get; set; }
        public virtual DbSet<LocalizedProperty> LocalizedProperties { get; set; }
        public virtual DbSet<NewsLetterEmail> NewsLetterEmails { get; set; }
        public virtual DbSet<PageContent> PageContents { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<PostGroup> PostGroups { get; set; }
        public virtual DbSet<PostGroupMapping> PostGroupMappings { get; set; }
        public virtual DbSet<Product_Group_Mapping> Product_Group_Mapping { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SettingValue> SettingValues { get; set; }
        public virtual DbSet<SiteStat> SiteStats { get; set; }
        public virtual DbSet<UsersData> UsersDatas { get; set; }
        public virtual DbSet<CodeValue> CodeValues { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    
        public virtual int f_GetDeafultLanquageId()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("f_GetDeafultLanquageId");
        }
    
        public virtual ObjectResult<getMainProductGroupsList_Result> getMainProductGroupsList(Nullable<int> langID)
        {
            var langIDParameter = langID.HasValue ?
                new ObjectParameter("LangID", langID) :
                new ObjectParameter("LangID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getMainProductGroupsList_Result>("getMainProductGroupsList", langIDParameter);
        }
    
        public virtual ObjectResult<getGroupDetailsByUrl_Result> getGroupDetailsByUrl(string url, Nullable<int> langID)
        {
            var urlParameter = url != null ?
                new ObjectParameter("url", url) :
                new ObjectParameter("url", typeof(string));
    
            var langIDParameter = langID.HasValue ?
                new ObjectParameter("LangID", langID) :
                new ObjectParameter("LangID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getGroupDetailsByUrl_Result>("getGroupDetailsByUrl", urlParameter, langIDParameter);
        }
    
        public virtual ObjectResult<getSubGroupsList_Result> getSubGroupsList(Nullable<int> langID, Nullable<int> parentID)
        {
            var langIDParameter = langID.HasValue ?
                new ObjectParameter("LangID", langID) :
                new ObjectParameter("LangID", typeof(int));
    
            var parentIDParameter = parentID.HasValue ?
                new ObjectParameter("ParentID", parentID) :
                new ObjectParameter("ParentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getSubGroupsList_Result>("getSubGroupsList", langIDParameter, parentIDParameter);
        }
    
        public virtual ObjectResult<getProductListByLang_Result> getProductListByLang(Nullable<int> langID, Nullable<int> take, Nullable<int> skip)
        {
            var langIDParameter = langID.HasValue ?
                new ObjectParameter("LangID", langID) :
                new ObjectParameter("LangID", typeof(int));
    
            var takeParameter = take.HasValue ?
                new ObjectParameter("Take", take) :
                new ObjectParameter("Take", typeof(int));
    
            var skipParameter = skip.HasValue ?
                new ObjectParameter("Skip", skip) :
                new ObjectParameter("Skip", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getProductListByLang_Result>("getProductListByLang", langIDParameter, takeParameter, skipParameter);
        }
    
        public virtual ObjectResult<getPageByUrl_Result> getPageByUrl(string url, Nullable<int> langID)
        {
            var urlParameter = url != null ?
                new ObjectParameter("url", url) :
                new ObjectParameter("url", typeof(string));
    
            var langIDParameter = langID.HasValue ?
                new ObjectParameter("LangID", langID) :
                new ObjectParameter("LangID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getPageByUrl_Result>("getPageByUrl", urlParameter, langIDParameter);
        }
    
        public virtual ObjectResult<getLatestNewsByLang_Result> getLatestNewsByLang(Nullable<int> langID, Nullable<int> take, Nullable<int> skip, Nullable<int> type)
        {
            var langIDParameter = langID.HasValue ?
                new ObjectParameter("LangID", langID) :
                new ObjectParameter("LangID", typeof(int));
    
            var takeParameter = take.HasValue ?
                new ObjectParameter("take", take) :
                new ObjectParameter("take", typeof(int));
    
            var skipParameter = skip.HasValue ?
                new ObjectParameter("skip", skip) :
                new ObjectParameter("skip", typeof(int));
    
            var typeParameter = type.HasValue ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getLatestNewsByLang_Result>("getLatestNewsByLang", langIDParameter, takeParameter, skipParameter, typeParameter);
        }
    
        public virtual ObjectResult<getLatestProjectHome_Result> getLatestProjectHome(Nullable<int> count, Nullable<bool> showHidden)
        {
            var countParameter = count.HasValue ?
                new ObjectParameter("count", count) :
                new ObjectParameter("count", typeof(int));
    
            var showHiddenParameter = showHidden.HasValue ?
                new ObjectParameter("showHidden", showHidden) :
                new ObjectParameter("showHidden", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getLatestProjectHome_Result>("getLatestProjectHome", countParameter, showHiddenParameter);
        }
    
        public virtual ObjectResult<getLatestDoneProjectHome_Result> getLatestDoneProjectHome(Nullable<int> count, Nullable<bool> showHidden)
        {
            var countParameter = count.HasValue ?
                new ObjectParameter("count", count) :
                new ObjectParameter("count", typeof(int));
    
            var showHiddenParameter = showHidden.HasValue ?
                new ObjectParameter("showHidden", showHidden) :
                new ObjectParameter("showHidden", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getLatestDoneProjectHome_Result>("getLatestDoneProjectHome", countParameter, showHiddenParameter);
        }
    
        public virtual ObjectResult<getProjectOffers_Result> getProjectOffers(Nullable<int> projID, Nullable<int> take, Nullable<int> skip)
        {
            var projIDParameter = projID.HasValue ?
                new ObjectParameter("projID", projID) :
                new ObjectParameter("projID", typeof(int));
    
            var takeParameter = take.HasValue ?
                new ObjectParameter("Take", take) :
                new ObjectParameter("Take", typeof(int));
    
            var skipParameter = skip.HasValue ?
                new ObjectParameter("Skip", skip) :
                new ObjectParameter("Skip", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getProjectOffers_Result>("getProjectOffers", projIDParameter, takeParameter, skipParameter);
        }
    
        public virtual ObjectResult<getFreelancerOffers_Result> getFreelancerOffers(string userID, Nullable<int> take, Nullable<int> skip)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(string));
    
            var takeParameter = take.HasValue ?
                new ObjectParameter("take", take) :
                new ObjectParameter("take", typeof(int));
    
            var skipParameter = skip.HasValue ?
                new ObjectParameter("skip", skip) :
                new ObjectParameter("skip", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getFreelancerOffers_Result>("getFreelancerOffers", userIDParameter, takeParameter, skipParameter);
        }
    
        public virtual ObjectResult<getUserChats_Result> getUserChats(string userID, Nullable<int> take, Nullable<int> skip)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(string));
    
            var takeParameter = take.HasValue ?
                new ObjectParameter("take", take) :
                new ObjectParameter("take", typeof(int));
    
            var skipParameter = skip.HasValue ?
                new ObjectParameter("skip", skip) :
                new ObjectParameter("skip", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getUserChats_Result>("getUserChats", userIDParameter, takeParameter, skipParameter);
        }
    
        public virtual ObjectResult<getLatestProductByLang_Result> getLatestProductByLang(Nullable<int> langID, Nullable<int> take, Nullable<int> skip)
        {
            var langIDParameter = langID.HasValue ?
                new ObjectParameter("LangID", langID) :
                new ObjectParameter("LangID", typeof(int));
    
            var takeParameter = take.HasValue ?
                new ObjectParameter("take", take) :
                new ObjectParameter("take", typeof(int));
    
            var skipParameter = skip.HasValue ?
                new ObjectParameter("skip", skip) :
                new ObjectParameter("skip", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getLatestProductByLang_Result>("getLatestProductByLang", langIDParameter, takeParameter, skipParameter);
        }
    
        public virtual ObjectResult<getProductByLang_Result> getProductByLang(string url, Nullable<int> langID)
        {
            var urlParameter = url != null ?
                new ObjectParameter("url", url) :
                new ObjectParameter("url", typeof(string));
    
            var langIDParameter = langID.HasValue ?
                new ObjectParameter("langID", langID) :
                new ObjectParameter("langID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getProductByLang_Result>("getProductByLang", urlParameter, langIDParameter);
        }
    
        public virtual ObjectResult<getGroupProductList_Result> getGroupProductList(Nullable<int> langID, Nullable<int> groupID, Nullable<int> take, Nullable<int> skip)
        {
            var langIDParameter = langID.HasValue ?
                new ObjectParameter("LangID", langID) :
                new ObjectParameter("LangID", typeof(int));
    
            var groupIDParameter = groupID.HasValue ?
                new ObjectParameter("groupID", groupID) :
                new ObjectParameter("groupID", typeof(int));
    
            var takeParameter = take.HasValue ?
                new ObjectParameter("take", take) :
                new ObjectParameter("take", typeof(int));
    
            var skipParameter = skip.HasValue ?
                new ObjectParameter("skip", skip) :
                new ObjectParameter("skip", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getGroupProductList_Result>("getGroupProductList", langIDParameter, groupIDParameter, takeParameter, skipParameter);
        }
    
        public virtual ObjectResult<GetGroupsByLang_Result> GetGroupsByLang(Nullable<int> langID)
        {
            var langIDParameter = langID.HasValue ?
                new ObjectParameter("LangID", langID) :
                new ObjectParameter("LangID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetGroupsByLang_Result>("GetGroupsByLang", langIDParameter);
        }
    }
}