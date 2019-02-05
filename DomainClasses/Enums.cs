using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
    public enum PostType : short
    {
        /// <summary>
        /// خبر
        /// </summary>
        [Display(Name = "خبر")]
        Post = 1,


        /// <summary>
        /// مقاله
        /// </summary>
        [Display(Name = "مقاله")]
        Article = 2,

        /// <summary>
        /// آموزش
        /// </summary>
        [Display(Name = "آموزش")]
        Learn = 3,


        /// <summary>
        /// سوال
        /// </summary>
        [Display(Name = "سوال")]
        Faq = 4
    }
    public enum AvailabilityType : short
    {
        /// <summary>
        /// موجود
        /// </summary>
        Available = 1,


        /// <summary>
        /// ناموجود
        /// </summary>
        NotAvailable = 2
    }

    public enum AccountType : short
    {

        [Display(Name = "ساده")]
        Free = 0,
        [Display(Name = "برنزی")]
        Level1 = 1,
        [Display(Name = "نقره ای")]
        Level2 = 2,
        [Display(Name = "طلایی")]
        Level3 = 3,
        [Display(Name = "پلاتینوم")]
        Level4 = 4
    }

    public enum ProjectStatus : byte
    {
        /// <summary>
        /// حذف شده
        /// </summary>
        Deleted = 0,

        /// <summary>
        /// در انتظار پرداخت توسط کاربر
        /// </summary>
        [Display(Name = "در انتظار پرداخت")]
        PayPending = 1,

        /// <summary>
        /// جدید برای مدیر، هنوز بررسی نشده
        /// </summary>
        [Display(Name = "در انتظار تایید")]
        NewToAdmin = 2,

        /// <summary>
        /// بررسی شده و تایید نشده
        /// </summary>
        [Display(Name = "عدم تایید")]
        NotApproved = 3,

        /// <summary>
        /// بررسی و تایید شده، در حال نمایش
        /// </summary>
        /// 
        [Display(Name = "در حال نمایش")]
        ApprovedShowing = 4,

        /// <summary>
        /// بعد از تایید، کاربر ویرایش کرده و مجدد در انتظار تایید
        /// </summary>
        [Display(Name = "در انتظار تایید")]
        EditedNotApproved = 5,



    }


    public enum ProgressStatus : byte
    {
        ///// <summary>
        ///// عدم نمایش به دلایلی چون: عدم تایید/حذف و...
        ///// </summary>
        //[Display(Name ="عدم نمایش")]
        //NotShowing = 0,

        /// <summary>
        /// در انتظار دریافت پیشنهاد
        /// </summary>
        [Display(Name ="باز، در انتظار دریافت پیشنهاد")]
        Waiting = 0,

        /// <summary>
        /// در حال انجام
        /// </summary>
        [Display(Name ="بسته، در حال انجام")]
        InProgress = 1,

        /// <summary>
        /// کنسل شده
        /// </summary>
        [Display(Name ="بسته، کنسل شده")]
        Canceled = 2,

        /// <summary>
        /// ناموفق
        /// </summary>
        [Display(Name ="بسته")]
        Failed = 3,

        /// <summary>
        /// پایان یافته
        /// </summary>
        [Display(Name ="بسته، با موفقیت انجام شده")]
        Done = 4
    }


    /// <summary>
    /// لیست انواع موجودی. مثلا برای جدول Bill
    /// </summary>
    public static class EntityTypes
    {
        public static string Project = "Project";
        public static string Payment = "Payment";
    }


    public enum CacheControlType
    {
        [Description("public")]
        _public,
        [Description("private")]
        _private,
        [Description("no-cache")]
        _nocache,
        [Description("no-store")]
        _nostore
    }



} 

public static class EnumExtensions
{
    /// <summary>
    ///     A generic extension method that aids in reflecting 
    ///     and retrieving any attribute that is applied to an `Enum`.
    /// </summary>
    public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
    {
        return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<TAttribute>();
    }
    /// <summary>
    /// Parse a string value to the given Enum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T ToEnum<T>(this string value)
    where T : struct
    {
        Debug.Assert(!string.IsNullOrEmpty(value));
        return (T)Enum.Parse(typeof(T), value, true);
    }

    /// <summary>
    /// Converts Enumeration type into a dictionary of names and values
    /// </summary>
    /// <param name="t">Enum type</param>
    public static IDictionary<string, int> EnumToDictionary(this Type t)
    {
        if (t == null) throw new NullReferenceException();
        if (!t.IsEnum) throw new InvalidCastException("object is not an Enumeration");

        string[] names = Enum.GetNames(t);
        Array values = Enum.GetValues(t);

        return (from i in Enumerable.Range(0, names.Length)
                select new { Key = names[i], Value = (int)values.GetValue(i) })
                    .ToDictionary(k => k.Key, k => k.Value);
    }


    public static class EnumHelper<T>
    {
        /// <summary>
        /// 
        /// Example:
        /// public enum EnumGrades
        ///{
        /// [Description("Passed")]
        /// Pass,
        /// [Description("Failed")]
        /// Failed,
        /// [Description("Promoted")]
        /// Promoted
        ///}
        ///
        /// string description = EnumHelper<![CDATA[<EnumGrades>]]>.GetEnumDescription("pass");
        /// </summary>
        /// <typeparam name="T">Enum Type</typeparam>
        public static string GetEnumDescription(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }


    /// 
    /// Method to get enumeration value from string value.
    /// </summary>
    public static T GetEnumValue<T>(string str) where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            throw new Exception("T must be an Enumeration type.");
        }
        T val = ((T[])Enum.GetValues(typeof(T)))[0];
        if (!string.IsNullOrEmpty(str))
        {
            foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (enumValue.ToString().ToUpper().Equals(str.ToUpper()))
                {
                    val = enumValue;
                    break;
                }
            }
        }

        return val;
    }


    ///<summary>
    /// Method to get enumeration value from int value.
    /// </summary>
    public static T GetEnumValue<T>(int intValue) where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            throw new Exception("T must be an Enumeration type.");
        }
        T val = ((T[])Enum.GetValues(typeof(T)))[0];

        foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
        {
            if (Convert.ToInt32(enumValue).Equals(intValue))
            {
                val = enumValue;
                break;
            }
        }
        return val;
    }

    ///
    //Example:
    //TestEnum reqValue = GetEnumValue<TestEnum>("Value1");  // Output: Value1
    //TestEnum reqValue2 = GetEnumValue<TestEnum>(2);
    //

}
