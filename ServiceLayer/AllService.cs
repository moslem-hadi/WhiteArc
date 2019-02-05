using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ServiceLayer
{
  public  class AllService : IDisposable
    {


        private DataLayer.CMSDataEntities db = null;


        public AllService()
        {
            db = new DataLayer.CMSDataEntities();
        }



        public ViewModels.DashboardViewModel GetDashboardInfo()
        {
            using (DataLayer.CMSDataEntities dbData = new DataLayer.CMSDataEntities())
            {
                var openTicket = 0;

                int unreadTicket = dbData.ContactPMs.Where(a => !a.IsRead) .Count();
                int todayViews = dbData.SiteStats.Where(a => a.DateStamp >= DateTime.Today).Count();
                int totalTicketCount = dbData.ContactPMs.Count();
                int userCount = dbData.UsersDatas.Count();
                var yesterday = DateTime.Today.AddDays(-1);
                var yesterdayNow = DateTime.Now.AddDays(-1);
                int yesterdayView = dbData.SiteStats.Where(a => a.DateStamp >= yesterday && a.DateStamp < yesterdayNow).Count();
                double viewGrowth = Math.Round(((double)(yesterdayView - todayViews) / yesterdayView), 2) * -100;

                DashboardViewModel model = new DashboardViewModel();
                model.OpenTicketCount = openTicket;
                model.UnAnswerdTicketCount = unreadTicket;
                model.PrevMonthShopSaleAmount = 0;
                model.ShopSaleAmount = 0;
                model.TodayViews = todayViews;
                model.TotalTicketCount = totalTicketCount;
                model.UserCount = userCount;
                model.YesterDayViews = yesterdayView;
                model.ViewGrowth = viewGrowth;

                return model;


            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
        ~AllService()
        {
            Dispose(false);
        }
    }
}
