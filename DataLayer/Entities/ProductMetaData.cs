using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLayer.Entities
{
    public class ProductMetaData
    {

        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ID { get; set; }


        [Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("عنوان")]
        [Display(Name = "عنوان")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string Title { get; set; }

         


        [DisplayName("آدرس در URL")]
        [Display(Name = "آدرس در URL")]
        [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        public string Url { get; set; }


        [DisplayName("تصویر")]
        [StringLength(700, ErrorMessage = "حداکثر 500 کاراکتر")]
        public string Pic { get; set; }

      
        [AllowHtml]
        [DisplayName("توضیح کامل")]
        [Display(Name = "توضیح کامل")] 
        [DataType(DataType.MultilineText)]
        public string FullDescription { get; set; }

         
        [DisplayName("عنوان سئو")]
        [Display(Name = "عنوان سئو")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string SeoTitle { get; set; }


        [DisplayName("توضیحات سئو")]
        [Display(Name = "توضیحات سئو")]
        [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        public string SeoDescription { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ViewCount { get; set; }

  

        [DisplayName("در حال نمایش")]
        [Display(Name = "در حال نمایش")]
        public bool IsActive { get; set; }

          

        [DisplayName("اولویت")]
        [Range(0, int.MaxValue, ErrorMessage = "یک عدد صحیح وارد کنید")]
        public int Priority { get; set; }
         
        public bool IsDeleted { get; set; }
         
    }






    public class ProductLocalizedModelMetaData
    {
        public int LanguageId { get; set; }

        [AllowHtml]
        [DisplayName("عنوان")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string Title { get; set; }

        [AllowHtml]
        [DisplayName("توضیح کامل")]
        public string FullDescription { get; set; }
 
        [AllowHtml]
        [DisplayName("توضیحات سئو")]
        [StringLength(200, ErrorMessage = "حداکثر 200 کاراکتر")]
        public string SeoDescription { get; set; }

        [AllowHtml]
        [DisplayName("عنوان سئو")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string SeoTitle { get; set; }
    }

}
