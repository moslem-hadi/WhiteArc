using System;
using System.Collections.Generic;
using System.Linq;

namespace DNT.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Provides a Distinct method that takes a key selector lambda as parameter. 
        /// The .net framework only provides a Distinct method that takes an instance of an implementation of 
        /// IEqualityComparer<![CDATA[<T>]]>> where the standard parameterless Distinct that uses the default equality comparer 
        /// doesn't suffice.
        /// 
        /// Example: 
        /// var instrumentSet = _instrumentBag.Distinct(i => i.Name);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="this"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> @this, Func<T, TKey> keySelector)
        {
            return @this.GroupBy(keySelector).Select(grps => grps).Select(e => e.First());
        }
    }
}
