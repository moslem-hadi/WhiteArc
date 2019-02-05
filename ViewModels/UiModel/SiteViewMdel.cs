using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    /// <summary>
    /// برای ذخیره اطلاعات صفحه اول
    /// </summary>
   public class HomePageViewModel
    {
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SiteName { get; set; }
    }

    /// <summary>
    /// لیست دانلود ها در صفحه اول
    /// </summary>
    public class HomeDownloadsViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Pic { get; set; }
        public string Size { get; set; }
        public string SubTitle { get; set; }
    }
    public class AjaxStat
    {
        public bool Stat { get; set; }
        public string Msg { get; set; }
    }
    public class ContactFormViewModel
    {
        public string FullName { get; set; }
        public string Tell { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }

    }
    /// <summary>
    /// دسته بندی گروه ها
    /// </summary>
    public class DownloadGroupViewModel
    {
        public string Title { get; set; }
        public List<HomeDownloadsViewModel> Dowloads { get; set; }
    }

    /// <summary>
    /// لیست مقاله ها ، خبر ها و ...در صفحه اول
    /// </summary>
    public class HomePagePostViewModel
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string RegDate { get; set; }
        public string Url { get; set; }
        public string Pic { get; set; }
        public string Brief { get; set; } 
    }




    /// <summary>
    /// لیست گروه ها در صفحه اول
    /// </summary>
    public class HomePageGroups
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Pic { get; set; }
        public List<HomePageSubGroups> SubGroups { get; set; }
    }

    public class HomePageSubGroups
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class LoginStatus
    {
        public LoginStatus(bool success, string status="خطا", string url="")
        {
            Success = success;
            Status = status;
            Url = url;
        }

        public bool Success { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
    }


    public class ContactUsForm
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }


    public class UserInfoViewModel
    {
        public UserInfoViewModel(string name, string logo, string creditDate, string openTicketCount, string notificationCount)
        {
            Name = name;
            Logo = logo;
            CreditDate = creditDate;
            OpenTicketCount = openTicketCount;
            NotificationCount = notificationCount;
        }

        public string Name { get; set; }
        public string Logo { get; set; }
        public string CreditDate { get; set; }
        public string OpenTicketCount { get; set; }
        public string NotificationCount { get; set; }
    }
    public class PostGroupPageViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
    }


}
