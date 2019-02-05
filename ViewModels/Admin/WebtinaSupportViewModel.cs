using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ViewModels
{
  public  class WebtinaSupportViewModel
    {
        public int ID { get; set; }
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string Title { get; set; }


        [AllowHtml]
        public string Text { get; set; }
        public string Priority { get; set; }
        public string Part { get; set; }
        public int TicketID { get; set; }
    }
}
