using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DomainClasses
{
   public static class HtmlExtensions
    {
        /// <summary>
        /// ایجاد یک تکست باکس و یک دکمه برای باز کردن پاپاپ فایل منیجر و انتخاب آدرس یک فایل
        /// </summary> 
        public static MvcHtmlString FileChoose(this HtmlHelper html, string name, string displayName,
        string folder=""  ,string classes = "" )
        {
            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("input-group "+ classes);

            var textbox = html.TextBox(name, "", new
            { @class = "form-control ltr", autocomplete = "off", maxlength = 2000 });

            wrapper.InnerHtml = textbox.ToString();


            var btngroup = new TagBuilder("span");
            btngroup.AddCssClass("input-group-btn");
            //closeButton.Attributes.Add("href", "");

            var btn = new TagBuilder("span");
            btn.AddCssClass("btn btn-inverse OpenPopUp");
            btn.Attributes.Add("data-folder", folder);
            btn.Attributes.Add("data-element", name);
            btn.InnerHtml = displayName;
            btngroup.InnerHtml = btn.ToString(); ;
             
            wrapper.InnerHtml += btngroup.ToString();

         
            return new MvcHtmlString(wrapper.ToString());
        }


        /// <summary>
        /// ایجاد یک تکست باکس و یک دکمه برای باز کردن پاپاپ فایل منیجر و انتخاب آدرس یک فایل
        /// </summary> 
        public static MvcHtmlString FileChooseFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string displayName,
        string folder = "", string classes = "")
        {
            TModel model = html.ViewData.Model;
            string name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);


            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("input-group " + classes);

            var textbox = html.TextBox(name, metadata.Model as string, new
            { @class = "form-control ltr", autocomplete = "off", maxlength = 2000 });
             
            wrapper.InnerHtml = textbox.ToString();

            var btngroup = new TagBuilder("span");
            btngroup.AddCssClass("input-group-btn");

            var btn = new TagBuilder("span");
            btn.AddCssClass("btn btn-inverse OpenPopUp");
            btn.Attributes.Add("data-folder", folder);
            btn.Attributes.Add("data-element", name);
            btn.InnerHtml = displayName;
            btngroup.InnerHtml = btn.ToString(); ;

            wrapper.InnerHtml += btngroup.ToString();

            return new MvcHtmlString(wrapper.ToString());
        }
    }
}
