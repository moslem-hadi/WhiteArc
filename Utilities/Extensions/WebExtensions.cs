using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DNT.Extensions
{
    public static class WebExtensions
    {
        private readonly static Regex DomainRegex = new Regex(@"(((?<scheme>http(s)?):\/\/)?([\w-]+?\.\w+)+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\-\=\+\\\/\?\.\:\;\,]*)?)", RegexOptions.Compiled | RegexOptions.Multiline);
        /// <summary>
        /// Takes a string of text and replaces text matching a link pattern to a hyperlink
        /// Example:
        /// Console.WriteLine("This goes to the https://www.test.com website".Linkify() );
        /// Console.WriteLine("This goes to the http://www.test.com website".Linkify("_blank") );
        /// Console.WriteLine("This goes to the www.test.com website".Linkify() );
        /// Console.WriteLine("This goes to the test.com website".Linkify("_blank") );
        /// Console.WriteLine("This goes to the test.com/page.html page".Linkify() );
        /// Console.WriteLine("This goes to the https://wwwtest.com/folder/page.html page".Linkify("_blank") );
        /// </summary>
        /// <param name="text"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string Linkify(this string text, string target = "_self")
        {
            return DomainRegex.Replace(
                text,
                match =>
                {
                    var link = match.ToString();
                    var scheme = match.Groups["scheme"].Value == "https" ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

                    var url = new UriBuilder(link) { Scheme = scheme }.Uri.ToString();

                    return string.Format(@"<a href=""{0}"" target=""{1}"">{2}</a>", url, target, link);
                }
            );
        }

        /// <summary>
        /// Gets a query string value from a System.Web.UI.UserControl HTTP Request object.
        /// Example:
        /// var queryStringVal = this.GetQueryStringValue<int>("param");
        /// </summary>
        /// <typeparam name="T">Query String Value Type</typeparam>
        /// <param name="control"></param>
        /// <param name="name">Query String Name</param>
        /// <returns></returns>
        public static T GetQueryStringValue<T>(this UserControl control, string name)
        {
            return (T)Convert.ChangeType(control.Request.QueryString[name], typeof(T));
        }

        /// <summary>
        /// Gets a query string value from a Web Page HTTP Request object.
        /// Example:
        /// var queryStringVal = this.GetQueryStringValue<int>("param");
        /// </summary>
        /// <typeparam name="T">Query String Value Type</typeparam>
        /// <param name="page"></param>
        /// <param name="name">Query String Name</param>
        /// <returns></returns>
        public static T GetQueryStringValue<T>(this Page page, string name)
        {
            return (T)Convert.ChangeType(page.Request.QueryString[name], typeof(T));
        }

        /// <summary>
        /// Gets a query string value from a HttpContext HTTP Request object.
        /// Example:
        /// var queryStringVal = this.GetQueryStringValue<int>("param");
        /// </summary>
        /// <typeparam name="T">Query String Value Type</typeparam>
        /// <param name="context"></param>
        /// <param name="name">Query String Name</param>
        /// <returns></returns>
        public static T GetQueryStringValue<T>(this HttpContext context, string name)
        {
            return (T)Convert.ChangeType(context.Request.QueryString[name], typeof(T));
        }

        /// <summary>
        /// Validates whether a string is a valid IPv4 address.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidIPAddress(this string s)
        {
            return Regex.IsMatch(s,
                    @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
        }

        /// <summary>
        /// check input string is valid url.
        /// </summary>
        /// <param name="s">string to validate</param>
        /// <returns>bool</returns>
        public static bool IsValidUrl(this string s)
        {
            var rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(s);
        }

        /// <summary>
        /// Used when we want to completely remove HTML code and not encode it with XML entities.
        /// Example:
        /// var cleanString = htmlText.StripHtml();
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StripHtml(this string input)
        {
            // Will this simple expression replace all tags???
            var tagsExpression = new Regex(@"</?.+?>");
            return tagsExpression.Replace(input, " ");
        }

        /// <summary>
        /// Remove BR Tags from input string.
        /// </summary>
        /// <param name="s">string to validate</param>
        /// <returns>string</returns>
        public static string CleanBrTags(this string s)
        {
            return s.Replace(@"<br \>", "");
        }

        /// <summary>
        /// Read html from url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ScreenScrapeHtml(this string url)
        {
            var objRequest = WebRequest.Create(url);
            var sr = new StreamReader(objRequest.GetResponse().GetResponseStream());
            string result = sr.ReadToEnd();
            sr.Close();
            return result;
        }

        #region Control (5)

        /// <summary>
        /// Raya Farayand Find Control Method. 
        /// Example: 
        /// Label theLabel = form1.FindControlR("ControlToFind") as Label;
        /// if (theLabel != null)
        /// {
        ///    theLabel.Text = "Found it!";
        /// } 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="id">Control Id as string</param>
        /// <returns>Control</returns>
        public static Control FindControlR(this Control root, string id)
        {
            if (root != null)
            {
                var controlFound = root.FindControl(id);
                if (controlFound != null)
                {
                    return controlFound;
                }
                foreach (Control c in root.Controls)
                {
                    controlFound = c.FindControlR(id);
                    if (controlFound != null)
                    {
                        return controlFound;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Select a item in a DropDownList by informed value.
        /// Example : cbo.SelectItem("1");
        /// </summary>
        /// <param name="ddl">DropDownList object.</param>
        /// <param name="value">Value to be selected.</param>
        public static void SelectItem(this DropDownList ddl, string value)
        {
            if (ddl.Items.FindByValue(value) != null)
                ddl.Items.FindByValue(value).Selected = true;
        }

        /// <summary>
        /// Convert Textbox to Persian Input
        /// </summary>
        /// <param name="t">Persian Input Text</param>
        public static void ToPersianInput(this TextBox t)
        {
            t.Attributes.Add("onkeypress", "return farsikey(this,event)");
            t.Attributes.Add("onkeydown", "javascript:changelang(this)");
        }

        /// <summary>
        /// Replace Plus with comma in Textbox
        /// </summary>
        /// <param name="t">Target Textbox</param>
        public static void ReplacePlus(this TextBox t)
        {
            t.Attributes.Add("onkeypress", "javascript:replaceplus(this)");
        }

        /// <summary>
        /// Find control on repeater header.
        /// </summary>
        /// <param name="repeater"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static Control FindControlInHeader(this Repeater repeater, string controlName)
        {
            return repeater.Controls[0].Controls[0].FindControl(controlName);
        }

        /// <summary>
        /// Find control in repeater footer.
        /// </summary>
        /// <param name="repeater"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static Control FindControlInFooter(this Repeater repeater, string controlName)
        {
            return repeater.Controls[repeater.Controls.Count - 1].Controls[0].FindControl(controlName);
        }

        /// <summary>
        /// Upload file in path. replace file name by GUID.
        /// </summary>
        /// <param name="fup"></param>
        /// <param name="path"></param>
        /// <returns>return file name if upload successed.</returns>
        public static string Upload(this FileUpload fup, string path)
        {
            if (!fup.HasFile) return string.Empty;
            var extension = fup.PostedFile.FileName.Right(4);
            if (!extension.Contains(".")) extension = "." + fup.FileName.Right(4);
            var fileName = Guid.NewGuid() + extension;
            fup.SaveAs(string.Format("{0}/{1}", path, fileName));
            return fileName;
        }

        #endregion
    }
}
