using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace DomainClasses.Entities
{
    internal class PageContentMetaData
    {

        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ID { get; set; }



        [Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("آدرس صفحه")]
        [Display(Name = "آدرس صفحه")]
        [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        public string Url { get; set; }




        [Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("عنوان صفحه")]
        [Display(Name = "عنوان صفحه")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string Title { get; set; }




        [DisplayName("متن صفحه")]
        [Display(Name = "متن صفحه")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }



        [ScaffoldColumn(false)]
        [Bindable(false)]
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
        [Display(Name = "کلمات کلیدی")]
        [StringLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        public string KeyWords { get; set; }



         
        [DisplayName("توضیحات سئو")]
        [Display(Name = "توضیحات سئو")]
        [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        public string Description { get; set; }



        
        [DisplayName("عنوان صفحه")]
        [Display(Name = "عنوان صفحه")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string SeoTitle { get; set; }



        [ScaffoldColumn(false)]
        [Bindable(false)]
        public Nullable<bool> Editable { get; set; }
    }
}



namespace DataLayer
{
    [MetadataType(typeof(DomainClasses.Entities.PageContentMetaData))]
    public partial class PageContent
    {

    }
}