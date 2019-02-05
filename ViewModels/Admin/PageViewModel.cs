using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{

    [MetadataType(typeof(DataLayer.Entities.PageContentMetaData))] //خوندن متادیتا ها
    public class PageViewModel
    {
        public PageViewModel()
        {
            Locales = new List<PageLocalizedModel>();
        }
        public int ID { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Nullable<int> ViewCount { get; set; }
        public Nullable<bool> OnlyUsersCanSee { get; set; }
        public string RobotAccess { get; set; }
        public string SeoTitle { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        public Nullable<bool> Editable { get; set; }
        public List<PageLocalizedModel> Locales { get; set; }




    }


    /// <summary>
    /// مواردی که در هر زبان تکرار می شود
    /// </summary>

    [MetadataType(typeof(DataLayer.Entities.PageLocalizedModelMetaData))] //خوندن متادیتا ها
    public class PageLocalizedModel 
    {
        public PageLocalizedModel(int lanqid)
        {
            LanguageId = lanqid;
        }
        public PageLocalizedModel( )
        { 
        }
        public int LanguageId { get; set; }
         
        [AllowHtml]
        //[DisplayName("عنوان صفحه")]
        //[StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string Title { get; set; }
         
        [AllowHtml]
        //[DisplayName("متن")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
         
        [AllowHtml]
        //[DisplayName("کلمات کلیدی")]
       // [StringLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        public string KeyWords { get; set; }
         
        [AllowHtml]
        //[DisplayName("توضیحات سئو")]
        //[StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        public string Description { get; set; }
         
        [AllowHtml]
        //[DisplayName("عنوان سئو صفحه")]
        //[StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string SeoTitle { get; set; }
    }



}

 