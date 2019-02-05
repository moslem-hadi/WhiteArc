using DataLayer;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.Web;




namespace ServiceLayer
{
    public class SiteStatService:IDisposable
    {

        private DataLayer.CMSDataEntities db = null;


        public SiteStatService()
        {
            db = new DataLayer.CMSDataEntities();
        }

         
        public List<ViewModels.StatsViewModel> getSiteStatList(int take, int skip , out int totalcount)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            //تعداد روزها
            totalcount = 365;

            DateTime now = DateTime.Now;
            DateTime today = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59).AddDays(-1* skip);
            DateTime start = today.AddDays(-1 * take).Date;

            var data = db.SiteStats.Where(a => a.DateStamp >= start && a.DateStamp < today).OrderByDescending(a => a.ID).ToList();

            
            var days= data
                 .GroupBy(n => n.DateStamp.ToShortDateString())
                     .Select(group =>
                         new 
                         {
                             Day = group.Key,
                             Count = group.Count()
                         })
                .OrderByDescending(a => a.Day) .ToList();


            List<ViewModels.StatsViewModel> lst = new List<ViewModels.StatsViewModel>();


            int cnt = 0;
            foreach (DateTime day in EachDay(start.AddDays(1), today))
            {
                cnt = 0;
                var singleDay = days.FirstOrDefault(a => a.Day == day.ToShortDateString());
                if (singleDay != null)
                    cnt = singleDay.Count;

                lst.Add(new ViewModels.StatsViewModel() { Day = ToPersianDate(day.ToShortDateString()), Count = cnt });

            }
            lst.Reverse();
            return lst;
        }


        public void AddView(string ip, string os, string page, string referer, string agent)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            DataLayer.SiteStat tmp = new SiteStat();
            try
            {
                tmp.IpAddress = ip;
                tmp.UserOs = os;
                tmp.PageViewed = page;
                tmp.Referer = referer;
                tmp.UserAgent = agent;
                tmp.DateStamp = DateTime.Now;
                db.SiteStats.Add(tmp);
                db.SaveChanges();
            }
            catch(Exception ex) {
            }
        }
   




        public string GetViewStat(int daycount)
        {
            if (db == null)
            {
                db = new DataLayer.CMSDataEntities();
            }

            string retVal = "";
            DateTime now = DateTime.Now;
            DateTime today = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            DateTime start = today.AddDays(-1* daycount).Date;

            var data = db.SiteStats.Where(a => a.DateStamp >= start && a.DateStamp < today).OrderByDescending(a=>a.ID).ToList();
            var dt=data
                 .GroupBy(n => n.DateStamp.ToShortDateString())
                     .Select(group =>
                         new
                         {
                             Day = group.Key,
                             Count = group.Count()
                         }).ToList();

            int cnt = 0;
            foreach (DateTime day in  EachDay(start.AddDays(1), today))
            {
                cnt = 0;
                var singleDay = dt.FirstOrDefault(a=>a.Day==day.ToShortDateString());
                if(singleDay != null) 
                    cnt = singleDay.Count;
                retVal += "{ y: '" + ToPersianDate(day.ToShortDateString()).Replace("/", "-") + "', a: " + cnt + " },";

            }
            //    foreach (var item in dt)
            //{
            //    cnt = item.Count;
            //    retVal += "{ y: '" + ToPersianDate(item.Day).Replace("/", "-") + "', a: " + cnt + " },";
            //}
            if (string.IsNullOrEmpty(retVal))
                return "{y:'0',a:'0'}";
            return retVal.Remove(retVal.Length - 1);

        }

        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
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
        ~SiteStatService()
        {
            Dispose(false);
        }





        public static string ToPersianDate(object input)
        {
            DateTime datetime = DateTime.Parse(input.ToString());
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(datetime).ToString() + "/" +
                persianCalendar.GetMonth(datetime).ToString("0#") + "/" +
                persianCalendar.GetDayOfMonth(datetime).ToString("0#");
        }







    }
}
