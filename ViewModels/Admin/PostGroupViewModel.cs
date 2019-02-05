using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ViewModels
{
    [MetadataType(typeof(DataLayer.Entities.PostGroupMetaData))] //خوندن متادیتا ها
    public class PostGroupViewModel
    {

        public PostGroupViewModel()
        { 

            AvailableParentCategories = new List<SelectListItem>();
            AvailableLanguage = new List<SelectListItem>();
        }
        public int ID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public byte Type{ get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public int Priority { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> LanguageID { get; set; }

         

        public List<SelectListItem> AvailableParentCategories { get; set; }
        public List<SelectListItem> AvailableLanguage { get; set; }
    }
}
