using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;



namespace DataLayer.Entities
{
    public class PageContentMetaData
    {

        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int? ID { get; set; }



        [Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("آدرس در URL")]
        [Display(Name = "آدرس در URL")]
        [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        public string Url { get; set; }




        [Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("عنوان صفحه")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        [Display(Name = "عنوان صفحه")]
        public string Title { get; set; }



        [AllowHtml]
        [DisplayName("متن")]
        [Display(Name = "متن")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }



        [ScaffoldColumn(false)]
        [Bindable(false)]
        [DefaultValue(0)]
        public Nullable<int> ViewCount { get; set; }



        [DisplayName("قابل مشاهده")]
        [Display(Name = "قابل مشاهده")]
        [DefaultValue(false)]
        public Nullable<bool> OnlyUsersCanSee { get; set; }




        [DisplayName("وضعیت موتور جستجو")]
        [Display(Name = "وضعیت موتور جستجو")]
        [DefaultValue("INDEX , FOLLOW")]
        public string RobotAccess { get; set; }




        [DisplayName("کلمات کلیدی")]
        [StringLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        [Display(Name = "کلمات کلیدی")]
        public string KeyWords { get; set; }




        [DisplayName("توضیحات سئو")]
        [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        [Display(Name = "توضیحات سئو")]
        public string Description { get; set; }




        [DisplayName("عنوان سئو صفحه")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        [Display(Name = "عنوان سئو صفحه")]
        public string SeoTitle { get; set; }



        [ScaffoldColumn(false)]
        [Bindable(false)]
        [DefaultValue(true)]
        public Nullable<bool> Editable { get; set; }
    }



    public class PageLocalizedModelMetaData
    {
        public int LanguageId { get; set; }

        [AllowHtml]
        [DisplayName("عنوان صفحه")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string Title { get; set; }

        [AllowHtml]
        [DisplayName("متن")]
        public string Text { get; set; }

        [AllowHtml]
        [DisplayName("کلمات کلیدی")]
        [StringLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        public string KeyWords { get; set; }

        [AllowHtml]
        [DisplayName("توضیحات سئو")]
        [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        public string Description { get; set; }

        [AllowHtml]
        [DisplayName("عنوان سئو صفحه")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string SeoTitle { get; set; }
    }




}
