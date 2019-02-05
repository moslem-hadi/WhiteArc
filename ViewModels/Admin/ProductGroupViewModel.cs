using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    [MetadataType(typeof(DataLayer.Entities.ProductGroupMetaData))] //خوندن متادیتا ها
    public class ProductGroupViewModel
    {

        public ProductGroupViewModel()
        {
            Locales = new List<ProductGroupLocalizedModel>();
            AvailableLanguage = new List<LanquageViewModel>();
            AvailableParentCategories = new List<SelectListItem>();
        }
        public int ID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public int Priority { get; set; }
        public int LanguageID { get; set; }
        public bool Show { get; set; }
        public string SeoKeywords { get; set; }
        public string Pic { get; set; }
        public bool Deleted { get; set; }
        public string FullTitle { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public List<ProductGroupLocalizedModel> Locales { get; set; }

        public List<SelectListItem> AvailableParentCategories { get; set; }
        public List<ViewModels.LanquageViewModel> AvailableLanguage { get; set; }
    }




    /// <summary>
    /// مواردی که در هر زبان تکرار می شود
    /// </summary>

    [MetadataType(typeof(DataLayer.Entities.ProductGroupLocalizedModel))] //خوندن متادیتا ها
    public class ProductGroupLocalizedModel
    {
        public ProductGroupLocalizedModel(int lanqid )
        {
            LanguageId = lanqid; 
        }
        public ProductGroupLocalizedModel()
        {
        }
        public int LanguageId { get; set; } 

        [AllowHtml]
        public string Title { get; set; }

        [AllowHtml]
        public string Text { get; set; }

        [AllowHtml]
        public string SeoKeywords { get; set; }

        [AllowHtml]
        public string SeoDescription { get; set; }

        [AllowHtml]
        public string SeoTitle { get; set; }
        [AllowHtml]
        public string Url { get; set; }
        [AllowHtml]
        public string FullTitle { get; set; }
    }



}
