using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLayer.Entities
{
    public class DownloadFileMetaData
    {

        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ID { get; set; }
 

        [Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("عنوان ")]
        [Display(Name = "عنوان ")]
        [StringLength(50, ErrorMessage = "حداکثر 50 کاراکتر")]
        public string Title { get; set; }


        [DisplayName("زیرعنوان")]
        [Display(Name = "زیرعنوان")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string SubTitle { get; set; }


        [DisplayName("دسته بندی")]
        [Display(Name = "دسته بندی")]
        [StringLength(50, ErrorMessage = "حداکثر 50 کاراکتر")]
        public string GroupName { get; set; }


         

        [ScaffoldColumn(false)]
        [Bindable(false)]
        [DefaultValue(0)]
        public int DownloadCount { get; set; }


        [DisplayName("اختصاص به کاربر")]
        [Display(Name = "اختصاص به کاربر")]
        [DefaultValue(false)]
        public int IsPrivate { get; set; }


        [DisplayName("اولویت")]
        [Display(Name = "اولویت")]
        [DefaultValue(0)]
        public int Priority { get; set; }


        [DisplayName("سایز")]
        [Display(Name = "سایز")]
        public string Size { get; set; }

        [DisplayName("تصویر ")]
        [StringLength(500, ErrorMessage = "حداکثر 500 کاراکتر")]
        public string PicUrl { get; set; }


        [Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("لینک فایل")]
        [StringLength(500, ErrorMessage = "حداکثر 500 کاراکتر")]
        public string FileUrl { get; set; }

    }
}

 