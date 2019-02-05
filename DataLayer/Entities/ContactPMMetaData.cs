using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLayer.Entities
{
    internal   class ContactPMMetaData
    {

        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ID { get; set; }
         

        [DisplayName("نام و نام خانوادگی")]
        [Display(Name = "نام و نام خانوادگی")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string FullName { get; set; }


        [DisplayName("تلفن تماس")]
        [Display(Name = "تلفن تماس")]
        [StringLength(50, ErrorMessage = "حداکثر 50 کاراکتر")]
        public string Tell { get; set; }

        
        [DisplayName("ایمیل")]
        [Display(Name = "ایمیل")]
        [StringLength(80, ErrorMessage = "حداکثر 80 کاراکتر")]
        public string Email { get; set; }


      

        [DisplayName("کد پیگیری")]
        [Display(Name = "کد پیگیری")] 
        public string TraceCode { get; set; }

        [DisplayName("پاسخ")]
        [Display(Name = "پاسخ")] 
        public string Reply { get; set; }


        [Bindable(false)]
        public Nullable<bool> IsRead { get; set; }


        [DisplayName("متن پیام")]
        [Display(Name = "متن پیام")]
        public string Text { get; set; }




        [DisplayName("تاریخ پاسخ")]
        public string ReplyDateFa { get; set; }

        [ScaffoldColumn(false)]
        public string RegDateFa { get; set; }



        [DisplayName("عنوان پیام")]
        public string Title { get; set; }

         
    }
}

 