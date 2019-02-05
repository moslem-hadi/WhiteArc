using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="ایمیل اجباری است")]
        [EmailAddress(ErrorMessage ="آدرس ایمیل صحیح وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "نام کاربری اجباری است")]
        [RegularExpression(@"[a-zA-Z0-9_-]+", ErrorMessage = "نام کاربری باید فقط کاراکتر و عدد انگلیسی باشد")]
        [Display(Name = "نام کاربری")]
        [MinLength(3,ErrorMessage ="نام کاربری باید حداقل 3 حرف باشد")]
        [MaxLength(25,ErrorMessage = "نام کاربری باید حداکثر 25 حرف باشد")]
        [StringLength(25)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رمز ورود اجباری است")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} کاراکتر باشد", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز ورود")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "تکرار رمز ورود")]
        //[Compare("Password", ErrorMessage = "رمز ورود و تکرار آن یکسان نیستند")]
        //public string ConfirmPassword { get; set; }
    }



    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "نام کاربری اجباری است")]
        [RegularExpression(@"[a-zA-Z0-9_-]+", ErrorMessage = "نام کاربری باید فقط کاراکتر و عدد انگلیسی باشد")]
        [Display(Name = "نام کاربری")]
        [MinLength(3, ErrorMessage = "نام کاربری باید حداقل 3 حرف باشد")]
        [MaxLength(25, ErrorMessage = "نام کاربری باید حداکثر 25 حرف باشد")]
        [StringLength(25)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز ورود")]
        public string Password { get; set; }

        [Display(Name = "ذخیره ورود")]
        public bool RememberMe { get; set; }
    }


    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} حرف باشد.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز")]
        [Compare("Password", ErrorMessage = "رمز ورود و تکرار آن یکسان نیست.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }
}
