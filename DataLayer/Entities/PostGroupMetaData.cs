using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace DataLayer.Entities
{
    public class PostGroupMetaData
    {

        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ID { get; set; }


        [DisplayName("گروه والد")]
        [Display(Name = "گروه والد")]
        public Nullable<int> ParentID { get; set; }



        [Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("عنوان گروه")]
        [Display(Name = "عنوان گروه")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string Title { get; set; }



        [AllowHtml]
        [DisplayName("متن گروه")]
        [Display(Name = "متن گروه")]
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


         
        [DisplayName("اولویت")]         
        [Range(0, int.MaxValue, ErrorMessage = "یک عدد صحیح وارد کنید")]
        public int Priority { get; set; }

        [DisplayName("زبان")]
        public Nullable<int> LanguageID { get; set; }
    }
}
