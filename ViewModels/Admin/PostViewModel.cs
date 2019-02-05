using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ViewModels
{

    [MetadataType(typeof(DataLayer.Entities.PostMetaData))] //خوندن متادیتا ها
    public class PostViewModel
    {
        public PostViewModel()
        {
            SelectedGroupIDs = new List<int>();
        }

        public int ID { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string SeoTitle { get; set; }
        public string Text { get; set; }
        public string KeyWords { get; set; }
        public string SeoDescription { get; set; }
        public string Pic { get; set; }
        public string Cover { get; set; }
        public string RegDate { get; set; }
        public string RobotAccess { get; set; }
        public bool OnlyUserCanComment { get; set; }
        public bool IsCommentActive { get; set; }
        public int ViewCount { get; set; }
        public string Brief { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public string PublishDate { get; set; }
        public int LikeCount { get; set; }
        public Nullable<short> Type { get; set; }
        public Nullable<int> LanguageID { get; set; }

        /// <summary>
        /// دسته بندی های انتخاب شده
        /// </summary>
        public IEnumerable<int> SelectedGroupIDs { get; set; }
    }
}
