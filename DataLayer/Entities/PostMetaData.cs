using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLayer.Entities
{
    public class PostMetaData
    {

        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ID { get; set; }

        
        [DisplayName("آدرس در URL")]
        [Display(Name = "آدرس در URL")]
        [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        public string Url { get; set; }


        [DisplayName("زبان")]
        public Nullable<int> LanguageID { get; set; }



        [Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("عنوان ")]
        [Display(Name = "عنوان ")]
        [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        public string Title { get; set; }






        [AllowHtml]
        [DisplayName("متن ")]
        [Display(Name = "متن ")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }



        [ScaffoldColumn(false)]
        [Bindable(false)]
        [DefaultValue(0)]
        public Nullable<int> ViewCount { get; set; }





        [DisplayName("تگ ها")]
        [Display(Name = "تگ ها")]
        [StringLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        public string KeyWords { get; set; }




        [DisplayName("توضیحات سئو")]
        [Display(Name = "توضیحات سئو")]
        [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        public string SeoDescription { get; set; }




        [DisplayName("عنوان سئو ")]
        [Display(Name = "عنوان سئو ")]
        [StringLength(70, ErrorMessage = "حداکثر 70 کاراکتر")]
        public string SeoTitle { get; set; }



        [DisplayName("تصویر ")]
        [StringLength(150, ErrorMessage = "حداکثر 150 کاراکتر")]
        public string Pic { get; set; }

        [ScaffoldColumn(false)]
        public string Cover { get; set; }



        [ScaffoldColumn(false)]
        public string RegDate { get; set; }
        


        [DisplayName("وضعیت موتور جستجو")]
        [Display(Name = "وضعیت موتور جستجو")]
        [DefaultValue("INDEX , FOLLOW")]
        public string RobotAccess { get; set; }




        [DisplayName("نظر دهی")]
        [Display(Name = "نظر دهی")]
        [DefaultValue(false)]
        public Nullable<bool> IsCommentActive { get; set; }




        [DisplayName("ارسال نظر برای")]
        [Display(Name = "ارسال نظر برای")]
        [DefaultValue(false)]
        public Nullable<bool> OnlyUserCanComment { get; set; }





        [DisplayName("توضیحات مختصر")]
        [Display(Name = "توضیحات مختصر")]
        [StringLength(500, ErrorMessage = "حداکثر 500 کاراکتر")]
        public string Brief { get; set; }



        [DisplayName("وضعیت")]
        [Display(Name = "وضعیت")]
        [DefaultValue(false)]
        public Nullable<bool> IsActive { get; set; }

        [DisplayName(" منتشر شده")]
        [Display(Name = " منتشر شده")]
        [DefaultValue(false)]
        public Nullable<bool> IsPublished { get; set; }



    }
}

 