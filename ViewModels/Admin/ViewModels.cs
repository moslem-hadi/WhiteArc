using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class DashboardViewModel
    {
        /// <summary>
        /// بازدید امروز
        /// </summary>
        public int TodayViews { get; set; }

        /// <summary>
        /// بازدید دیروز
        /// </summary>
        public int YesterDayViews { get; set; }

        /// <summary>
        /// درصد رشد بازدید نسبت ب دیروز
        /// </summary>
        public double ViewGrowth { get; set; }

        /// <summary>
        /// تعداد تیکت بی پاسخ
        /// </summary>
        public int UnAnswerdTicketCount { get; set; }

        /// <summary>
        /// تعداد همه تیکت ها
        /// </summary>
        public int TotalTicketCount { get; set; }

        /// <summary>
        /// تعداد تیکت بسته
        /// </summary>
        public int OpenTicketCount { get; set; }

        /// <summary>
        /// تعداد کاربر
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// میزان فروش فروشگاه در 1 ماه
        /// </summary>
        public int ShopSaleAmount { get; set; }

        /// <summary>
        /// میزان فروش فروشگاه در ماه گذشته
        /// </summary>
        public int PrevMonthShopSaleAmount { get; set; }

    }




    public class StatsViewModel
    {
        public string Day { get; set; }
        public int Count { get; set; }


    }
    public class LatestContactPmViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string RegDate { get; set; }
        public string Name { get; set; }
    }


}
