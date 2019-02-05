using System;
using System.Collections.Generic;
using System.Linq;
using Persia;

namespace DNT.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Determines if a date is within a given date range
        /// Example:
        /// var monday = DateTime.Now.ThisWeekMonday();
        /// var friday = DateTime.Now.ThisWeekFriday();
        /// If (DateTime.Now.IsInRange(monday, friday) {
        ///     ...do something...
        ///}
        /// </summary>
        /// <param name="currentDate"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static bool IsInRange(this DateTime currentDate, DateTime beginDate, DateTime endDate)
        {
            return (currentDate >= beginDate && currentDate <= endDate);
        }

        /// <summary>
        /// A simple date range
        /// Example Get next 80 days:
        /// IEnumerable<DateTime> dateRange = DateTime.Now.GetDateRangeTo(DateTime.Now.AddDays(80));
        /// </summary>
        /// <param name="self"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetDateRangeTo(this DateTime self, DateTime toDate)
        {
            var range = Enumerable.Range(0, new TimeSpan(toDate.Ticks - self.Ticks).Days);

            return from p in range
                   select self.Date.AddDays(p);
        }

        /// <summary>
        /// Wraps DateTime.TryParse() and all the other kinds of code you need to determine 
        /// if a given string holds a value that can be converted into a DateTime object.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDate(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                DateTime dt;
                return (DateTime.TryParse(input, out dt));
            }
            return false;
        }


        #region Shamsi Date
        /// <summary>
        /// Swap to int number.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void Swap(ref int x, ref int y)
        {
            var temp = x;
            x = y;
            y = temp;
        }

        /// <summary>
        /// Split year, month and day from persian date string.
        /// </summary>
        /// <param name="stringPersianDate"></param>
        /// <param name="y"></param>
        /// <param name="m"></param>
        /// <param name="d"></param>
        public static void SplitSolarDate(string stringPersianDate, out int y, out int m, out int d)
        {
            stringPersianDate = stringPersianDate.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace(
                    "۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
            if (stringPersianDate.IndexOf('/') < 2)
            {
                y = 1300;
                m = 1;
                d = 1;
            }
            else
            {
                var strItems = new string[3];
                var itemCounter = 0;
                foreach (var ch in stringPersianDate)
                {
                    if (ch == '/')
                    {
                        itemCounter++;
                    }
                    else
                    {
                        strItems[itemCounter] += ch;
                    }
                }
                try
                {
                    y = int.Parse(strItems[2]);
                    m = int.Parse(strItems[1]);
                    d = int.Parse(strItems[0]);
                    if (d > y)
                        Swap(ref d, ref y);
                }
                catch
                {
                    y = 1300;
                    m = 1;
                    d = 1;
                }
            }
        }

        /// <summary>
        /// Convert Shamsi Date To Miladi
        /// </summary>
        /// <param name="persianDate">Shamsi Date</param>
        /// <returns></returns>
        public static DateTime ToMiladiDate(this string persianDate)
        {
            int y, m, d;
            SplitSolarDate(persianDate, out y, out m, out d);
            return Calendar.ConvertToGregorian(y, m, d, DateType.Persian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToFriendlyDateString(this DateTime date, string format)
        {
            var solarDate = Calendar.ConvertToPersian(date);
            return solarDate.ToString(format);
        }

        /// <summary>
        /// Convert To Long Persian (Shamsi) Date. example : (سه شنبه 25 آذر 1387)
        /// </summary>
        /// <param name="dt">miladi date</param>
        /// <returns></returns>
        public static string ToLongPersianDate(this object dt)
        {
            var temp = dt.ToDateTime();
            var solarDate = Calendar.ConvertToPersian(temp);
            return solarDate.ToString("N");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="f">Extension.PersiaDateFormatString.Tx</param>
        /// <returns></returns>
        public static string ToPersianDate(this object dt, string f)
        {
            var temp = dt.ToDateTime();
            var solarDate = Calendar.ConvertToPersian(temp);
            return solarDate.ToString(f);
        }

        /// <summary>
        /// Get Shamsi Year
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>YEAR 1390</returns>
        public static int ToPersianYear(this object dt)
        {
            var temp = dt.ToDateTime();
            var solarDate = Calendar.ConvertToPersian(temp);
            return solarDate.ArrayType[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToPersianDate(this object dt)
        {
            var temp = dt.ToDateTime();
            var solarDate = Calendar.ConvertToPersian(temp);
            return solarDate.ToString();
        }

        /// <summary>
        /// اکنون، x دقیقه پیش، x ساعت پیش
        /// </summary>
        /// <param name="dt">Miladi Date</param>
        /// <returns></returns>
        public static string ToRelativeDate(this object dt)
        {
            var temp = dt.ToDateTime();
            var solarDate = Calendar.ConvertToPersian(temp);
            return solarDate.ToRelativeDateString("p");
        }

        /// <summary>
        /// Convert To  Persian (Shamsi) Date. example : (آذر- 1387)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToMounthYearDate(this object dt)
        {
            var temp = dt.ToDateTime();
            var solarDate = Calendar.ConvertToPersian(temp);
            return solarDate.ToString("e");
        }

        /// <summary>
        /// Convert.ToDatetime if can not cast object to date time data type return datetime.now.
        /// [Mabna Method]
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object o)
        {
            DateTime dt;
            try
            {
                dt = Convert.ToDateTime(o);
            }
            catch (Exception)
            {
                dt = DateTime.Now;
            }
            return dt;
        }

        /// <summary>
        /// Calculate Age and Return 24 Years, 5 Months, 21 Days
        /// </summary>
        /// <param name="birthDate">Birth Date</param>
        /// <param name="day">DAY</param>
        /// <param name="month">MONTH</param>
        /// <param name="year">YEAR</param>
        /// <param name="week">WEEK</param>
        /// <returns>24 Years, 5 Months, 21 Days</returns>
        public static string GetAge(this DateTime birthDate, out int day, out int month, out int year, out int week)
        {
            var x = DateTime.Now - birthDate;
            var age = DateTime.MinValue + x;
            year = age.Year - 1;
            month = age.Month - 1;
            day = age.Day - 1;
            week = day / 7;
            day = day % 7;
            var r = string.Empty;
            if (year != 0) r += string.Format("{0} سال ", year);
            if (month != 0) r += string.Format("{0} ماه ", month);
            if (week != 0) r += string.Format("{0} هفته ", week);
            if (day != 0) r += string.Format("{0} روز ", day);
            return r;
        }

        /// <summary>
        /// Calculate Age and Return 24 Years, 5 Months, 21 Days
        /// </summary>
        /// <param name="birthDate">Birth Date</param>
        /// <returns>24 Years, 5 Months, 21 Days</returns>
        public static string GetAge(this DateTime birthDate)
        {
            var x = DateTime.Now - birthDate;
            var age = DateTime.MinValue + x;
            int year = age.Year - 1;
            int month = age.Month - 1;
            int day = age.Day - 1;
            int week = day / 7;
            day = day % 7;
            var r = string.Empty;
            if (year != 0) r += string.Format("{0} سال ", year);
            if (month != 0) r += string.Format("{0} ماه ", month);
            if (week != 0) r += string.Format("{0} هفته ", week);
            if (day != 0) r += string.Format("{0} روز ", day);
            return r;
        }

        /// <summary>
        /// Return SQL Friendly Date Time (yyy-MM-dd hh:mm:ss)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSqlFriendly(this DateTime? dt)
        {
            return dt != null ? dt.Value.ToString("yyy-MM-dd hh:mm:ss") : null;
        }

        #endregion

    }
}
