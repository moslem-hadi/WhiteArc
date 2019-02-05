﻿using System;
using System.ComponentModel;
using System.Linq;

namespace DNT.Extensions
{
    public static class CommonExtensions
    {
        /// <summary>
        /// converts one type to another
        /// Example:
        /// var age = "28";
        /// var intAge = age.To<int>();
        /// var doubleAge = intAge.To<double>();
        /// var decimalAge = doubleAge.To<decimal>();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T To<T>(this IConvertible value)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// c# version of "Between" clause of sql query.
        /// Example:
        /// DateTime today = DateTime.Now;
        /// var from = new DateTime(2012, 2, 1);
        /// var to = new DateTime(2012, 12, 20);
        ///
        /// bool isBetween = today.Between(from, to);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static bool Between<T>(this T value, T from, T to) where T : IComparable<T>
        {
            return value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;
        }

        /// <summary>
        /// C# version of In clause of sql query.
        /// Example:
        /// string value = "net";
        ///    bool isIn = value.In("dot", "net", "languages"); //true
        ///    isIn = value.In("dot", "note", "languages"); //false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool In<T>(this T value, params T[] list)
        {
            return list.Contains(value);
        }


        /// <summary>
        /// Converts any type to another.
        /// Example:
        /// string a = "1234";
        /// int b = a.ChangeType<int>(); //Successful conversion to int (b=1234)
        /// string c = b.ChangeType<string>(); //Successful conversion to string (c="1234")
        /// string d = "foo";
        /// int e = d.ChangeType<int>(); //Exception System.InvalidCastException
        /// int f = d.ChangeType(0); //Successful conversion to int (f=0)
        /// </summary>
        /// <typeparam name="TU"></typeparam>
        /// <param name="source"></param>
        /// <param name="returnValueIfException"></param>
        /// <returns></returns>
        public static TU ChangeType<TU>(this object source, TU returnValueIfException)
        {
            try
            {
                return source.ChangeType<TU>();
            }
            catch
            {
                return returnValueIfException;
            }
        }

        /// <summary>
        /// Converts any type to another.
        /// Example:
        /// string a = "1234";
        /// int b = a.ChangeType<int>(); //Successful conversion to int (b=1234)
        /// string c = b.ChangeType<string>(); //Successful conversion to string (c="1234")
        /// string d = "foo";
        /// int e = d.ChangeType<int>(); //Exception System.InvalidCastException
        /// int f = d.ChangeType(0); //Successful conversion to int (f=0)
        /// </summary>
        /// <typeparam name="TU"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TU ChangeType<TU>(this object source)
        {
            if (source is TU)
                return (TU)source;

            var destinationType = typeof(TU);
            if (destinationType.IsGenericType && destinationType.GetGenericTypeDefinition() == typeof(Nullable<>))
                destinationType = new NullableConverter(destinationType).UnderlyingType;

            return (TU)Convert.ChangeType(source, destinationType);
        }

    }
}
