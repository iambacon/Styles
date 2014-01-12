using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IAmBacon.Extensions
{
    public static class SelectListItemExtensions
    {
        /// <summary>
        /// Select list extension.
        /// Converts a List into select list items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="getKey">The get key.</param>
        /// <param name="getValue">The get value.</param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> items, Func<T, string> getKey,
            Func<T, string> getValue)
        {
            var selectList = items.Select(x => new SelectListItem
            {
                Text = getKey(x), 
                Value = getValue(x)
            }).ToList();

            return selectList.OrderBy(x => x.Text).ToList();
        }
    }
}