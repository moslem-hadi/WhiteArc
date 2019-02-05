using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLayer.Entities
{
    public class ProjectCategoryMetaData
    {

        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ID { get; set; }


        [DisplayName("گروه والد")]
        [Display(Name = "گروه والد")]
        public Nullable<int> ParentID { get; set; }



        [Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("عنوان")]
        [Display(Name = "عنوان")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string Title { get; set; }


        [AllowHtml]
        [DisplayName("عنوان کامل")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string FullTitle { get; set; }

        [AllowHtml]
        [DisplayName("متن")]
        [Display(Name = "متن")]
        [StringLength(2000, ErrorMessage = "حداکثر 2000 کاراکتر")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }





        [DisplayName("آدرس در Url")]
        [StringLength(150, ErrorMessage = "حداکثر 150 کاراکتر")]
        public string Url { get; set; }




        //[Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("عنوان سئو")]
        [Display(Name = "عنوان سئو")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string SeoTitle { get; set; }




        //[Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("توضیحات سئو")]
        [Display(Name = "توضیحات سئو")]
        [StringLength(150, ErrorMessage = "حداکثر 150 کاراکتر")]
        public string SeoDescription { get; set; }

        [AllowHtml]
        [DisplayName("کلمات کلیدی")]
        [Display(Name = "کلمات کلیدی")]
        [StringLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        public string SeoKeywords { get; set; }


        [DisplayName("اولویت")]
        [Range(0, int.MaxValue, ErrorMessage = "یک عدد صحیح وارد کنید")]
        public int Priority { get; set; }




        [DisplayName("نمایش داده شود")]
        public bool Show { get; set; }


        [DisplayName("تصویر")]
        public bool Pic { get; set; }
    }
}
