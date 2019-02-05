using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ViewModels
{

    public class AdminLoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "به خاطر بسپار مرا!")]
        public bool RememberMe { get; set; }
    }

    public class AddUserViewModel
    {
        public string ID { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "نام {0} حداقل {2} کاراکتر.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }

        //[Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        //[Required]
       // [StringLength(100, ErrorMessage = "{0} باید حداقل {2} حرف باشد.", MinimumLength = 4)]
        //[DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "تکرار رمز عبور")]
        //[Compare("Password", ErrorMessage = "رمزعبور با تکرار آن مطابقت ندارد.")]
        //public string ConfirmPassword { get; set; }





    }


}
