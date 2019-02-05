using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    [MetadataType(typeof(DataLayer.Entities.ProductMetaData))] //خوندن متادیتا ها
    public class ProductViewModel
    {

        public ProductViewModel()
        {
            Locales = new List<ProductLocalizedModel>();
         //   SelectedGroupIDs = new List<int>();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Pic { get; set; }
        public string FullDescription { get; set; }
        public string SeoTitle { get; set; }
        public bool IsActive { get; set; }
        public string SeoDescription { get; set; }
        public int ViewCount { get; set; }
        public int Priority { get; set; }
        public int CategoryID { get; set; }
        public bool IsDeleted { get; set; }
        public List<ProductLocalizedModel> Locales { get; set; }
        /// <summary>
        /// دسته بندی های انتخاب شده
        /// </summary>
       // public IEnumerable<int> SelectedGroupIDs { get; set; }

    }


    /// <summary>
    /// مواردی که در هر زبان تکرار می شود
    /// </summary>

    [MetadataType(typeof(DataLayer.Entities.ProductLocalizedModelMetaData))] //خوندن متادیتا ها
    public class ProductLocalizedModel
    {
        public ProductLocalizedModel(int lanqid)
        {
            LanguageId = lanqid;
        }
        public ProductLocalizedModel()
        {
        }
        public int LanguageId { get; set; }

        [AllowHtml] 
        public string Title { get; set; }

        [AllowHtml] 
        public string FullDescription { get; set; }
         

        [AllowHtml] 
        public string SeoDescription { get; set; }

        [AllowHtml] 
        public string SeoTitle { get; set; }
    }


}
