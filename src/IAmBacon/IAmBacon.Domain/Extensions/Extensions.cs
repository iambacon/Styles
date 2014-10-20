using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAmBacon.Domain.Extensions
{
    public static class Extensions
    {
        public static T NullSafe<T>(this T input) where T : class, new()
        {
            return input ?? new T();
        }
    }
}
