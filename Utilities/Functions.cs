using DomainClasses;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Utilities
{
    public static class Functions
    {
        //متدهای الحاقی
        public static int WordCount(this string input)
        {
            var count = 0;
            try
            {
                // Exclude whitespaces, Tabs and line breaks
                var re = new Regex(@"[^\s]+");
                var matches = re.Matches(input);
                count = matches.Count;
            }
            catch (Exception)
            {
                return -1;
            }
            return count;
        }



        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public static string SplitString(this String s, Int32 partLength)
        {
            var x = string.Join(" ", s.SplitInParts(partLength).ToArray());

            return x;
        }
        public static int RandomNumber(int start = 100, int end = 999)
        {
            Random random = new Random();
            int randomNumber = random.Next(start, end);

            return randomNumber;
        }


        //***********************COMMON*******************************
        //public static string ReplaceSpace(object str)
        //{
        //    if (str == null) return "";

        //    return RemoveDiacritics(Regex.Replace(str.ToString(), @"[^A-Za-z0-9-آ-ی-_\.~]+", "-"));
        //}


        public static string GenerateSlug(string title)
        {
            var slug = RemoveAccent(title).ToLower();
            slug = Regex.Replace(slug, @"[^a-z0-9-\u0600-\u06FF]", "-");
            slug = Regex.Replace(slug, @"\s+", "-").Trim();
            slug = Regex.Replace(slug, @"-+", "-");
            //زیر 45 کاراکتر باشه بهتره
            // slug = slug.Substring(0, slug.Length <= 45 ? slug.Length : 45).Trim();

            return slug;
        }

        private static string RemoveAccent(string text)
        {
            var bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }



        public static string PersianDateTime(object input = null)
        {
            DateTime datetime = input != null ? DateTime.Parse(input.ToString()) : DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(datetime).ToString() + "/" +
                persianCalendar.GetMonth(datetime).ToString("0#") + "/" +
                persianCalendar.GetDayOfMonth(datetime).ToString("0#") + " - " + datetime.ToString("HH:mm");
        }
        public static string PersianDate(object input = null)
        {
            DateTime datetime = input != null ? DateTime.Parse(input.ToString()) : DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(datetime).ToString() + "/" +
                persianCalendar.GetMonth(datetime).ToString("0#") + "/" +
                persianCalendar.GetDayOfMonth(datetime).ToString("0#")  ;
        }

        public static string GetPlainTextFromHtml(string Html)
        {
            try
            {
                return System.Text.RegularExpressions.Regex.Replace(Html, "<[^>]*>", string.Empty);
            }
            catch { return ""; }
        }

        public static string GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }

        public static string ConvertNumLa2Fa(string num)
        {
            string result = string.Empty;
            foreach (char c in num.ToCharArray())
            {
                switch (c)
                {
                    case '0':
                        result += "٠";
                        break;
                    case '1':
                        result += "١";
                        break;
                    case '2':
                        result += "٢";
                        break;
                    case '3':
                        result += "٣";
                        break;
                    case '4':
                        result += "٤";
                        break;
                    case '5':
                        result += "٥";
                        break;
                    case '6':
                        result += "٦";
                        break;
                    case '7':
                        result += "٧";
                        break;
                    case '8':
                        result += "٨";
                        break;
                    case '9':
                        result += "٩";
                        break;
                    default:
                        result += c;
                        break;

                }
            }
            return result;
        }

        public static bool IsEmail(string email)
        {
            const string MatchEmailPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            if (email != null)
            {
                bool a = System.Text.RegularExpressions.Regex.IsMatch(email, MatchEmailPattern);
                return a;
            }
            else return false;
        }

        public static string ConvertNumFa2La(string num)
        {
            string result = string.Empty;
            foreach (char c in num.ToCharArray())
            {
                int i = System.Convert.ToInt32(c);
                switch (i)
                {
                    case 1776:
                        result += "0";
                        break;
                    case 1777:
                        result += "1";
                        break;
                    case 1778:
                        result += "2";
                        break;
                    case 1779:
                        result += "3";
                        break;
                    case 1780:
                        result += "4";
                        break;
                    case 1781:
                        result += "5";
                        break;
                    case 1782:
                        result += "6";
                        break;
                    case 1783:
                        result += "7";
                        break;
                    case 1784:
                        result += "8";
                        break;
                    case 1785:
                        result += "9";
                        break;
                    default:
                        result += c;
                        break;

                }
            }
            return result;
        }

        public static string String2date(object input, int Mode, string type)
        {
            //using http://www.persiadevelopers.com/articles/Persia.NET.aspx
            //همنطور که در جدول فوق مشاهده می شود ۶ فرمت آخری مربوط به فرمتهای ساعت می شوند. این فرمتها می توانند به همراه فرمت تاریخ نیز به کار روند مانند کد زیر:

            //string str = solarDate.ToString("H,w");
            Persia.SolarDate solarDate = Persia.Calendar.ConvertToPersian(DateTime.Parse(input.ToString()));
            if (Mode == 1)
            {
                /*
                 D = X  روز پیش
                 TY = امروز، دیروز، x   روز پیش
                 p = اکنون، x  دقیقه پیش، x  ساعت پیش
                 */
                return solarDate.ToRelativeDateString(type);
            }
            else if (Mode == 2)
            {
                /*
                 D = ۱۳۸۹/۹/۳۰
                 d = ۸۹/۹/۳۰
                 W = سه شنبه  ۱۳۸۹/۹/۳۰
                 M = ۳۰ آذر ۱۳۸۹
                 H =   ۱۹  : ۵۰
                 N = سه شنبه  ۳۰ آذر ۱۳۸۹
                 n= سه شنبه  ۳۰ آذر ۸۹
                 */
                if (type == "H")
                {
                    string tm = solarDate.ToString(type);
                    return tm.Remove(tm.LastIndexOf(':'));
                }
                return solarDate.ToString(type);
            }
            else
                return solarDate.ToString();
        }


        public static string SetCama(object InputText)
        {
            string num = "0";
            try
            {
                if (InputText == null || string.IsNullOrEmpty(InputText.ToString()))
                    return "0";
                num = InputText.ToString();
                double number = 0;
                double.TryParse(InputText.ToString(), out number);
                string res = string.Format("{0:###,###.####}", number);
                if (string.IsNullOrEmpty(res)) return "0";
                else
                    return res;
            }
            catch
            {
                return num;
            }
        }
        public static string SetCamaHezar(object InputText)
        {
            string num = "0";
            try
            {
                if (InputText == null || string.IsNullOrEmpty(InputText.ToString()))
                    return "0";
                num = InputText.ToString();
                double number = 0;
                double.TryParse(InputText.ToString(), out number);


                string res = string.Format("{0:###,###.####}", number / 1000);
                if (string.IsNullOrEmpty(res)) return "0";
                else
                    return res;
            }
            catch
            {
                return num;
            }
        }


        public static string ValidPersian(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            else
            {
                str = Regex.Replace(Regex.Replace(str, "ك", "ک", RegexOptions.IgnoreCase), "ي", "ی", RegexOptions.IgnoreCase);
                return str;

            }
        }

        public static Boolean CheckMeliCode(string nationalCode)
        {
            //در صورتی که کد ملی وارد شده تهی باشد

            if (string.IsNullOrEmpty(nationalCode))
                return false;
            //throw new Exception("لطفا کد ملی را صحیح وارد نمایید");


            //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
            if (nationalCode.Length != 10)
                return false;
            //throw new Exception("طول کد ملی باید ده کاراکتر باشد");

            //در صورتی که کد ملی ده رقم عددی نباشد
            var regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalCode))
                return false;
            //throw new Exception("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalCode))
                return false;


            //عملیات شرح داده شده در بالا
            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = Convert.ToInt32(chArray[9].ToString());

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }

        private static readonly Random _rng = new Random();
        private static string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer).ToLower();
        }


        public static string GetDayAndMonth(object input)
        {
            DateTime datetime = DateTime.Parse(input.ToString());

            string maindate = String2date(datetime, 2, "M");
            string[] final = maindate.Split(' ');
            return final[0] + "<br>" + final[1];
        }
        public static string SubStringHtml(object InputHtml, object StartIndex, object Length)
        {
            return SubStringText(GetPlainTextFromHtml(InputHtml.ToString()), StartIndex, Length);
        }
        public static string SubStringText(object InputText, object StartIndex, object Length)
        {
            string StrText = InputText.ToString();
            int StrLenght = Convert.ToInt32(Length);
            if (StrText.Length > StrLenght)
            {
                return StrText.Substring(Convert.ToInt32(StartIndex), StrLenght)  ;
            }
            else
            {
                return StrText;
            }
        }






        public static bool SendMail(string mail, string title, string text, bool useTemplate=false,
            string filepath="", string ReplyTo="")
        {
            bool isSeant = false;
           
            try
            {
                string Body = "";
                MailMessage obMsg = new MailMessage();
                string mailservice = GetSettingVal("mailservice");
                string WebsiteName = GetSettingVal("WebsiteName");
                string WebsiteUrl = GetSettingVal("WebsiteUrl");
                string WebsiteMail = GetSettingVal("WebsiteMail");
                SmtpClient ob = new SmtpClient(mailservice, int.Parse(GetSettingVal("mailserviceport")));

                MailAddress sendermail = new MailAddress(WebsiteMail, WebsiteName, System.Text.Encoding.UTF8);
                System.Net.NetworkCredential objNC = new System.Net.NetworkCredential(WebsiteMail, GetSettingVal("WebsiteMailPass"));

                obMsg.From = sendermail;
                obMsg.Sender = sendermail;
                obMsg.BodyEncoding = System.Text.Encoding.UTF8;
                if (!string.IsNullOrEmpty(ReplyTo))
                    obMsg.ReplyTo = new MailAddress(ReplyTo);
                obMsg.SubjectEncoding = System.Text.Encoding.UTF8;
                if (mailservice.ToLower() == "smtp.gmail.com")
                    ob.EnableSsl = true;
                ob.Credentials = objNC;


                if (!string.IsNullOrEmpty(filepath))
                {
                    Attachment attach = new Attachment(filepath);
                    string fileExt = System.IO.Path.GetExtension(filepath);
                    attach.ContentDisposition.FileName = "attachedFile" + fileExt;
                    attach.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    obMsg.Attachments.Add(attach);
                }


                if (useTemplate)
                {
                    try
                    {
                        System.IO.StreamReader st = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/content/sysFiles/commonmail.html"));

                        Body = st.ReadToEnd()
                            .Replace("[[WebsiteUrl]]", WebsiteUrl)
                            .Replace("[[WebsiteName]]", WebsiteName)
                            .Replace("[[title]]", title)
                            .Replace("[[text]]", text);
                    }
                    catch { }
                }
                else
                    Body = text;
                obMsg.Subject = title;

                obMsg.From = sendermail;
                obMsg.Sender = sendermail;
                obMsg.To.Add(new MailAddress(mail));
                obMsg.Body = Body;
                obMsg.IsBodyHtml = true;
                ob.Send(obMsg);

                isSeant = true;
            }
            catch { isSeant = false; }
            return isSeant;
        }

        public static async Task SendMailAsync(string mail, string title, string text, bool useTemplate = false,
            string filepath = "", string ReplyTo = "")
        {
            
            try
            {
                string Body = "";
                MailMessage obMsg = new MailMessage();
                string mailservice = GetSettingVal("mailservice");
                string WebsiteName = GetSettingVal("siteName");
                string WebsiteUrl = GetSettingVal("siteurl");
                string WebsiteMail = GetSettingVal("siteMail");
                SmtpClient ob = new SmtpClient(mailservice, int.Parse(GetSettingVal("mailserviceport")));

                MailAddress sendermail = new MailAddress(WebsiteMail, WebsiteName, System.Text.Encoding.UTF8);
                System.Net.NetworkCredential objNC = new System.Net.NetworkCredential(WebsiteMail, GetSettingVal("siteMailPass"));

                obMsg.From = sendermail;
                obMsg.Sender = sendermail;
                obMsg.BodyEncoding = System.Text.Encoding.UTF8;
                if (!string.IsNullOrEmpty(ReplyTo))
                    obMsg.ReplyTo = new MailAddress(ReplyTo);
                obMsg.SubjectEncoding = System.Text.Encoding.UTF8;
                if (mailservice.ToLower() == "smtp.gmail.com")
                    ob.EnableSsl = true;
                ob.Credentials = objNC;


                if (!string.IsNullOrEmpty(filepath))
                {
                    Attachment attach = new Attachment(filepath);
                    string fileExt = System.IO.Path.GetExtension(filepath);
                    attach.ContentDisposition.FileName = "attachedFile" + fileExt;
                    attach.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    obMsg.Attachments.Add(attach);
                }


                if (useTemplate)
                {
                    try
                    {
                        System.IO.StreamReader st = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/content/sysFiles/commonmail.html"));

                        Body = st.ReadToEnd()
                            .Replace("[[siteurl]]", WebsiteUrl)
                            .Replace("[[siteName]]", WebsiteName)
                            .Replace("[[title]]", title)
                            .Replace("[[text]]", text);
                    }
                    catch { }
                }
                else
                    Body = text;
                obMsg.Subject = title;

                obMsg.From = sendermail;
                obMsg.Sender = sendermail;
                obMsg.To.Add(new MailAddress(mail));
                obMsg.Body = Body;
                obMsg.IsBodyHtml = true;
                await ob.SendMailAsync(obMsg);
                 
            }
            catch(Exception ex)
            {
            }
            
        }



        /// <summary>
        /// گرفتن مقدار یک تنظیمات
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="lang">زبان؟؟ کد زبان به ته نام میچسبه.</param>
        /// <param name="encoded">آیا بصورت کدشده ذخیره شده؟</param>
        /// <param name="loadFromDatabase">آیا نیاز به لود از دیتابیس هست؟</param>
        /// <returns>مقدار Value</returns>
        public static string GetSettingVal(string name,string lang="", bool encoded = false)
        {
            string retVal = "";

            //زبان فارسی، پیشفرضه. برای همین نیازی به کد زبان نیست.
            if (lang == "fa")
                lang = "";


            if(lang!="")
                lang = "-" + lang;
             
                SystemConfig sys = SystemConfig.Instance;
                retVal = sys.GetConfigVale(name+lang, encoded);
               if(retVal==null)
            {
                using (ServiceLayer.SettingValueService service = new SettingValueService())
                {

                    if (encoded)
                    {
                        string EncriptedName = Encrypt.EncryptString(name  + lang);

                        retVal = Encrypt.DecryptString(service.GetValue(EncriptedName));

                    }
                    else
                        retVal = service.GetValue(name  + lang);


                }
            }

            return retVal;
        }


        /// <summary>
        /// حذف اعراب از یک متن
        /// </summary>
        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// لوپ کردن بین 2 تاریخ
        /// </summary>
        /// <param name="from">تاریخ شروع</param>
        /// <param name="thru">تاریخ پایان</param>
        /// <param name="thru">پرش چند روزه؟</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru,int interval=1)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(interval))
                yield return day;
        }







        /// <summary>
        /// درست کردن متا صبحات
        /// </summary>  
        public static string GenerateMetaTag(string title, string description, bool allowIndexPage, bool allowFollowLinks, string author = "", string lastmodified = "", string expires = "never", string language = "fa", CacheControlType cacheControlType = CacheControlType._private)
        {
         int MaxLenghtTitle = 60;
         int MaxLenghtDescription = 170;
            title = title.Substring(0, title.Length <= MaxLenghtTitle ? title.Length : MaxLenghtTitle).Trim();
            description = description.Substring(0, description.Length <= MaxLenghtDescription ? description.Length : MaxLenghtDescription).Trim();

            var meta = "";
            meta += string.Format("<title>{0}</title>\n", title);
            meta += string.Format("<meta http-equiv=\"content-language\" content=\"{0}\"/>\n", language);
            meta += string.Format("<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\"/>\n");
            meta += string.Format("<meta charset=\"utf-8\"/>\n");
            meta += string.Format("<meta name=\"description\" content=\"{0}\"/>\n", description);
          meta += string.Format("<meta http-equiv=\"Cache-control\" content=\"{0}\"/>\n", EnumExtensions.EnumHelper<CacheControlType>.GetEnumDescription(cacheControlType.ToString()));
            meta += string.Format("<meta name=\"robots\" content=\"{0}, {1}\" />\n", allowIndexPage ? "index" : "noindex", allowFollowLinks ? "follow" : "nofollow");
            meta += string.Format("<meta name=\"expires\" content=\"{0}\"/>\n", expires);

            if (!string.IsNullOrEmpty(lastmodified))
                meta += string.Format("<meta name=\"last-modified\" content=\"{0}\"/>\n", lastmodified);

            if (!string.IsNullOrEmpty(author))
                meta += string.Format("<meta name=\"author\" content=\"{0}\"/>\n", author);

            //------------------------------------Google & Bing Doesn't Use Meta Keywords ...
            //meta += string.Format("<meta name=\"keywords\" content=\"{0}\"/>\n", keywords);

            return meta;
        }

        //----------------------------------------------------


        
            //به صورت استاتیک تعریف شده. در اصل باید از دیتابیس بخونه کد ها رو
        public static int GetLanguageIdByCode(string code)
        {
            switch (code)
            {
                case "fa":
                    return 4;
                case "en":
                    return 5;
                default:
                    throw new Exception("کد زبان تعریف نشده");
            }
        }


        public static string GetLangCodeName()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag.ToLower();
        }






        public static void AddViewCount()
        {
            using (SiteStatService service = new SiteStatService())
            {
                var ctx = System.Web.HttpContext.Current;
                Task.Factory.StartNew(() => {
                    
                System.Web.HttpContext context =ctx;
                service.AddView(
                    GetIPAddress(),
                     GetUserOS(context.Request.UserAgent),
                     context.Request.Url.AbsolutePath,
                    context.Request.UrlReferrer?.ToString() ?? "Direct",
                    context.Request.Browser.Browser
                     );

                });
            }
        }

        #region

        //list of bots and crawlers
        private static readonly System.Collections.Generic.List<string> KnownCrawlers = new System.Collections.Generic.List<string>
        {
            "bot","crawler","baiduspider","80legs","ia_archiver","ahrefsBot","twitterbot",
            "yoozbot","yandexBot","bitlybot","other", "sogou web spider", "python requests",
            "voyager","curl","wget","yahoo! slurp","mediapartners-google", "mj12bot",
            "seznamBot", "Sogou web spider", "360Spider", "sogouwebspider"
        };

        //detect the crawlers and bots
        public static bool IsBotOrCrawler(string agent)
        {
            agent = agent.ToLower();
            return KnownCrawlers.Any(crawler => agent.Contains(crawler) || agent.Equals(crawler));
        }

        public static string GetUserOS(string userAgent)
        {
            // get a parser with the embedded regex patterns
            var uaParser = UAParser.Parser.GetDefault();
            UAParser.ClientInfo c = uaParser.Parse(userAgent);
            return c.OS.Family;
        }
        public static string GetIPAddress()
        {
            try {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        return addresses[0];
                    }
                }

                return context.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch { return "0"; }
        }
        #endregion


        /// <summary>
        /// گرفتن آمار برای نمودار
        /// </summary>
        /// <param name="daycount">تعداد روز موردی نیاز</param>
        /// <returns></returns>
        public static string GetViewStat(int daycount)
        {
            using (SiteStatService service = new SiteStatService())
            {
                return service.GetViewStat(daycount);
            }
        }
         
        public static string GetPostCount()
        {
            using (DataLayer.CMSDataEntities db = new DataLayer.CMSDataEntities())
            {
                return db.Posts.Count().ToString();
            }
        }
        public static string GetPageCount()
        {
            using (DataLayer.CMSDataEntities db = new DataLayer.CMSDataEntities())
            {
                return db.PageContents.Count().ToString();
            }
        }
         
        public static int GetUnreadContactPMCount()
        {
            using (DataLayer.CMSDataEntities db = new DataLayer.CMSDataEntities())
            {
                int cnt= db.ContactPMs.Where(a => !(bool)a.IsRead).Count();
                return cnt;
            }
        }
         
    }
}
