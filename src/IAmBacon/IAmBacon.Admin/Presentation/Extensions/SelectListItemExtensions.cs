using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IAmBacon.Admin.Presentation.Extensions
{
    public static class SelectListItemExtensions
    {
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