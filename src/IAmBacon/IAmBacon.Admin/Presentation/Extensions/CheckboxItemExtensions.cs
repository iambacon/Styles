using System;
using System.Collections.Generic;
using System.Linq;
using IAmBacon.Admin.Presentation.Models;

namespace IAmBacon.Admin.Presentation.Extensions
{
    public static class CheckboxItemExtensions
    {
        public static List<CheckboxItem> ToCheckboxList<T>(this IEnumerable<T> items, Func<T, string> getKey,
            Func<T, int> getValue)
        {
            var checkboxList = items.Select(x => new CheckboxItem
            {
                IsChecked = false,
                Label = getKey(x),
                Id = getValue(x)
            }).ToList();

            return checkboxList.OrderBy(x => x.Label).ToList();
        }
    }
}
